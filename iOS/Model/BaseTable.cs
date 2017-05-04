using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectMato.iOS.Model
{
    /// <summary>
    /// BaseTable class other tables can extend so that BaseModel can ensure models correspond to tables
    /// </summary>
    public abstract class BaseTable : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
