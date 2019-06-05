using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    protected Dictionary<string, TextureData> _textures;

    public ReadOnlyDictionary<string, TextureData> Textures
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

        this._textures = new Dictionary<string, TextureData>();

        this.Textures = new ReadOnlyDictionary<string, TextureData>(this._textures);

        foreach (TextureData texture in this.textures)
        {
            this._textures.Add(texture.Name, texture);
        }

        return true;
    }

    public virtual bool AddTexture(Texture2D texture)
    {
        var data = new TextureData(texture);

        if (this._textures.ContainsKey(data.Name))
        {
            return false;
        }

        this.textures.Add(data);
        this._textures.Add(data.Name, data);

        this.onTextureAdded.Invoke(data);

        return true;
    }

    public virtual bool RemoveTexture(TextureData data)
    {
        return this.RemoveTexture(data.Name);
    }

    public virtual bool RemoveTexture(string name)
    {
        if (!this._textures.ContainsKey(name))
        {
            return false;
        }

        var data = this._textures[name];

        this.textures.Remove(data);
        this._textures.Remove(name);

        this.onTextureRemoved.Invoke(data);

        return true;
    }

    public TextureData Pick(int index)
    {
        return this.textures[index];
    }

    public TextureData RandomPick()
    {
        return this.textures[Random.Range(0, this.textures.Count)];
    }

    #endregion Method
}