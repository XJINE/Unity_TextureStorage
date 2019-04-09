using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
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

    #endregion Field

    #region Property

    public bool IsInitialized
    {
        get; protected set;
    }

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

        return true;
    }

    public virtual TextureData GetRandomTexture(bool proportion = false)
    {
        if (proportion)
        {
            float seedValue = Random.value;

            foreach (var data in this._textures)
            {
                seedValue -= data.Value.proportion;

                if (seedValue <= 0)
                {
                    return data.Value;
                }
            }
        }

        // CAUTION:
        // seedValue may keep it value more than 0.

        return this._textures[this.textures[Random.Range(0, this.textures.Count)].Hash];
    }

    #endregion Method
}