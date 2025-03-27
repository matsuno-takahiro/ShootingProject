/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomGameView : EditorWindow {

	private RenderTexture rt;

	private int renderWidth;
	private int renderHeight;

	public int RenderWidth {
		get {
			return renderWidth;
		}

		set {
			renderWidth = value;
		}
	}

	public int RenderHeight {
		get {
			return renderHeight;
		}

		set {
			renderHeight = value;
			rt = new RenderTexture(renderWidth, renderHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			rt.anisoLevel = 0;
			rt.antiAliasing = 1;
			rt.autoGenerateMips = false;
			rt.filterMode = FilterMode.Point;
			rt.Create();
		}
	}

	private void Update() {
		Repaint();
	}
	
	private void OnGUI() {
		if (Camera.main != null && rt != null && rt.IsCreated()) {
			rt.anisoLevel = 0;
			rt.antiAliasing = 1;
			rt.filterMode = FilterMode.Point;
			Camera.main.targetTexture = rt;
			//Camera.main.Render();
			float aspect = renderHeight / (float)renderWidth;
			GUI.DrawTexture(new Rect(0, 0, position.width, position.width * aspect), rt);

			GUILayout.BeginArea(new Rect(5, position.width * aspect + 5, 400, 20));
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(renderWidth + " x " + renderHeight, GUILayout.MaxWidth(120))) { 
				applyNativeResolution();
			}
			if (GUILayout.Button("2x", GUILayout.MaxWidth(60))) {
				applyNativeResolution(2);
			}
			if (GUILayout.Button("3x", GUILayout.MaxWidth(60))) {
				applyNativeResolution(3);
			}
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
	}

	private void applyNativeResolution(int scale = 1) {
		Rect pos = this.position;
		pos.width = renderWidth * scale;
		pos.height = renderHeight * scale + 30;
		pos.y += 5; //hack to fix window moving up by 5px each time this function is called
		this.position = pos;
	}

	private void OnDestroy() {
		if (Camera.main != null) {
			if (Camera.main.targetTexture == rt) {
				Camera.main.targetTexture = null;
			}
		}
	}
}
