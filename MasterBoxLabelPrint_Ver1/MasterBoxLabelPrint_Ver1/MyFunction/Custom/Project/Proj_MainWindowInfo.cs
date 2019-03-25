using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom
{
    public class Proj_MainWindowInfo : MasterBoxLabelPrint_Ver1.MyFunction.Ulti.zCNotifyPropertyChanged {

        public Proj_MainWindowInfo() {
            WindowOpacity = 1;
        }

        double _window_opacity;
        public double WindowOpacity {
            get { return _window_opacity; }
            set {
                _window_opacity = value;
                OnPropertyChanged(nameof(WindowOpacity));
            }
        }

    }
}
