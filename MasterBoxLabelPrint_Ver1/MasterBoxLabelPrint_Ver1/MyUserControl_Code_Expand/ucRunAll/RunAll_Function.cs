﻿using System;
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
using System.Diagnostics;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Implement;
using MasterBoxLabelPrint_Ver1.MyFunction.Lamp;

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

        //normal production
        bool _run_All() {
            Thread zzz = new Thread(new ThreadStart(() => {
                //set off lamp
                VNPT_Lamp.Output(LampStatus.AllOFF);

                //run check
                var runall = new imp_RunAll();
                if (!runall.Execute()) _jud_fail();
                else _jud_pass();
            }));
            zzz.IsBackground = true;
            zzz.Start();

            return true;
        }

        //bulk rework
        bool _run_Bulk_Rework() {
            Thread zzz = new Thread(new ThreadStart(() => {
                //set off lamp
                VNPT_Lamp.Output(LampStatus.AllOFF);

                //run check
                var run = new imp_BulkRework();
                if (!run.Execute()) _jud_fail();
                else _jud_pass();
            }));
            zzz.IsBackground = true;
            zzz.Start();

            return true;
        }

        //setting
        bool _run_Setting() {
            var run = new imp_Setting();
            run.Execute();
            MyGlobal.MyTesting.ProductSerial = "";
            return true;
        }


        //sub function --------------------------------------------//
        void _jud_pass() {
            Stopwatch st = new Stopwatch();
            st.Start();

            MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number is valid.");
            MyGlobal.MyTesting.PassParameters();
            MyGlobal.MyTesting.LotCount = string.Format("{0}", int.Parse(MyGlobal.MyTesting.LotCount) + 1);

            _save_log_(); //save log
            st.Stop();
            MyGlobal.MyTesting.TestTime += string.Format("<save_log={0}>", st.ElapsedMilliseconds);

            st.Reset();
            st.Restart();
            if (MyGlobal.MyTesting.LotCount.Equals(MyGlobal.MyTesting.LotLimit)) {  //gen lot
                //set green lamp
                VNPT_Lamp.Output(LampStatus.YellowON);

                //Thread t = new Thread(new ThreadStart(() => {
                MyGlobal.MasterBox.Print_Access_Report("IMEI_SN_fPrint"); //print label
                Thread.Sleep(200);
                new io_msaccdb_tbIMEISerialPrint().DeleteAll(); //delete all row in table imei print

                MyGlobal.MyTesting.LotCount = "0";
                MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, true).Gererate();
                //}));
                //t.IsBackground = true;
                //t.Start();


            }
            else {
                //set green lamp
                VNPT_Lamp.Output(LampStatus.GreenON);
            }
            st.Stop();
            MyGlobal.MyTesting.TestTime += string.Format("<set_lamp={0}>", st.ElapsedMilliseconds);

            st.Reset();
            st.Restart();
            _load_ms_datatable_(); //load ms database
            st.Stop();
            MyGlobal.MyTesting.TestTime += string.Format("<load_dblog={0}>", st.ElapsedMilliseconds);

            st.Reset();
            st.Restart();
            io_dll_Recent.ToFile(MyGlobal.MyTesting.LotName, MyGlobal.MyTesting.LotProgress); //save recent file
            Dispatcher.Invoke(new Action(() => { txt_SN.IsEnabled = true; txt_SN.Focus(); }));  //set focus for txt_SN
            st.Stop();
            MyGlobal.MyTesting.TestTime += string.Format("<save_recent={0}>", st.ElapsedMilliseconds);

            //save log test time
            if (System.IO.Directory.Exists(string.Format("{0}\\Log", AppDomain.CurrentDomain.BaseDirectory)) == false) {
                System.IO.Directory.CreateDirectory(string.Format("{0}\\Log", AppDomain.CurrentDomain.BaseDirectory));
                Thread.Sleep(100);
            } 
            var sw = new System.IO.StreamWriter(string.Format("{0}\\Log\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyyMMdd")), true);
            sw.WriteLine(MyGlobal.MyTesting.TestTime);
            sw.Close();
        }


        void _jud_fail() {
            MyGlobal.MyTesting.FailParameters(); //display fail
            _save_log_(); //save log

            _load_ms_datatable_(); //load ms database

            //set red lamp
            VNPT_Lamp.Output(LampStatus.RedON);

            Dispatcher.Invoke(new Action(() => { txt_SN.Clear(); txt_SN.IsEnabled = true; txt_SN.Focus(); })); //set focus for txt_SN
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
