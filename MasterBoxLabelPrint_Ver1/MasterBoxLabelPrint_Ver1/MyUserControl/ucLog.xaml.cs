using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Scale;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {
    /// <summary>
    /// Interaction logic for ucLog.xaml
    /// </summary>
    public partial class ucLog : UserControl {

        class CalibInfo : zCNotifyPropertyChanged {
            string _weight_data;
            public string WeightData {
                get { return _weight_data; }
                set {
                    _weight_data = value;
                    OnPropertyChanged(nameof(WeightData));
                }
            }
        }


        Thread calib_thread = null;
        DispatcherTimer timer = null;
        CalibInfo _calibInfo = new CalibInfo();

        public ucLog() {
            InitializeComponent();
            this.cbb_logtype.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.logtype }).Where(x => x.logtype.Trim() != "").Select(x => x.logtype.ToString()).ToList();
            this.datagrid_calib.ItemsSource = MyGlobal.calibWeightGridItem;
            this.DataContext = _calibInfo;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += ((sender, e) => {
                if (btn_startcalib.IsEnabled == false) {
                    this._scrollViewer.ScrollToEnd();
                }
            });
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            switch (b.Content) {
                case "Go": {
                        if (cbb_logtype.SelectedValue != null) {
                            string text = cbb_logtype.SelectedValue.ToString();
                            string logpath = "";
                            string dir = string.Format(@"{0}\{1}\{2}\{3}",
                                MyGlobal.MySetting.DirLog,
                                MyGlobal.MySetting.ProductName,
                                MyGlobal.MySetting.StationName,
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
                        lbl_avr.Content = "--";
                        btn_startcalib.IsEnabled = false;
                        btn_stopcalib.IsEnabled = true;
                        MyGlobal.calibWeightGridItem.Clear();
                        _calibInfo.WeightData = "";

                        int count = 0, max_count = 1;
                        bool r = int.TryParse(txt_CalibQty.Text, out max_count);
                        max_count = r == true ? max_count : 100;

                        calib_thread = new Thread(new ThreadStart(() => {

                            while (count < max_count) {
                                string data = "", w = "0";
                                //cho den khi can on dinh va gia tri > 100g
                                try {
                                    while (!(data.Contains("ST") == true && double.Parse(w) > 100)) {
                                        data = CAS_EDH.GetDataFromScale();
                                        _calibInfo.WeightData += data + "\r\n";
                                        if (data != null) {
                                            w = CAS_EDH.GetWeightFromScaleString(data);
                                        }
                                    }
                                } catch {
                                    Dispatcher.Invoke(new Action(() => {
                                        btn_startcalib.IsEnabled = true;
                                        btn_stopcalib.IsEnabled = false;
                                    }));
                                    return;
                                }
                                
                                //get weight
                                Dispatcher.Invoke(new Action(() => {
                                    MyGlobal.calibWeightGridItem.Add(new MyFunction.Implement.calib_WeightInfo() { Order = string.Format("{0}", count + 1), Weight = w, UOM = "g" });
                                    _cal_avr_weight();
                                }));

                                //cho den khi sp nhac ra khoi can thi reset
                                while (!(data.Contains("US") == true)) {
                                    data = CAS_EDH.GetDataFromScale();
                                    _calibInfo.WeightData += data + "\r\n";
                                }

                                count++;

                                //exit calib weight
                                if (count >= max_count) {
                                    break;
                                }
                            }

                            Dispatcher.Invoke(new Action(() => {
                                btn_startcalib.IsEnabled = true;
                                btn_stopcalib.IsEnabled = false;
                            }));

                        }));
                        calib_thread.IsBackground = true;
                        calib_thread.Start();

                        break;
                    }

                case "Stop Calib": {
                        _stop_calib();
                        break;
                    }
            }
        }

        void _stop_calib() {
            Dispatcher.Invoke(new Action(() => {
                btn_startcalib.IsEnabled = true;
                btn_stopcalib.IsEnabled = false;
            }));
            if (calib_thread != null && calib_thread.IsAlive) calib_thread.Abort();
        }

        void _cal_avr_weight() {
            double value = 0.0;
            if (MyGlobal.calibWeightGridItem.Count > 0) {
                List<double> weights = MyGlobal.calibWeightGridItem.Select(x => double.Parse(x.Weight)).ToList();
                value = Math.Round(weights.Average(), 2);
            }
            lbl_avr.Content = value.ToString() + " g";
        }
    }
}
