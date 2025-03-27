/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.UI.Panels {

	[System.Serializable]
    public class PanelEvent : UnityEvent<Panel> { }

    /// <summary>
    /// Panelは、開閉可能なGUIパネルの基本クラスです。パネルは一般的に、PanelManagerを通じて作成される
    /// シングルインスタンスオブジェクトを意図しています。
    /// </summary>
    /// 複雑さ: 中級
    /// 概念: 入力、継承、コルーチン
    [RequireComponent(typeof(RectTransform))]
    public class Panel : MonoBehaviour
    {

        [Tooltip("ジョイスティックが接続されている場合に最初に選択されるコントロール")]
        [SerializeField] protected Selectable defaultSelection;

        [Tooltip("名前が指定されている場合、その名前の仮想ボタンが押されたときにパネルが閉じます")]
        [SerializeField] protected string closeButtonVirtualName;

        /// <summary>
        /// パネルが破棄されるときに呼び出されるイベント
        /// </summary>
        [DrawConnections(ColorName.Red)]
        [SerializeField] protected PanelEvent panelDestroyedEvent = new PanelEvent();
        public PanelEvent PanelDestroyedEvent { get { return panelDestroyedEvent; } }

        virtual protected void OnEnable()
        {
            //ユーザーがジョイスティックを使用している場合、デフォルトのボタンや他のコントロールを選択する必要があります。
            //そうしないと、ジョイスティックでメニューをナビゲートできません。ジョイスティックが接続されていない場合、
            //デフォルトのボタンを選択したくありません。なぜなら、他のボタンにマウスを移動してもハイライトされたままになるからです。
            var joysticks = Input.GetJoystickNames();
            if (defaultSelection != null && joysticks.Length > 0 && !string.IsNullOrEmpty(joysticks[0]))
            {
                StartCoroutine(selectDefaultControl());
            }

            if (!string.IsNullOrEmpty(closeButtonVirtualName))
            {
                StartCoroutine(waitForCloseInput());
            }
        }

        /// <summary>
        /// "closeButtonVirtualName"が入力されている場合、ユーザーがその仮想ボタンを押したときにパネルを閉じます
        /// </summary>
        virtual protected IEnumerator waitForCloseInput()
        {
            //クローズボタンを待つ前に短い期間待ちます。そうしないと、1回のボタン押下で複数のネストされたパネルが閉じる可能性があります。
            yield return new WaitForSecondsRealtime(.1f);
            yield return new WaitUntil(() => Input.GetButtonDown(closeButtonVirtualName));
            Close();
        }

        virtual protected IEnumerator selectDefaultControl()
        {
            yield return new WaitForEndOfFrame();
            defaultSelection.Select();
        }

        /// <summary>
        /// このパネルインスタンスを破棄します
        /// </summary>
        virtual public void Close()
        {
            Destroy(gameObject);
        }

        virtual protected void OnDestroy()
        {
            panelDestroyedEvent.Invoke(this);
        }
    }
}
