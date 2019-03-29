﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.Scale;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Implement {
    public class imp_RunAll {


        public bool Execute() {
            bool r = false;

            try {
                //init log
                MyGlobal.testFunctionLogInfo = new IO.VnptAsmTestFunctionLogInfo() {
                    Rework = "-",
                    Production_Command = MyGlobal.MySetting.ProductionCommand,
                    ProductionLot = MyGlobal.MyTesting.LotName,
                    LotProgress = MyGlobal.MyTesting.LotProgress,
                };

                MyGlobal.MyTesting.ErrorMessage = string.Format("Product: \"{0}\"\n", MyGlobal.MyTesting.ProductSerial);

                //validate serial number
                if (!_validate_product_serialnumber()) goto END;

                //check serial printed or not
                if (!_serial_was_printed()) goto END;

                //check product weight
                if (!_product_weight_is_valid()) goto END;

                r = true;
                goto END;

            } catch {
                goto END;
            }

            
        END:
            new LogTotal().To_CSV_File(MyGlobal.testFunctionLogInfo, new VnptLogMoreInfo());
            return r;
        }


        //validate product serial number
        bool _validate_product_serialnumber() {
            bool r = false;
            MyGlobal.testFunctionLogInfo.Product_Serial = MyGlobal.MyTesting.ProductSerial;
            ValidateProductSerialNumber validate = new ValidateProductSerialNumber(MyGlobal.MyTesting.ProductSerial);
            r = validate.Validate();
            if (!r) MyGlobal.MyTesting.ErrorMessage += validate.Validate_Error_Message;
            MyGlobal.testFunctionLogInfo.SerialFormat.Result = r ? "PASS" : "FAIL";
            MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;

            return r;
        }

        //check serial printed or not
        bool _serial_was_printed() {
            bool r = false;
            msaccdb_tbDataProductionLOT tb = MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", "ProductSerial", MyGlobal.MyTesting.ProductSerial);
            r = tb == null;
            if (!r) MyGlobal.MyTesting.ErrorMessage += string.Format("Serial Number was printed in lot {0}, date printed {1}.", tb.Lot, tb.DateTimeCreated);
            MyGlobal.testFunctionLogInfo.SerialPrinted.Result = r ? "PASS" : "FAIL";
            MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;
            return r;
        }

        //check product weight
        bool _product_weight_is_valid() {
            bool r = false;

            if (MyGlobal.MyTesting.UseScaleFlag == true) {
                int count = 0;
                double ul = double.Parse(MyGlobal.MySetting.WeightUL);
                double ll = double.Parse(MyGlobal.MySetting.WeightLL);
                MyGlobal.testFunctionLogInfo.ProductWeight.Upper_Limit = MyGlobal.MySetting.WeightUL;
                MyGlobal.testFunctionLogInfo.ProductWeight.Lower_Limit = MyGlobal.MySetting.WeightLL;
                MyGlobal.testFunctionLogInfo.ProductWeight.Unit_Of_Measurement = "g";

            REP:
                count++;
                string weight_string = CAS_EDH.GetWeight();

                if (weight_string == null) {
                    if (count < 5) goto REP;
                    else {
                        MyGlobal.MyTesting.ErrorMessage += string.Format("Product weight can't is NULL.", weight_string);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = "NULL";
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;
                        return false;
                    }
                }

                double weight_value;
                if (!double.TryParse(weight_string, out weight_value)) {
                    if (count < 5) goto REP;
                    else {
                        MyGlobal.MyTesting.ErrorMessage += string.Format("Product weight {0} is not valid.", weight_string);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = weight_string;
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;
                        return false;
                    }
                }

                MyGlobal.MyTesting.WeightActual = weight_value.ToString();
                MyGlobal.testFunctionLogInfo.ProductWeight.Actual_Value = MyGlobal.MyTesting.WeightActual;
                r = weight_value >= ll && weight_value <= ul;

                if (!r) {
                    if (count < 5) goto REP;
                    else {
                        MyGlobal.MyTesting.ErrorMessage += string.Format("Product weight {0} is out of range {1}.", weight_string, MyGlobal.MyTesting.WeightStandard);
                        MyGlobal.testFunctionLogInfo.ProductWeight.Result = "FAIL";
                        MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;
                        return false;
                    }
                }
                else {
                    MyGlobal.testFunctionLogInfo.ProductWeight.Result = "PASS";
                    MyGlobal.testFunctionLogInfo.Error_Message = MyGlobal.MyTesting.ErrorMessage;
                    return true;
                }
            }
            else return true;
        }





    }
}
