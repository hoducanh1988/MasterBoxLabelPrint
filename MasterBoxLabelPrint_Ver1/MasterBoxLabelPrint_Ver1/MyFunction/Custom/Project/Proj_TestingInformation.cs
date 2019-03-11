using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom {
    public class Proj_TestingInformation : MasterBoxLabelPrint_Ver1.MyFunction.Ulti.zCNotifyPropertyChanged {
        //constructor -----------------------------------//
        public Proj_TestingInformation() {
            InitParameters();
        }

        //method ----------------------------------------//
        public void InitParameters() {
            ProductSerial = "";
            WeightActual = "";
            TotalResult = "";
            ErrorMessage = "";
            ErrorCode = "";
        }
        public void WaitingParameters() {
            WeightActual = "";
            TotalResult = "Waiting...";
            ErrorMessage = "";
            ErrorCode = "";
        }
        public void FailParameters() {
            TotalResult = "FAIL";
            //ProductSerial = "";
        }
        public void PassParameters() {
            TotalResult = "PASS";
            //ProductSerial = "";
        }

        //property --------------------------------------//
        string _product_serial;
        public string ProductSerial {
            get { return _product_serial; }
            set {
                _product_serial = value;
                OnPropertyChanged(nameof(ProductSerial));
            }
        }
        string _product_name;
        public string ProductName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        string _lot_name;
        public string LotName {
            get { return _lot_name; }
            set {
                _lot_name = value;
                OnPropertyChanged(nameof(LotName));
            }
        }
        string _lot_count;
        public string LotCount {
            get { return _lot_count; }
            set {
                _lot_count = value;
                OnPropertyChanged(nameof(LotCount));
                LotProgress = string.Format("{0}/{1}", LotCount, LotLimit);
            }
        }
        string _lot_limit;
        public string LotLimit {
            get { return _lot_limit; }
            set {
                _lot_limit = value;
                OnPropertyChanged(nameof(LotLimit));
                LotProgress = string.Format("{0}/{1}", LotCount, LotLimit);
            }
        }
        string _lot_progress;
        public string LotProgress {
            get { return _lot_progress; }
            set {
                _lot_progress = value;
                OnPropertyChanged(nameof(LotProgress));
            }
        }
        string _weight_standard;
        public string WeightStandard {
            get { return _weight_standard; }
            set {
                _weight_standard = value;
                OnPropertyChanged(nameof(WeightStandard));
            }
        }
        string _weight_actual;
        public string WeightActual {
            get { return _weight_actual; }
            set {
                _weight_actual = value;
                OnPropertyChanged(nameof(WeightActual));
            }
        }
        string _total_result;
        public string TotalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(TotalResult));
            }
        }
        string _error_message;
        public string ErrorMessage {
            get { return _error_message; }
            set {
                _error_message = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        string _error_code;
        public string ErrorCode {
            get { return _error_code; }
            set {
                _error_code = value;
                OnPropertyChanged(nameof(ErrorCode));
            }
        }
        bool _use_scale; //use scale flag
        public bool UseScaleFlag {
            get { return _use_scale; }
            set {
                _use_scale = value;
                OnPropertyChanged(nameof(UseScaleFlag));
            }
        }
    }
}
