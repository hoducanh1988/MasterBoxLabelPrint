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

using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        List<Label> labels; List<Control> list;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = MyGlobal.myWindow;

            labels = new List<Label>() { lblRunAll, lblRework, lblSetting, lblLog, lblSOP, lblHelp, lblAbout, lblDebug };
            list = new List<Control>() { ucRunAll, ucRework, ucSOP, ucDebug, ucSetting, ucLog, ucHelp, ucAbout };

            foreach (var item in list) {
                item.Visibility = Visibility.Collapsed;
                Canvas.SetZIndex(item, 0);
            }

            ucRunAll.Visibility = Visibility.Visible;
            Canvas.SetZIndex(ucRunAll, 1);

            //load recent lot ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 2019/04/05
            string _lotname, _lotprogress;
            bool r = io_dll_Recent.FromFile(out _lotname, out _lotprogress);
            if (r) {
                MyGlobal.MyTesting.LotName = _lotname;
                MyGlobal.MyTesting.LotProgress = _lotprogress;
                MyGlobal.MyTesting.LotCount = _lotprogress.Split('/')[0];
            }
            else {
                bool flag = string.IsNullOrEmpty(MyGlobal.MySetting.ProductNumber) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionPlace) || string.IsNullOrEmpty(MyGlobal.MySetting.ProductionYear) || string.IsNullOrEmpty(MyGlobal.MySetting.LineIndex);
                if (!flag) {
                    //gen LOT
                    MyGlobal.MyTesting.LotCount = "0";
                    MyGlobal.MyTesting.LotName = new GenerateLOT(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber, MyGlobal.MySetting.LOTIndex, false).Gererate();

                    //delete IMEI_SN_Print talbe
                    new io_msaccdb_tbIMEISerialPrint().DeleteAll();
                }
            }
        }


        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            Label lb = sender as Label;
            //change label foreground --------------//

            foreach (var label in labels) label.Foreground = Brushes.Black;
            lb.Foreground = Brushes.Orange;

            //change under bar ---------------------//
            this.border_underline.Margin = lb.Margin;
            this.border_underline.Width = lb.Width - 10;
            //change user control ------------------//

            foreach (var item in list) {
                item.Visibility = Visibility.Collapsed;
                Canvas.SetZIndex(item, 0);
            }

            switch (lb.Content) {
                case "RunAll": {
                        ucRunAll.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucRunAll, 1);
                        break;
                    }
                case "Rework": {
                        ucRework.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucRework, 1);
                        break;
                    }
                case "SOP": {
                        ucSOP.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucSOP, 1);
                        break;
                    }
                case "Debug": {
                        //ucDebug.Visibility = Visibility.Visible;
                        //Canvas.SetZIndex(ucDebug, 1);
                        break;
                    }
                case "Setting": {
                        ucSetting.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucSetting, 1);
                        break;
                    }
                case "Log": {
                        ucLog.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucLog, 1);
                        break;
                    }
                case "Help": {
                        ucHelp.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucHelp, 1);
                        break;
                    }
                case "About": {
                        ucAbout.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucAbout, 1);
                        break;
                    }
            }
        }


    }
}
