using System;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Holds the Menu option to create the asset bundles of the proyect
/// </summary>
public class CreateAssetBundles
{
    /// <summary>
    /// Build the bundles of the Scene.
    /// Steps described in the method
    /// </summary>
    [MenuItem("Assets/Build Spawner AssetBundles")]
    static void BuildSpawnerBundles()
    {
        // Get all Spawners in the open Scene
        var spawners = GetSpawners();
        if (spawners.Length == 0)
            return;

        // Creates the output path and it creates it if it doesn't exists
        var outputPath = Path.Combine(Application.streamingAssetsPath , AssetManager.AssetBundleConfig.assetBundleFolderName);
        if(!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        // Update Spawners with bundle and prefab name so they now later what to load
        UpdateSpawnerBundleData(spawners);

        // Creates the data to tell the AssetBundle API how to create the bundles
        var spawnerNames = GetSpawnerPrefabNames(spawners);
        var buildParameters = CreateBuildParameters(spawnerNames, outputPath);
        BuildPipeline.BuildAssetBundles(buildParameters);

        // Refresh Assets
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log("AssetBundles build complete");
    }

    static Spawner[] GetSpawners()
    {
        var spawners = Object.FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        if (spawners.Length == 0)
            return Array.Empty<Spawner>();

        return spawners;
    }

    static string[] GetSpawnerPrefabNames(Spawner[] spawners)
    {
        var names = spawners.Select(spawner => AssetDatabase.GetAssetPath(spawner.Prefab)).ToList();
        return names.Distinct().ToArray();
    }

    static void UpdateSpawnerBundleData(Spawner[] spawners)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            var spawner = spawners[i];

            var prefabName = spawner.Prefab.name;
            spawner.PrefabName = prefabName;
            spawner.AssetBundleName = AssetManager.AssetBundleConfig.spawnerAssetBundleName;

            EditorUtility.SetDirty(spawner);
        }
    }

    static BuildAssetBundlesParameters CreateBuildParameters(string[] assetNames, string outputPath)
    {
        var prefabBuild = new AssetBundleBuild
        {
            assetBundleName = AssetManager.AssetBundleConfig.spawnerAssetBundleName,
            assetNames = assetNames,
        };
        BuildAssetBundlesParameters buildParameters = new()
        {
            outputPath = outputPath,
            options = BuildAssetBundleOptions.None,
            bundleDefinitions = new []{prefabBuild},
            targetPlatform = EditorUserBuildSettings.activeBuildTarget
        };

        return buildParameters;
    }
}