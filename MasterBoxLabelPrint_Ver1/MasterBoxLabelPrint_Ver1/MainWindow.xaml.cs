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

using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }


        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lb = sender as Label;
            //change label foreground --------------//
            List<Label> labels = new List<Label>() {lblRunAll, lblRework, lblSetting, lblLog, lblSOP, lblHelp, lblAbout, lblDebug};
            foreach (var label in labels) label.Foreground = Brushes.Black;
            lb.Foreground = Brushes.Orange;

            //change under bar ---------------------//
            this.border_underline.Margin = lb.Margin;
            this.border_underline.Width = lb.Width - 10;
            //change user control ------------------//
            List<Control> list = new List<Control>() { ucRunAll, ucRework, ucSOP, ucDebug, ucSetting, ucLog, ucHelp, ucAbout };
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
                case "Debug":
                    {
                        //ucDebug.Visibility = Visibility.Visible;
                        //Canvas.SetZIndex(ucDebug, 1);
                        break;
                    }
                case "Setting":
                    {
                        ucSetting.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucSetting, 1);
                        break;
                    }
                case "Log":
                    {
                        ucLog.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucLog, 1);
                        break;
                    }
                case "Help":
                    {
                        ucHelp.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucHelp, 1);
                        break;
                    }
                case "About":
                    {
                        ucAbout.Visibility = Visibility.Visible;
                        Canvas.SetZIndex(ucAbout, 1);
                        break;
                    }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            new GetRecentProductionLot().SetData();
        }
    }
}
