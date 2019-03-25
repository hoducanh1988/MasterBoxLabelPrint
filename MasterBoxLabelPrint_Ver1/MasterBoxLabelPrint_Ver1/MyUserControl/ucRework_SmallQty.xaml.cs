using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
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

namespace MasterBoxLabelPrint_Ver1.MyUserControl
{
    /// <summary>
    /// Interaction logic for ucRework_SmallQty.xaml
    /// </summary>
    public partial class ucRework_SmallQty : UserControl
    {
        Proj_ReworkInformation Proj_Rework = null;


        public ucRework_SmallQty()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Tag) {
                case "search_lot_rework": {
                        List<msaccdb_tbDataProductionLOT> listdataproductionlot = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", txt_product_serial.Text, "Rework", "-", "Lot", txt_lot.Text);
                        this.data_grid_lot.ItemsSource = listdataproductionlot;
                        MessageBox.Show(string.Format("Success. Found {0} results.", listdataproductionlot.Count), "Search Log MSAccess", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MenuItem menu = sender as MenuItem;

            string lot_selected = "";
            msaccdb_tbDataProductionLOT row_selected = null;

            try {
                row_selected = (msaccdb_tbDataProductionLOT)this.data_grid_lot.SelectedValue;
                lot_selected = row_selected.Lot;
            }
            catch { return; }


            switch (menu.Header) {
                case "Reprint label": {
                        string msg = "";

                        if (lot_selected != "") {
                            Thread t = new Thread(new ThreadStart(() => {
                                bool r = new ReprintProductionLot(lot_selected).Print(out msg);
                                MessageBox.Show(r ? "Success" : "Fail\n" + msg, "Reprint LOT " + lot_selected, MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        else {
                            MessageBox.Show("Please select a production lot.", "Reprint", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                    }
                case "Rework": {
                        Init_Product product = MyGlobal.Products.Single(x => x.name.Equals(row_selected.ProductName));

                        this.Proj_Rework = new Proj_ReworkInformation() {
                            ProductionLot = row_selected.Lot,
                            LotProgress = row_selected.LotProgress,
                            ProductName = row_selected.ProductName,
                            ReworkSerial = row_selected.ProductSerial,
                            ProductInfo = product.ToString(),
                            Line = row_selected.Line,
                            LotQuantity = row_selected.LotQuantity,
                            WeightLimit = string.Format("{0} ~ {1}", double.Parse(product.weight) - double.Parse(product.bias), double.Parse(product.weight) + double.Parse(product.bias))
                        };

                        MyGlobal.myWindow.WindowOpacity = 0.3;

                        ReasonWindow reason = new ReasonWindow(ref Proj_Rework);
                        reason.ShowDialog();
                        if (Proj_Rework.Operator.Trim() == "" || Proj_Rework.Reason.Trim() == "") {
                            MessageBox.Show("Tên người thao tác hoặc lý do rework không hợp lệ.", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                            break;
                        }

                        ReworkWindow rework = new ReworkWindow(Proj_Rework, product);
                        rework.ShowDialog();
                        break;
                    }
                case "Refresh": {
                        break;
                    }
            }
            MyGlobal.myWindow.WindowOpacity = 1;
        }
    }
}
