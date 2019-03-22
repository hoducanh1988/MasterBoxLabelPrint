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
