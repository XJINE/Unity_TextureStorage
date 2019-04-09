# Unity_TextureManager

TextureManager provides simple system to manage some textures.

## Import to Your Project

You can import this asset from UnityPackage.

- [TextureManager.unitypackage](https://github.com/XJINE/Unity_TextureManager/blob/master/TextureManager.unitypackage)


### Dependencies

You have to import following assets to use this asset.

- [Unity_IInitializable](https://github.com/XJINE/Unity_IInitializable)

## Limitation

Textures are managed with a unique hash from the name.
So manager can not manage some textures which have the same name.

This is a quite important limitation to sync in each environment, pc, and application.