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

                    //gen lot
                    if (!System.IO.File.Exists(MyGlobal.Setting_FileFullName)) {
                        MyGlobal.MySetting.LOTIndex = "000001";
                        bool flag = string.IsNullOrEmpty(MyGlobal.MySetting.ProductNumber) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionPlace) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionYear) || string.IsNullOrEmpty(MyGlobal.MySetting.LineIndex);
                        if (!flag) {
                            //gen LOT
                            MyGlobal.MyTesting.LotCount = "0";
                            MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, false).Gererate();
                            //delete IMEI_SN_Print talbe
                            new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                        }
                    }
                    else {
                        var obj = XmlHelper<Proj_SettingInformation>.FromXmlFile(MyGlobal.Setting_FileFullName); //load du lieu file setting
                        bool kq = (obj.ProductionPlace != MyGlobal.MySetting.ProductionPlace) || (obj.ProductionYear != MyGlobal.MySetting.ProductionYear) || (obj.LineIndex != MyGlobal.MySetting.LineIndex);
                        if (kq) MyGlobal.MySetting.LOTIndex = "000001"; //reset lot

                        if (kq) {
                            bool flag = string.IsNullOrEmpty(MyGlobal.MySetting.ProductNumber) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionPlace) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionYear) || string.IsNullOrEmpty(MyGlobal.MySetting.LineIndex);
                            MyGlobal.MyTesting.LotCount = "0";
                            MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, false).Gererate();
                            //delete IMEI_SN_Print talbe
                            new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                        }
                        else {
                            if (obj.ProductName != MyGlobal.MySetting.ProductName) {
                                bool flag = string.IsNullOrEmpty(MyGlobal.MySetting.ProductNumber) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionPlace) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionYear) || string.IsNullOrEmpty(MyGlobal.MySetting.LineIndex);
                                if (!flag) {
                                    //gen LOT
                                    bool flag_index_change = MyGlobal.MyTesting.LotCount == "0" ? false : true; //true = +1 vao chi so LOT, false = ko thay doi
                                    MyGlobal.MyTesting.LotCount = "0";
                                    MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, flag_index_change).Gererate();
                                    //delete IMEI_SN_Print talbe
                                    new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                                }
                            }
                        }
                    }

                    //save setting
                    XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName);
                    MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                    //update change related value                                                                                                             
                    
                    MyGlobal.MyTesting.ProductSerial = "";
                    return;
                }

                if (MyGlobal.ModeSetting) {
                    bool r = _run_Setting();
                }
                else {
                    this.txt_buffer.Clear();
                    this.txt_buffer.Focus();

                    txt_SN.IsEnabled = false; //
                    MyGlobal.MyTesting.ProductSerial = MyGlobal.MyTesting.ProductSerial.ToUpper();
                    bool r = MyGlobal.MySetting.ProductionStatus == "Normal" ? _run_All() : _run_Bulk_Rework();
                }
            }
        }

    }
}
