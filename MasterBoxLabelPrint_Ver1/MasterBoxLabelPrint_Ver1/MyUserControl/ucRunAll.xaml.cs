using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace MasterBoxLabelPrint_Ver1.MyUserControl {

    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {
        public ucRunAll() {
            InitializeComponent();
            this.cbb_list_result.ItemsSource = MyParameter.Results;

            this._grid_testing.DataContext = MyGlobal.MyTesting;
            this._grid_setting.DataContext = MyGlobal.MySetting;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Tag) {
                case "search_serial": {
                        List< msaccdb_tbDataLog> listSN = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataLog>("tb_DataLog", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_sn.Text, "TotalResult", cbb_list_result.Text, "Lot", txt_lot_name.Text);
                        this.datagrid_recentdatalog.ItemsSource = listSN;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "search_lot": {
                        List<msaccdb_LotProduction> listSN = MyGlobal.MasterBox.Get_Distinct_Newest_DataRow_From_Access_DB_Table<msaccdb_LotProduction>("SN_Print_Log", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "LoSX");
                        this.datagrid_recentlot.ItemsSource = listSN;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                default: break;
            }
        }



        private void Txt_SN_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                txt_SN.IsEnabled = false;
                MyGlobal.MyTesting.ErrorMessage = string.Format("Product: \"{0}\"\n", MyGlobal.MyTesting.ProductSerial);
                
                //validate serial number
                ValidateProductSerialNumber validate = new ValidateProductSerialNumber(MyGlobal.MyTesting.ProductSerial);
                bool serial_is_valid = validate.Validate();
                if (!serial_is_valid) {
                    MyGlobal.MyTesting.ErrorMessage += validate.Validate_Error_Message;
                    goto FAIL;
                }

                //check serial printed or not
                msaccdb_SerialPrintLog tb = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_SerialPrintLog>("SN_Print_Log", "SN", MyGlobal.MyTesting.ProductSerial);
                if (tb != null) {
                    MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number was printed in lot {0}, date printed {1}.", tb.LoSX, tb.TimeScan);
                    goto FAIL;
                }

                //check product weight
                if (MyGlobal.MySetting.UseScaleFlag  == true) {

                }

                goto PASS;

            
            FAIL:
                {
                    txt_SN.IsEnabled = true;
                    MyGlobal.MyTesting.FailParameters();
                    return;
                }

            PASS:
                {
                    MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number is valid.");
                    txt_SN.IsEnabled = true;
                    MyGlobal.MyTesting.PassParameters();




                    //save log

                    //gen lot
                    MyGlobal.MyTesting.LotCount = string.Format("{0}", int.Parse(MyGlobal.MyTesting.LotCount) + 1);
                    if (MyGlobal.MyTesting.LotCount.Equals(MyGlobal.MyTesting.LotLimit)) {
                        MyGlobal.MyTesting.LotCount = "0";
                        MyGlobal.MyTesting.LotName =  new GenerateProductionLot().Gererate();
                    }

                    //save testing value

                    return;
                }
            
            }
        }

        private void Txt_SN_TextChanged(object sender, TextChangedEventArgs e) {
            if (MyGlobal.MyTesting.ProductSerial.Length > 0) { MyGlobal.MyTesting.WaitingParameters(); }
            else {
                if (MyGlobal.MyTesting.TotalResult.ToLower().Equals("waiting...")) MyGlobal.MyTesting.TotalResult = "";
            }
        }

    }
}
