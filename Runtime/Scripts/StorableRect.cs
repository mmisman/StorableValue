using UnityEngine;

namespace Mmisman.StorableValue
{
	[System.Serializable]
	public class StorableRect : StorableValue<Rect>
	{
		/// <summary>
		/// Gets the stored value in <see cref="PlayerPrefs"/>.
		/// Use <see cref="StorableValue{T}.Value"/> to store the current <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public override Rect SavedValue { get { return PlayerPrefsExtensions.GetRect(Key, Value); } }

		// The default constructor is needed for the sake of Unity's serialization.
		protected StorableRect() { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		public StorableRect(string key) : this(key, default) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		public StorableRect(string key, Rect value) : this(key, value, g => g, s => s) { }
		/// <summary>
		/// Creates a storable value.
		/// </summary>
		/// <param name="key">The string identifier to store <see cref="StorableValue{T}.Value"/> in <see cref="PlayerPrefs"/>.</param>
		/// <param name="value">The value to be stored.</param>
		/// <param name="getterFunc">The mapping function whenever getting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the getter of a property. Note that this cannot be null.</param>
		/// <param name="setterFunc">The mapping function whenever setting <see cref="StorableValue{T}.Value"/>. Use this as if you were writing the setter of a property. Note that this cannot be null.</param>
		public StorableRect(string key, Rect value, System.Func<Rect, Rect> getterFunc, System.Func<Rect, Rect> setterFunc) : base(key, value, getterFunc, setterFunc) { }

		protected override void SaveValueToPlayerPrefs(Rect value)
		{
			PlayerPrefsExtensions.SetRect(Key, value);
		}
	}
}