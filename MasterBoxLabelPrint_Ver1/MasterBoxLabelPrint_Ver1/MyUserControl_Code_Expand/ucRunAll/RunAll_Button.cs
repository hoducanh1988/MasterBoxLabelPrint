using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using System;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {

    /// <summary>
    /// Interaction logic for ucRunAll.xaml
    /// </summary>
    public partial class ucRunAll : UserControl {


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Tag) {
                case "search_datalog": {
                        List<msaccdb_tbDataLog> listdatalog = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataLog>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataLog" : "tb_DataLog_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_datalog_sn.Text, "TotalResult", cbb_list_result.Text, "Lot", txt_lot_name.Text);
                        this.datagrid_recentdatalog.ItemsSource = listdatalog;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "search_printed": {
                        List<msaccdb_tbDataProductionLOT> listdataproductionlot = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_search_printed_sn.Text, "LotStatus", "", "Lot", txt_printed_lot.Text);
                        this.datagrid_recentproduct.ItemsSource = listdataproductionlot;
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "search_lot": {
                        this.datagrid_recentlot.ItemsSource = new io_msaccdb_tbDataProductionLot().ReadProductionLot(txt_lot_recent.Text);
                        MessageBox.Show("Success.", "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                case "export_excel": {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        saveFileDialog.Filter = "Excel 1997 - 2003 (*.xls)|*.xls";
                        if (saveFileDialog.ShowDialog() == true) {
                            string file_name = saveFileDialog.FileName;
                            new io_msaccdb_tbDataLog().ExportToExcel(file_name);
                            MessageBox.Show("Success.", "Export Log MSAccess To Excel File", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    }
                default: break;
            }
        }

    }
}
