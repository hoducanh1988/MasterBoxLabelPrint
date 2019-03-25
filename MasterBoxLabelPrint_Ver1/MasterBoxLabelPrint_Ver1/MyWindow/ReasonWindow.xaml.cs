using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
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
using System.Windows.Shapes;

namespace MasterBoxLabelPrint_Ver1
{
    /// <summary>
    /// Interaction logic for ReasonWindow.xaml
    /// </summary>
    public partial class ReasonWindow : Window
    {
        Proj_ReworkInformation rework = null;

        public ReasonWindow(ref Proj_ReworkInformation proj_Rework)
        {
            InitializeComponent();
            rework = proj_Rework;
            //
            this.txt_operator.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            rework.Operator = txt_operator.Text;
            rework.Reason = new TextRange(rtb_reason.Document.ContentStart, rtb_reason.Document.ContentEnd).Text;
        }
    }
}
