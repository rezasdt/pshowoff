using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Tag))]
public class TagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var tagProperty = property.FindPropertyRelative("tag");
        var tags = UnityEditorInternal.InternalEditorUtility.tags;
        var currentTag = tagProperty.stringValue;
        var selectedIndex = Mathf.Max(0, System.Array.IndexOf(tags, currentTag));
        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, tags);

        tagProperty.stringValue = tags[selectedIndex];

        EditorGUI.EndProperty();
    }
}
