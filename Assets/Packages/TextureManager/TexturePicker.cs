using UnityEngine;
using System.Collections.ObjectModel;

public class TexturePicker : MonoBehaviour, IInitializable
{
    #region Field

    [SerializeField]
    protected TextureManager[] textureManagers;

    #endregion Field

    #region Property

    public bool IsInitialized { get; protected set; }

    public ReadOnlyCollection<TextureManager> TextureManagers { get; protected set; }

    #endregion Property

    #region Indexer

    public TextureData this [string name]
    {
        get
        {
            foreach (TextureManager manager in this.textureManagers)
            {
                if (manager.Textures.ContainsKey(name))
                {
                    return manager.Textures[name];
                }
            }

            throw new System.Collections.Generic.KeyNotFoundException
            (name + " is not found in all of the managers.");
        }
    }

    #endregion Indexer

    #region Method

    protected virtual void Awake()
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

        this.TextureManagers = new ReadOnlyCollection<TextureManager>(this.textureManagers);

        foreach (TextureManager manager in this.textureManagers)
        {
            manager.Initialize();
        }

        return true;
    }

    public virtual TextureData Pick(int managerIndex, int textureIndex)
    {
        return this.textureManagers[managerIndex].Pick(textureIndex);
    }

    public virtual TextureData RandomPick()
    {
        return this.textureManagers
        [Random.Range(0, this.textureManagers.Length)].RandomPick();
    }

    #endregion Method
}