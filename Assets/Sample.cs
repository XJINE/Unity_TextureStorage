using UnityEngine;

public class Sample : MonoBehaviour
{
    #region Field

    private TextureManager textureManager;
    private TexturePicker  texturePicker;
    private TextureData    pickedTexture;

    #endregion Field

    #region Method

    private void Awake()
    {
        this.textureManager = base.GetComponent<TextureManager>();
        this.texturePicker  = base.GetComponent<TexturePicker>();
        this.pickedTexture  = this.texturePicker.RandomPick();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.pickedTexture = this.textureManager.RandomPick(true);
        }
    }

    private void OnGUI()
    {
        this.textureManager.Initialize();

        foreach (var data in this.textureManager.Textures)
        {
            GUILayout.Label(data.Value.ToString());
        }

        GUILayout.Label("Press Return to pick random texture with priority.");
        GUILayout.Label("- " + this.pickedTexture.ToString());
    }

    #endregion Method
}