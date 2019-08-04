using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableBool))]
	public class StorableBoolDrawer : StorableValueDrawer<bool>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.boolValue = valueProperty.boolValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.Toggle(rect, label, PlayerPrefsExtensions.GetBool(keyProperty.stringValue));
		}
	}
}