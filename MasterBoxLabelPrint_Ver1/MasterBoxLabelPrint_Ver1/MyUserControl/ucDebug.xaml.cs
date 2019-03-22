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

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Scale;

namespace MasterBoxLabelPrint_Ver1.MyUserControl
{
    /// <summary>
    /// Interaction logic for ucManual.xaml
    /// </summary>
    public partial class ucDebug : UserControl
    {
        public ucDebug()
        {
            InitializeComponent();
            this.cbb_debugtype.ItemsSource = MyParameter.debugTypes;
            this.DataContext = MyGlobal.myDebug;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            
            switch (b.Content) {
                case "Connect Scale": {
                        //new CAS_EDH().Start();
                        break;
                    }
                case "Disconnect Scale": {
                        break;
                    }
            }
        }
    }
}
