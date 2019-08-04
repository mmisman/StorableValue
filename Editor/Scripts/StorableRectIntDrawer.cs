using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableRectInt))]
	public class StorableRectIntDrawer : StorableValueMultilineDrawer<RectInt>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.rectIntValue = valueProperty.rectIntValue;
		}

		protected override void DrawValueField(Rect rect, GUIContent label)
		{
			valueProperty.rectIntValue = DrawField(rect, label, valueProperty.rectIntValue);
		}

		protected override void DrawDefaultValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, defaultValueProperty.rectIntValue);
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, PlayerPrefsExtensions.GetRectInt(keyProperty.stringValue));
		}

		RectInt DrawField(Rect rect, GUIContent label, RectInt value)
		{
			float subLabelWidth = labelWidth - 1;
			Rect labelRect = new Rect(rect.x, rect.y, subLabelWidth, rect.height);
			EditorGUI.LabelField(labelRect, label);

			Rect controlRect = new Rect(rect.x + subLabelWidth, rect.y, rect.width - subLabelWidth, rect.height);
			GUIContent[] subLables = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("W", "Width"), new GUIContent("H", "Height") };
			int[] subValues = { value.x, value.y, value.width, value.height };
			EditorGUI.MultiIntField(controlRect, subLables, subValues);
			return new RectInt(subValues[0], subValues[1], subValues[2], subValues[3]);
		}
	}
}