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
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
using UtilityPack.IO;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {
    /// <summary>
    /// Interaction logic for ucRework_BulkQty.xaml
    /// </summary>
    public partial class ucRework_BulkQty : UserControl {
        public ucRework_BulkQty() {
            InitializeComponent();

            Thread t = new Thread(new ThreadStart(() => {
                while (true) {
                    if (MyGlobal.MySetting.ProductionStatus == "Normal") {
                        try {
                            Dispatcher.Invoke(new Action(() => {
                                btnstartbulk.IsEnabled = true;
                                btnendbulk.IsEnabled = false;
                            }));
                        }
                        catch { }
                    }
                    else {
                        try {
                            Dispatcher.Invoke(new Action(() => {
                                btnstartbulk.IsEnabled = false;
                                btnendbulk.IsEnabled = true;
                            }));
                        }
                        catch { }
                        
                    }
                    Thread.Sleep(500);
                }
            }));
            t.IsBackground = true;
            t.Start();

        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            if (MyGlobal.MyTesting.LotName == null || MyGlobal.MyTesting.LotName.Length != 14) {
                MessageBox.Show("Thông tin LOT chưa cài đặt.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            switch (b.Content) {
                case "Start Bulk Rework": {
                        MyGlobal.MySetting.ProductionStatus = "BulkRework";

                        //delete all data in table: IMEI_SN_Print, tb_DataProductionLOT_Bulk
                        bool r = false;
                        r = new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                        r = new io_msaccdb_tbDataProductionLot().DeleteAll();
                        break;
                    }
                case "End Bulk Rework": {
                        MyGlobal.MySetting.ProductionStatus = "Normal";
                        //delete all data in table: IMEI_SN_Print, tb_DataProductionLOT_Bulk
                        bool r = false;
                        r = new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                        break;
                    }
            }

            //gen LOT
            bool flag_index_change = MyGlobal.MyTesting.LotCount == "0" ? false : true; //true = +1 vao chi so LOT, false = ko thay doi
            MyGlobal.MyTesting.LotCount = "0";
            MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, flag_index_change).Gererate();
            //delete IMEI_SN_Print talbe
            new io_msaccdb_tbIMEISerialPrint().DeleteAll();

            //save setting
            XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName);
        }
    }
}
