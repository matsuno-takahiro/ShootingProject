/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

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
    /// プレイヤーがゲームオーバーになったときに表示されるパネルにこのコンポーネントをアタッチします
    /// </summary>
    /// 複雑さ: 初心者
    /// 概念: パネル、コルーチン
    [DisallowMultipleComponent]
    public class GameOverPanel : MenuPanel
    {

        [SerializeField] protected Text levelScoreText;
        [SerializeField] protected Text totalScoreText;

        [SerializeField] protected string mainMenuSceneName = "MainMenu";

        private Game game;
        private int score;

        protected virtual void Awake()
        {
            Assert.IsNotNull<Text>(levelScoreText, "レベルスコアのテキストを選択するのを忘れました");
            Assert.IsNotNull<Text>(totalScoreText, "合計スコアのテキストを選択するのを忘れました");
            game = GameObject.FindObjectOfType<Game>();
            Assert.IsNotNull<Game>(game);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            game.IsPaused = true;
        }

        override protected void OnDestroy()
        {
            game.IsPaused = false;
            base.OnDestroy();
        }

        /// <summary>
        /// プレイヤーがこのレベルで獲得したスコア（前のレベルのポイントを含まない）
        /// </summary>
        virtual public int LevelScore
        {
            set
            {
                score = value;
                levelScoreText.text = score.ToString("n0");
                totalScoreText.text = (Game.TotalScore + score).ToString();
            }
            get
            {
                return score;
            }
        }

        /// <summary>
        /// 現在のレベルを再開します（プレイヤーはレベルのスコアを失います）
        /// </summary>
        virtual public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// メインメニューに戻ります
        /// </summary>
        virtual public void Exit()
        {
            SceneManager.LoadSceneAsync(mainMenuSceneName);
        }

    }
}
