using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour, IInitializable
{
    #region Field

    [SerializeField]
    protected List<TextureData> textures;

    public TextureDataEvent onTextureAdded;

    public TextureDataEvent onTextureRemoved;

    #endregion Field

    #region Property

    public bool IsInitialized { get; protected set; }

    protected List<TextureData> _Textures;

    public IReadOnlyList<TextureData> Textures
    {
        get; protected set;
    }

    #endregion Property

    #region Method

    public virtual void Awake()
    {
        Initialize();
    }

    public virtual bool Initialize()
    {
        if (this.IsInitialized)
        {
            return false;
        }

        this.IsInitialized = true;

        this._Textures = new List<TextureData>();

        this.Textures = this._Textures.AsReadOnly();

        foreach (TextureData texture in this.textures)
        {
            this._Textures.Add(texture);
        }

        return true;
    }

    public virtual bool AddTexture(Texture2D texture)
    {
        var data = new TextureData(texture);

        this.textures.Add(data);
        this._Textures.Add(data);

        this.onTextureAdded.Invoke(data);

        return true;
    }

    public virtual bool RemoveTexture(TextureData data)
    {
        if (!this._Textures.Contains(data))
        {
            return false;
        }

        this.textures.Remove(data);
        this._Textures.Remove(data);

        this.onTextureRemoved.Invoke(data);

        return true;
    }

    #endregion Method
}