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

        //class ---------------------------------------------------//
        class _lot_info {
            public string ProductionLot { get; set; }
        }

        
        //main function -------------------------------------------//
        void _run_All() {
            Thread zzz = new Thread(new ThreadStart(() => {
                var runall = new imp_RunAll();
                if (!runall.Execute()) _jud_fail();
                else _jud_pass();
            }));
            zzz.IsBackground = true;
            zzz.Start();
        }
        

        //sub function --------------------------------------------//
        void _jud_pass() {
            MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number is valid.");
            MyGlobal.MyTesting.PassParameters();
            MyGlobal.MyTesting.LotCount = string.Format("{0}", int.Parse(MyGlobal.MyTesting.LotCount) + 1);

            _save_log_(); //save log
            Dispatcher.Invoke(new Action(() => { txt_SN.IsEnabled = true; txt_SN.Focus(); }));  //set focus for txt_SN

            if (MyGlobal.MyTesting.LotCount.Equals(MyGlobal.MyTesting.LotLimit)) {  //gen lot
                MyGlobal.MyTesting.LotCount = "0";
                MyGlobal.MyTesting.LotName = new GenerateProductionLot(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber).Gererate();

                Thread t = new Thread(new ThreadStart(() => {
                    MyGlobal.MasterBox.Print_Access_Report("IMEI_SN_fPrint"); //print label
                    Thread.Sleep(100);
                    new io_msaccdb_tbIMEISerialPrint().DeleteAll(); //delete all row in table imei print
                }));
                t.IsBackground = true;
                t.Start();
            }
            
            _load_ms_datatable_(); //load ms database
            
        } 


        void _jud_fail() {
            MyGlobal.MyTesting.FailParameters(); //display fail
            _save_log_(); //save log
            Dispatcher.Invoke(new Action(() => { txt_SN.IsEnabled = true; txt_SN.Focus(); })); //set focus for txt_SN
            _load_ms_datatable_(); //load ms database
        }



        bool _save_log_() {
            bool r = false;
            r = new io_msaccdb_tbDataLog().WriteData(); //save log to tb_datalog
            if (MyGlobal.MyTesting.TotalResult == "PASS") {
                r = new io_msaccdb_tbDataProductionLot().WriteData(); //write serial to tb_DataProductionLot
                r = new io_msaccdb_tbIMEISerialPrint().WriteData(); //write product to tbIMEISerialPrint
            }
            MyGlobal.MyTesting.ProductSerial = ""; //clear product serial number
            return r;
        }


        bool _load_ms_datatable_() {
            bool r = false;
            Dispatcher.Invoke(new Action(() => {
                this.datagrid_recentdatalog.ItemsSource = new io_msaccdb_tbDataLog().ReadData(); //read log from tb_datalog
                //this.datagrid_recentproduct.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadData(); //read serial from tb_DataProductionLot
                //this.datagrid_recentlot.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadProductionLot(); //read all lot from tb_DataProductionLot
            }));
            return r;

        }

    }
}
