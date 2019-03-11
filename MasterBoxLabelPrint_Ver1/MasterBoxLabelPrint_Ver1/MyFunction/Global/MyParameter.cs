using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Global {
    public class MyParameter {

        public static List<string> logTypes = new List<string>() { "Log MS Access", "Log File" };
        public static List<string> debugTypes = new List<string>() {
            "verify algorithm::check product serial number",
            "verify device::label printer",
            "verify device::scale",
            "verify device::ms access database file"

        };
        //public static List<string> IndexList = MyBase.Get_Init_File("IndexList.ini");
        //public static List<string> ProductList = MyBase.Get_Init_File("ProductList.ini");
        //public static List<string> StationList = MyBase.Get_Init_File("StationList.ini");
        public static List<string> PrintModes = new List<string>() { "Only print label", "Combine label printing with product weighing" };

        //public static List<string> ProductCodeList = MyBase.Get_Init_File("ProductCodeList.ini");
        //public static List<string> ProductionPlaceList = MyBase.Get_Init_File("ProductionPlaceList.ini");
        //public static List<string> ProductionYearList = MyBase.Get_Init_File("ProductionYearList.ini");

        public static List<string> SerialPorts = null;
        public static List<string> baudRates = new List<string>() { "75", "110", "134", "150", "300", "600", "1200", "1800", "2400", "4800", "7200", "9600", "14400", "19200", "38400", "57600", "115200", "128000", "256000" };
        public static List<string> dataBits = new List<string>() { "4", "5", "6", "7", "8" };
        public static List<string> paritys = new List<string>() { "Even", "Odd", "None", "Mark", "Space" };
        public static List<string> stopBits = new List<string>() { "1", "1.5", "2" };

        public static List<string> Results = new List<string>() { "PASS", "FAIL" };

        //public static Dictionary<string, string> dictGuide = new Dictionary<string, string>() {
        //    //CÀI ĐẶT TRẠM TEST --------------------------------------------------------------//
        //    {
        //        "lineindex",
        //        "<Dây chuyền>\n" +
        //        "Chỉ số của dây chuyền thiết lập trạm trong nhà máy. Giá trị là số: 1,2,3,4...\n" +
        //        "Cài đặt: lựa chọn trong danh sách được load từ file: \\ini_config\\IndexList.ini."
        //    },
        //    {
        //        "stationname",
        //        "<Tên Trạm Test>\n" +
        //        "Là tên trạm in tem. Nên viết liền không để khoảng trắng. Ví dụ: MasterBoxLabelPrint...\n" +
        //        "Cài đặt: lựa chọn trong danh sách được load từ file: \\ini_config\\StationList.ini."
        //    },
        //    {
        //        "stationindex",
        //        "<Số thứ tự của trạm>\n" +
        //        "Chỉ số của trạm test. Giá trị là số: 1,2,3,4...\n" +
        //        "Cài đặt: lựa chọn trong danh sách được load từ file: \\ini_config\\IndexList.ini."
        //    },
        //    {
        //        "jigindex",
        //        "<Số thứ tự Jig>\n" +
        //        "Chỉ số của jig trong 1 trạm. Trong trường hợp trạm chỉ có 1 jig hoặc không có jig thì chọn là 1.\n" +
        //        "Cài đặt: lựa chọn trong danh sách được load từ file: \\ini_config\\IndexList.ini."
        //    },
        //    {
        //        "operator",
        //        "<Tên người thao tác>\n" +
        //        "Tên người thao tác trực tiếp tại trạm test. Khi người thao tác thay đổi thì cũng phải thay đổi tham số này tương ứng.\n" +
        //        "Cài đặt: nhập từ bàn phím."
        //    },


        //    //CÀI ĐẶT SẢN PHẨM --------------------------------------------------------------//
        //    {
        //        "productname",
        //        "<Tên sản phẩm>\n" +
        //        "Thiết lập là tên sản phẩm đang in tem. Ví dụ: GW020BoB, GW040H... Khi sản phẩm thay đổi thì cũng phải thay đổi tham số này tương ứng.\n" +
        //        "Cài đặt: lựa chọn trong danh sách được load từ file: \\ini_config\\ProductList.ini."
        //    },
        //    {
        //        "printmode",
        //        "<Chế độ in tem>\n" +
        //        "Chọn 1 trong 2 chế độ: in tem thông thường hoặc in tem kết hợp với cân trọng lượng sản phẩm.\n" +
        //        "Cài đặt: lựa chọn trong danh sách sẵn có."
        //    },
        //    {
        //        "weightlowerlimit",
        //        "<Trọng lượng tối thiểu>\n" +
        //        "Là ngưỡng giá trị trọng lượng của sản phẩm thấp nhất cho phép. Đơn vị cài đặt là g.\n" +
        //        "Cài đặt: nhập từ bàn phím."
        //    },
        //    {
        //        "weightupperlimit",
        //        "<Trọng lượng tối đa>\n" +
        //        "Là ngưỡng giá trị trọng lượng của sản phẩm cao nhất cho phép. Đơn vị cài đặt là g.\n" +
        //        "Cài đặt: nhập từ bàn phím."
        //    },


        //    //CÀI ĐẶT TẠO SERIAL NUMBER --------------------------------------------------------//
        //    {
        //        "productcode",
        //        "<Mã sản phẩm>\n"
        //    },
        //    {
        //        "productionplace",
        //        "<Nơi sản xuất>\n"
        //    },
        //    {
        //        "productionyear",
        //        "<Năm sản xuất>\n"
        //    },
        //    {
        //        "productversion",
        //        "<Version sản phẩm>\n"
        //    },
        //    {
        //        "productcolorcode",
        //        "<Mã màu sản phẩm>\n"
        //    },


        //    //CÀI ĐẶT MÁY IN NHÃN --------------------------------------------------------//
        //    {
        //        "printname",
        //        "<Tên máy in nhãn>\n"
        //    },
        //    {
        //        "printpage",
        //        "<Số lượng bản in>\n"
        //    },
        //    {
        //        "msaccessfile",
        //        "<File in tem (file MS Access)>\n"
        //    },


        //    //CÀI ĐẶT QUÉT SERIAL MASTERBOX --------------------------------------------------------//
        //    {
        //        "lotquantity",
        //        "<Số lượng serial / 1 tem>\n"
        //    },
        //    {
        //        "productcodeinline",
        //        "<Mã sản phẩm theo chuyền sản xuất>\n"
        //    },


        //    //CÀI ĐẶT CÂN TRỌNG LƯỢNG --------------------------------------------------------//
        //    {
        //        "serialportname",
        //        "<Tên cổng giao tiếp>\n"
        //    },
        //    {
        //        "serialbaudrate",
        //        "<Tốc độ baud>\n"
        //    },
        //    {
        //        "serialdatabits",
        //        "<Data Bits>\n"
        //    },
        //    {
        //        "serialparity",
        //        "<Parity>\n"
        //    },
        //    {
        //        "serialstopbits",
        //        "<Stop bits>\n"
        //    },


        //    //CÀI ĐẶT LƯU LOG --------------------------------------------------------//
        //     {
        //        "dirlog",
        //        "<Đường dẫn lưu log>\n"
        //    },
        //    {
        //        "visiblelogquantity",
        //        "<Số lượng log SN hiển thị>\n"
        //    },

        //};


        static MyParameter() {
            //SerialPorts = new List<string>();
            //for (int i = 1; i < 100; i++) { SerialPorts.Add(string.Format("COM{0}", i)); }
        }

    }
}
