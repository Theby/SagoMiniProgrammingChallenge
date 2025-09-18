using UnityEngine;

/// <summary>
/// Spawns a prefab instance through AssetBundles
/// </summary>
public class Spawner : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] GameObject prefab;
    public GameObject Prefab => prefab;
#endif

    // Normally I'd have these variables as ReadOnly, but to avoid adding unnecessary code creating the decorator they are left as is
    [Header("Asset Bundle Data")]
    [SerializeField] string assetBundleName;
    [SerializeField] string prefabName;

    public string AssetBundleName { get => assetBundleName; set => assetBundleName = value; }
    public string PrefabName { get => prefabName; set => prefabName = value; }

    void Start()
    {
        var asset = AssetManager.LoadAssetBundle<GameObject>(AssetBundleName, PrefabName);
        InstantiateAsset(asset);
    }

    void InstantiateAsset(GameObject assetPrefab)
    {
        Instantiate(assetPrefab, transform);
    }
}