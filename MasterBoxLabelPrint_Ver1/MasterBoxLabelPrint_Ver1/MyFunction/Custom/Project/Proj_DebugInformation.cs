using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom
{
    public class Proj_DebugInformation : MasterBoxLabelPrint_Ver1.MyFunction.Ulti.zCNotifyPropertyChanged {

        string _debug_log;
        public string DebugLog {
            get { return _debug_log; }
            set {
                _debug_log = value;
                OnPropertyChanged(nameof(DebugLog));
            }
        }
        
    }
}
