using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyChanged
{
    public class ChildViewModel : INotifyPropertyChanged
    {
        public string ChildGivenName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
