using UnityEditor;
using UnityEngine;

namespace Mmisman.StorableValue
{
	[CustomPropertyDrawer(typeof(StorableFloat))]
	public class StorableFloatDrawer : StorableValueDrawer<float>
	{
		protected override void SetDefaultValueProperty()
		{
			if (!EditorApplication.isPlaying && Mathf.Abs(defaultValueProperty.floatValue - valueProperty.floatValue) > float.Epsilon)
			{
				defaultValueProperty.floatValue = valueProperty.floatValue;
			}
		}
	}
}