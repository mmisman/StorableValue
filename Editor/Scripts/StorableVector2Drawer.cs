using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableVector2))]
	public class StorableVector2Drawer : StorableValueMultilineDrawer<Vector2>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.vector2Value = valueProperty.vector2Value;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.Vector2Field(rect, label, PlayerPrefsExtensions.GetVector2(keyProperty.stringValue));
		}
	}
}
