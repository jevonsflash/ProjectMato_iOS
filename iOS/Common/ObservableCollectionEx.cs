using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProjectMato.iOS.Common
{
    public class ObservableCollectionEx<T> : ObservableCollection<T>, IObservableCollectionEx
    {
        public ObservableCollectionEx(IEnumerable<T> e) : base(e)
        {

        }
        public ObservableCollectionEx(List<T> e) : base(e)
        {

        }
        public void Add(int index, object item)
        {
            if (Items.IsReadOnly)
                throw new NotSupportedException("NotSupported_ReadOnlyCollection");

            InsertItem(index, (T)item);
        }
    }
}
