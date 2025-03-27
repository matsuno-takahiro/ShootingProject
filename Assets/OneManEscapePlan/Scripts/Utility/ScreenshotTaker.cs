/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OneManEscapePlan.Scripts.Utility {
	public class ScreenshotTaker : MonoBehaviour {

		public KeyCode key;
		[Range(1, 8)]
		public int scale = 1;

		private void Update() {
			if (Input.GetKeyDown(key)) {
				string fileName = DateTime.Now.ToString().Replace('/', '-').Replace(':',' ') + ".png";
				string folder = "Screenshots";
				ScreenCapture.CaptureScreenshot(folder + "/" + fileName, scale);
			}
		}

	}
}
