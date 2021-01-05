using UnityEngine;

public static class TexturePickerProbability
{
    public static TextureData Pick(TextureManager manager)
    {
        // NOTE:
        // This is valid when the TextureData.meta.x shows probability.

        float seed = Random.value;

        foreach (var data in manager.Textures)
        {
            seed -= data.meta.x;

            if (seed <= 0)
            {
                return data;
            }
        }

        return TexturePicker.RandomPick(manager);
    }
}