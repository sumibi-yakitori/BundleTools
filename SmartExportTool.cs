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
		} 

		public override void Run() {
			//EditorUtility.DisplayCustomMenu(
			SmartExport();

			// EditorUtility.CollectDependenciesした結果をそれぞれGetAssetPathみたいにして、その結果まとめてExport APIにぶっこみゃいいのかな
		}

		[MenuItem( "TEST/SmartExport", false, 0 )]
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

			//AssetDatabase.ExportPackage(
			//		@"Assets\Resources\Prefabs\StageObjects\Common\TreasureChest\Silver\ChestRotation.cs"
			//		, @"D:\Test.unitypackage", ExportPackageOptions.Default);
			//AssetDatabase.ExportPackage(
			//		@"D:\SVN\Artifacts-new\Development\UnityClient\Assets\Resources\Prefabs\StageObjects\Common\TreasureChest\Silver\ChestRotation.cs"
			//		, "Test.unitypackage", ExportPackageOptions.Default);

			//AssetDatabase.ExportPackage(new string[] { assetPathNames[0] }, "Test.unitypackage", ExportPackageOptions.Default);
			//AssetDatabase.ExportPackage(assetPathNames[0], "Test.unitypackage", ExportPackageOptions.Default);
		}
	}
}
