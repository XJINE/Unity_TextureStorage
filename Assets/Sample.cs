using UnityEngine;

public class Sample : MonoBehaviour
{
    #region Field

    private TextureStorage _storage;
    private TextureData    _picked;

    #endregion Field

    #region Method

    private void Awake()
    {
        _storage = GetComponent<TextureStorage>();
        _storage.Initialize();

        _picked = _storage.RandomPick();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _picked = _storage.RandomPick();
        }
    }

    private void OnGUI()
    {
        _storage.Initialize();

        GUILayout.Label("Managed Textures : ");

        foreach (var texture in _storage.Textures)
        {
            GUILayout.Label(texture.ToString());
        }

        GUILayout.Label("Press Return to pick random texture.");
        GUILayout.Label("Picked Texture : " + _picked);
    }

    #endregion Method
}