# SagoMiniProgrammingChallenge

This project was made in Unity 6.0 LTS (6000.0.57f1).

## How to Use and Change

The project is ready to press Play, with 6 diferent spawners using 6 different prefabs all in the same AssetBundle.
It plays a little animation of the objects growing in size and rotating while some particles fly around to some soundwave cyberpunk music.

Every Spawner has assigned a prefab in its inspector, this field is only editable on the Editor using compiler conditionals, meaning its values and code are not carried over when doing a build.

Feel free to change the assigned prefabs, to add, copy or remove spawners, or create a new prefab and assing it to one spawner. 

If changes are done to the Spawners the AssetBundles must be rebuilt to update their content and references. In order to do this go to the "Assets/Build Spawner AssetBundles" option on the toolbar at the top of the Unity window. Pressing this option will update the AssetBundles with the information of the current scene and its spawners.

The way it works is that the CreateAssetBundles script reads all Spawners in the Scene, takes the prefab assigned, updates the Spawners with the name of the bundle and prefab and creates the bundles with this information.

After assigning a prefab to the Spawner this prefab can be renamed and moved to another folder in the project, it doesn't matter as long as you rebuild the AssetBundles after the changes. 

There is an ScriptableObject with some strings for the name of the AssetBundle and the location within the StreamingAssets.

## Limitations of the implementation

- All prefabs in the Spawners are built together in one single AssetBundle.
- If an AssetBundle has a dependency with another AssetBundle this is not handle in the AssetManager.
- It can only load AssetBundles from disk and not download through the web.

## Additional Material used

Color Palette: https://colorhunt.co/palette/090040471396b13bffffcc00
Music from: https://pixabay.com/users/lnplusmusic-47631836