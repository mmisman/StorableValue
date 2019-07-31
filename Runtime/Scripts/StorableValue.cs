using UnityEngine;

namespace Mmisman.StorableValue
{
	public abstract class StorableValue<T>
	{
		[SerializeField] string key;
		[SerializeField] T value;
		[SerializeField] T defaultValue;

		/// <summary>
		/// The string identifier to store <see cref="Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public string Key { get { return key; } set { AssertKeyShouldNotBeNullOrEmpty(value); key = value; } }
		/// <summary>
		/// The storeable value.
		/// </summary>
		public T Value
		{
			get { return getterFunc(value); }
			set
			{
				value = setterFunc(value);
				if (!this.value.Equals(value))
				{
					this.value = value;
					Changed?.Invoke(value);
				}
			}
		}
		/// <summary>
		/// Gets the initial value of <see cref="Value"/>.
		/// Use <see cref="Revert"/> to restore the current <see cref="Value"/> to this <see cref="DefaultValue"/>.
		/// </summary>
		public T DefaultValue { get { return defaultValue; } }
		/// <summary>
		/// Gets the stored value in <see cref="PlayerPrefs"/>.
		/// Use <see cref="Save"/> to store the current <see cref="Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public abstract T SavedValue { get; }

		// The default constructor is needed for the sake of Unity's serialization.
		protected StorableValue() { }
		protected StorableValue(string key) : this(key, default) { }
		protected StorableValue(string key, T value) : this(key, value, g => g, s => s) { }
		protected StorableValue(string key, T value, System.Func<T, T> getterFunc, System.Func<T, T> setterFunc)
		{
			AssertKeyShouldNotBeNullOrEmpty(key);
			this.getterFunc = getterFunc ?? throw new System.ArgumentNullException(nameof(getterFunc), "The getterFunc should not be null.");
			this.setterFunc = setterFunc ?? throw new System.ArgumentNullException(nameof(setterFunc), "The setterFunc should not be null.");

			this.key = key;
			this.defaultValue = this.value = this.setterFunc(value);
		}

		void AssertKeyShouldNotBeNullOrEmpty(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new System.ArgumentNullException(nameof(key), "The key should not be null or empty.");
			}
		}

		public static implicit operator T(StorableValue<T> value) { return value.Value; }

		readonly System.Func<T, T> getterFunc = g => g;
		readonly System.Func<T, T> setterFunc = s => s;

		/// <summary>
		/// Occurs when <see cref="Value"/> is changed.
		/// </summary>
		event System.Action<T> Changed;
		/// <summary>
		/// Occurs when <see cref="Save"/> is called.
		/// </summary>
		event System.Action<T> Saved;
		/// <summary>
		/// Occurs when <see cref="Load"/> is called.
		/// </summary>
		event System.Action<T> Loaded;
		/// <summary>
		/// Occurs when <see cref="Revert"/> is called.
		/// </summary>
		event System.Action<T> Reverted;

		/// <summary>
		/// Stores <see cref="Value"/> in <see cref="PlayerPrefs"/>.
		/// </summary>
		public void Save()
		{
			SaveValueToPlayerPrefs(value);
			Saved?.Invoke(value);
		}

		protected abstract void SaveValueToPlayerPrefs(T value);

		/// <summary>
		/// Reads <see cref="SavedValue"/> from <see cref="PlayerPrefs"/> and set it to <see cref="Value"/>.
		/// Note that, when you call this method before calling <see cref="Save"/>, nothing happens to <see cref="Value"/>.
		/// </summary>
		public void Load()
		{
			Value = SavedValue;
			Loaded?.Invoke(value);
		}

		/// <summary>
		/// Restores <see cref="Value"/> to <see cref="DefaultValue"/>.
		/// </summary>
		public void Revert()
		{
			Value = defaultValue;
			Reverted?.Invoke(value);
		}

		public override string ToString()
		{
			return $"(Key: {key}, Value: {value}, DefaultValue: {defaultValue}, SavedValue: {SavedValue})";
		}
	}
}