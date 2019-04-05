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
                    //save setting
                    XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName); 
                    //update change related value                                                                                                             
                    MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                    new GetRecentProductionLot(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber).GetData(); //gen lot
                    MyGlobal.MyTesting.ProductSerial = "";
                    return;
                }

                if (MyGlobal.ModeSetting) {
                    bool r = _run_Setting();
                }
                else {
                    txt_SN.IsEnabled = false; //
                    MyGlobal.MyTesting.ProductSerial = MyGlobal.MyTesting.ProductSerial.ToUpper();
                    bool r = MyGlobal.MySetting.ProductionStatus == "Normal" ? _run_All() : _run_Bulk_Rework();
                }
            }
        }

    }
}
