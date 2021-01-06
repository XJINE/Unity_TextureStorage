using UnityEngine;

public class Sample : MonoBehaviour
{
    #region Field

    private TextureManager textureManager;
    private TextureData    pickedTexture;

    #endregion Field

    #region Method

    private void Awake()
    {
        textureManager = GetComponent<TextureManager>();
        textureManager.Initialize();

        pickedTexture = TexturePicker.RandomPick(textureManager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            pickedTexture = TexturePicker.RandomPick(textureManager);
        }
    }

    private void OnGUI()
    {
        textureManager.Initialize();

        GUILayout.Label("Managed Textures : ");

        foreach (var data in textureManager.Textures)
        {
            GUILayout.Label(data.ToString());
        }

        GUILayout.Label("Press Return to pick random texture.");
        GUILayout.Label("Picked Texture : " + pickedTexture.ToString());
    }

    #endregion Method
}