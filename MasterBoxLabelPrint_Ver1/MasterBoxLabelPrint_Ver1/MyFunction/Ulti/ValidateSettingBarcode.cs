using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class ValidateSettingBarcode {

        class validate_item {
            public string ItemName { get; set; }
            public string CaseCode { get; set; }
            public string ItemCode { get; set; }
            public string ItemValue { get; set; }
        }

        public string Validate_Error_Message = "";
        public string Validate_Error_Code = "";
        string Barcode="";


        List<validate_item> ValidateItems = new List<validate_item>() {
            //CÀI ĐẶT SẢN XUẤT
            new validate_item(){ ItemName = "ProductionPlace", CaseCode = "PD", ItemCode = "FAC", ItemValue = "" },
            new validate_item(){ ItemName = "ProductionYear", CaseCode = "PD", ItemCode = "YEA", ItemValue = "" },
            new validate_item(){ ItemName = "ProductionCommand", CaseCode = "PD", ItemCode = "PDO", ItemValue = "" },
            new validate_item(){ ItemName = "PrintMode", CaseCode = "PD", ItemCode = "PLM", ItemValue = "" },
            
            //CÀI ĐẶT TRẠM IN TEM
            new validate_item(){ ItemName = "LineIndex", CaseCode = "ST", ItemCode = "LIN", ItemValue = "" },
            new validate_item(){ ItemName = "StationName", CaseCode = "ST", ItemCode = "SNA", ItemValue = "" },
            new validate_item(){ ItemName = "StationIndex", CaseCode = "ST", ItemCode = "SOD", ItemValue = "" },
            new validate_item(){ ItemName = "JigIndex", CaseCode = "ST", ItemCode = "SJI", ItemValue = "" },
            new validate_item(){ ItemName = "Operator", CaseCode = "ST", ItemCode = "SOP", ItemValue = "" },

            //CÀI ĐẶT SẢN PHẨM
            new validate_item(){ ItemName = "FileProduct", CaseCode = "PT", ItemCode = "FLP", ItemValue = "" },
            new validate_item(){ ItemName = "ProductName", CaseCode = "PT", ItemCode = "PNA", ItemValue = "" },
            new validate_item(){ ItemName = "ProductVersion", CaseCode = "PT", ItemCode = "PVE", ItemValue = "" },

            //CÀI ĐẶT MÁY IN NHÃN
            new validate_item(){ ItemName = "PrintPage", CaseCode = "PL", ItemCode = "LQT", ItemValue = "" },
            new validate_item(){ ItemName = "MSAccessFile", CaseCode = "PL", ItemCode = "FLA", ItemValue = "" },

            //CÀI ĐẶT CÂN TRỌNG LƯỢNG
            new validate_item(){ ItemName = "SerialPortName", CaseCode = "SC", ItemCode = "SRN", ItemValue = "" },
            new validate_item(){ ItemName = "SerialBaudRate", CaseCode = "SC", ItemCode = "SRB", ItemValue = "" },
            new validate_item(){ ItemName = "SerialDataBits", CaseCode = "SC", ItemCode = "SRD", ItemValue = "" },
            new validate_item(){ ItemName = "SerialParity", CaseCode = "SC", ItemCode = "SRP", ItemValue = "" },
            new validate_item(){ ItemName = "SerialStopBits", CaseCode = "SC", ItemCode = "SRS", ItemValue = "" },
            new validate_item(){ ItemName = "WaitTime", CaseCode = "SC", ItemCode = "TST", ItemValue = "" },

            //CÀI ĐẶT ĐÈN HIỂN THỊ
            new validate_item(){ ItemName = "LampPortName", CaseCode = "LA", ItemCode = "SRN", ItemValue = "" },
            new validate_item(){ ItemName = "LampBaudRate", CaseCode = "LA", ItemCode = "SRB", ItemValue = "" },
            new validate_item(){ ItemName = "LampDataBits", CaseCode = "LA", ItemCode = "SRD", ItemValue = "" },
            new validate_item(){ ItemName = "LampParity", CaseCode = "LA", ItemCode = "SRP", ItemValue = "" },
            new validate_item(){ ItemName = "LampStopBits", CaseCode = "LA", ItemCode = "SRS", ItemValue = "" },

            //CÀI ĐẶT LƯU LOG
            new validate_item(){ ItemName = "DirLog", CaseCode = "LO", ItemCode = "DRP", ItemValue = "" },
            new validate_item(){ ItemName = "VisibleLogQuantity", CaseCode = "LO", ItemCode = "LOQ", ItemValue = "" },

            //CÀI ĐẶT SOP
            new validate_item(){ ItemName = "SOPServer", CaseCode = "SO", ItemCode = "SPS", ItemValue = "" },
        };


        public ValidateSettingBarcode(string _barcode) {
            this.Barcode = _barcode;
        }

        //check format barcode
        bool _barcode_is_true(ref validate_item item) {
            try {
                string[] buffer = this.Barcode.Split(new string[] { "::" }, StringSplitOptions.None);
                bool r = buffer.Length == 3;
                if (!r) {
                    Validate_Error_Message += "Barcode is incorrect.";
                    return false;
                }

                item.CaseCode = buffer[0];
                item.ItemCode = buffer[1];
                string _itemvalue = buffer[2];

                r = false;
                foreach (var it in ValidateItems) {
                    if (it.CaseCode.Equals(item.CaseCode) && it.ItemCode.Equals(item.ItemCode)) {
                        item.ItemName = it.ItemName;
                        if (item.ItemName == "PrintMode") {
                            item.ItemValue = _itemvalue.Equals("0") ? "Only print label" : "Combine label printing with product weighing";
                        }
                        else {
                            item.ItemValue = _itemvalue;
                        }
                        r = true;
                        break;
                    } 
                }

                if (!r) {
                    Validate_Error_Message += "Barcode is incorrect.";
                    return false;
                }

                Validate_Error_Message += string.Format("Setting Item: {0}, Value: {1}", item.ItemName, item.ItemValue);
                return r;
            }
            catch {
                Validate_Error_Message += "Barcode is incorrect.";
                return false;
            }
        }

        //validate product SN
        public bool Validate() {

            //validate item 
            validate_item _Item = new validate_item();
            if (!_barcode_is_true(ref _Item)) return false;

            //set setting value
            PropertyInfo propertyInfo = MyGlobal.MySetting.GetType().GetProperty(_Item.ItemName);
            propertyInfo.SetValue(MyGlobal.MySetting, Convert.ChangeType(_Item.ItemValue, propertyInfo.PropertyType), null);

            return true;
        }
    }
}
