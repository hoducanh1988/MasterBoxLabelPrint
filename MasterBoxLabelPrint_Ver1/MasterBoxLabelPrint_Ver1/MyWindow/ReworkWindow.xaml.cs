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
using System.Windows.Shapes;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Implement;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1
{
    /// <summary>
    /// Interaction logic for ReworkWindow.xaml
    /// </summary>
    public partial class ReworkWindow : Window
    {
        Proj_ReworkInformation Proj_Rework = null;
        Init_Product init_Product = null;



        public ReworkWindow(Proj_ReworkInformation _ReworkInformation, Init_Product _Product)
        {
            InitializeComponent();
            //
            this.Proj_Rework = _ReworkInformation;
            this.init_Product = _Product;
            //
            this.DataContext = this.Proj_Rework;
            //
            this.txtnewproduct.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            TextBox tb = sender as TextBox;
            if (e.Key == Key.Enter) {
                tb.IsEnabled = false; //

                _run_();

            }
        }

        //main function -------------------------------------------//
        void _run_() {
            Thread zzz = new Thread(new ThreadStart(() => {
                var run = new imp_Rework(Proj_Rework, init_Product);
                if (!run.Execute()) _jud_fail();
                else _jud_pass();
            }));
            zzz.IsBackground = true;
            zzz.Start();
        }


        //sub function --------------------------------------------//
        void _jud_pass() {
            Proj_Rework.ErrorMessage += string.Format("Serial Number is valid.");
            Proj_Rework.PassParameters();

            _save_log_(); //save log

        }


        void _jud_fail() {
            Proj_Rework.FailParameters();
            //_save_log_(); //save log
            Proj_Rework.NewProductSerial = ""; //clear product serial number
            Dispatcher.Invoke(new Action(() => { txtnewproduct.IsEnabled = true; txtnewproduct.Focus(); })); //set focus for txt_SN
        }


        bool _save_log_() {
            bool r = false;
            r = new io_msaccdb_tbDataProductionLot().UpdateReworkData(Proj_Rework.ProductionLot, Proj_Rework.ReworkSerial, Proj_Rework.NewProductSerial, Proj_Rework.Reason, Proj_Rework.Operator);
            r = new io_msaccdb_tbDataProductionLot().WriteData(Proj_Rework);

            //print label
            Thread t = new Thread(new ThreadStart(() => {
                string msg;
                bool ret = new ReprintProductionLot(Proj_Rework.ProductionLot).Print(out msg);
                MessageBox.Show(ret ? "Success" : "Fail\n" + msg, "Reprint LOT " + Proj_Rework.ProductionLot, MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
            }));
            t.IsBackground = true;
            t.Start();

            Proj_Rework.NewProductSerial = ""; //clear product serial number
            return r;
        }

        private void Txtnewproduct_TextChanged(object sender, TextChangedEventArgs e) {
            if (Proj_Rework.NewProductSerial.Length > 0) { Proj_Rework.WaitingParameters(); }
            else {
                if (Proj_Rework.ReworkResult.ToLower().Equals("waiting...")) Proj_Rework.ReworkResult = "";
            }
        }
    }
}
