using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
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

namespace MasterBoxLabelPrint_Ver1.MyUserControl
{
    /// <summary>
    /// Interaction logic for ucAbout.xaml
    /// </summary>
    public partial class ucAbout : UserControl
    {
        private class history {
            public string ID { get; set; }
            public string VERSION { get; set; }
            public string CONTENT { get; set; }
            public string DATE { get; set; }
            public string CHANGETYPE { get; set; }
            public string PERSON { get; set; }
        }

        List<history> listHist = new List<history>();

        public ucAbout()
        {
            InitializeComponent();

            MyGlobal.SuggestionTexts = Utility.GetSuggestionTexts();
            MyGlobal.Guidelines = Utility.GetGuidelines();

            //load setting
            if (System.IO.File.Exists(MyFunction.Global.MyGlobal.Setting_FileFullName)) {
                try {
                    MyFunction.Global.MyGlobal.MySetting = UtilityPack.IO.XmlHelper<MyFunction.Custom.Proj_SettingInformation>.FromXmlFile(
                        MyFunction.Global.MyGlobal.Setting_FileFullName);
                    MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                }
                catch { }
            }
            
            //load production lot
            new GetRecentProductionLot(MyGlobal.MySetting.LineIndex, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.ProductNumber).GetData();


            listHist.Add(new history() {
                ID = "1",
                VERSION = "1.0.0.0",
                CONTENT = "- Phát hành lần đầu",
                DATE = "05/03/2019",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });


            this.GridAbout.ItemsSource = listHist;
        }


        
    }
}
