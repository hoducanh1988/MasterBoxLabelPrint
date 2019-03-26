using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class ValidateProductSerialNumber {
        class validate_item {
            public string ActionName { get; set; }
            public string ActualValue { get; set; }
            public string StandardValue { get; set; }
        }

        public string Validate_Error_Message = "";
        public string Validate_Error_Code = "";
        static string validate_product_serialnumber;

        //danh sach hang muc can validate
        List<validate_item> ValidateItems = new List<validate_item>();


        //Construtor ------------------------------------------//
        //normal production
        public ValidateProductSerialNumber(string sn) {
            validate_product_serialnumber = sn;
            ValidateItems.Clear();
            ValidateItems.Add(new validate_item() { ActionName = "Invalid serial number length", ActualValue = validate_product_serialnumber.Length.ToString(), StandardValue = "15" });
            ValidateItems.Add(new validate_item() { ActionName = "Product code is not correct", ActualValue = _get_value_from_serial(0, 3), StandardValue = MyGlobal.MySetting.ProductNumber });
            ValidateItems.Add(new validate_item() { ActionName = "The place of production is not correct", ActualValue = _get_value_from_serial(3, 1), StandardValue = MyGlobal.MySetting.ProductionPlace });
            ValidateItems.Add(new validate_item() { ActionName = "Product version is not correct", ActualValue = _get_value_from_serial(7, 1), StandardValue = MyGlobal.MySetting.ProductVersion });
            ValidateItems.Add(new validate_item() { ActionName = "Color code is not correct", ActualValue = _get_value_from_serial(8, 1), StandardValue = MyGlobal.MySetting.ColorCode });
            ValidateItems.Add(new validate_item() { ActionName = "Year of manufacture is not correct", ActualValue = _get_value_from_serial(4, 1), StandardValue = _convert_production_year() });
            ValidateItems.Add(new validate_item() { ActionName = "The production week is not correct", ActualValue = _get_value_from_serial(5, 2), StandardValue = "01~52" });
            ValidateItems.Add(new validate_item() { ActionName = "The last six characters of the serial number are not hexadecimal", ActualValue = _get_value_from_serial(9, 6), StandardValue = "^[0-9,A-F]{6}$" });
        }

        //bulk rework
        public ValidateProductSerialNumber(string sn_bulk_rework, string dummy) {
            validate_product_serialnumber = sn_bulk_rework;
            ValidateItems.Clear();
            ValidateItems.Add(new validate_item() { ActionName = "Invalid serial number length", ActualValue = validate_product_serialnumber.Length.ToString(), StandardValue = "15" });
            ValidateItems.Add(new validate_item() { ActionName = "Product code is not correct", ActualValue = _get_value_from_serial(0, 3), StandardValue = MyGlobal.MySetting.ProductNumber });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: The place of production is not correct", ActualValue = _get_value_from_serial(3, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: Product version is not correct", ActualValue = _get_value_from_serial(7, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "Color code is not correct", ActualValue = _get_value_from_serial(8, 1), StandardValue = MyGlobal.MySetting.ColorCode });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: Year of manufacture is not correct", ActualValue = _get_value_from_serial(4, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "The production week is not correct", ActualValue = _get_value_from_serial(5, 2), StandardValue = "01~52" });
            ValidateItems.Add(new validate_item() { ActionName = "The last six characters of the serial number are not hexadecimal", ActualValue = _get_value_from_serial(9, 6), StandardValue = "^[0-9,A-F]{6}$" });
        }

        //small rework
        public ValidateProductSerialNumber(string sn_rework, Init_Product productInfo) {
            validate_product_serialnumber = sn_rework;
            ValidateItems.Clear();
            ValidateItems.Add(new validate_item() { ActionName = "Invalid serial number length", ActualValue = validate_product_serialnumber.Length.ToString(), StandardValue = "15" });
            ValidateItems.Add(new validate_item() { ActionName = "Product code is not correct", ActualValue = _get_value_from_serial(0, 3), StandardValue = productInfo.number });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: The place of production is not correct", ActualValue = _get_value_from_serial(3, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: Product version is not correct", ActualValue = _get_value_from_serial(7, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "Color code is not correct", ActualValue = _get_value_from_serial(8, 1), StandardValue = productInfo.color });
            ValidateItems.Add(new validate_item() { ActionName = "Rework: Year of manufacture is not correct", ActualValue = _get_value_from_serial(4, 1), StandardValue = "" });
            ValidateItems.Add(new validate_item() { ActionName = "The production week is not correct", ActualValue = _get_value_from_serial(5, 2), StandardValue = "01~52" });
            ValidateItems.Add(new validate_item() { ActionName = "The last six characters of the serial number are not hexadecimal", ActualValue = _get_value_from_serial(9, 6), StandardValue = "^[0-9,A-F]{6}$" });
        }


        //get actual value
        string _get_value_from_serial(int start, int len) {
            try {
                return validate_product_serialnumber.Substring(start, len);
            }
            catch {
                return "";
            }
        }

        //convert production year from number to alphabet
        string _convert_production_year() {
            try {
                int _bias = 13; //<=>2013
                int _year = int.Parse(MyGlobal.MySetting.ProductionYear); //19,20,21...
                int _diff_year = _year - _bias;

                return _diff_year < 10 ? _diff_year.ToString() : Char.ConvertFromUtf32(55 + _diff_year);
            } catch {
                return "";
            }
        }

        //generic method --------------------------------------//
        int _is_value_validate_same_setting(validate_item validate_Item) {
            int r = 1;
            try {
                if (validate_Item.ActionName.ToLower().Equals("the production week is not correct")) {
                    int value = int.Parse(validate_Item.ActualValue);
                    r = (value < 1 || value > 52) == true ? 1 : 0;
                }
                else if (validate_Item.ActionName.ToLower().Equals("the last six characters of the serial number are not hexadecimal")) {
                    r = (Regex.IsMatch(validate_Item.ActualValue, validate_Item.StandardValue) == true) ? 0 : 1;
                }
                else if (validate_Item.ActionName.ToLower().Equals("rework: the place of production is not correct") || validate_Item.ActionName.ToLower().Equals("rework: product version is not correct")) {
                    int value;
                    bool kq = int.TryParse(validate_Item.ActualValue, out value);
                    if (kq == false) return 1;
                    return r = value > 0 ? 0 : 1;
                }
                else if (validate_Item.ActionName.ToLower().Equals("rework: year of manufacture is not correct")) {
                    bool kq = Regex.IsMatch(validate_Item.ActualValue, "[0-9,A-Z]");
                    return r = kq == true ? 0 : 1;
                }
                else {
                    r = validate_Item.ActualValue.ToLower().Equals(validate_Item.StandardValue.ToLower()) == true ? 0 : 1;
                }
            }
            catch { }
            Validate_Error_Message += r == 1 ? string.Format("{0}. Actual value is \"{1}\",  Standard or setting value is \"{2}\".", validate_Item.ActionName, validate_Item.ActualValue, validate_Item.StandardValue) : "";
            return r;
        }

        //validate product SN
        public bool Validate() {
            foreach (var item in ValidateItems) {
                if (_is_value_validate_same_setting(item) != 0) return false;
            }
            return true;
        }

    }

}
