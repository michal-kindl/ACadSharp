﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace ACadSharp
{
	public class CadObjectCollection<T> : IObservableCollection<T>
		where T : CadObject
	{
		public event EventHandler<ReferenceChangedEventArgs> OnAdd;
		public event EventHandler<ReferenceChangedEventArgs> OnRemove;

		public CadObject Owner { get; }

		private readonly List<T> _entries = new List<T>();

		public CadObjectCollection(CadObject owner)
		{
			this.Owner = owner;
		}

		public void Add(T item)
		{
			if (this._entries.Contains(item))
				throw new ArgumentException($"Item {item.GetType().FullName} is already in the collection", nameof(item));

			this._entries.Add(item);
			item.Owner = this.Owner;

			OnAdd?.Invoke(this, new ReferenceChangedEventArgs(item));
		}

		public void AddRange(IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				this.Add(item);
			}
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}
	}
}
