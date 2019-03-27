using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;
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
            bool r = false;

            try {
                //init log
                MyGlobal.testFunctionLogInfo = new IO.VnptAsmTestFunctionLogInfo() {
                    Rework = string.Format("small:{0}", reworkInformation.ReworkSerial),
                    Production_Command = "",
                    ProductionLot = reworkInformation.ProductionLot,
                    LotProgress = reworkInformation.LotProgress,
                };

                reworkInformation.ErrorMessage = string.Format("Product: \"{0}\"\n", reworkInformation.NewProductSerial);

                //validate serial number
                if (!_validate_product_serialnumber()) goto END;

                //check serial printed or not
                if (!_serial_was_printed()) goto END;

                //check product weight
                if (!_product_weight_is_valid()) goto END;

                r = true;
                goto END;
            }
            catch {
                goto END;
            }

        END:
            new LogTotal().To_CSV_File(MyGlobal.testFunctionLogInfo, new VnptLogMoreInfo());
            return r;
        }


        //validate product serial number
        bool _validate_product_serialnumber() {
            bool r = false;
            MyGlobal.testFunctionLogInfo.Product_Serial = reworkInformation.NewProductSerial;
            ValidateProductSerialNumber validate = new ValidateProductSerialNumber(reworkInformation.NewProductSerial, this.product);
            r = validate.Validate();
            if (!r) reworkInformation.ErrorMessage += validate.Validate_Error_Message;
            MyGlobal.testFunctionLogInfo.SerialFormat.Result = r ? "PASS" : "FAIL";
            MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
            return r;
        }

        //check serial printed or not
        bool _serial_was_printed() {
            bool r = false;
            msaccdb_tbDataProductionLOT tb = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", "ProductSerial", reworkInformation.NewProductSerial);
            r = tb == null;
            if (!r) reworkInformation.ErrorMessage += string.Format("Serial Number was printed in lot {0}, date printed {1}.", tb.Lot, tb.DateTimeCreated);
            MyGlobal.testFunctionLogInfo.SerialPrinted.Result = r ? "PASS" : "FAIL";
            MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
            return r;
        }

        //check product weight
        bool _product_weight_is_valid() {
            bool r = false;

            if (reworkInformation.PrintMode == "Combine label printing with product weighing") {
                int count = 0;
                double ul = double.Parse(product.weight) + double.Parse(product.bias);
                double ll = double.Parse(product.weight) - double.Parse(product.bias);
                MyGlobal.testFunctionLogInfo.ProductWeight.Upper_Limit = ul.ToString();
                MyGlobal.testFunctionLogInfo.ProductWeight.Lower_Limit = ll.ToString();
                MyGlobal.testFunctionLogInfo.ProductWeight.Unit_Of_Measurement = "g";

            REP:
                count++;
                string weight_string = CAS_EDH.GetWeight();

                if (weight_string == null) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight can't is NULL.", weight_string);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = "NULL";
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
                        return false;
                    }
                }

                double weight_value;
                if (!double.TryParse(weight_string, out weight_value)) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight {0} is not valid.", weight_string);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = weight_string;
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
                        return false;
                    }
                }

                reworkInformation.WeightActual = weight_value.ToString();
                MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = reworkInformation.WeightActual;
                r = weight_value >= ll && weight_value <= ul;

                if (!r) {
                    if (count < 5) goto REP;
                    else {
                        reworkInformation.ErrorMessage += string.Format("Product weight {0} is out of range {1}.", weight_string, reworkInformation.WeightLimit);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
                        return false;
                    }
                }
                else {
                    MyGlobal.testFunctionLogInfo.ProductWeight.Result = "PASS";
                    MyGlobal.testFunctionLogInfo.Error_Message = reworkInformation.ErrorMessage;
                    return true;
                }
            }
            else return true;
        }

    }
}
