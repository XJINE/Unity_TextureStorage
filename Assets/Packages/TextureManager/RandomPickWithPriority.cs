using UnityEngine;

public static class RandomPickWithPriority
{
    // NOTE:
    // RandomPick with priority is valid when the TextureData.meta.x shows priority.

    public static TextureData RandomPick(this TexturePicker picker, bool priority)
    {
        TextureManager manager = picker.TextureManagers
                                 [Random.Range(0, picker.TextureManagers.Count)];

        return RandomPick(manager, priority);
    }

    public static TextureData RandomPick(this TextureManager manager, bool priority)
    {
        if (priority)
        {
            float seed = Random.value;

            foreach (var data in manager.Textures)
            {
                seed -= data.Value.meta.x;

                if (seed <= 0)
                {
                    return data.Value;
                }
            }
        }

        return manager.RandomPick();
    }
}