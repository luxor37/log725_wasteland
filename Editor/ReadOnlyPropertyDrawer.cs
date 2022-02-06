using UnityEngine;
using UnityEditor;

/// <summary>
/// This class contain custom drawer for ReadOnly attribute.
/// https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/
/// https://vintay.medium.com/creating-custom-unity-attributes-readonly-d279e1e545c9
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyPropertyDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Disabling edit for property
        GUI.enabled = false;

        // Drawing Property
        EditorGUI.PropertyField(position, property, label);

        // Setting old GUI enabled value
        GUI.enabled = true;
    }
}