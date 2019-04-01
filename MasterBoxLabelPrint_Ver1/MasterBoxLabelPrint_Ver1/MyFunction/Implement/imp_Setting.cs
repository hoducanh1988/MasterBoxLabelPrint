using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterBoxLabelPrint_Ver1.MyFunction.Ulti;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Implement {
    public class imp_Setting {

        public bool Execute() {
            bool r = false;

            try {
                //init log

                MyGlobal.MyTesting.ErrorMessage = string.Format("Value: \"{0}\"\n", MyGlobal.MyTesting.ProductSerial);

                //validate setting value
                if (!_validate_setting_value()) goto END;

                r = true;
                goto END;

            }
            catch {
                goto END;
            }


        END:
            return r;
        }


        //validate product serial number
        bool _validate_setting_value() {
            bool r = false;
            var validate = new ValidateSettingBarcode(MyGlobal.MyTesting.ProductSerial);
            r = validate.Validate();
            MyGlobal.MyTesting.ErrorMessage += validate.Validate_Error_Message;
            MyGlobal.MyTesting.TotalResult = r ? "PASS" : "FAIL";
            return r;
        }

    }
}
