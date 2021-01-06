using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class TexturePicker
{
    #region Method

    public static TextureData Pick(TextureManager manager, int index)
    {
        return manager.Textures[index];
    }

    public static TextureData First(TextureManager manager, string name)
    {
        return manager.Textures.First(texture => texture.Name == name);
    }

    public static TextureData RandomPick(TextureManager manager)
    {
        return manager.Textures[Random.Range(0, manager.Textures.Count)];
    }

    public static TextureData RandomPick(IList<TextureManager> managers)
    {
        return RandomPick(managers[Random.Range(0, managers.Count)]);
    }

    #endregion Method
}