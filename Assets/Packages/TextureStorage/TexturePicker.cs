using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class TexturePicker
{
    #region Method

    public static TextureData Pick(this TextureStorage storage, int index)
    {
        return storage.Textures[index];
    }

    public static TextureData First(this TextureStorage storage, string name)
    {
        return storage.Textures.First(texture => texture.Name == name);
    }

    public static TextureData RandomPick(this TextureStorage storage)
    {
        return storage.Textures[Random.Range(0, storage.Textures.Count)];
    }

    public static TextureData RandomPick(this IList<TextureStorage> storages)
    {
        return RandomPick(storages[Random.Range(0, storages.Count)]);
    }

    #endregion Method
}