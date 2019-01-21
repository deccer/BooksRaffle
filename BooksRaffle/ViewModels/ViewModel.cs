using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BooksRaffle.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task LoadAsync() => Task.FromResult(0);

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool SetValue<T>(ref T field, T value, [CanBeNull] [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void SetValue<T>(ref T field, T value, Action onChanged, [CallerMemberName]string propertyName = null)
        {
            if (SetValue(ref field, value, propertyName))
            {
                onChanged();
            }
        }
    }
}