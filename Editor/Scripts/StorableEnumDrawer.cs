using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	public class StorableEnumDrawer<T> : StorableValueDrawer<T> where T : struct
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.enumValueIndex = valueProperty.enumValueIndex;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.EnumPopup(rect, label, PlayerPrefsExtensions.GetEnum<T>(keyProperty.stringValue) as System.Enum);
		}
	}
}