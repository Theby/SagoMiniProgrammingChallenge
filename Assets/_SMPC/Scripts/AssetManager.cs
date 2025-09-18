using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Load and unload AssetBundles
/// </summary>
public class AssetManager : MonoBehaviour
{
    static AssetBundleConfig _assetBundleConfig;
    public static AssetBundleConfig AssetBundleConfig
    {
        get
        {
            if (_assetBundleConfig == null)
            {
                _assetBundleConfig = Resources.Load<AssetBundleConfig>("AssetBundleConfig");
            }
            return _assetBundleConfig;
        }
    }

    static readonly Dictionary<string, AssetBundle> LoadedAssetBundles = new Dictionary<string, AssetBundle>();
    
    void OnDestroy()
    {
        UnloadAllAssetBundles();
    }

    /// <summary>
    /// Load a given asset from a given AssetBundle
    /// </summary>
    /// <param name="bundleName">The name of the bundle</param>
    /// <param name="assetName">The name of the asset to Load</param>
    /// <returns>Loaded Asset</returns>
    public static T LoadAssetBundle<T>(string bundleName, string assetName) where T : Object
    {
        string assetBundleFolderPath = Path.Combine(Application.streamingAssetsPath, AssetBundleConfig.assetBundleFolderName);
        string bundlePath = Path.Combine(assetBundleFolderPath, bundleName);

        // If bundle is already loaded don't try to load again
        if (!LoadedAssetBundles.TryGetValue(bundleName, out var assetBundle))
        {
            assetBundle = AssetBundle.LoadFromFile(bundlePath);
            LoadedAssetBundles.Add(bundleName, assetBundle);
        }

        // Maybe a similar cache could be done for loaded assets if it was necessary
        var asset = assetBundle.LoadAsset<T>(assetName);
        return asset;
    }

    public static void UnloadAssetBundle(string assetBundleName)
    {
        if (!LoadedAssetBundles.TryGetValue(assetBundleName, out var assetBundle))
            return;

        assetBundle.Unload(true);
        LoadedAssetBundles.Remove(assetBundleName);
    }

    public static void UnloadAllAssetBundles()
    {
        foreach (var loadedAssetBundle in LoadedAssetBundles)
        {
            var assetBundle = loadedAssetBundle.Value;
            assetBundle.Unload(true);
        }

        LoadedAssetBundles.Clear();
    }
}