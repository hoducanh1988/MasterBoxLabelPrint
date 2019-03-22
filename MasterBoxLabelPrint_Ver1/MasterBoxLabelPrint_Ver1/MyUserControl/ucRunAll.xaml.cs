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

        class _lot_info {
            public string ProductionLot { get; set; }
        }

        
        public ucRunAll() {
            InitializeComponent();
            this.cbb_list_result.ItemsSource = MyParameter.Results;

            this._grid_testing.DataContext = MyGlobal.MyTesting;
            this._grid_setting.DataContext = MyGlobal.MySetting;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Tag) {
                case "search_datalog": {
                        List< msaccdb_tbDataLog> listdatalog = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataLog>("tb_DataLog", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_datalog_sn.Text, "TotalResult", cbb_list_result.Text, "Lot", txt_lot_name.Text);
                        this.datagrid_recentdatalog.ItemsSource = listdatalog;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "search_printed": {
                        List<msaccdb_tbDataProductionLOT> listdataproductionlot = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_printed_sn.Text, "LotStatus", "", "Lot", txt_printed_lot.Text);
                        this.datagrid_recentproduct.ItemsSource = listdataproductionlot;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "search_lot": {
                        this.datagrid_recentlot.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadProductionLot(txt_lot_recent.Text);
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                default: break;
            }
        }



        private void Txt_SN_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                txt_SN.IsEnabled = false;


                Thread zzz = new Thread(new ThreadStart(() => {
                    MyGlobal.MyTesting.ErrorMessage = string.Format("Product: \"{0}\"\n", MyGlobal.MyTesting.ProductSerial);

                    var runall = new imp_RunAll();
                    if (!runall.Execute()) goto FAIL;
                    else goto PASS;


                FAIL:
                    {
                        MyGlobal.MyTesting.FailParameters();

                        //save log
                        _save_log_();

                        //load ms database
                        _load_ms_datatable_();

                        //
                        Dispatcher.Invoke(new Action(() => { txt_SN.IsEnabled = true; txt_SN.Focus(); }));
                        return;
                    }

                PASS:
                    {
                        MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number is valid.");
                        MyGlobal.MyTesting.PassParameters();
                        MyGlobal.MyTesting.LotCount = string.Format("{0}", int.Parse(MyGlobal.MyTesting.LotCount) + 1);

                        //save log
                        _save_log_();

                        //gen lot
                        if (MyGlobal.MyTesting.LotCount.Equals(MyGlobal.MyTesting.LotLimit)) {
                            MyGlobal.MyTesting.LotCount = "0";
                            MyGlobal.MyTesting.LotName = new GenerateProductionLot(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductCode).Gererate();

                            Thread t = new Thread(new ThreadStart(() => {
                                //print label
                                MyGlobal.MasterBox.Print_Access_Report("IMEI_SN_fPrint");
                                Thread.Sleep(100);

                                //delete all row in table imei print
                                new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }

                        //load ms database
                        _load_ms_datatable_();

                        //
                        Dispatcher.Invoke(new Action(() => { txt_SN.IsEnabled = true; txt_SN.Focus(); }));
                        return;
                    }
                }));
                zzz.IsBackground = true;
                zzz.Start();

            }
        }

        private void Txt_SN_TextChanged(object sender, TextChangedEventArgs e) {
            if (MyGlobal.MyTesting.ProductSerial.Length > 0) { MyGlobal.MyTesting.WaitingParameters(); }
            else {
                if (MyGlobal.MyTesting.TotalResult.ToLower().Equals("waiting...")) MyGlobal.MyTesting.TotalResult = "";
            }
        }


        bool _save_log_() {
            bool r = false;
            r = new io_msaccdb_tbDataLog().WriteData(); //save log to tb_datalog
            if (MyGlobal.MyTesting.TotalResult == "PASS") {
                r = new io_msaccdb_tbDataProductionLot().WriteData(); //write serial to tb_DataProductionLot
                r = new io_msaccdb_tbIMEISerialPrint().WriteData(); //write product to tbIMEISerialPrint
            }
            return r;
        }
        bool _load_ms_datatable_() {
            bool r = false;
            Dispatcher.Invoke(new Action(() => {
                this.datagrid_recentdatalog.ItemsSource = new io_msaccdb_tbDataLog().ReadData(); //read log from tb_datalog
                this.datagrid_recentproduct.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadData(); //read serial from tb_DataProductionLot
                //this.datagrid_recentlot.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadProductionLot(); //read all lot from tb_DataProductionLot
                MyGlobal.MyTesting.ProductSerial = ""; //clear product serial number
            }));
            return r;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem item = sender as MenuItem;

            switch (item.Header) {
                case "Reprint label": {
                        string msg = "";
                        string lot_selected = "";

                        try {
                            msaccdb_ProductionLOT row = (msaccdb_ProductionLOT)this.datagrid_recentlot.SelectedValue;
                            lot_selected = row.Lot;
                        }
                        catch { }


                        if (lot_selected != "") {
                            Thread t = new Thread(new ThreadStart(() => {
                                bool r = new ReprintProductionLot(lot_selected).Print(out msg);
                                MessageBox.Show(r ? "Success" : "Fail\n" + msg, "Reprint", MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        else {
                            MessageBox.Show("Please select a production lot.", "Reprint", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                    }
            }
        }
    }
}
