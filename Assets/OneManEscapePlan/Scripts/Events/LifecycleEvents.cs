/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Events {

	/// <summary>
	/// Provides UnityEvents that are invoked when the corresponding lifecycle event function is
	/// called, giving us an easy means to wire functionality to these lifecycle events in the
	/// inspector.
	/// 
	/// For example, if we want a UI panel to play a sound effect every time it appears, and a
	/// different sound effect every time it is closed, we could add the LifecycleEvents script
	/// and use the "EnableEvent" and "DisableEvent" events to play the desired audio clips from
	/// an audio source.
	/// 
	/// In some cases, we may need to add a one-frame delay before the UnityEvent is invoked. For
	/// example, if we add this script to a Panel and use the AwakeEvent to Select() a Button
	/// without the one-frame delay, the button won't change color because we called Select() too
	/// early. However, adding the delay causes the button to change color correctly.
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS:  Unity event functions (https://docs.unity3d.com/Manual/ExecutionOrder.html), UnityEvents, Coroutines
	public class LifecycleEvents : MonoBehaviour {

		[Tooltip("Delay activation of Awake/Start/Enable events by one frame")]
		[SerializeField] protected bool delayOneFrame = false;

		[DrawConnections(ColorName.Yellow)]
		[SerializeField] private GameObjectEvent awakeEvent = new GameObjectEvent();
		public GameObjectEvent AwakeEvent { get { return awakeEvent; } }

		[DrawConnections(ColorName.Blue)]
		[SerializeField] private GameObjectEvent enableEvent = new GameObjectEvent();
		public GameObjectEvent EnableEvent { get { return enableEvent;  } }

		[DrawConnections(ColorName.Green)]
		[SerializeField] private GameObjectEvent startEvent = new GameObjectEvent();
		public GameObjectEvent StartEvent { get { return startEvent; } }

		[DrawConnections(ColorName.Gray)]
		[SerializeField] private GameObjectEvent disableEvent = new GameObjectEvent();
		public GameObjectEvent DisableEvent { get { return disableEvent; } }

		[DrawConnections(ColorName.Red)]
		[SerializeField] private GameObjectEvent destroyEvent = new GameObjectEvent();
		public GameObjectEvent DestroyEvent { get { return destroyEvent; } }

		private void Awake() {
			StartCoroutine(invokeEventWithDelay(awakeEvent));
		}

		private void Start() {
			StartCoroutine(invokeEventWithDelay(startEvent));
		}

		private void OnEnable() {
			StartCoroutine(invokeEventWithDelay(enableEvent));
		}

		private void OnDisable() {
			//coroutines can't run on a disabled object, so the delay isn't possible (or necessary) here
			disableEvent.Invoke(gameObject);
		}

		private void OnDestroy() {
			//we can't use a delay here, because this gameobject won't exist next frame
			destroyEvent.Invoke(gameObject);
		}

		/// <summary>
		/// Invoke the given event
		/// </summary>
		/// <param name="event">The GameObjectEvent to invoke</param>
		virtual protected IEnumerator invokeEventWithDelay(GameObjectEvent @event) {
			if (delayOneFrame) yield return new WaitForEndOfFrame();
			@event.Invoke(gameObject);
		}
	}
}
