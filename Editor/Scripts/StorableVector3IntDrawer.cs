using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue.Editor
{
	[CustomPropertyDrawer(typeof(StorableVector3Int))]
	public class StorableVector3IntDrawer : StorableValueMultilineDrawer<Vector3Int>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.vector3IntValue = valueProperty.vector3IntValue;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.Vector3IntField(rect, label, PlayerPrefsExtensions.GetVector3Int(keyProperty.stringValue));
		}
	}
}
