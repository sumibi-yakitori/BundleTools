using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.IO;

namespace SumibiYakitori.PlaTools {
	public class SmartExportTool : Tool {
		public SmartExportTool() : base() { 
			this.Text = "SmartExport";
			this.Filters.NeedSelection = true;
			this.Filters.NeedType = typeof(GameObject);
		} 

		public override void Run() {
			SmartExport();
		}

		//[MenuItem( "TEST/SmartExport", false, 0 )]
		public static void SmartExport() {
			var target = Selection.activeGameObject;
			var dependObjs = EditorUtility.CollectDependencies(new UnityEngine.Object[] { target });
			var assetPathNames = dependObjs
				//.Select(obj => 
				//	Path.Combine((new DirectoryInfo(Application.dataPath)).Parent.FullName.Replace('\\', Path.DirectorySeparatorChar)
				//	, AssetDatabase.GetAssetPath(obj).Replace('/', Path.DirectorySeparatorChar)))
				.Select(obj => AssetDatabase.GetAssetPath(obj))
				.Distinct()
				.Where(path => File.Exists(path))
				.ToArray();
			Debug.Log(string.Join("\n", assetPathNames));
			AssetDatabase.ExportPackage(assetPathNames, target.name + ".unitypackage");
		}
	}
}
