using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableString))]
	public class StorableStringDrawer : StorableValueDrawer<string>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.stringValue = valueProperty.stringValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.TextField(rect, label, PlayerPrefs.GetString(keyProperty.stringValue));
		}
	}
}