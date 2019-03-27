using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {
    public class io_msaccdb_tbDataLog {
        msaccdb_tbDataLog tbDataLog = null;
        

        /// <summary>
        /// 
        /// </summary>
        public io_msaccdb_tbDataLog() {
            var testing = MyGlobal.MyTesting;
            var setting = MyGlobal.MySetting;
            
            this.tbDataLog = new msaccdb_tbDataLog();

            tbDataLog.Color = setting.ColorCode;
            tbDataLog.DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            tbDataLog.ErrorCode = testing.ErrorCode;
            tbDataLog.ErrorMessage = testing.ErrorMessage;
            tbDataLog.Factory = setting.ProductionPlace;
            tbDataLog.JigIndex = setting.JigIndex;
            tbDataLog.Line = setting.LineIndex;
            tbDataLog.Lot = testing.LotName;
            tbDataLog.LotProgress = testing.LotProgress;
            tbDataLog.Operator = setting.Operator;
            tbDataLog.ProductCode = setting.ProductCode;
            tbDataLog.ProductNumber = setting.ProductNumber;
            tbDataLog.ProductionCommand = setting.ProductionCommand;
            tbDataLog.ProductName = setting.ProductName;
            tbDataLog.ProductSerial = testing.ProductSerial;
            tbDataLog.Station = setting.StationName;
            tbDataLog.StationIndex = setting.StationIndex;
            tbDataLog.TotalResult = testing.TotalResult;
            tbDataLog.WeightAct = testing.WeightActual;
            tbDataLog.WeightLower = setting.WeightLL;
            tbDataLog.WeightUpper = setting.WeightUL;
            tbDataLog.Rework = "-";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteData() {
            try {
                var box = MyGlobal.MasterBox;
                return box.Input_New_DataRow_To_Access_DB_Table<msaccdb_tbDataLog>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataLog" : "tb_DataLog_Bulk", this.tbDataLog, "tb_ID");
            } catch {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_tbDataLog> ReadData() {
            return MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataLog>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataLog" : "tb_DataLog_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", "", "TotalResult", "", "Lot", "");
        }
        
    }
}
