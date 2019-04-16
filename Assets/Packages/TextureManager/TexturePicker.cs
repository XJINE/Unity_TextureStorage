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

    public virtual TextureManager.TextureData Pick(int index, bool withProportion = false)
    {
        return this.textureManagers[index].RandomPick(withProportion);
    }

    public virtual TextureManager.TextureData RandomPick(bool withProportion = false)
    {
        return this.textureManagers[Random.Range(0, this.textureManagers.Length)]
                   .RandomPick(withProportion);
    }

    #endregion Method
}