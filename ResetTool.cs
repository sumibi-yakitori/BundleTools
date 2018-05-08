using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SumibiYakitori.PlaTools {
	public class ResetTool : Tool {
    public ResetTool() : base() {
      this.Text = "Reset Position";
			this.Size = new Vector2(1, 2); // ボタンのサイズ
			this.Filters.NeedSelection = true;
			this.Filters.NeedComponentType = typeof(Transform);
		} 

		public override void Run() {
			var mouseButtonType = Event.current.button;
			if (mouseButtonType == 0) {
				Selection.transforms.ForEach(t => {
					// ほぼ全てのプロパティの変更点はこの関数で記録することが出来ます。しかし、Transformの親の変更、コンポーネントの追加、オブジェクトの破棄はこの関数で記録することが出来ないので専用の関数を使用してください。 内部的にはオブジェクトの状態を一時的にコピーしフレームの最後で差分を取り、正確に何が変更されたかを検出します。変更されたプロパティはUndoスタックに保持されます。もし変更点がなにもない場合（バイナリの正確な比較は全てのプロパティで行われます）Undoスタックには保持されません。
					Undo.RecordObject(t, this.GetType().Name);
					t.localPosition = Vector3.zero;
					t.localRotation = Quaternion.identity;
					t.localScale = Vector3.one;
					EditorUtility.SetDirty(t);
				});
			}
			else {
				EditorUtility.DisplayPopupMenu(new Rect(Input.mousePosition.x, Input.mousePosition.y, 0, 0), "GameObject/", null);
			}
		}
	}

	public class SnapResetTool : Tool {
    public SnapResetTool() : base() {
      this.Text = "Snap Position";
			this.Size = new Vector2(1, 1); // ボタンのサイズ
			this.Filters.NeedSelection = true;
			this.Filters.NeedComponentType = typeof(Transform);
		} 

		public override void Run() {
			var mouseButtonType = Event.current.button;
			if (mouseButtonType == 0) {
				Selection.transforms.ForEach(t => {
					// ほぼ全てのプロパティの変更点はこの関数で記録することが出来ます。しかし、Transformの親の変更、コンポーネントの追加、オブジェクトの破棄はこの関数で記録することが出来ないので専用の関数を使用してください。 内部的にはオブジェクトの状態を一時的にコピーしフレームの最後で差分を取り、正確に何が変更されたかを検出します。変更されたプロパティはUndoスタックに保持されます。もし変更点がなにもない場合（バイナリの正確な比較は全てのプロパティで行われます）Undoスタックには保持されません。
					Undo.RecordObject(t, this.GetType().Name);
          var pos = t.localPosition;
          pos.x = Mathf.RoundToInt(pos.x);
          pos.y = Mathf.RoundToInt(pos.y);
          pos.z = Mathf.RoundToInt(pos.z);
					t.localPosition = pos;
					EditorUtility.SetDirty(t);
				});
			}
			else {
				EditorUtility.DisplayPopupMenu(new Rect(Input.mousePosition.x, Input.mousePosition.y, 0, 0), "GameObject/", null);
			}
		}
	}
}
