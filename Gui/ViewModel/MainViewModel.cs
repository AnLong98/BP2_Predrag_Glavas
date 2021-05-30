using Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Gui.ViewModel
{
    public class MainViewModel
    {
        ObservableCollection<object> _children;

        public MainViewModel(ChronometersService _chrono)
        {
            _children = new ObservableCollection<object>();
            _children.Add(new ChronometerViewModel(_chrono));
        }

        public ObservableCollection<object> Children { get { return _children; } }
    }
}
