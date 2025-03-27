/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Events;
using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using OneManEscapePlan.Scripts.Properties;
using OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay.ScoringSystem;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Gameplay {

	[System.Serializable]
    public class PlayerEvent : UnityEvent<Player> { }

	[System.Serializable]
    public class PlayerIntEvent : UnityEvent<Player, int> { }

    /// <summary>
    /// Playerクラスは、プレイヤーのLivesやScoreなど、プレイヤーのSpacecraftに属さない一般的なプロパティを管理します。
    /// </summary>
    /// 複雑さ: 初心者
    /// 概念: UnityEvents
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour, IScore, IReceivePoints
    {

        [Tooltip("プレイヤーの現在の宇宙船")]
        [DrawConnections(ColorName.PulsingBlue)]
        [SerializeField] protected PlayerSpacecraft spacecraft;

        [Tooltip("プレイヤーの現在のライフ")]
        [SerializeField] protected IntProperty lives = new IntProperty(3);
        [Tooltip("プレイヤーが持てる最大ライフ数")]
        [SerializeField] protected int maxLives = 10;
        [Tooltip("プレイヤーの現在のスコア")]
        [SerializeField] protected IntProperty score = new IntProperty(0);

        [Tooltip("プレイヤーのスコアが変わったときに呼び出されるイベント")]
        [DrawConnections(ColorName.Magenta)]
        [SerializeField] protected PlayerIntEvent scoreChangedEvent = new PlayerIntEvent();
        public PlayerIntEvent ScoreChangedEvent { get { return scoreChangedEvent; } }

        [Tooltip("プレイヤーがライフを獲得したときに呼び出されるイベント")]
        [DrawConnections(ColorName.Green)]
        [SerializeField] protected PlayerIntEvent gainedLifeEvent = new PlayerIntEvent();
        public PlayerIntEvent GainedLifeEvent { get { return gainedLifeEvent; } }

        [Tooltip("プレイヤーがライフを失ったときに呼び出されるイベント")]
        [DrawConnections(ColorName.Red)]
        [SerializeField] protected PlayerIntEvent lostLifeEvent = new PlayerIntEvent();
        public PlayerIntEvent LostLifeEvent { get { return lostLifeEvent; } }

        [Tooltip("プレイヤーがライフを使い果たしたときに呼び出されるイベント")]
        [DrawConnections(ColorName.Gray)]
        [SerializeField] protected PlayerEvent gameOverEvent = new PlayerEvent();
        public PlayerEvent GameOverEvent { get { return gameOverEvent; } }

        [Tooltip("プレイヤーがリスポーンしたときに呼び出されるイベント")]
        [DrawConnections(ColorName.Violet)]
        [SerializeField] protected Vector3Event respawnEvent = new Vector3Event();
        public Vector3Event RespawnEvent { get { return respawnEvent; } }

        virtual protected void Awake()
        {
            if (spacecraft != null)
            {
                Spacecraft = spacecraft;
            }
        }

        /// <summary>
        /// プレイヤーの現在のライフ数
        /// </summary>
        public int Lives
        {
            get
            {
                return lives.Value;
            }

            set
            {
                Assert.IsFalse(lives < 0, "プレイヤーに負のライフを与えようとしました");

                int oldLives = lives.Value;
                int newLives = value;
                if (newLives > maxLives) newLives = maxLives;
                lives.Value = newLives;
                if (lives < oldLives) lostLifeEvent.Invoke(this, lives.Value);
                else if (lives > oldLives) gainedLifeEvent.Invoke(this, lives.Value);
                if (oldLives > 0 && lives <= 0) gameOverEvent.Invoke(this);
            }
        }

        /// <summary>
        /// プレイヤーが持てる最大ライフ数
        /// </summary>
        public int MaxLives
        {
            get
            {
                return maxLives;
            }

            set
            {
                Assert.IsFalse(value < 1, "MaxLivesは正の値でなければなりません");
                maxLives = value;
                if (lives > maxLives) Lives = MaxLives;
            }
        }

        /// <summary>
        /// プレイヤーの現在のスコア。負の値には制約されます。
        /// </summary>
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                int oldScore = score;
                int newScore = value;
                if (newScore < 0) newScore = 0;
                score.Value = newScore;
                if (oldScore != score) scoreChangedEvent.Invoke(this, score);
            }
        }

        /// <summary>
        /// 指定されたポイント数をプレイヤーのスコアに追加します
        /// </summary>
        /// <param name="points">追加するポイント数</param>
        public void AddPoints(int points)
        {
            Score += points;
        }

        virtual public void CompleteLevel()
        {
            PanelManager pm = PanelManager.Current;
            Assert.IsNotNull<PanelManager>(pm);
            pm.ShowLevelCompletePanel(score);
        }

        virtual public PlayerSpacecraft Spacecraft
        {
            get
            {
                return spacecraft;
            }
            set
            {
                //以前の宇宙船からイベントを削除します（該当する場合）
                if (spacecraft != null)
                {
                    IHealth health = spacecraft.GetComponent<IHealth>();
                    if (health != null) health.DeathEvent.RemoveListener(onDeath);
                    spacecraft.GainPointsEvent.RemoveListener(AddPoints);
                }

                spacecraft = value;

                //新しい宇宙船にイベントを追加します（該当する場合）
                if (spacecraft != null)
                {
                    IHealth health = spacecraft.GetComponent<IHealth>();
                    Assert.IsNotNull<IHealth>(health);
                    health.DeathEvent.AddListener(onDeath);
                    spacecraft.GainPointsEvent.AddListener(AddPoints);
                }
            }
        }

        virtual protected void onDeath(IHealth health)
        {
            Lives--;

            if (Lives > 0)
            {
                Transform transform = health.GetComponent<Transform>();
                Vector3 position = transform.position;
                respawnEvent.Invoke(position);
            }
            else
            {
                PanelManager pm = PanelManager.Current;
                Assert.IsNotNull<PanelManager>(pm);
                pm.ShowGameOverPanel(score.Value);
            }
        }
    }

}
