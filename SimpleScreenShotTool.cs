using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// using UniRx;
// using UniRx.Triggers;

namespace SumibiYakitori.PlaTools {
	public class SimpleScreenShotTool : Tool {
		public SimpleScreenShotTool() : base() { 
			this.Text = "ScreenShot";
			this.Filters.IsEnabled = true;
			this.Filters.NeedSelection = false;
		} 

		public override void Run() {
			var t = SceneView.lastActiveSceneView.camera.transform;
			var comp = new GameObject().AddComponent<CaptureComponent>();
			comp.sourcePosition = t.position;
			comp.sourceRotation = t.rotation;

			EditorApplication.isPlaying = true;
		}
	}

	public class CaptureComponent : MonoBehaviour {
		// エディタモード時に値を保存するためにフィールドに
		[SerializeField] public Vector3 sourcePosition = Vector3.zero;
		[SerializeField] public Quaternion sourceRotation = Quaternion.identity;

		protected void Start() {
			var dest = Camera.main.gameObject.transform;
			dest.position = this.sourcePosition;
			dest.rotation = this.sourceRotation;

			ScreenCapture.CaptureScreenshot("Capture.png", 4);

			EditorApplication.playmodeStateChanged += this.Dispose;
			EditorApplication.isPlaying = false;
		}

		private void Dispose() {
			if (EditorApplication.isPlaying == false) {
				EditorApplication.playmodeStateChanged -= this.Dispose;
				Object.DestroyImmediate(this.gameObject);
			}
		}
	}
}

