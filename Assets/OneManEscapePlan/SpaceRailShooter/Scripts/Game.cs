/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts {

    /// <summary>
    /// フレームレートなどのゲーム設定を構成します。
    /// </summary>
    /// 複雑さ: 初心者
    public class Game : MonoBehaviour
    {

        #region Static
        private static int totalScore = 0;
        /// <summary>
        /// プレイヤーが完了したすべてのレベルの現在の合計スコア
        /// </summary>
        public static int TotalScore
        {
            get
            {
                return totalScore;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "TotalScoreは負の値にできません");
                totalScore = value;
            }
        }
        /// <summary>
        /// TotalScoreを初期化するメソッド
        /// </summary>
        public static void ResetTotalScore()
        {
            totalScore = 0;
        }

        #endregion

        [Tooltip("アプリケーションの目標フレームレート（フレーム毎秒）")]
        [Range(10, 120)]
        [SerializeField] protected int frameRate = 20;

        private bool isPaused = false;

        virtual protected void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = frameRate;
            Camera.main.allowDynamicResolution = false;
            //これがエディタでは機能しないことに注意してください
            //Screen.SetResolution(screenResolution.x, screenResolution.y, true);
        }

        /// <summary>
        /// 目標フレームレート
        /// </summary>
        public int FrameRate
        {
            get
            {
                return frameRate;
            }

            set
            {
                frameRate = value;
            }
        }


        public bool IsPaused
        {
            get
            {
                return isPaused;
            }

            set
            {
                isPaused = value;
                if (value) Time.timeScale = 0;
                else Time.timeScale = 1;
            }
        }

    }
}
