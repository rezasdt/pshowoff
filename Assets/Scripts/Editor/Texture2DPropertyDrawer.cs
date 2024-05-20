using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Texture2D))]
public class Texture2DPropertyDrawer : PropertyDrawer
{
    private const float PREVIEW_SIZE = 64f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.width -= (PREVIEW_SIZE);
        position.height = EditorGUIUtility.singleLineHeight;

        EditorGUI.PropertyField(position, property, label);

        Texture2D texture = property.objectReferenceValue as Texture2D;

        if (texture != null)
        {
            Rect previewRect = new Rect(position.x + position.width, position.y, PREVIEW_SIZE, PREVIEW_SIZE);

            GUI.DrawTexture(previewRect, texture, ScaleMode.ScaleToFit);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return PREVIEW_SIZE;
    }
}
