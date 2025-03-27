using OneManEscapePlan.Scripts.Gameplay.Spawning;
using OneManEscapePlan.Scripts.Utility;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.Scripts.Gameplay.Spawning {

	/// <summary>
	/// Spawns objects within the volume of the attached collider.
	/// </summary>
	/// COMPLEXITY: Moderate
	/// CONCEPTS: Colliders, Factories, Bounds
	[RequireComponent(typeof(Collider))]
	[DisallowMultipleComponent()]
	public class SimpleVolumeSpawner : MonoBehaviour {

		[SerializeField] protected FactoryBase factory;
		[SerializeField] protected Color previewColor = Color.yellow;

		new protected Collider collider;

		virtual protected void Awake() {
			collider = GetComponent<Collider>();

			if (factory == null) factory = GetComponent<FactoryBase>();
			Assert.IsNotNull<FactoryBase>(factory, "You forgot to select a factory");
		}

		/// <summary>
		/// Spawn the given number of objects from the factory at random positions within the collider bounds
		/// </summary>
		/// <param name="count">The number of objects to spawn</param>
		public void Spawn(int count) {
			for (int i = 0; i < count; i++) {
				Bounds bounds = collider.bounds;
				float x = Random.Range(-1f, 1f) * bounds.extents.x;
				float y = Random.Range(-1f, 1f) * bounds.extents.y;
				float z = Random.Range(-1f, 1f) * bounds.extents.z;

				Vector3 position = new Vector3(x, y, z) + bounds.center;
				GameObject go = factory.SpawnObject();
				if (go != null) go.transform.position = position;
			}
		}

		#region Editor
#if UNITY_EDITOR
		/// <summary>
		/// Try to draw the collider in the scene view using the GizmoUtils helper class
		/// </summary>
		virtual protected void OnDrawGizmos() {
			Gizmos.color = previewColor;

			if (collider == null) collider = GetComponent<Collider>();
			if (collider != null) {
				GizmoUtils.DrawCollider(collider);
			}
		}
#endif
#endregion
	}
}
