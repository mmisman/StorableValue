using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	[CustomPropertyDrawer(typeof(StorableRect))]
	public class StorableRectDrawer : StorableValueMultilineDrawer<Rect>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.rectValue = valueProperty.rectValue;
		}

		protected override void DrawValueField(Rect rect, GUIContent label)
		{
			valueProperty.rectValue = DrawField(rect, label, valueProperty.rectValue);
		}

		protected override void DrawDefaultValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, defaultValueProperty.rectValue);
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, PlayerPrefsExtensions.GetRect(keyProperty.stringValue));
		}

		Rect DrawField(Rect rect, GUIContent label, Rect value)
		{
			GUIContent[] subLables = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("W", "Width"), new GUIContent("H", "Height") };
			float[] subValues = { value.x, value.y, value.width, value.height };
			EditorGUI.MultiFloatField(rect, label, subLables, subValues);
			return new Rect(subValues[0], subValues[1], subValues[2], subValues[3]);
		}
	}
}