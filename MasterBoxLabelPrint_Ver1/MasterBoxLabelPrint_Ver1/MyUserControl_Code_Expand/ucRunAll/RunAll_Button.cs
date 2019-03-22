using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;


namespace MasterBoxLabelPrint_Ver1.MyUserControl {

    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Tag) {
                case "search_datalog": {
                        List<msaccdb_tbDataLog> listdatalog = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataLog>("tb_DataLog", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_datalog_sn.Text, "TotalResult", cbb_list_result.Text, "Lot", txt_lot_name.Text);
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

    }
}
