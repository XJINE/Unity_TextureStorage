# Unity_TextureManager

<img src="https://github.com/XJINE/Unity_TextureManager/blob/master/Screenshot.png" width="100%" height="auto" />

``TextureManager`` provides simple system to manage some textures.
Each texture data could have some meta values.

## Note

``TextureManager`` manages some textures with these name.
Because of the ``GetHashCode()`` in .Net is not guarantee the return value is unique.
In another Apps or with another version .Net, it may return another value.

- Reference
-- https://docs.microsoft.com/ja-jp/dotnet/api/system.string.gethashcode?view=netframework-4.8

### Limitation

Texture name is must be unique because Manager manages with the name.

This is a quite important limitation to sync with another apps.

## Import to Your Project

You can import this asset from UnityPackage.

- [TextureManager.unitypackage](https://github.com/XJINE/Unity_TextureManager/blob/master/TextureManager.unitypackage)

### Dependencies

You have to import following assets to use this asset.

- [Unity_IInitializable](https://github.com/XJINE/Unity_IInitializable)
