using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom
{
    public class Proj_SettingInformation : MasterBoxLabelPrint_Ver1.MyFunction.Ulti.zCNotifyPropertyChanged {

        //constructor
        public Proj_SettingInformation() {
            //com
            SerialBaudRate = "9600";
            SerialDataBits = "8";
            SerialParity = "None";
            SerialStopBits = "1";

            //log
            DirLog = "D:\\LOGDATA";
            VisibleLogQuantity = "100";

            //SOP
            SOPServer = "192.168.10.1";

            //printer
            MSAccessFile = "MASTER_LABLE_PRINT_20190109.accdb";
            PrintPage = "1";

            //cai dat sx
            PrintMode = "Combine label printing with product weighing";

            //temp
            ProductionCommand = "#12345";
            Operator = "ABCD";
            SerialPortName = "COM3";
            PrinterName = "Microsoft Print to PDF";
            StationName = "MasterBoxLabelPrint";
            StationIndex = "1";
            JigIndex = "1";
            ProductVersion = "3";
        }

        //method
        public void Get_product_info() {
            if (MyGlobal.Products != null) {
                Init_Product product = MyGlobal.Products.Single(x => x.name.Equals(MyGlobal.MySetting.ProductName));
                if (product != null) {
                    MyGlobal.MySetting.ProductNumber = product.number;
                    MyGlobal.MySetting.ProductCode = product.code;
                    MyGlobal.MySetting.ColorCode = product.color;
                    MyGlobal.MySetting.ProductWeight = product.weight;
                    MyGlobal.MySetting.WeightBias = product.bias;
                    MyGlobal.MySetting.LotQuantity = product.lotqty;

                    MyGlobal.MySetting.WeightLL = string.Format("{0}", double.Parse(product.weight) - double.Parse(product.bias));
                    MyGlobal.MySetting.WeightUL = string.Format("{0}", double.Parse(product.weight) + double.Parse(product.bias));

                    MyGlobal.MySetting.ProductInfo = product.ToString();
                }
            }
        }

        //1
        #region CAI_DAT_SAN_XUAT ########################################

        string _production_place; //factory
        public string ProductionPlace {
            get { return _production_place; }
            set {
                _production_place = value;
                OnPropertyChanged(nameof(ProductionPlace));
                new GetRecentProductionLot(LineIndex, ProductionPlace, ProductionYear, ProductNumber).GetData();
            }
        }

        string _production_year; //production year
        public string ProductionYear {
            get { return _production_year; }
            set {
                _production_year = value;
                OnPropertyChanged(nameof(ProductionYear));
                new GetRecentProductionLot(LineIndex, ProductionPlace, ProductionYear, ProductNumber).GetData();
            }
        }

        string _production_command; //production command
        public string ProductionCommand {
            get { return _production_command; }
            set {
                _production_command = value;
                OnPropertyChanged(nameof(ProductionCommand));
            }
        }

        string _print_mode; //print mode
        public string PrintMode {
            get { return _print_mode; }
            set {
                _print_mode = value;
                OnPropertyChanged(nameof(PrintMode));
                if (PrintMode == "Combine label printing with product weighing") MyGlobal.MyTesting.UseScaleFlag = true;
                else MyGlobal.MyTesting.UseScaleFlag = false;
            }
        }

        #endregion

        //2
        #region CAI_DAT_TRAM_IN_TEM #####################################

        string _line_index; //line
        public string LineIndex {
            get { return _line_index; }
            set {
                _line_index = value;
                OnPropertyChanged(nameof(LineIndex));
                new GetRecentProductionLot(LineIndex, ProductionPlace, ProductionYear, ProductNumber).GetData();
            }
        }

        string _station_name; //station name
        public string StationName {
            get { return _station_name; }
            set {
                _station_name = value;
                OnPropertyChanged(nameof(StationName));
            }
        }

        string _station_index; //station index
        public string StationIndex {
            get { return _station_index; }
            set {
                _station_index = value;
                OnPropertyChanged(nameof(StationIndex));
            }
        }

        string _jig_index; //jig index
        public string JigIndex {
            get { return _jig_index; }
            set {
                _jig_index = value;
                OnPropertyChanged(nameof(JigIndex));
            }
        }

        string _operator; //operator
        public string Operator {
            get { return _operator; }
            set {
                _operator = value;
                OnPropertyChanged(nameof(Operator));
            }
        }

        #endregion

        //3
        #region CAI_DAT_SAN_PHAM ########################################

        string _file_product; //file product
        public string FileProduct {
            get { return _file_product; }
            set {
                _file_product = value;
                OnPropertyChanged(nameof(FileProduct));
            }
        }

        string _product_name; //product name
        public string ProductName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(ProductName));
                MyGlobal.MyTesting.ProductName = ProductName;

                Get_product_info();
                new GetRecentProductionLot(LineIndex, ProductionPlace, ProductionYear, ProductNumber).GetData();
            }
        }

        string _product_code; //product code in line
        public string ProductCode {
            get { return _product_code; }
            set {
                _product_code = value;
                OnPropertyChanged(nameof(ProductCode));
            }
        }

        string _product_version; //product version
        public string ProductVersion {
            get { return _product_version; }
            set {
                _product_version = value;
                OnPropertyChanged(nameof(ProductVersion));
            }
        }

        #endregion

        //4
        #region CAI_DAT_MAY_IN_NHAN #####################################

        string _printer_name; //printer name
        public string PrinterName {
            get { return _printer_name; }
            set {
                _printer_name = value;
                OnPropertyChanged(nameof(PrinterName));
            }
        }

        string _print_page; //printer page
        public string PrintPage {
            get { return _print_page; }
            set {
                _print_page = value;
                OnPropertyChanged(nameof(PrintPage));
            }
        }

        string _ms_access_file; //database file
        public string MSAccessFile {
            get { return _ms_access_file; }
            set {
                _ms_access_file = value;
                OnPropertyChanged(nameof(MSAccessFile));
            }
        }

        #endregion

        //5
        #region CAI_DAT_CAN_TRONG_LUONG #################################

        string _serial_port_name; //serial port name
        public string SerialPortName {
            get { return _serial_port_name; }
            set {
                _serial_port_name = value;
                OnPropertyChanged(nameof(SerialPortName));
            }
        }

        string _serial_baud_rate; //baud rate
        public string SerialBaudRate {
            get { return _serial_baud_rate; }
            set {
                _serial_baud_rate = value;
                OnPropertyChanged(nameof(SerialBaudRate));
            }
        }

        string _serial_data_bits; //data bits
        public string SerialDataBits {
            get { return _serial_data_bits; }
            set {
                _serial_data_bits = value;
                OnPropertyChanged(nameof(SerialDataBits));
            }
        }

        string _serial_parity; //parity
        public string SerialParity {
            get { return _serial_parity; }
            set {
                _serial_parity = value;
                OnPropertyChanged(nameof(SerialParity));
            }
        }

        string _serial_stop_bits; //stop bits
        public string SerialStopBits {
            get { return _serial_stop_bits; }
            set {
                _serial_stop_bits = value;
                OnPropertyChanged(nameof(SerialStopBits));
            }
        }

        string _connection_timeout; //connection timeout
        public string ConnectionTimeout {
            get { return _connection_timeout; }
            set {
                _connection_timeout = value;
                OnPropertyChanged(nameof(ConnectionTimeout));
            }
        }

        #endregion

        //6
        #region CAI_DAT_LUU_LOG #########################################

        string _dir_log; //log directory
        public string DirLog {
            get { return _dir_log; }
            set {
                _dir_log = value;
                OnPropertyChanged(nameof(DirLog));
            }
        }

        string _visible_log_quantity; //visible log quantity
        public string VisibleLogQuantity {
            get { return _visible_log_quantity; }
            set {
                _visible_log_quantity = value;
                OnPropertyChanged(nameof(VisibleLogQuantity));
            }
        }

        #endregion

        //7
        #region CAI_DAT_SOP #############################################

        string _sop_server; //SOP Server
        public string SOPServer {
            get { return _sop_server; }
            set {
                _sop_server = value;
                OnPropertyChanged(nameof(SOPServer));
            }
        }

        #endregion

        //8
        #region THONG_TIN_SAN_PHAM ######################################

        string _product_number; //product Number
        public string ProductNumber {
            get { return _product_number; }
            set {
                _product_number = value;
                OnPropertyChanged(nameof(ProductNumber));
            }
        }

        string _color_code; //color code
        public string ColorCode {
            get { return _color_code; }
            set {
                _color_code = value;
                OnPropertyChanged(nameof(ColorCode));
            }
        }

        string _product_weight; //product weight
        public string ProductWeight {
            get { return _product_weight; }
            set {
                _product_weight = value;
                OnPropertyChanged(nameof(ProductWeight));
            }
        }

        string _weight_bias; //weight tolerance
        public string WeightBias {
            get { return _weight_bias; }
            set {
                _weight_bias = value;
                OnPropertyChanged(nameof(WeightBias));
            }
        }

        string _lot_quantity; //lot quantity
        public string LotQuantity {
            get { return _lot_quantity; }
            set {
                _lot_quantity = value;
                OnPropertyChanged(nameof(LotQuantity));
                MyGlobal.MyTesting.LotLimit = LotQuantity;
            }
        }

        #endregion

        //9
        #region OTHER ###################################################
        string _weight_lower_limit; //weight lower
        public string WeightLL {
            get { return _weight_lower_limit; }
            set {
                _weight_lower_limit = value;
                OnPropertyChanged(nameof(WeightLL));
                MyGlobal.MyTesting.WeightStandard = string.Format("{0} ~ {1}", WeightLL, WeightUL);
            }
        }

        string _weight_upper_limit; //weight upper
        public string WeightUL {
            get { return _weight_upper_limit; }
            set {
                _weight_upper_limit = value;
                OnPropertyChanged(nameof(WeightUL));
                MyGlobal.MyTesting.WeightStandard = string.Format("{0} ~ {1}", WeightLL, WeightUL);
            }
        }

        string _product_info;  //product info
        public string ProductInfo {
            get { return _product_info; }
            set {
                _product_info = value;
                OnPropertyChanged(nameof(ProductInfo));
            }
        }

        #endregion

    }
}
