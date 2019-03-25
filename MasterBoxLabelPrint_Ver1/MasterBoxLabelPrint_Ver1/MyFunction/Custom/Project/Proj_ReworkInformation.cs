using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom
{
    public class Proj_ReworkInformation : MasterBoxLabelPrint_Ver1.MyFunction.Ulti.zCNotifyPropertyChanged {
        public Proj_ReworkInformation() {
            AutoPrintLabel = true;
            PrintMode = MyGlobal.MySetting.PrintMode;
        }

        public void WaitingParameters() {
            WeightActual = "";
            ReworkResult = "Waiting...";
            ErrorMessage = "";
        }
        public void FailParameters() {
            ReworkResult = "FAIL";
            //ProductSerial = "";
        }
        public void PassParameters() {
            ReworkResult = "PASS";
            //ProductSerial = "";
        }


        public string ProductionLot { get; set; }
        public string LotProgress { get; set; }
        public string ReworkSerial { get; set; }
        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public string WeightLimit { get; set; }
        public string Operator { get; set; }
        public string Reason { get; set; }
        public string PrintMode { get; set; }
        public bool AutoPrintLabel { get; set; }
        public string Line { get; set; }
        public string LotQuantity { get; set; }

        string _new_product_serial;
        public string NewProductSerial {
            get { return _new_product_serial; }
            set {
                _new_product_serial = value;
                OnPropertyChanged(nameof(NewProductSerial));
            }
        }
        string _rework_result;
        public string ReworkResult {
            get { return _rework_result; }
            set {
                _rework_result = value;
                OnPropertyChanged(nameof(ReworkResult));
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
        string _weight_actual;
        public string WeightActual {
            get { return _weight_actual; }
            set {
                _weight_actual = value;
                OnPropertyChanged(nameof(WeightActual));
            }
        }
    }
}
