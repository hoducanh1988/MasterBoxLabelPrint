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

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;

namespace MasterBoxLabelPrint_Ver1
{
    /// <summary>
    /// Interaction logic for ReworkWindow.xaml
    /// </summary>
    public partial class ReworkWindow : Window
    {
        Proj_ReworkInformation Proj_Rework = null;
        Init_Product init_Product = null;



        public ReworkWindow(Proj_ReworkInformation _ReworkInformation, Init_Product _Product)
        {
            InitializeComponent();

            this.Proj_Rework = _ReworkInformation;
            this.init_Product = _Product;

            this.DataContext = this.Proj_Rework;
        }
    }
}
