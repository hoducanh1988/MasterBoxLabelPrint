using System.Windows.Controls;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using System.Threading;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


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

            Thread t = new Thread(new ThreadStart(() => {
                while (true) {
                    if (MyGlobal.MySetting.ProductionStatus == "Normal") {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                this.Background = Brushes.White;
                                this.lblproductionstatus.Content = "";
                            } catch { }
                            
                        }));
                    }
                    else {
                        Dispatcher.Invoke(new Action(() => {
                            try {
                                this.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#D0D0D0");
                                this.lblproductionstatus.Content = "=> Bulk Rework";
                            } catch { }
                        }));
                    }
                    Thread.Sleep(1000);
                }
            }));
            t.IsBackground = true;
            t.Start();
        }
        
    }
}
