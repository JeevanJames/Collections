#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2020 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;

#if EXPLICIT
using System;

namespace Collections.Net.Specialized
#else
namespace System.Collections.Specialized
#endif
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a most-recently-used collection, where the most recently added or accessed item
    ///     is moved to the top of the collection. If the item already exists in the collection at any
    ///     position, it is also moved to the top.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public partial class MruCollection<T> : Collection<T>
    {
        private int _capacity;
        private readonly MruCollectionOptions<T> _options;

        // If true, indicates that the collection is being initialized and the MRU logic should not be
        // considered.
        private readonly bool _creatingCollection;

        public MruCollection(int capacity, MruCollectionOptions<T> options = null)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity should be greater than zero");
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _capacity = capacity;

            _options = options ?? MruCollectionOptions<T>.Default;

            if (_options.InitialData != null)
            {
                _creatingCollection = true;
                foreach (T data in _options.InitialData)
                    Add(data);
                _creatingCollection = false;
            }

            if (_options.EqualityComparer == null)
            {
                // If T implements IEquatable<T>, then create an IEqualityComparer<T> instance from it
                // using the EquatableEqualityComparer<TItem> class declared below.
                Type equatableType = typeof(IEquatable<>).MakeGenericType(typeof(T));
                if (!equatableType.IsAssignableFrom(typeof(T)))
                    throw new ArgumentException($"Generic type '{typeof(T).FullName}' should implement IEquatable<>. Otherwise, use the constructor where an equality comparer can be explicitly specified.");
                _options.EqualityComparer = new EquatableEqualityComparer<T>();
            }
        }

        /// <summary>
        ///     Gets the item at the specified index without triggering the MRU logic that causes the
        ///     item to be moved to the top of the collection.
        /// </summary>
        /// <param name="index">The index of the item to be retrieved</param>
        /// <returns>The item at the specified index</returns>
        public T Peek(int index)
        {
            // Access the item directly through the internal collection to prevent the MRU logic from
            // being triggered.
            return Items[index];
        }

        /// <summary>
        ///     Overrides the indexer's getter to return the item at the specified index and then move
        ///     it to the top of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get or set.</param>
        /// <returns>The item at the specified index</returns>
        public new T this[int index]
        {
            get
            {
                T result = base[index];
                if (HasTrigger(MruTriggers.ItemAccessed))
                {
                    RemoveAt(index);
                    base.InsertItem(0, result);
                }
                return result;
            }
            set => base[index] = value;
        }

        public int Capacity
        {
            get => _capacity;
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "MRU collection should have a capacity of two elements or more.");
                _capacity = value;
                TrimExcess();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Forces the new item to be inserted at the first position in the collection.
        ///     If the item already exists in the collection, it is moved to the first position.
        /// </summary>
        protected override void InsertItem(int index, T item)
        {
            bool shouldUseMruLogic = HasTrigger(MruTriggers.NewItemInserted) || HasTrigger(MruTriggers.ExistingItemInserted);
            if (_creatingCollection || !shouldUseMruLogic)
                base.InsertItem(index, item);
            else
            {
                // Remove the item if it exists
                bool removed = RemoveExisting(item);

                // If the item exists and there is a trigger for existing item inserted, or if the
                // item does not exist and there is a trigger for new item inserted, apply the MRU
                // logic and move the item to the top of the collection
                if ((removed && HasTrigger(MruTriggers.ExistingItemInserted)) ||
                    (!removed && HasTrigger(MruTriggers.NewItemInserted)))
                {
                    base.InsertItem(0, item);
                    if (!removed)
                        TrimExcess();
                }
                else
                    base.InsertItem(index, item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Forces the new item to be inserted at the first position in the collection.
        ///     If the item already exists in the collection, it is moved to the first position.
        /// </summary>
        protected override void SetItem(int index, T item)
        {
            bool shouldUseMruLogic =
                HasTrigger(MruTriggers.NewItemSet) || HasTrigger(MruTriggers.ExistingItemSet);
            if (_creatingCollection || !shouldUseMruLogic)
                base.SetItem(index, item);
            else if (_options.EqualityComparer.Equals(item, Peek(index)))
            {

            }
            else
            {
                InsertItem(index, item);
            }
        }

        /// <summary>
        ///     Removes any existing item in the collection that matches the specified item parameter.
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <returns>True if an item was matched and removed; otherwise false</returns>
        private bool RemoveExisting(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_options.EqualityComparer.Equals(item, Peek(i)))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     Removes any extra items from the collection that are beyond the expected capacity.
        /// </summary>
        private void TrimExcess()
        {
            if (Count > _capacity)
            {
                for (int i = Count - 1; i >= _capacity; i--)
                    RemoveAt(i);
            }
        }

        private bool HasTrigger(MruTriggers trigger)
        {
            return (_options.Triggers & trigger) == trigger;
        }
    }

    public partial class MruCollection<T>
    {
        /// <inheritdoc />
        /// <summary>
        ///     <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation that
        ///     wraps an <see cref="T:System.IEquatable`1" />
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        private sealed class EquatableEqualityComparer<TItem> : IEqualityComparer<TItem>
        {
            bool IEqualityComparer<TItem>.Equals(TItem x, TItem y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                return x.Equals(y);
            }

            int IEqualityComparer<TItem>.GetHashCode(TItem obj)
            {
                return obj == null ? 0 : obj.GetHashCode();
            }
        }
    }

    /// <summary>
    ///     Options for initializing a <see cref="MruCollection{T}" /> instance.
    /// </summary>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    public sealed class MruCollectionOptions<T>
    {
        public MruCollectionOptions()
        {
            Triggers = MruTriggers.Default;
        }

        /// <summary>
        ///     Initial data that is populated into the collection. This data is not affected by MRU logic.
        /// </summary>
        public IEnumerable<T> InitialData { get; set; }

        /// <summary>
        ///     <see cref="IEqualityComparer{T}" /> instance to use to compare two items for equality.
        /// </summary>
        public IEqualityComparer<T> EqualityComparer { get; set; }

        /// <summary>
        ///     Actions on the collection that cause the MRU logic to be executed.
        ///     Examples include inserting or setting items, and accessing items through the indexer.
        ///     This property allows you to configure which actions trigger the MRU logic.
        /// </summary>
        public MruTriggers Triggers { get; set; }

        public static readonly MruCollectionOptions<T> Default = new MruCollectionOptions<T>();
    }

    /// <summary>
    ///     Triggers that cause the MRU logic to be executed on an MRU collection.
    /// </summary>
    [Flags]
    public enum MruTriggers
    {
        /// <summary>
        ///     An existing item in the collection is overwritten with a new item
        /// </summary>
        NewItemSet = 0x01,

        /// <summary>
        ///     A new item is inserted into the collection
        /// </summary>
        NewItemInserted = 0x02,

        /// <summary>
        ///     An existing item in the collection is overwritten with an item that already exists in the collection
        /// </summary>
        ExistingItemSet = 0x04,

        /// <summary>
        ///     An item that already exists in the collection is inserted again
        /// </summary>
        ExistingItemInserted = 0x08,

        /// <summary>
        ///     An item in the collection is accessed using the indexer
        /// </summary>
        ItemAccessed = 0x10,

        ItemSet = NewItemSet | ExistingItemSet,
        ItemInserted = NewItemInserted | ExistingItemInserted,
        NewItemSetOrInserted = NewItemSet | NewItemInserted,
        ExistingItemSetOrInserted = ExistingItemSet | ExistingItemInserted,
        ItemSetOrInserted = NewItemSetOrInserted | ExistingItemSetOrInserted,

        /// <summary>
        ///     Combination of all triggers.
        ///     New or existing items are inserted or set, or an item is accessed.
        /// </summary>
        Default = ItemSetOrInserted | ItemAccessed
    }
}
