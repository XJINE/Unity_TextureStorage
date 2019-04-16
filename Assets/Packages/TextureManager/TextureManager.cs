using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TextureManager : MonoBehaviour, IInitializable
{
    #region Class

    [System.Serializable]
    public class TextureData
    {
        #region Field

        public float proportion;

        public Texture2D texture;

        #endregion Field

        #region Property

        public int Hash { get { return this.texture.name.GetHashCode(); } }

        #endregion Property

        #region Constructor

        public TextureData(float proportion, Texture2D texture)
        {
            this.proportion = proportion;
            this.texture    = texture;
        }

        #endregion Constructor

        #region Method

        public override string ToString()
        {
            return this.proportion + " : " + this.texture.name + " (" + this.Hash + ")";
        }

        #endregion Method
    }

    [System.Serializable]
    public class TextureDataEvent : UnityEvent<TextureData> { }

    #if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(TextureData))]
    public class TextureDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            Rect proportionRect = new Rect(position.xMin,       position.y, position.width / 2, position.height);
            Rect textureRect    = new Rect(proportionRect.xMax, position.y, position.width / 2, position.height);

            EditorGUI.PropertyField(proportionRect, property.FindPropertyRelative("proportion"), GUIContent.none);
            EditorGUI.PropertyField(textureRect,    property.FindPropertyRelative("texture"),    GUIContent.none);

            EditorGUI.EndProperty();
        }
    }

    #endif // UNITY_EDITOR

    #endregion Class

    #region Field

    [SerializeField]
    protected List<TextureData> textures;

    public TextureDataEvent onTextureAdded;

    public TextureDataEvent onTextureRemoved;

    #endregion Field

    #region Property

    public bool IsInitialized { get; protected set; }

    protected Dictionary<int, TextureData> _textures;

    public ReadOnlyDictionary<int, TextureData> Textures
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

        this._textures = new Dictionary<int, TextureData>();

        this.Textures = new ReadOnlyDictionary<int, TextureData>(this._textures);

        foreach (TextureData texture in this.textures)
        {
            this._textures.Add(texture.Hash, texture);
        }

        return true;
    }

    public virtual bool AddTexture(float proportion, Texture2D texture)
    {
        var data = new TextureData(proportion, texture);

        if (this._textures.ContainsKey(data.Hash))
        {
            return false;
        }

        this.textures.Add(data);
        this._textures.Add(data.Hash, data);

        this.onTextureAdded.Invoke(data);

        return true;
    }

    public virtual bool RemoveTexture(TextureData data)
    {
        return this.RemoveTexture(data.Hash);
    }

    public virtual bool RemoveTexture(int hash)
    {
        if (!this._textures.ContainsKey(hash))
        {
            return false;
        }

        var data = this._textures[hash];

        this.textures.Remove(data);
        this._textures.Remove(hash);

        this.onTextureRemoved.Invoke(data);

        return true;
    }

    public TextureData Pick(int index)
    {
        return this.textures[index];
    }

    public TextureData RandomPick(bool withProportion = false)
    {
        return this.textures[Random.Range(0, this.textures.Count)];
    }

    #endregion Method
}