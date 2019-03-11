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
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

using UtilityPack.IO;

namespace MasterBoxLabelPrint_Ver1.MyUserControl {
    /// <summary>
    /// Interaction logic for ucSetting.xaml
    /// </summary>
    public partial class ucSetting : UserControl {

        public ucSetting() {
            InitializeComponent();

            //load _ref to combobox
            MyGlobal.Guidelines = Utility.GetGuidelines();
            MyGlobal.SuggestionTexts = Utility.GetSuggestionTexts();
            _init_ItemSource_For_Combobox_();

            //load setting
            if (System.IO.File.Exists(MyFunction.Global.MyGlobal.Setting_FileFullName)) {
                try {
                    MyFunction.Global.MyGlobal.MySetting = UtilityPack.IO.XmlHelper<MyFunction.Custom.Proj_SettingInformation>.FromXmlFile(
                        MyFunction.Global.MyGlobal.Setting_FileFullName);
                    MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                }
                catch { }
            }

            //load product name
            if (!string.IsNullOrEmpty(MyGlobal.MySetting.FileProduct)) MyGlobal.Products = Utility.GetProducts(MyGlobal.MySetting.FileProduct);
            if (MyGlobal.Products != null) {
                this.cbb_product_name.ItemsSource = MyGlobal.Products.Select(x => new { x.name }).Where(x => x.name.Trim() != "").Select(x => x.name.ToString()).ToList();
                MyGlobal.MySetting.Get_product_info();
            }

            //
            this.DataContext = MyGlobal.MySetting;
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;

            switch (b.Tag) {
                case "select_file_product": {
                        System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                        fileDialog.InitialDirectory = string.Format("{0}conf_", AppDomain.CurrentDomain.BaseDirectory);
                        fileDialog.Filter = "*.csv|*.csv";
                        if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            //get csv file
                            MyGlobal.MySetting.FileProduct = fileDialog.SafeFileName;

                            //load products
                            MyGlobal.Products = Utility.GetProducts(MyGlobal.MySetting.FileProduct);
                            if (MyGlobal.Products != null) {
                                this.cbb_product_name.ItemsSource = MyGlobal.Products.Select(x => new { x.name }).Where(x => x.name.Trim() != "").Select(x => x.name.ToString()).ToList();
                                MyGlobal.MySetting.Get_product_info();
                            }
                        }
                        break;
                    }
                case "select_printer": {
                        // Create the print dialog object and set options
                        PrintDialog printDialog = new PrintDialog();
                        // Display the dialog. This returns true if the user presses the Print button.
                        Nullable<Boolean> print = printDialog.ShowDialog();
                        //
                        if (print == true) {
                            MyGlobal.MySetting.PrinterName = printDialog.PrintQueue.FullName;
                        }
                        break;
                    }
                case "select_msdb": {
                        System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                        fileDialog.InitialDirectory = string.Format("{0}", AppDomain.CurrentDomain.BaseDirectory);
                        fileDialog.Filter = "*.accdb|*.accdb";
                        if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            //get msdb file
                            MyGlobal.MySetting.MSAccessFile = fileDialog.SafeFileName;
                        }
                        break;
                    }
                case "select_logdir": {
                        System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
                        if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            //get folder
                            MyGlobal.MySetting.DirLog = folderBrowser.SelectedPath;
                        }
                        break;
                    }
                case "save_setting": {
                        bool r = false;
                        try {
                            XmlHelper<MyFunction.Custom.Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName);
                            MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                            r = true;
                        }
                        catch { }
                        MessageBox.Show(r == true ? "Success." : "Error.", "Save Setting", MessageBoxButton.OK, r == true ? MessageBoxImage.Information : MessageBoxImage.Error);
                        break;
                    }
                default: break;
            }

        }

        private void FrameWorkElement_Focus(object sender, RoutedEventArgs e) {
            //string data;

            //FrameworkElement element = sender as FrameworkElement;
            //MyFunction.Global.MyParameter.dictGuide.TryGetValue(element.Tag.ToString(), out data);
            //tbGuide.Text = data;
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            e.Handled = !((ComboBox)sender).IsDropDownOpen;
        }


        #region sub functions

        private void _init_ItemSource_For_Combobox_() {

            if (MyGlobal.SuggestionTexts != null) {
                this.cbb_baud_rate.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.baud }).Where(x => x.baud.Trim() != "").Select(x => x.baud.ToString()).ToList();
                this.cbb_data_bit.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.databit }).Where(x => x.databit.Trim() != "").Select(x => x.databit.ToString()).ToList();
                this.cbb_jig_index.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.index }).Where(x => x.index.Trim() != "").Select(x => x.index.ToString()).ToList();
                this.cbb_line.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.line }).Where(x => x.line.Trim() != "").Select(x => x.line.ToString()).ToList();
                this.cbb_parity.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.parity }).Where(x => x.parity.Trim() != "").Select(x => x.parity.ToString()).ToList();
                this.cbb_port_name.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.port }).Where(x => x.port.Trim() != "").Select(x => x.port.ToString()).ToList();
                this.cbb_print_mode.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.mode }).Where(x => x.mode.Trim() != "").Select(x => x.mode.ToString()).ToList();
                this.cbb_print_page.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.index }).Where(x => x.index.Trim() != "").Select(x => x.index.ToString()).ToList();
                this.cbb_station_index.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.index }).Where(x => x.index.Trim() != "").Select(x => x.index.ToString()).ToList();
                this.cbb_product_version.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.index }).Where(x => x.index.Trim() != "").Select(x => x.index.ToString()).ToList();
                this.cbb_station_name.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.station }).Where(x => x.station.Trim() != "").Select(x => x.station.ToString()).ToList();
                this.cbb_production_place.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.place }).Where(x => x.place.Trim() != "").Select(x => x.place.ToString()).ToList();
                this.cbb_production_year.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.year }).Where(x => x.year.Trim() != "").Select(x => x.year.ToString()).ToList();
                this.cbb_stop_bit.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.stopbit }).Where(x => x.stopbit.Trim() != "").Select(x => x.stopbit.ToString()).ToList();
                this.cbb_visible_log_quantity.ItemsSource = MyGlobal.SuggestionTexts.Select(x => new { x.quantity }).Where(x => x.quantity.Trim() != "").Select(x => x.quantity.ToString()).ToList();
            }
        }

        #endregion

       
    }
}
