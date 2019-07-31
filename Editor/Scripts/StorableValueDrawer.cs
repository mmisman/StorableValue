using UnityEngine;
using UnityEditor;

namespace Mmisman.StorableValue
{
	public abstract class StorableValueDrawer<T> : PropertyDrawer
	{
		protected SerializedProperty keyProperty, valueProperty, defaultValueProperty;

		protected Rect controlRect;
		protected float labelWidth = 12f, gapWidth = 2f;
		float defaultLabelWidth;
		int defaultIndentLevel;
		Color defaultBackgroundColor;

		Color32 colorOnSaved = new Color32(0, 161, 223, 255);
		Color32 colorOnNotSaved = new Color32(255, 0, 41, 255);

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
			DrawKeyProperty(property);
			DrawValueProperty();
			DrawDefaultValueProperty();
			//DrawSavedValueProperty();
			//RestoreEditorGUI();
		}

		protected virtual void InitControlStyles()
		{
			// Need to show array's labels.
			defaultIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			defaultLabelWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = labelWidth;
		}

		void DrawKeyProperty(SerializedProperty property)
		{
			DrawKeyLabelAndPropertyField();
			FillKeyPropertyIfEmpty(property.propertyPath);
		}

		// Draggable property: https://answers.unity.com/questions/606325/how-do-i-implement-draggable-properties-with-custo.html
		protected virtual void DrawKeyLabelAndPropertyField()
		{
			EditorGUI.PropertyField(GetPropertyRect(0), keyProperty, new GUIContent("K", "Key"));
		}

		// Vector3-like drawing: https://forum.unity.com/threads/making-a-proper-drawer-similar-to-vector3-how.385532/
		Rect GetPropertyRect(int ith)
		{
			float propertyWidth = controlRect.width * 0.25f - gapWidth * 3f;
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

		void DrawValueProperty()
		{
			DrawValueLabelAndPropertyField();
		}

		protected virtual void DrawValueLabelAndPropertyField()
		{
			EditorGUI.PropertyField(GetPropertyRect(1), valueProperty, new GUIContent("V", "Value"));
		}

		void DrawDefaultValueProperty()
		{
			SetDefaultValueProperty();
			DrawDefaultValueLabelAndPropertyField();
		}

		protected abstract void SetDefaultValueProperty();

		protected virtual void DrawDefaultValueLabelAndPropertyField()
		{
			EditorGUI.BeginDisabledGroup(true);
			EditorGUI.PropertyField(GetPropertyRect(2), defaultValueProperty, new GUIContent("D", "Default value"));
			EditorGUI.EndDisabledGroup();
		}

		//void DrawSavedValueProperty()
		//{
		//	EditorGUI.BeginChangeCheck();
		//	DrawSavedValuePropertyAndBackground();
		//	if (EditorGUI.EndChangeCheck())
		//	{
		//		Undo.RecordObjects(savedValueProperty.serializedObject.targetObjects, "Saved Value Change");
		//		PlayerPrefsEliteUtility.MakePlayerPrefsEliteAvailableInEditMode();
		//		ApplyChangeToInspectedSavedValues();
		//		SaveChangeToInspectedToryValues();
		//	}
		//	if (SavedButInconsistent())
		//	{
		//		PlayerPrefsEliteUtility.MakePlayerPrefsEliteAvailableInEditMode();
		//		FetchInspectedToryValueSavedValue();
		//	}
		//}

		//void DrawSavedValuePropertyAndBackground()
		//{
		//	CacheBackgroundColor();
		//	DrawBackground();
		//	DrawSavedValueLabelField();
		//	DrawSavedValuePropertyField();
		//	RestoreBackgroundColor();
		//}

		//void CacheBackgroundColor()
		//{
		//	cachedBackgroundColor = GUI.backgroundColor;
		//}

		//void DrawBackground()
		//{
		//	GUI.backgroundColor = Saved() ? (Color)colorOnSaved : (Color)colorOnNotSaved;
		//}

		//protected abstract bool Saved();

		//protected virtual void DrawSavedValueLabelField()
		//{
		//	// We draw the both label and property fields in the DrawSavedPropertyField method.
		//}

		//protected virtual void DrawSavedValuePropertyField()
		//{
		//	Rect savedValuePosition = GetRelativePropertyPositionAt(3);
		//	string tooltip = "Saved value (" + (Saved() ? "saved" : "not saved") + ")";
		//	EditorGUI.PropertyField(savedValuePosition, savedValueProperty, new GUIContent("S", tooltip));
		//}

		//void RestoreBackgroundColor()
		//{
		//	GUI.backgroundColor = cachedBackgroundColor;
		//}

		//void ApplyChangeToInspectedSavedValues()
		//{
		//	for (int i = 0; i < insprectedStorableValues.Length; i++)
		//	{
		//		ApplyChangeToInspectedSavedValue(insprectedStorableValues[i]);
		//		PrefabUtility.RecordPrefabInstancePropertyModifications(savedValueProperty.serializedObject.targetObjects[i]);
		//	}
		//}

		//protected abstract void ApplyChangeToInspectedSavedValue(ToryValue<T> toryValue);

		//void SaveChangeToInspectedToryValues()
		//{
		//	for (int i = 0; i < insprectedStorableValues.Length; i++)
		//	{
		//		SaveInspectedToryValue(insprectedStorableValues[i]);
		//	}
		//}

		//protected abstract void SaveInspectedToryValue(ToryValue<T> toryValue);

		//protected abstract bool SavedButInconsistent();

		//protected abstract void FetchInspectedToryValueSavedValue();

		//void RestoreEditorGUI()
		//{
		//	EditorGUI.indentLevel = cachedIndentLevel;
		//	EditorGUIUtility.labelWidth = cachedLabelWidth;
		//}
	}
}