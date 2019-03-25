using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Scale;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Implement
{
    public class imp_Rework
    {
        Proj_ReworkInformation reworkInformation;
        Init_Product product;

        public imp_Rework(Proj_ReworkInformation _ReworkInformation, Init_Product _Product) {
            this.reworkInformation = _ReworkInformation;
            this.product = _Product;
        }

        public bool Execute() {
            try {
                reworkInformation.ErrorMessage = string.Format("Product: \"{0}\"\n", reworkInformation.NewProductSerial);

                //validate serial number
                if (!_validate_product_serialnumber()) return false;

                //check serial printed or not
                if (!_serial_was_printed()) return false;

                //check product weight
                if (!_product_weight_is_valid()) return false;

                return true;
            }
            catch {
                return false;
            }
        }


        //validate product serial number
        bool _validate_product_serialnumber() {
            bool r = false;
            ValidateProductSerialNumber validate = new ValidateProductSerialNumber(reworkInformation.NewProductSerial, this.product);
            r = validate.Validate();
            if (!r) reworkInformation.ErrorMessage += validate.Validate_Error_Message;
            return r;
        }

        //check serial printed or not
        bool _serial_was_printed() {
            bool r = false;
            msaccdb_tbDataProductionLOT tb = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", "ProductSerial", reworkInformation.NewProductSerial);
            r = tb == null;
            if (!r) reworkInformation.ErrorMessage += string.Format("Serial Number was printed in lot {0}, date printed {1}.", tb.Lot, tb.DateTimeCreated);
            return r;
        }

        //check product weight
        bool _product_weight_is_valid() {
            bool r = false;

            if (reworkInformation.PrintMode == "Combine label printing with product weighing") {
                int count = 0;
                double ul = double.Parse(product.weight) + double.Parse(product.bias);
                double ll = double.Parse(product.weight) - double.Parse(product.bias);

            REP:
                count++;
                string weight_string = CAS_EDH.GetWeight();

                if (weight_string == null) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight can't is NULL.", weight_string);
                        return false;
                    }
                }

                double weight_value;
                if (!double.TryParse(weight_string, out weight_value)) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight {0} is not valid.", weight_string);
                        return false;
                    }
                }

                reworkInformation.WeightActual = weight_value.ToString();
                r = weight_value >= ll && weight_value <= ul;

                if (!r) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight {0} is out of range {1}.", weight_string, reworkInformation.WeightLimit);
                        return false;
                    }
                }
                else return true;
            }
            else return true;
        }

    }
}
