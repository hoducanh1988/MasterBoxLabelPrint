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
            LampPortName = "COM2";
            SerialPortName = "COM1";
            SerialBaudRate = "9600";
            SerialDataBits = "8";
            SerialParity = "None";
            SerialStopBits = "1";
            WaitTime = "100";

            //log
            DirLog = "D:\\LOGDATA";
            VisibleLogQuantity = "100";
            SaveLogNoMeaning = "No";
            IsSaveLogFormat = true;
            IsSaveLogPrinted = true;
            IsSaveLogWeight = true;
            DuplicateQuantity = "10000";

            //SOP
            SOPServer = "10.5.1.254";

            //printer
            MSAccessFile = "MASTER_LABLE_PRINT_20190109.accdb";
            PrintPage = "1";
            

            //cai dat sx
            PrintMode = "Combine label printing with product weighing";

            //temp
            ProductionCommand = "-";
            Operator = "-";
            PrinterName = "-";
            StationName = "MasterBoxLabelPrint";
            JigIndex = "1";
            ProductVersion = "3";

            //other
            ProductionStatus = "Normal";
            LOTIndex = "000001";
        }

        //method
        public void Get_product_info() {
            if (MyGlobal.Products != null) {
                if (MyGlobal.MySetting.ProductName == null) return;

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
            }
        }

        string _production_year; //production year
        public string ProductionYear {
            get { return _production_year; }
            set {
                _production_year = value;
                OnPropertyChanged(nameof(ProductionYear));
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
                LampBaudRate = value;
                OnPropertyChanged(nameof(SerialBaudRate));
            }
        }

        string _serial_data_bits; //data bits
        public string SerialDataBits {
            get { return _serial_data_bits; }
            set {
                _serial_data_bits = value;
                LampDataBits = value;
                OnPropertyChanged(nameof(SerialDataBits));
            }
        }

        string _serial_parity; //parity
        public string SerialParity {
            get { return _serial_parity; }
            set {
                _serial_parity = value;
                LampParity = value;
                OnPropertyChanged(nameof(SerialParity));
            }
        }

        string _serial_stop_bits; //stop bits
        public string SerialStopBits {
            get { return _serial_stop_bits; }
            set {
                _serial_stop_bits = value;
                LampStopBits = value;
                OnPropertyChanged(nameof(SerialStopBits));
            }
        }

        string _wait_time; //connection timeout
        public string WaitTime {
            get { return _wait_time; }
            set {
                _wait_time = value;
                OnPropertyChanged(nameof(WaitTime));
            }
        }

        #endregion

        //6
        #region CAI_DAT_DEN_HIEN_THI #################################

        string _lamp_port_name; //serial port name
        public string LampPortName {
            get { return _lamp_port_name; }
            set {
                _lamp_port_name = value;
                OnPropertyChanged(nameof(LampPortName));
            }
        }

        string _lamp_baud_rate; //baud rate
        public string LampBaudRate {
            get { return _lamp_baud_rate; }
            set {
                _lamp_baud_rate = value;
                OnPropertyChanged(nameof(LampBaudRate));
            }
        }

        string _lamp_data_bits; //data bits
        public string LampDataBits {
            get { return _lamp_data_bits; }
            set {
                _lamp_data_bits = value;
                OnPropertyChanged(nameof(LampDataBits));
            }
        }

        string _lamp_parity; //parity
        public string LampParity {
            get { return _lamp_parity; }
            set {
                _lamp_parity = value;
                OnPropertyChanged(nameof(LampParity));
            }
        }

        string _lamp_stop_bits; //stop bits
        public string LampStopBits {
            get { return _lamp_stop_bits; }
            set {
                _lamp_stop_bits = value;
                OnPropertyChanged(nameof(LampStopBits));
            }
        }

        #endregion



        //7
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

        string _save_log_no_meaning; //Save log no meaning
        public string SaveLogNoMeaning {
            get { return _save_log_no_meaning; }
            set {
                _save_log_no_meaning = value;
                OnPropertyChanged(nameof(SaveLogNoMeaning));
            }
        }

        bool _is_save_log_format; //Is save log format
        public bool IsSaveLogFormat {
            get { return _is_save_log_format; }
            set {
                _is_save_log_format = value;
                OnPropertyChanged(nameof(IsSaveLogFormat));
            }
        }

        bool _is_save_log_printed; //Is save log printed
        public bool IsSaveLogPrinted {
            get { return _is_save_log_printed; }
            set {
                _is_save_log_printed = value;
                OnPropertyChanged(nameof(IsSaveLogPrinted));
            }
        }

        bool _is_save_log_weight; //Is save log weight
        public bool IsSaveLogWeight {
            get { return _is_save_log_weight; }
            set {
                _is_save_log_weight = value;
                OnPropertyChanged(nameof(IsSaveLogWeight));
            }
        }

        string _duplicate_quantity; //Duplicate Quantity
        public string DuplicateQuantity {
            get { return _duplicate_quantity; }
            set {
                _duplicate_quantity = value;
                OnPropertyChanged(nameof(DuplicateQuantity));
            }
        }


        #endregion

        //8
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

        //9
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

        //10
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
        string _production_status;
        public string ProductionStatus {
            get { return _production_status; }
            set {
                _production_status = value;
                OnPropertyChanged(nameof(ProductionStatus));
            }
        }
        string _lot_index;
        public string LOTIndex {
            get { return _lot_index; }
            set {
                _lot_index = value;
                OnPropertyChanged(nameof(LOTIndex));
            }
        }

        #endregion

    }
}
