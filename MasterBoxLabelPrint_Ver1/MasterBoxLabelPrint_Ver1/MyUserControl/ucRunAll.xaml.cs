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
using UtilityPack.IO;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {

    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {

        
        private void Txt_SN_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {

                if (MyGlobal.MyTesting.ProductSerial.Contains("CO::STA::1")) {
                    MyGlobal.ModeSetting = true;
                    MyGlobal.MyTesting.ProductSerial = "";
                    return;
                }
                if (MyGlobal.MyTesting.ProductSerial.Contains("CO::SET::1")) {
                    MyGlobal.ModeSetting = false;
                    XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName); //save setting
                    MyGlobal.MyTesting.ProductSerial = "";
                    return;
                }

                if (MyGlobal.ModeSetting) {
                    bool r = _run_Setting();
                }
                else {
                    txt_SN.IsEnabled = false; //
                    bool r = MyGlobal.MySetting.ProductionStatus == "Normal" ? _run_All() : _run_Bulk_Rework();
                }
            }
        }

    }
}
