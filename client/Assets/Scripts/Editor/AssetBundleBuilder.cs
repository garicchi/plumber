using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class AssetBundleBuilder : MonoBehaviour
{
    [MenuItem("Assets/Build Asset Bundles")]
    static void BuildAssetBundle() 
    {
        var options = BuildAssetBundleOptions.None;
        var outputPath = "AssetBundles/Android";
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        var target = BuildTarget.Android;
        BuildPipeline.BuildAssetBundles(outputPath, options, target);

        outputPath = "AssetBundles/Windows";
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        options = BuildAssetBundleOptions.None;
        target = BuildTarget.StandaloneWindows;
        BuildPipeline.BuildAssetBundles(outputPath, options, target);
    }
}
