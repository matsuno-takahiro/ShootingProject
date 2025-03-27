/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Gameplay.DamageSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace OneManEscapePlan.Scripts.UI.HUD {

	/// <summary>
	/// Attach this component to a Slider that will represent the current hit points
	/// of a HealthComponent
	/// </summary>
	/// COMPLEXITY: Beginner
	/// CONCEPTS: Sliders, UnityEvents
	[RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour {

		[DrawConnections(ColorName.PulsingYellow)]
		[SerializeField] protected HealthComponent healthComponent;

		private Slider slider;

		// Use this for initialization
		void Start() {
            Assert.IsNotNull<HealthComponent>(healthComponent);

            slider = GetComponent<Slider>();
            slider.minValue = 0;

			HealthComponent = healthComponent;
        }

        private void onHealthChange(IHealth hp) {
            slider.maxValue = hp.MaxHP;
            slider.value = hp.HP;
        }

		/// <summary>
		/// The HealthComponent represented by this slider
		/// </summary>
		public HealthComponent HealthComponent {
			get {
				return healthComponent;
			}

			set {
				//remove event from any previous healthComponent
				if (healthComponent != null) {
					healthComponent.HealthChangeEvent.RemoveListener(onHealthChange);
				}

				//assign new healthComponent
				healthComponent = value;
				if (healthComponent != null) {
					healthComponent.HealthChangeEvent.AddListener(onHealthChange);
					onHealthChange(healthComponent);
				}
			}
		}
	}

}
