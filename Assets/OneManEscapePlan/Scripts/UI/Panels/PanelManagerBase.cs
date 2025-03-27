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
	/// A base class for panel managers, which are used to create instances of UI panels.
	/// Intended for single-instance panels (only one copy of a particular panel can be open 
	/// at a time).
	/// 
	/// The intended use is to extend PanelManagerBase with one or more classes that have
	/// references to your panel prefabs and functions for getting instances of panels. 
	/// PanelManagerBase provides helper methods which will be used by the child class to 
	/// manage the panels.
	/// 
	/// Note that each unique panel spawned by a PanelManager must have a unique behaviour
	/// extending Panel, since we locate panels by their behaviour script.
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Generics, Dictionaries, Prefabs
	[DisallowMultipleComponent]
	abstract public class PanelManagerBase : MonoBehaviour {

		/// <summary>
		/// Maintains references to our active panels; a bit faster than calling GameObject.FindObjectOfType<>()
		/// </summary>
		protected Dictionary<Type, Panel> activePanels = new Dictionary<Type, Panel>();

		/// <summary>
		/// Check if a Panel of the given type is currently showing
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool IsPanelShowing<T>() where T : Panel {
			return activePanels.ContainsKey(typeof(T));
		}

		/// <summary>
		/// Close the Panel of the given type, if it is open
		/// </summary>
		/// <typeparam name="T">The type of the Panel</typeparam>
		/// <returns><c>true</c> if the panel was closed, <c>false</c> if it was not open to begin with</returns>
		public bool ClosePanel<T>() where T : Panel {
			Panel panel = null;
			//look for an active instance of this panel
			activePanels.TryGetValue(typeof(T), out panel);
			if (panel != null) {
				panel.Close();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Retrieve a panel by its type. If no instance exists, a new instance will be
		/// created from the given prefab
		/// </summary>
		/// <typeparam name="T">A behaviour which is attached to the panel</typeparam>
		/// <param name="prefab">The panel's prefab</param>
		/// <returns>The requested MonoBehaviour attached to the active panel</returns>
		protected T getPanel<T>(GameObject prefab) where T : Panel {
			Panel panel = null;
			//look for an active instance of this panel in the dictionary
			activePanels.TryGetValue(typeof(T), out panel);
			//if we didn't find it, create a new one
			if (panel == null) {
				GameObject panelGO = Instantiate(prefab);
				panel = panelGO.GetComponent<Panel>();
				Assert.IsNotNull<Panel>(panel, "Your " + prefab.name + " prefab does not contain a script that inherits from Panel");
				activePanels[typeof(T)] = panel;
				panel.PanelDestroyedEvent.AddListener(onPanelDestroyed);
			}

			T component = panel.GetComponent<T>();
			Assert.IsNotNull<T>(component, "Your " + prefab.name + " prefab does not contain the script " + typeof(T).Name);
			return component;
		}

		/// <summary>
		/// When a panel is being destroyed, remove it from the dictionary
		/// </summary>
		/// <param name="panel">The panel that is being destroyed</param>
		private void onPanelDestroyed(Panel panel) {
			foreach (Type type in activePanels.Keys) {
				if (activePanels.ContainsKey(type) && activePanels[type] == panel) {
					activePanels.Remove(type);
					break;
				}
			}
		}
	}

}
