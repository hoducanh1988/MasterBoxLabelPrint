using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MasterBoxLabelPrint_Ver1.MyUserControl
{
    /// <summary>
    /// Interaction logic for ucLog.xaml
    /// </summary>
    public partial class ucLog : UserControl
    {
        public ucLog()
        {
            InitializeComponent();
            this.cbb_logtype.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.logtype }).Where(x => x.logtype.Trim() != "").Select(x => x.logtype.ToString()).ToList();

            this.datagrid_calib.ItemsSource = MyGlobal.calibWeightGridItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Content) {
                case "Go": {
                        if (cbb_logtype.SelectedValue != null) {
                            string text = cbb_logtype.SelectedValue.ToString();
                            string logpath = "";
                            string dir = string.Format(@"{0}\{1}\{2}\{3}\{4}", 
                                MyGlobal.MySetting.DirLog,
                                MyGlobal.MySetting.ProductName,
                                MyGlobal.MySetting.StationName,
                                MyGlobal.MySetting.StationIndex,
                                MyGlobal.MySetting.JigIndex
                                );

                            switch (text) {
                                case "LogTotal": {
                                        logpath = System.IO.Path.Combine(dir, "LogTotal");
                                        break;
                                    }
                                case "LogSingle": {
                                        logpath = System.IO.Path.Combine(dir, "LogSingle");
                                        break;
                                    }
                                case "LogDetail": {
                                        logpath = System.IO.Path.Combine(dir, "LogDetail");
                                        break;
                                    }
                                case "Default": {
                                        logpath = AppDomain.CurrentDomain.BaseDirectory;
                                        break;
                                    }
                                default: {
                                        logpath = AppDomain.CurrentDomain.BaseDirectory;
                                        break;
                                    }
                            }

                            if (System.IO.Directory.Exists(logpath)) {
                                Process.Start(logpath);
                            }
                            else {
                                MessageBox.Show(string.Format("path {0} is not exist.", logpath), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            
                        }
                        else {
                            MessageBox.Show("Please select a log type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }

                case "Start Calib": {
                        break;
                    }

                case "Stop Calib": {
                        break;
                    }
            }
        }
    }
}
