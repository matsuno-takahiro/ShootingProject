/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.UI.Panels;
using System.Collections;
using UnityEngine;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.UI.Panels {

	abstract public class MenuPanel : Panel {

		/// <summary>
		/// Show the given panel, and listen for its PanelDestroyedEvent.
		/// Hide the main menu for now; we will show it again when the child
		/// panel is destroyed.
		/// </summary>
		/// <param name="panel"></param>
		virtual protected void showChildPanel(Panel panel) {
			panel.PanelDestroyedEvent.AddListener(onChildPanelDestroyed);
			StartCoroutine(hide());
		}

		//wait one frame before hiding this panel, to avoid showing 1 frame of black before
		//a new panel appears
		virtual protected IEnumerator hide() {
			yield return new WaitForEndOfFrame();
			gameObject.SetActive(false);
		}

		virtual protected void onChildPanelDestroyed(Panel childPanel) {
			gameObject.SetActive(true);
		}
	}
}
