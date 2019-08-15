using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableQuaternion))]
	public class StorableQuaternionDrawer : StorableValueMultilineDrawer<Quaternion>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.quaternionValue = valueProperty.quaternionValue;
		}

		protected override void DrawValueField(Rect rect, GUIContent label)
		{
			valueProperty.quaternionValue = DrawField(rect, label, valueProperty.quaternionValue);
		}

		protected override void DrawDefaultValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, defaultValueProperty.quaternionValue);
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			DrawField(rect, label, PlayerPrefsExtensions.GetQuaternion(keyProperty.stringValue));
		}

		Quaternion DrawField(Rect rect, GUIContent label, Quaternion value)
		{
			GUIContent[] subLables = { new GUIContent("X"), new GUIContent("Y"), new GUIContent("Z"), new GUIContent("W") };
			float[] subValues = { value.x, value.y, value.z, value.w };
			EditorGUI.MultiFloatField(rect, label, subLables, subValues);
			return new Quaternion(subValues[0], subValues[1], subValues[2], subValues[3]);
		}
	}
}