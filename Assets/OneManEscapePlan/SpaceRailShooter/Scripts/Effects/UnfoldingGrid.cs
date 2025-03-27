/// © 2018-2019 Kevin Foley
/// For distribution only on the Unity Asset Store
/// Terms/EULA: https://unity3d.com/legal/as_terms

using OneManEscapePlan.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace OneManEscapePlan.SpaceRailShooter.Scripts.Effects {

	/// <summary>
	/// Arranges square sprites into a grid pattern. The squares appear to "unfold" into the grid.
	/// This script is rather complicated and I can't help thinking there's probably a better way
	/// to do this, but nothing's coming to mind...
	/// </summary>
	/// COMPLEXITY: Advanced
	/// CONCEPTS: Coroutines, Assembling GameObjects in code
	[RequireComponent(typeof(BoxCollider))]
	public class UnfoldingGrid : MonoBehaviour {

		[SerializeField] protected Sprite sprite;
		[SerializeField] protected Sprite cornerSprite;
		[SerializeField] protected Material spriteMaterial;
		[SerializeField] protected Color color = Color.cyan;

		[Range(1, 10)]
		[SerializeField] protected int halfWidth = 5;
		[Range(1, 10)]
		[SerializeField] protected int halfHeight = 3;

		[Range(0, 5)]
		[SerializeField] protected float speed = 1.5f;

		[SerializeField] protected float delayBetweenLayers = .1f;

		[Range(.01f, 2f)]
		[SerializeField] protected float fadeDuration = .75f;

		protected SpriteRenderer centerSquare;
		protected List<SpriteRenderer> squares = new List<SpriteRenderer>(45);
		new protected BoxCollider collider;
		protected float squareLength = 0;
		protected float totalWidth;
		protected float totalHeight;

		void Start() {
			Assert.IsNotNull<Material>(spriteMaterial);

			collider = GetComponent<BoxCollider>();

			//we start out with one square in what will be the center of the grid
			centerSquare = createSquare(sprite, new Vector3(), Vector3.one, new Vector3());
			centerSquare.transform.Translate(new Vector3(0, squareLength / 2f, 0));
			squareLength /= transform.localScale.x;
			totalWidth = totalHeight = squareLength;

			updateCollider();

			StartCoroutine(unfold());

			//If the shield has the DestroyTimer component, we will fade it out when it's about to expire
			DestroyTimer timer = GetComponent<DestroyTimer>();
			if (timer != null) {
				StartCoroutine(fadeOut(timer.Time));
			}
		}

		/// <summary>
		/// Expand the collider to fit the current dimensions of the grid
		/// </summary>
		private void updateCollider() {
			collider.size = new Vector3(totalWidth, totalHeight, collider.size.z);
		}

		private SpriteRenderer createSquare(Sprite sprite, Vector3 position, Vector3 scale, Vector3 rotation) {
			GameObject go = new GameObject("GridSquare", typeof(SpriteRenderer));
			SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
			sr.sprite = sprite;
			sr.color = color;
			sr.material = spriteMaterial;
			//calculate square length before setting the parent and rotation of the square; otherwise
			//the bounds would be distorted
			if (squareLength <= 0) squareLength = sr.bounds.size.x * scale.x * transform.localScale.x;
			//set parent/position/rotation/etc
			go.transform.SetParent(this.transform);
			go.transform.localPosition = position;
			go.transform.localScale = scale;
			go.transform.localRotation = Quaternion.Euler(rotation);
			squares.Add(sr);
			return sr;
		}

		private IEnumerator unfold() {
			float overlap = squareLength * .05f;

			for (int i = 1; i < halfWidth || i < halfHeight; i++) {
				int squaresAlongTopBottom = Mathf.RoundToInt(totalWidth / squareLength);
				int squaresAlongLeftRight = Mathf.RoundToInt(totalHeight / squareLength);
				float distX = (totalWidth) / 2f - overlap;
				float distY = (totalHeight) / 2f - overlap;
	
				SpriteRenderer[] topEdge = null;
				SpriteRenderer[] bottomEdge = null;
				SpriteRenderer[] leftEdge = null;
				SpriteRenderer[] rightEdge = null;
				SpriteRenderer cornerTL = null;
				SpriteRenderer cornerTR = null;
				SpriteRenderer cornerBR = null;
				SpriteRenderer cornerBL = null;

				//create new squares along the top and bottom edges if we haven't reached our target height
				if (i < halfHeight) {
					float center = Mathf.FloorToInt(squaresAlongTopBottom / 2f);
					topEdge = new SpriteRenderer[squaresAlongTopBottom];
					bottomEdge = new SpriteRenderer[squaresAlongTopBottom];
					for (int j = 0; j < squaresAlongTopBottom; j++) {
						float offset = (j - center) * squareLength;
						topEdge[j] = createSquare(sprite, new Vector3(offset, -distY, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 0));
						bottomEdge[j] = createSquare(sprite, new Vector3(offset, distY, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 180));
					}
				}
				//create new squares along the left and right edges if we haven't reached our target width
				if (i < halfWidth) {
					float center = Mathf.FloorToInt(squaresAlongLeftRight / 2f);
					leftEdge = new SpriteRenderer[squaresAlongLeftRight];
					rightEdge = new SpriteRenderer[squaresAlongLeftRight];
					for (int j = 0; j < squaresAlongLeftRight; j++) {
						float offset = (j - center) * squareLength;
						leftEdge[j] = createSquare(sprite, new Vector3(-distX, offset, 0), new Vector3(1, 0, 1), new Vector3(0, 0, -90));
						rightEdge[j] = createSquare(sprite, new Vector3(distX, offset, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 90));
					}
				}
				
				//create corners if we are expanding width and height
				if (i < halfWidth && i < halfHeight) {
					cornerTL = createSquare(cornerSprite, new Vector3(-distX, distY, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 180));
					cornerTR = createSquare(cornerSprite, new Vector3(distX, distY, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 90));
					cornerBR = createSquare(cornerSprite, new Vector3(distX, -distY, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 0));
					cornerBL = createSquare(cornerSprite, new Vector3(-distX, -distY, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -90));
				}

				//scale this layer outward to create a "growing" effect
				float scale = 0;
				while (scale <= 1) {
					yield return new WaitForFixedUpdate();
					float change = Time.fixedDeltaTime * speed;
					scale += change;

					//corners
					if (i < halfWidth && i < halfHeight) {
						Vector3 cornerScale = new Vector3(scale, scale, 1);
						cornerTL.transform.localScale = cornerScale;
						cornerTR.transform.localScale = cornerScale;
						cornerBR.transform.localScale = cornerScale;
						cornerBL.transform.localScale = cornerScale;
					}

					Vector3 sideScale = new Vector3(1, scale, 1);
					//top/bottom
					if (i < halfHeight) {
						for (int j = 0; j < squaresAlongTopBottom; j++) {
							topEdge[j].transform.localScale = sideScale;
							bottomEdge[j].transform.localScale = sideScale;
						}
						totalHeight += change * squareLength * 2;
					}
					//sides
					if (i < halfWidth) {
						for (int j = 0; j < squaresAlongLeftRight; j++) {
							leftEdge[j].transform.localScale = sideScale;
							rightEdge[j].transform.localScale = sideScale;
						}
						totalWidth += change * squareLength * 2;
					}
					updateCollider();
				}

				if (delayBetweenLayers > 0) yield return new WaitForSeconds(delayBetweenLayers);
			}
		}

		/// <summary>
		/// Fade out when the shield is about to expire
		/// </summary>
		/// <param name="endTime"></param>
		/// <returns></returns>
		private IEnumerator fadeOut(float endTime) {
			float delay = endTime - fadeDuration;

			float alpha = color.a;

			yield return new WaitForSeconds(delay);
			float t = delay;
			while (t < endTime) {
				t += Time.deltaTime;
				color.a = alpha * (endTime - t) / (endTime - delay);
				foreach (var square in squares) {
					square.color = color;
				}
				yield return null;
			}
		}
	}
}
