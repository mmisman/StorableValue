using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableFloat))]
	public class StorableFloatDrawer : StorableValueDrawer<float>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.floatValue = valueProperty.floatValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.FloatField(rect, label, PlayerPrefs.GetFloat(keyProperty.stringValue));
		}
	}
}