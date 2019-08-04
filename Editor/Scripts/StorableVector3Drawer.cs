using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	[CustomPropertyDrawer(typeof(StorableVector3))]
	public class StorableVector3Drawer : StorableValueMultilineDrawer<Vector3>
	{
		protected override void SyncDefaultValueWithtValue()
		{
			defaultValueProperty.vector3Value = valueProperty.vector3Value;
		}

		protected override void DrawSavedValueField(Rect rect, GUIContent label)
		{
			EditorGUI.Vector3Field(rect, label, PlayerPrefsExtensions.GetVector3(keyProperty.stringValue));
		}
	}
}
