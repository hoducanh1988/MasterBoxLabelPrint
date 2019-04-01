using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {

    public abstract class AbstractLogger {

        protected string dir_Jig_Index = ""; //log
        
        public AbstractLogger() {
            bool r = string.IsNullOrEmpty(MyGlobal.MySetting.DirLog) || MyGlobal.MySetting.DirLog.ToLower().Contains("default");
            string _dir = r == true ? AppDomain.CurrentDomain.BaseDirectory : MyGlobal.MySetting.DirLog;

            //Create RootLogDirectory folder
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            //Create ProductName Folder
            _dir = Path.Combine(_dir, MyGlobal.MySetting.ProductName);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            //Create StationName Folder
            _dir = Path.Combine(_dir, MyGlobal.MySetting.StationName);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            //Create StationIndex Folder
            _dir = Path.Combine(_dir, MyGlobal.MySetting.StationIndex);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            //Create JigIndex Folder
            _dir = Path.Combine(_dir, MyGlobal.MySetting.JigIndex);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            dir_Jig_Index = _dir;
        }
    }


    public class VnptAsmTestFunctionLogInfo {

        public VnptAsmTestFunctionLogInfo() {
            Software_Version = "1.0.0.0";
            Machine_Name = System.Environment.MachineName;
            Operator = MyGlobal.MySetting.Operator;

            Date_Time = Mac_Address = Product_Serial = Total_Result = Error_Message = System_Log = "";

            SerialFormat = new VnptTestItemInfo(); //ok

            SerialPrinted = new VnptTestItemInfo(); //ok

            ProductWeight = new VnptTestItemInfo(); //ok
        }

        public string Date_Time { get; set; }
        public string Machine_Name { get; set; }
        public string Software_Version { get; set; }
        public string Mac_Address { get; set; }
        public string Product_Serial { get; set; }
        public string Operator { get; set; }
        public string Total_Result { get; set; }
        public string Error_Message { get; set; }
        public string System_Log { get; set; }
        public string Production_Command { get; set; }
        public string Rework { get; set; }
        public string ProductionLot { get; set; }
        public string LotProgress { get; set; }

        public VnptTestItemInfo SerialFormat { get; set; }
        public VnptTestItemInfo SerialPrinted { get; set; }
        public VnptTestItemInfo ProductWeight { get; set; }
    }

    public class VnptTestItemInfo {
        public VnptTestItemInfo() {
            Lower_Limit = Upper_Limit = Actual_Value = Unit_Of_Measurement = Result = "NONE";
        }

        public string Lower_Limit { get; set; }
        public string Upper_Limit { get; set; }
        public string Actual_Value { get; set; }
        public string Unit_Of_Measurement { get; set; }
        public string Result { get; set; }

    }


    public class VnptLogMoreInfo {

        public VnptLogMoreInfo() {
            Info1 = Info2 = Info3 = Info4 = Info5 = Info6 = Info7 = Info8 = Info9 = Info10 = "";
        }

        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public string Info6 { get; set; }
        public string Info7 { get; set; }
        public string Info8 { get; set; }
        public string Info9 { get; set; }
        public string Info10 { get; set; }
    }



}
