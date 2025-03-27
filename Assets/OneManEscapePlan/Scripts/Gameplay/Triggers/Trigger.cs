using OneManEscapePlan.Scripts.Events;
using OneManEscapePlan.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace OneManEscapePlan.Scripts.Gameplay.Triggers {

	/// <summary>
	/// The Trigger class is a general-purpose class for detecting collisions and trigger interactions. It
	/// combines logic for handling collisions and triggers, condensing all of the different 2D and 3D
	/// collision/trigger types into three events: Enter, Stay, Exit
	/// It is primarily intended to be used by itself and wired to other components using events, although
	/// it can also be extended by child classes.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Collisions, Gizmos
	public class Trigger : MonoBehaviour {

		#region Fields
		[SerializeField] private bool triggerOnEnter = true;
		[SerializeField] private bool triggerOnStay = false;
		[SerializeField] private bool triggerOnExit = false;

		[Tooltip("GameObjects in this list will be ignored. However, it's better for performance to edit the Layer Collision Matrix in project physics settings instead")]
		[SerializeField] protected List<GameObject> objectsToIgnore = new List<GameObject>();
		[Tooltip("GameObjects with tags in this list will be ignored. However, it's better for performance to edit the Layer Collision Matrix in project physics settings instead")]
		[SerializeField] protected List<string> tagsToIgnore = new List<string>();

		[Header("Editor")]
		[SerializeField] private bool drawWithGizmos = true;
		[Tooltip("The color used to draw this trigger in the Scene view, if drawWithGizmos is checked")]
		[SerializeField] private Color previewColor = new Color(1, .92f, .016f, .5f);

		[Tooltip("This event is invoked during the 'Enter' phase of a collision")]
		[SerializeField] protected GameObjectEvent enterEvent = new GameObjectEvent();
		public GameObjectEvent EnterEvent { get { return enterEvent; } }

		[Tooltip("This event is invoked during the 'Stay' phase of a collision")]
		[SerializeField] protected GameObjectEvent stayEvent = new GameObjectEvent();
		public GameObjectEvent StayEvent { get { return stayEvent; } }

		[Tooltip("This event is invoked during the 'Exit' phase of a collision")]
		[SerializeField] protected GameObjectEvent exitEvent = new GameObjectEvent();
		public GameObjectEvent ExitEvent { get { return exitEvent; } }
		#endregion

		virtual protected void Awake() {

		}

		#region Collision/TriggerEnter
		private void OnCollisionEnter(Collision collision) {
			handleCollisionEnter(collision.gameObject);
		}

		private void OnTriggerEnter(Collider other) {
			handleCollisionEnter(other.gameObject);
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			handleCollisionEnter(collision.gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision) {
			handleCollisionEnter(collision.gameObject);
		}

		private void handleCollisionEnter(GameObject go) {
			if (!triggerOnEnter) return;
			if (objectsToIgnore.Contains(go)) return;
			if (tagsToIgnore.Contains(go.tag)) return;
			enterEvent.Invoke(go);
		}
		#endregion

		#region Collision/TriggerStay
		private void OnCollisionStay(Collision collision) {
			if (!triggerOnStay) return;
			handleCollisionStay(collision.gameObject);
		}

		private void OnTriggerStay(Collider other) {
			if (!triggerOnStay) return;
			handleCollisionStay(other.gameObject);
		}

		private void OnCollisionStay2D(Collision2D collision) {
			if (!triggerOnStay) return;
			handleCollisionStay(collision.gameObject);
		}

		private void OnTriggerStay2D(Collider2D other) {
			if (!triggerOnStay) return;
			handleCollisionStay(other.gameObject);
		}

		private void handleCollisionStay(GameObject go) {
			if (objectsToIgnore.Contains(go)) return;
			if (tagsToIgnore.Contains(go.tag)) return;
			stayEvent.Invoke(go);
		}
		#endregion

		#region Collision/TriggerExit
		private void OnCollisionExit(Collision collision) {
			handleCollisionExit(collision.gameObject);
		}

		private void OnTriggerExit(Collider other) {
			handleCollisionExit(other.gameObject);
		}

		private void OnCollisionExit2D(Collision2D collision) {
			handleCollisionExit(collision.gameObject);
		}

		private void OnTriggerExit2D(Collider2D other) {
			handleCollisionExit(other.gameObject);
		}

		private void handleCollisionExit(GameObject go) {
			if (!triggerOnExit) return;
			if (objectsToIgnore.Contains(go)) return;
			if (tagsToIgnore.Contains(go.tag)) return;
			exitEvent.Invoke(go);
		}
		#endregion

		#region Properties
		public bool TriggerOnEnter {
			get {
				return triggerOnEnter;
			}

			set {
				triggerOnEnter = value;
			}
		}

		public bool TriggerOnStay {
			get {
				return triggerOnStay;
			}

			set {
				triggerOnStay = value;
			}
		}

		public bool TriggerOnExit {
			get {
				return triggerOnExit;
			}

			set {
				triggerOnExit = value;
			}
		}
		#endregion

		#region Editor
#if UNITY_EDITOR
		/// <summary>
		/// Try to draw the collider in the scene view using the GizmoUtils helper class, if that option is enabled
		/// </summary>
		private void OnDrawGizmos() {
			if (!drawWithGizmos) return;

			Gizmos.color = previewColor;

			Collider collider = GetComponentInChildren<Collider>();
			if (collider != null) {
				GizmoUtils.DrawCollider(collider);
			}

			Collider2D collider2D = GetComponentInChildren<Collider2D>();
			if (collider2D != null) {
				GizmoUtils.DrawCollider2D(collider2D);
			}
		}

		//When this trigger is selected in the Scene view, draw color-coded lines from it to all objects that are triggered by it
		private void OnDrawGizmosSelected() {
			this.DrawLinesToListeners(enterEvent, Color.green);
			this.DrawLinesToListeners(stayEvent, Color.yellow);
			this.DrawLinesToListeners(exitEvent, Color.red);
		}
#endif
		#endregion
	}
}
