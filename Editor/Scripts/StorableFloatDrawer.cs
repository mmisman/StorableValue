using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	[CustomPropertyDrawer(typeof(StorableFloat))]
	public class StorableFloatDrawer : StorableValueDrawer<float>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.floatValue = valueProperty.floatValue;
		}

		protected override float DrawSavedValue(Rect rect, GUIContent label)
		{
			return EditorGUI.FloatField(rect, label, PlayerPrefs.GetFloat(keyProperty.stringValue));
		}

		protected override void Save(float value)
		{
			PlayerPrefs.SetFloat(keyProperty.stringValue, value);
		}
	}
}