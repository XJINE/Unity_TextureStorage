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

    public Vector4 meta;

    #endregion Field

    #region Property

    public string Name { get { return this.texture.name; } }

    #endregion Property

    #region Constructor

    public TextureData(Texture2D texture)
    {
        this.texture  = texture;
    }

    public TextureData(Texture2D texture, Vector4 meta)
    {
        this.texture  = texture;
    }

    #endregion Constructor

    #region Method

    public override string ToString()
    {
        return this.Name + ", "  + this.meta.ToString();
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

        float height = position.height / 2;
        float metaW  = position.width  / 4;
        float metaY  = position.y + height;

        Rect texRect   = new Rect(position.xMin, position.y, position.width, height);
        Rect metaRectX = new Rect(position.xMin + metaW * 0, metaY, metaW, height);
        Rect metaRectY = new Rect(position.xMin + metaW * 1, metaY, metaW, height);
        Rect metaRectZ = new Rect(position.xMin + metaW * 2, metaY, metaW, height);
        Rect metaRectW = new Rect(position.xMin + metaW * 3, metaY, metaW, height);

        int prevIndentLevel = EditorGUI.indentLevel;

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