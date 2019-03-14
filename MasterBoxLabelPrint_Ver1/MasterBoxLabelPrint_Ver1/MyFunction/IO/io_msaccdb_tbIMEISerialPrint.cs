using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {

    public class io_msaccdb_tbIMEISerialPrint {
        msaccdb_IMEISerialPrint tbIMEISerialPrint = null;

        public io_msaccdb_tbIMEISerialPrint() {

            var testing = MyGlobal.MyTesting;
            var setting = MyGlobal.MySetting;
            this.tbIMEISerialPrint = new msaccdb_IMEISerialPrint();

            tbIMEISerialPrint.IMEI = "";
            tbIMEISerialPrint.LoSX = testing.LotName;
            tbIMEISerialPrint.NoLine = setting.ProductCode;
            tbIMEISerialPrint.NoLine1 = setting.LineIndex;
            tbIMEISerialPrint.Colour = setting.ColorCode;
            tbIMEISerialPrint.SN = testing.ProductSerial;
            tbIMEISerialPrint.TimeScan = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteData() {
            try {
                var box = MyGlobal.MasterBox;
                return box.Input_New_DataRow_To_Access_DB_Table<msaccdb_IMEISerialPrint>("IMEI_SN_Print", this.tbIMEISerialPrint);
            }
            catch {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll() {
            try {
                var box = MyGlobal.MasterBox;
                return box.Delete_All_DataRow_From_Access_DB_Table("IMEI_SN_Print");
            } catch {
                return false;
            }
        }


    }
}
