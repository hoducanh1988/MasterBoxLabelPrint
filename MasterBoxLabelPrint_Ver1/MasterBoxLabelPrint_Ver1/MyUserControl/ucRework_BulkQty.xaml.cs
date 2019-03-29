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
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
using UtilityPack.IO;

namespace MasterBoxLabelPrint_Ver1.MyUserControl
{
    /// <summary>
    /// Interaction logic for ucRework_BulkQty.xaml
    /// </summary>
    public partial class ucRework_BulkQty : UserControl
    {
        public ucRework_BulkQty()
        {
            InitializeComponent();

            Thread t = new Thread(new ThreadStart(() => {
                while (true) {
                    if (MyGlobal.MySetting.ProductionStatus == "Normal") {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                btnstartbulk.IsEnabled = true;
                                btnendbulk.IsEnabled = false;
                            } catch { }
                        }));
                    }
                    else {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                btnstartbulk.IsEnabled = false;
                                btnendbulk.IsEnabled = true;
                            } catch { }
                        }));
                    }
                    Thread.Sleep(500);
                }
            }));
            t.IsBackground = true;
            t.Start();

        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;

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

            MyGlobal.IncreasementLot = true;
            //gen lot when start/end bulk rework
            new GetRecentProductionLot(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber).GetData();
            //save setting
            XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName);
        }
    }
}
