/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// When the camera pans or yaws, move the attached image in the opposite direction
/// to convey the illusion of it being a skybox. For this to work properly, several
/// conditions must be met:
///  
/// - The canvas must be set to "Screen Space - Camera", with a Plane Distance 
///   that puts it behind all other visible objects (ideally, the plane distance
///   will be very slightly less than the camera's far clipping plane)
///   
/// - The image must be scaled large enough so that it fills the whole screen, and
///   we must not rotate the camera too far
///   
/// - If we are using a starfield-style skybox and don't want the stars to flicker,
///   we may need to turn on "Pixel Perfect"
///   
/// - The pan speed must be set to an appropriate value that matches the camera FOV
/// 
/// - The camera must not tilt along the z-axis, as this would require rotating the
///   background, but "Pixel Perfect" does not work on rotated images.
/// 
/// </summary>
/// COMPLEXITY: Beginner
[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class SimulatedBackground : MonoBehaviour {

	[Tooltip("Controls how fast the background pans as the camera pans and yaws")]
	[SerializeField] protected float panSpeed = 3.6f;

	[SerializeField] protected bool setPanSpeedAutomatically = true;

	new private Camera camera;

	protected Image image;

	private void Start() {
		camera = Camera.main;
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {
		//Sorry for the magic number formula here. 3.6f seems to be a good value for 60 degree FOV, and
		//the appropriate pan speed seems to be roughly inversely proportional to the FOV (e.g. 7.2 is
		//a good value for an FOV of 30). Does not hold for extremely large FOVs (> 90 degrees)
		if (setPanSpeedAutomatically) panSpeed = (60 / camera.fieldOfView) * 3.6f;

		Vector3 rotation = camera.transform.rotation.eulerAngles;
		float x = MathUtils.constrainAngle(rotation.y) * -panSpeed;
		float y = MathUtils.constrainAngle(rotation.x) * panSpeed;
		image.rectTransform.anchoredPosition = new Vector2(x , y);
	}
}
