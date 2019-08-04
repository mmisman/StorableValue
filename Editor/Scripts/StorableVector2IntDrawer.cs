using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	[CustomPropertyDrawer(typeof(StorableVector2Int))]
	public class StorableVector2IntDrawer : StorableValueMultilineDrawer<Vector2Int>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.vector2IntValue = valueProperty.vector2IntValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.Vector2IntField(rect, label, PlayerPrefsExtensions.GetVector2Int(keyProperty.stringValue));
		}
	}
}
