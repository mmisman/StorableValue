using UnityEngine;

namespace Mmisman.StorableValue
{
	[System.Serializable]
	public class StorableVector3Int : StorableValue<Vector3Int>
	{
		/// <summary>
		/// Gets the stored value in <see cref="PlayerPrefs"/>.
		/// Use <see cref="StorableValue{T}.Value"/> to store the current <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public override Vector3Int SavedValue { get { return PlayerPrefsExtensions.GetVector3Int(Key, Value); } }

		// The default constructor is needed for the sake of Unity's serialization.
		protected StorableVector3Int() { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		public StorableVector3Int(string key) : this(key, default) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		public StorableVector3Int(string key, Vector3Int value) : this(key, value, g => g, s => s) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="getterFunc">The mapping function whenever getting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the getter of a property. Note that this cannot be null.</param>
		/// <param name="setterFunc">The mapping function whenever setting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the setter of a property. Note that this cannot be null.</param>
		public StorableVector3Int(string key, Vector3Int value, System.Func<Vector3Int, Vector3Int> getterFunc, System.Func<Vector3Int, Vector3Int> setterFunc) : base(key, value, getterFunc, setterFunc) { }

		protected override void SaveValueToPlayerPrefs(Vector3Int value)
		{
			PlayerPrefsExtensions.SetVector3Int(Key, value);
		}
	}
}