namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private IList<TItem> l = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => l.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => l.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => l[index];
            set
            {
                ElementChanged?.Invoke(this , value, l[index], index);
                l[index] = value;
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => l.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator() => l.GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            l.Add(item);
            ElementInserted?.Invoke(this, item, l.IndexOf(item));
        }
        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            foreach(TItem i in l)
            {
                ElementRemoved?.Invoke(this, i, l.IndexOf(i));
            }
            l.Clear();
        }
        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) => l.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => l.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
           ElementRemoved?.Invoke(this, item, l.IndexOf(item));
           return  l.Remove(item);
        }
        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => l.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            ElementInserted?.Invoke(this, item, index);
            l.Insert(index, item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            if (l.Count > index)
            {
                ElementRemoved?.Invoke(this, l[index], index);
                l.RemoveAt(index);
            }
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            return obj is ObservableList<TItem> list &&
                   EqualityComparer<IList<TItem>>.Default.Equals(l, list.l) &&
                   IsReadOnly == list.IsReadOnly;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return HashCode.Combine(l, IsReadOnly);
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            // TODO improve
            return l.ToString();
        }


    }
}
