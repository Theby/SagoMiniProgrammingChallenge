using UnityEngine;

/// <summary>
/// Configuration File for easily changing the folder and the AssetBundle name
/// </summary>
[CreateAssetMenu(fileName = "AssetBundleConfig", menuName = "ScriptableObjects/AssetBundleConfig")]
public class AssetBundleConfig : ScriptableObject
{
    public string assetBundleFolderName = "AssetBundles";
    public string spawnerAssetBundleName = "Spawners";
}