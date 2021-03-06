﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;

namespace BooksRaffle.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static readonly DependencyObject _dummyDependencyObject = new DependencyObject();

        protected static bool IsDesignMode => !DesignerProperties.GetIsInDesignMode(_dummyDependencyObject);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetValue<T>(ref T field, T value, [CanBeNull] [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void SetValue<T>(ref T field, T value, Action onChanged, [CallerMemberName]string propertyName = null)
        {
            if (SetValue(ref field, value, propertyName))
            {
                onChanged();
            }
        }
    }
}