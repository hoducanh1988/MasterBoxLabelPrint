using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Implement;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {

    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {

        private void Txt_SN_TextChanged(object sender, TextChangedEventArgs e) {
            if (MyGlobal.MyTesting.ProductSerial.Length > 0) { MyGlobal.MyTesting.WaitingParameters(); }
            else {
                if (MyGlobal.MyTesting.TotalResult.ToLower().Equals("waiting...")) MyGlobal.MyTesting.TotalResult = "";
            }
        }

    }
}
