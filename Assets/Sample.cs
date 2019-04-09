using UnityEngine;

public class Sample : MonoBehaviour
{
    #region Field

    public TextureManager textureManager;

    #endregion Field

    private void OnGUI()
    {
        this.textureManager.Initialize();

        foreach (var data in this.textureManager.Textures)
        {
            GUILayout.Label(data.Value.ToString());
        }
    }
}