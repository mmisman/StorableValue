using UnityEngine;
using UnityEditor;

namespace Mmisman.StorableValue.Editor
{
	public abstract class StorableValueDrawer<T> : PropertyDrawer
	{
		protected SerializedProperty keyProperty, valueProperty, defaultValueProperty;

		protected Rect controlRect;
		protected readonly float labelWidth = 12f, gapWidth = 2f;

		readonly Color32 colorOnSaved = new Color32(0, 161, 223, 255);
		readonly Color32 colorOnNotSaved = new Color32(255, 0, 41, 255);

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			InitSerializedProperties(property);
			DrawPrefixLabel(position, label);
			DrawControls(property);
			EditorGUI.EndProperty();
		}

		void InitSerializedProperties(SerializedProperty property)
		{
			keyProperty = property.FindPropertyRelative("key");
			valueProperty = property.FindPropertyRelative("value");
			defaultValueProperty = property.FindPropertyRelative("defaultValue");
		}

		void DrawPrefixLabel(Rect rect, GUIContent label)
		{
			DrawTooltip(label);
			controlRect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);
		}

		void DrawTooltip(GUIContent label)
		{
			var tooltipAttributes = fieldInfo.GetCustomAttributes(typeof(TooltipAttribute), true) as TooltipAttribute[];
			if (tooltipAttributes.Length > 0)
			{
				label.tooltip = tooltipAttributes[0].tooltip;
			}
		}

		void DrawControls(SerializedProperty property)
		{
			InitControlStyles();
			DrawKey(property);
			DrawValue();
			DrawDefaultValue();
			DrawSavedValue();
			RestoreControlStyles();
		}

		int defaultIndentLevel;
		float defaultLabelWidth;
		void InitControlStyles()
		{
			// Followings are needed to show array's labels correctly.
			defaultIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			defaultLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = labelWidth;
		}

		void RestoreControlStyles()
		{
			EditorGUI.indentLevel = defaultIndentLevel;
			EditorGUIUtility.labelWidth = defaultLabelWidth;
		}

		void DrawKey(SerializedProperty property)
		{
			DrawKeyField();
			FillKeyFieldIfEmpty(property.propertyPath);
		}

		// Draggable property: https://answers.unity.com/questions/606325/how-do-i-implement-draggable-properties-with-custo.html
		void DrawKeyField()
		{
			EditorGUI.PropertyField(GetPropertyRect(0), keyProperty, new GUIContent("K", "Key"));
		}

		// Vector3-like drawing: https://forum.unity.com/threads/making-a-proper-drawer-similar-to-vector3-how.385532/
		protected virtual Rect GetPropertyRect(int ith)
		{
			float propertyWidth = (controlRect.width - gapWidth * 3f) * .25f;
			float posX = controlRect.x + (propertyWidth + gapWidth) * ith;
			return new Rect(posX, controlRect.y, propertyWidth, controlRect.height);
		}

		void FillKeyFieldIfEmpty(string key)
		{
			if (keyProperty.stringValue.Equals(""))
			{
				keyProperty.stringValue = key;
			}
		}

		void DrawValue()
		{
			EditorGUI.BeginChangeCheck();
			DrawValueField(GetPropertyRect(1), new GUIContent("V", "Value"));
			if (EditorGUI.EndChangeCheck() && !EditorApplication.isPlaying)
			{
				SyncDefaultValueWithtValue();
			}
		}

		protected virtual void DrawValueField(Rect rect, GUIContent label)
		{
			EditorGUI.PropertyField(rect, valueProperty, label);
		}

		protected abstract void SyncDefaultValueWithtValue();

		void DrawDefaultValue()
		{
			EditorGUI.BeginDisabledGroup(true);
			DrawDefaultValueField(GetPropertyRect(2), new GUIContent("D", "Default value"));
			EditorGUI.EndDisabledGroup();
		}

		protected virtual void DrawDefaultValueField(Rect rect, GUIContent label)
		{
			EditorGUI.PropertyField(rect, defaultValueProperty, label);
		}

		void DrawSavedValue()
		{
			CacheBackgroundColor();
			DrawBackground();
			DrawSavedValueField();
			RestoreBackgroundColor();
		}

		Color defaultBackgroundColor;
		void CacheBackgroundColor()
		{
			defaultBackgroundColor = GUI.backgroundColor;
		}

		void RestoreBackgroundColor()
		{
			GUI.backgroundColor = defaultBackgroundColor;
		}

		void DrawBackground()
		{
			GUI.backgroundColor = PlayerPrefs.HasKey(keyProperty.stringValue) ? (Color)colorOnSaved : (Color)colorOnNotSaved;
			if (keyProperty.hasMultipleDifferentValues)
			{
				GUI.backgroundColor = defaultBackgroundColor;
			}
		}

		void DrawSavedValueField()
		{
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.showMixedValue = keyProperty.hasMultipleDifferentValues;
			string tooltip = $"Saved value ({(PlayerPrefs.HasKey(keyProperty.stringValue) ? "saved" : "not saved")})";
			DrawSavedValueField(GetPropertyRect(3), new GUIContent("S", tooltip));
			EditorGUI.showMixedValue = false;
			EditorGUI.EndDisabledGroup();
		}

		protected abstract void DrawSavedValueField(Rect rect, GUIContent label);
	}
}