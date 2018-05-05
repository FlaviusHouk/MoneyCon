﻿using GalaSoft.MvvmLight;

namespace CostControl.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class InputNameWindowViewModel : ViewModelBase
    {
        public string Name { get; set; }
    }
}