/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.SceneManagement;
using OneManEscapePlan.Scripts.UI.Panels;
using OneManEscapePlan.Scripts.Utility;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.HUD;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

    /// <summary>
    /// プレイヤーがレベルをクリアしたときに表示されるパネルにこのコンポーネントをアタッチします。このパネルは
    /// GameOverPanelを拡張し、「レベル完了」メッセージを表示し、次のレベルに進むための追加機能を提供します。
    /// </summary>
    /// 複雑さ: 初心者
    /// 概念: パネル、コルーチン
    [DisallowMultipleComponent]
    public class LevelCompletePanel : GameOverPanel
    {

        [SerializeField] protected Text titleText;

        protected LinearLevelManager levelManager;

        override protected void Awake()
        {
            base.Awake();
            Assert.IsNotNull<Text>(titleText, "タイトルテキストを選択するのを忘れました");

            levelManager = GameObject.FindObjectOfType<LinearLevelManager>();
            Assert.IsNotNull<LinearLevelManager>(levelManager);
            int index = levelManager.CurrentLevelIndex;
            titleText.text = levelManager.LevelSequence.GetEntryAt(index).DisplayName + " 完了";
        }

        virtual public void Continue()
        {
            Game.TotalScore += LevelScore;
            //SumScore.Add(Game.TotalScore); // SumScoreにスコアを追加
            levelManager.NextLevel();

        }
    }
}
