using UnityEngine;

namespace Mmisman.StorableValue
{
	[System.Serializable]
	public class StorableVector4 : StorableValue<Vector4>
	{
		/// <summary>
		/// Gets the stored value in <see cref="PlayerPrefs"/>.
		/// Use <see cref="StorableValue{T}.Value"/> to store the current <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public override Vector4 SavedValue { get { return PlayerPrefsExtensions.GetVector4(Key, Value); } }

		// The default constructor is needed for the sake of Unity's serialization.
		protected StorableVector4() { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		public StorableVector4(string key) : this(key, default) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		public StorableVector4(string key, Vector4 value) : this(key, value, g => g, s => s) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="getterFunc">The mapping function whenever getting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the getter of a property. Note that this cannot be null.</param>
		/// <param name="setterFunc">The mapping function whenever setting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the setter of a property. Note that this cannot be null.</param>
		public StorableVector4(string key, Vector4 value, System.Func<Vector4, Vector4> getterFunc, System.Func<Vector4, Vector4> setterFunc) : base(key, value, getterFunc, setterFunc) { }

		protected override void SaveValueToPlayerPrefs(Vector4 value)
		{
			PlayerPrefsExtensions.SetVector4(Key, value);
		}
	}
}