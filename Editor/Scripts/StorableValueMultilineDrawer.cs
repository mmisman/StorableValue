using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	public abstract class StorableValueMultilineDrawer<T> : StorableValueDrawer<T>
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 4f + EditorGUIUtility.standardVerticalSpacing * 3f;
		}

		protected override Rect GetPropertyRect(int ith)
		{
			float propertyHeight = (controlRect.height - EditorGUIUtility.standardVerticalSpacing * 3f) * .25f;
			float posY = controlRect.y + (propertyHeight + EditorGUIUtility.standardVerticalSpacing) * ith;
			return new Rect(controlRect.x, posY, controlRect.width, propertyHeight);
		}
	}
}