#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TextureData
{
    #region Field

    public Texture2D texture;
    public Vector4   meta;

    #endregion Field

    #region Property

    public string Name => texture.name;

    #endregion Property

    #region Constructor

    public TextureData(Texture2D texture)
    {
        this.texture = texture;
    }

    public TextureData(Texture2D texture, Vector4 meta)
    {
        this.texture = texture;
        this.meta    = meta;
    }

    #endregion Constructor

    #region Method

    public override string ToString()
    {
        return Name + ", "  + meta;
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

        var height = position.height / 2f;
        var metaW  = position.width  / 4f;
        var metaY  = position.y + height;

        var texRect   = new Rect(position.xMin, position.y, position.width, height);
        var metaRectX = new Rect(position.xMin + metaW * 0, metaY, metaW, height);
        var metaRectY = new Rect(position.xMin + metaW * 1, metaY, metaW, height);
        var metaRectZ = new Rect(position.xMin + metaW * 2, metaY, metaW, height);
        var metaRectW = new Rect(position.xMin + metaW * 3, metaY, metaW, height);

        var prevIndentLevel = EditorGUI.indentLevel;

        EditorGUI.indentLevel = 0;

        EditorGUI.PropertyField(texRect,   property.FindPropertyRelative("texture"), GUIContent.none);
        EditorGUI.PropertyField(metaRectX, property.FindPropertyRelative("meta.x"),  GUIContent.none);
        EditorGUI.PropertyField(metaRectY, property.FindPropertyRelative("meta.y"),  GUIContent.none);
        EditorGUI.PropertyField(metaRectZ, property.FindPropertyRelative("meta.z"),  GUIContent.none);
        EditorGUI.PropertyField(metaRectW, property.FindPropertyRelative("meta.w"),  GUIContent.none);

        EditorGUI.indentLevel = prevIndentLevel;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2;
    }
}

#endif // UNITY_EDITOR