using System;
using System.Collections.Generic;
using UnityEngine;
#if  UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class TextureStorage : MonoBehaviour, IInitializable
{
    #region Field

    [SerializeField]
    protected List<TextureData> textures;

    public TextureDataEvent onTextureAdded;
    public TextureDataEvent onTextureRemoved;

    #endregion Field

    #region Property

    public bool                       IsInitialized { get; protected set; }
    public IReadOnlyList<TextureData> Textures      { get; protected set; }

    #endregion Property

    #region Method

    public virtual void Awake()
    {
        Initialize();
    }

    public virtual bool Initialize()
    {
        if (IsInitialized)
        {
            return false;
        }

        IsInitialized = true;

        textures ??= new List<TextureData>();
        Textures = textures.AsReadOnly();

        return true;
    }

    public virtual bool AddTexture(Texture2D texture)
    {
        return AddTexture(texture, new Vector4(0, 0, 0, 0));
    }

    public virtual bool AddTexture(Texture2D texture, Vector4 meta)
    {
        var data = new TextureData(texture, meta);

        textures.Add(data);
        onTextureAdded.Invoke(data);

        return true;
    }

    public virtual bool RemoveTexture(TextureData data)
    {
        if (!textures.Contains(data))
        {
            return false;
        }

        textures.Remove(data);
        onTextureRemoved.Invoke(data);

        return true;
    }

    #endregion Method
}

#if UNITY_EDITOR
[CustomEditor(typeof(TextureStorage))]
public class TextureStorageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var texturesProperty = serializedObject.FindProperty("textures");

        EditorGUILayout.PropertyField(texturesProperty);

        var rect = GUILayoutUtility.GetLastRect();
        DropArea(rect, (droppedObject) =>
        {
            var texture        = droppedObject as Texture2D;
            var textureStorage = target        as TextureStorage;

            if (texture == null || textureStorage == null)
            {
                return;
            }

            // NOTE:
            // textures shows null when it is empty.

            textureStorage.Initialize();
            textureStorage.AddTexture(texture);
        });

        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTextureAdded"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTextureRemoved"));

        serializedObject.ApplyModifiedProperties();
    }

    public static void DropArea(Rect dropArea, Action<UnityEngine.Object> action)
    {
        if ((Event.current.type != EventType.DragUpdated
          && Event.current.type != EventType.DragPerform)
          || !dropArea.Contains(Event.current.mousePosition))
        {
            return;
        }

        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

        if (Event.current.type == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();

            foreach (var droppedObject in DragAndDrop.objectReferences)
            {
                action(droppedObject);
            }
        }
    }
}
#endif // UNITY_EDITOR