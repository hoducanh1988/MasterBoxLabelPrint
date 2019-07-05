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
                    MyFunction.Global.MyGlobal.MySetting = UtilityPack.IO.XmlHelper<MyFunction.Custom.Proj_SettingInformation>.FromXmlFile(MyFunction.Global.MyGlobal.Setting_FileFullName);
                    MyGlobal.MasterBox = new MyFunction.AccessDatabase.MasterBoxAccessDB(MyGlobal.MySetting.MSAccessFile);
                }
                catch { }
            }
            

            listHist.Add(new history() {
                ID = "1",
                VERSION = "1.0.0.0",
                CONTENT = "- Phát hành lần đầu",
                DATE = "2019/03/05",
                CHANGETYPE = "Tạo mới",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "2",
                VERSION = "1.0.0.1",
                CONTENT = "- Lưu log theo định dạng mới của BCN yêu cầu : \n" + "DATE_TIME,WORK_ORDER,OPERATOR,PN,UID1,UID2,TESTNAME,L_LIMIT,U_LIMIT,MEASURE_VAL,RESULT,INFO1,INFO2,INFO3,INFO4,INFO5,INFO6,INFO7,INFO8,INFO9,INFO10.\n" +
                          "- Thêm chức năng export bảng tb_DataLog to excel file.\n" +
                          "- Giới hạn check trùng lặp số SN với 10,000 sản phẩm gần nhất => tránh đơ phần mềm khi số lượng dữ liệu log lớn.\n" +
                          "- Fix bug lỗi số lượng sản phẩm trong 1 lot lớn hơn quy định.",
                DATE = "2019/04/08",
                CHANGETYPE = "Chỉnh sửa",
                PERSON = "Hồ Đức Anh"
            });

            listHist.Add(new history() {
                ID = "3",
                VERSION = "1.0.0.2",
                CONTENT = "- Thêm chức năng tùy chọn lưu hoặc không lưu log chưa kiểm tra.\n" +
                          "- Thêm chức năng tùy chọn lưu hoặc không lưu log check FORMAT, PRINTED, WEIGHT.",
                DATE = "2019/04/11 && 2019/04/25",
                CHANGETYPE = "Chỉnh sửa",
                PERSON = "Hồ Đức Anh"
            });

            this.GridAbout.ItemsSource = listHist;
        }


        
    }
}
