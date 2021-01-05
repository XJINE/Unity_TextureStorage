using System.Collections.Generic;
using UnityEngine;

public class TextureManagers : MonoBehaviour, IInitializable
{
    #region Field

    [SerializeField]
    protected List<TextureManager> managers;

    #endregion Field

    #region Property

    public bool IsInitialized { get; protected set; }

    protected List<TextureManager> _Managers;

    public IReadOnlyList<TextureManager> Managers
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

        this._Managers = new List<TextureManager>();

        this.Managers = this._Managers.AsReadOnly();

        foreach (TextureManager manager in this.managers)
        {
            this._Managers.Add(manager);
        }

        return true;
    }


    #endregion Method
}