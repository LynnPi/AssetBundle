using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 实现不同平台的
/// 
/// </summary>
public class BuildAssetBundle : MonoBehaviour {

	[MenuItem("Assets/Build AssetBundle")]
	public static void Build(){
		//核心的代码其实就一句

		float startTime = Time.realtimeSinceStartup;

		UnityEditor.BuildPipeline.BuildAssetBundles(
			GetOutputPath(), 
			GetAssetBundleOptions(),
			GetTargetPlatform());

		float escaped = Time.realtimeSinceStartup - startTime;
		Debug.LogFormat("used:{0}", escaped);
	}


	/// <summary>
	/// 获取AB包的生成路径 
	/// 设计为通过配置文件获取，这样方便更改，而不用更改代码
	/// 默认为工程的 dataPath目录，如果是，设计一个弹窗作为提示
	/// </summary>
	/// <returns>The output path.</returns>
	public static string GetOutputPath(){
		string outputPath = Application.dataPath;

		var config = Resources.Load<TextAsset>("BundleOutputPath");
		if(config == null){
			Debug.Log("read BundleOutputPath file from resources FAILED!");
			return outputPath;
		}
		if(outputPath == Application.dataPath){
			EditorUtility.DisplayDialog("Warning", "project not set build AssetBundle outputPath!",
				"go ahead", "cancel and set");
		}
		return outputPath;
	}


	private static BuildAssetBundleOptions GetAssetBundleOptions(){
		return BuildAssetBundleOptions.UncompressedAssetBundle;
	}

	private static BuildTarget GetTargetPlatform(){
		return BuildTarget.Android;
	}
}
