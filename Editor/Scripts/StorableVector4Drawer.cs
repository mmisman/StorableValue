using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableVector4))]
	public class StorableVector4Drawer : StorableValueMultilineDrawer<Vector4>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.vector4Value = valueProperty.vector4Value;
		}

		protected override void DrawValueField(Rect rect, GUIContent label)
		{
			valueProperty.vector4Value = DrawField(rect, label, valueProperty.vector4Value);
		}

		protected override void DrawDefaultValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, defaultValueProperty.vector4Value);
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, PlayerPrefsExtensions.GetVector4(keyProperty.stringValue));
		}

		Vector4 DrawField(Rect rect, GUIContent label, Vector4 value)
		{
			GUIContent[] subLables = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z"), new GUIContent("W") };
			float[] subValues = { value.x, value.y, value.z, value.w };
			EditorGUI.MultiFloatField(rect, label, subLables, subValues);
			return new Vector4(subValues[0], subValues[1], subValues[2], subValues[3]);
		}
	}
}