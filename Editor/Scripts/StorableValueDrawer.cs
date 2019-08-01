using UnityEngine;
using UnityEditor;

namespace Mmisman.StorableValue
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
			DrawSerializedKey(property);
			DrawSerializedValue();
			DrawSerialziedDefaultValue();
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

		void DrawSerializedKey(SerializedProperty property)
		{
			DrawKeyPropertyField();
			FillKeyPropertyIfEmpty(property.propertyPath);
		}

		// Draggable property: https://answers.unity.com/questions/606325/how-do-i-implement-draggable-properties-with-custo.html
		void DrawKeyPropertyField()
		{
			EditorGUI.PropertyField(GetPropertyRect(0), keyProperty, new GUIContent("K", "Key"));
		}

		// Vector3-like drawing: https://forum.unity.com/threads/making-a-proper-drawer-similar-to-vector3-how.385532/
		protected virtual Rect GetPropertyRect(int ith)
		{
			float propertyWidth = (controlRect.width - gapWidth * 3f) * 0.25f;
			float posX = controlRect.x + (propertyWidth + gapWidth) * ith;
			return new Rect(posX, controlRect.y, propertyWidth, controlRect.height);
		}

		void FillKeyPropertyIfEmpty(string key)
		{
			if (keyProperty.stringValue.Equals(""))
			{
				keyProperty.stringValue = key;
			}
		}

		void DrawSerializedValue()
		{
			EditorGUI.BeginChangeCheck();
			DrawValuePropertyField();
			if (EditorGUI.EndChangeCheck() && !EditorApplication.isPlaying)
			{
				SyncDefaultValueWithtValue();
			}
		}

		void DrawValuePropertyField()
		{
			EditorGUI.PropertyField(GetPropertyRect(1), valueProperty, new GUIContent("V", "Value"));
		}

		protected abstract void SyncDefaultValueWithtValue();

		void DrawSerialziedDefaultValue()
		{
			DrawDefaultValuePropertyField();
		}

		void DrawDefaultValuePropertyField()
		{
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.PropertyField(GetPropertyRect(2), defaultValueProperty, new GUIContent("D", "Default value"));
			EditorGUI.EndDisabledGroup();
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
			DrawSavedValue(GetPropertyRect(3), new GUIContent("S", tooltip));
			EditorGUI.showMixedValue = false;
			EditorGUI.EndDisabledGroup();
		}

		protected abstract void DrawSavedValue(Rect rect, GUIContent label);
	}
}