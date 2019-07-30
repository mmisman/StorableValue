using UnityEngine;

namespace Mmisman.StorableValue
{
	[System.Serializable]
	public class StorableEnum<T> : StorableValue<T> where T : struct
	{
		/// <summary>
		/// Gets the stored value in <see cref="PlayerPrefs"/>.
		/// Use <see cref="StorableValue{T}.Value"/> to store the current <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public override T SavedValue { get { return PlayerPrefsExtensions.GetEnum(Key, Value); } }

		// The default constructor is needed for the sake of Unity's serialization.
		protected StorableEnum() { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		public StorableEnum(string key) : this(key, default) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		public StorableEnum(string key, T value) : this(key, value, g => g, s => s) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="getterFunc">The mapping function whenever getting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the getter of a property. Note that this cannot be null.</param>
		/// <param name="setterFunc">The mapping function whenever setting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the setter of a property. Note that this cannot be null.</param>
		public StorableEnum(string key, T value, System.Func<T, T> getterFunc, System.Func<T, T> setterFunc) : base(key, value, getterFunc, setterFunc) { }

		protected override void SaveValueToPlayerPrefs(T value)
		{
			PlayerPrefsExtensions.SetEnum(Key, value);
		}
	}
}