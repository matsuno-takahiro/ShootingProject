/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.UI.Panels;
using OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

    /// <summary>
    /// PanelManagerは、プレハブからUIパネルのインスタンスを作成するために使用されます。パネルは
    /// フルスクリーンやポップアップウィンドウ/ダイアログなどがあります。
    /// </summary>
    /// 複雑さ: 初心者
    /// 概念: パネル、継承
    [DisallowMultipleComponent]
    public class PanelManager : PanelManagerBase
    {

        protected static PanelManager current;
        public static PanelManager Current
        {
            get
            {
                return current;
            }
        }

        [SerializeField] protected GameObject controlsPanelPrefab;
        [SerializeField] protected GameObject creditsPanelPrefab;
        [SerializeField] protected GameObject settingsPanelPrefab;
        [SerializeField] protected GameObject pausePanelPrefab;
        [SerializeField] protected GameObject gameOverPanelPrefab;
        [SerializeField] protected GameObject levelCompletePanelPrefab;

        virtual protected void Start()
        {
            Assert.IsNotNull<GameObject>(controlsPanelPrefab, "コントロールパネルのプレハブを選択するのを忘れました");
            Assert.IsNotNull<GameObject>(creditsPanelPrefab, "クレジットパネルのプレハブを選択するのを忘れました");
            Assert.IsNotNull<GameObject>(settingsPanelPrefab, "設定パネルのプレハブを選択するのを忘れました");
            Assert.IsNotNull<GameObject>(pausePanelPrefab, "ポーズパネルのプレハブを選択するのを忘れました");
            Assert.IsNotNull<GameObject>(gameOverPanelPrefab, "ゲームオーバーパネルのプレハブを選択するのを忘れました");
            Assert.IsNotNull<GameObject>(levelCompletePanelPrefab, "レベル完了パネルのプレハブを選択するのを忘れました");

            current = this;
        }

        virtual public ControlsPanel ShowControlsPanel()
        {
            return getPanel<ControlsPanel>(controlsPanelPrefab);
        }

        virtual public CreditsPanel ShowCreditsPanel()
        {
            return getPanel<CreditsPanel>(creditsPanelPrefab);
        }

        virtual public SettingsPanel ShowSettingsPanel()
        {
            return getPanel<SettingsPanel>(settingsPanelPrefab);
        }

        virtual public PausePanel ShowPausePanel()
        {
            return getPanel<PausePanel>(pausePanelPrefab);
        }

        virtual public GameOverPanel ShowGameOverPanel(int score)
        {
            GameOverPanel panel = getPanel<GameOverPanel>(gameOverPanelPrefab);
            panel.LevelScore = score;
            return panel;
        }

        virtual public LevelCompletePanel ShowLevelCompletePanel(int score)
        {
            LevelCompletePanel panel = getPanel<LevelCompletePanel>(levelCompletePanelPrefab);
            panel.LevelScore = score;
            return panel;
        }

        virtual protected void OnDestroy()
        {
            if (current == this) current = null;
        }
    }
}
