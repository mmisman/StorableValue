using UnityEngine;

namespace Mmisman.StorableValue
{
	public static class PlayerPrefsExtensions
	{

		#region bool
		public static bool GetBool(string key, bool defaultValue = default)
		{
			int tmp = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0);
			return tmp == 1;
		}

		public static void SetBool(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}
		#endregion

		#region Vector2
		public static Vector2 GetVector2(string key, Vector2 defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}").Split('|');
			Vector2 result = new Vector2(float.Parse(tmp[0]), float.Parse(tmp[1]));
			return result;
		}

		public static void SetVector2(string key, Vector2 value)
		{
			string tmp = $"{value.x}|{value.y}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Vector2Int
		public static Vector2Int GetVector2Int(string key, Vector2Int defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}").Split('|');
			Vector2Int result = new Vector2Int(int.Parse(tmp[0]), int.Parse(tmp[1]));
			return result;
		}

		public static void SetVector2Int(string key, Vector2Int value)
		{
			string tmp = $"{value.x}|{value.y}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Vector3
		public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.z}").Split('|');
			Vector3 result = new Vector3(float.Parse(tmp[0]), float.Parse(tmp[1]), float.Parse(tmp[2]));
			return result;
		}

		public static void SetVector3(string key, Vector3 value)
		{
			string tmp = $"{value.x}|{value.y}|{value.z}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Vector3Int
		public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.z}").Split('|');
			Vector3Int result = new Vector3Int(int.Parse(tmp[0]), int.Parse(tmp[1]), int.Parse(tmp[2]));
			return result;
		}

		public static void SetVector3Int(string key, Vector3Int value)
		{
			string tmp = $"{value.x}|{value.y}|{value.z}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Vector4
		public static Vector4 GetVector4(string key, Vector4 defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.z}|{defaultValue.w}").Split('|');
			Vector4 result = new Vector4(float.Parse(tmp[0]), float.Parse(tmp[1]), float.Parse(tmp[2]), float.Parse(tmp[3]));
			return result;
		}

		public static void SetVector4(string key, Vector4 value)
		{
			string tmp = $"{value.x}|{value.y}|{value.z}|{value.w}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Quaternion
		public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.z}|{defaultValue.w}").Split('|');
			Quaternion result = new Quaternion(float.Parse(tmp[0]), float.Parse(tmp[1]), float.Parse(tmp[2]), float.Parse(tmp[3]));
			return result;
		}

		public static void SetQuaternion(string key, Quaternion value)
		{
			string tmp = $"{value.x}|{value.y}|{value.z}|{value.w}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Rect
		public static Rect GetRect(string key, Rect defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.width}|{defaultValue.height}").Split('|');
			Rect result = new Rect(float.Parse(tmp[0]), float.Parse(tmp[1]), float.Parse(tmp[2]), float.Parse(tmp[3]));
			return result;
		}

		public static void SetRect(string key, Rect value)
		{
			string tmp = $"{value.x}|{value.y}|{value.width}|{value.height}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region RectInt
		public static RectInt GetRectInt(string key, RectInt defaultValue = default)
		{
			string[] tmp = PlayerPrefs.GetString(key, $"{defaultValue.x}|{defaultValue.y}|{defaultValue.width}|{defaultValue.height}").Split('|');
			RectInt result = new RectInt(int.Parse(tmp[0]), int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
			return result;
		}

		public static void SetRectInt(string key, RectInt value)
		{
			string tmp = $"{value.x}|{value.y}|{value.width}|{value.height}";
			PlayerPrefs.SetString(key, tmp);
		}
		#endregion

		#region Enum
		public static T GetEnum<T>(string key, T defaultValue = default) where T : struct
		{
			int tmp = PlayerPrefs.GetInt(key, (int)(object)defaultValue);
			return (T)System.Enum.ToObject(typeof(T), tmp);
		}

		public static void SetEnum<T>(string key, T value) where T : struct
		{
			PlayerPrefs.SetInt(key, (int)(object)value);
		}
		#endregion
	}
}