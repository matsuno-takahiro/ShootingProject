/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.UI.Panels {

    /// <summary>
    /// パネルマネージャーの基本クラスで、UIパネルのインスタンスを作成するために使用されます。
    /// シングルインスタンスパネル（特定のパネルのコピーが一度に1つだけ開くことができる）を意図しています。
    /// 
    /// PanelManagerBaseを拡張し、パネルのプレハブへの参照とパネルのインスタンスを取得するための
    /// 関数を持つクラスを1つ以上作成することを意図しています。
    /// PanelManagerBaseは、子クラスがパネルを管理するために使用するヘルパーメソッドを提供します。
    /// 
    /// 各ユニークなパネルは、その動作スクリプトによってパネルを特定するため、PanelManagerによって
    /// スポーンされる各ユニークなパネルは、Panelを拡張するユニークな動作を持つ必要があることに注意してください。
    /// </summary>
    /// 複雑さ: 上級
    /// 概念: ジェネリクス、ディクショナリ、プレハブ
    [DisallowMultipleComponent]
    abstract public class PanelManagerBase : MonoBehaviour
    {

        /// <summary>
        /// アクティブなパネルへの参照を保持します。GameObject.FindObjectOfType<>()を呼び出すよりも少し高速です。
        /// </summary>
        protected Dictionary<Type, Panel> activePanels = new Dictionary<Type, Panel>();

        /// <summary>
        /// 指定されたタイプのパネルが現在表示されているかどうかを確認します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsPanelShowing<T>() where T : Panel
        {
            return activePanels.ContainsKey(typeof(T));
        }

        /// <summary>
        /// 指定されたタイプのパネルが開いている場合、それを閉じます
        /// </summary>
        /// <typeparam name="T">パネルのタイプ</typeparam>
        /// <returns>パネルが閉じられた場合は<c>true</c>、最初から開いていなかった場合は<c>false</c></returns>
        public bool ClosePanel<T>() where T : Panel
        {
            Panel panel = null;
            //アクティブなこのパネルのインスタンスを探します
            activePanels.TryGetValue(typeof(T), out panel);
            if (panel != null)
            {
                panel.Close();
                return true;
            }
            return false;
        }

        /// <summary>
        /// パネルをそのタイプで取得します。インスタンスが存在しない場合は、指定されたプレハブから新しいインスタンスが作成されます
        /// </summary>
        /// <typeparam name="T">パネルにアタッチされている動作</typeparam>
        /// <param name="prefab">パネルのプレハブ</param>
        /// <returns>アクティブなパネルにアタッチされている要求されたMonoBehaviour</returns>
        protected T getPanel<T>(GameObject prefab) where T : Panel
        {
            Panel panel = null;
            //ディクショナリでアクティブなこのパネルのインスタンスを探します
            activePanels.TryGetValue(typeof(T), out panel);
            //見つからなかった場合は、新しいものを作成します
            if (panel == null)
            {
                GameObject panelGO = Instantiate(prefab);
                panel = panelGO.GetComponent<Panel>();
                Assert.IsNotNull<Panel>(panel, prefab.name + "プレハブにはPanelから継承されたスクリプトが含まれていません");
                activePanels[typeof(T)] = panel;
                panel.PanelDestroyedEvent.AddListener(onPanelDestroyed);
            }

            T component = panel.GetComponent<T>();
            Assert.IsNotNull<T>(component, prefab.name + "プレハブには" + typeof(T).Name + "スクリプトが含まれていません");
            return component;
        }

        /// <summary>
        /// パネルが破棄されるときに、それをディクショナリから削除します
        /// </summary>
        /// <param name="panel">破棄されるパネル</param>
        private void onPanelDestroyed(Panel panel)
        {
            foreach (Type type in activePanels.Keys)
            {
                if (activePanels.ContainsKey(type) && activePanels[type] == panel)
                {
                    activePanels.Remove(type);
                    break;
                }
            }
        }
    }

}
