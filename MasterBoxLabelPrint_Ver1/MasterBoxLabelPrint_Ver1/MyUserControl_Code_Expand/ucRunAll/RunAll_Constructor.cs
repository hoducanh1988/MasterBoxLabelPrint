using System.Windows.Controls;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;


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
        }
        
    }
}
