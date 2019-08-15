using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableInt))]
	public class StorableIntDrawer : StorableValueDrawer<int>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.intValue = valueProperty.intValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.IntField(rect, label, PlayerPrefs.GetInt(keyProperty.stringValue));
		}
	}
}