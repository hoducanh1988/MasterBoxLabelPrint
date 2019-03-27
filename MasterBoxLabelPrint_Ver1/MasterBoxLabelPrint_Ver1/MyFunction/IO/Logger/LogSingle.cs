using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {
    public class LogSingle : AbstractLogger {

        string _dir_Log_Single = "";
        string _file_Name = "";


        /// <summary>
        /// 
        /// </summary>
        public LogSingle() : base() {
            _dir_Log_Single = Path.Combine(base.dir_Jig_Index, "LogSingle");
            if (!Directory.Exists(_dir_Log_Single)) Directory.CreateDirectory(_dir_Log_Single);
            //get file name
            this._file_Name = string.Format("{0}_{1}_Station{2}_Jig{3}",
                MyGlobal.MySetting.ProductName,
                MyGlobal.MySetting.StationName,
                MyGlobal.MySetting.StationIndex,
                MyGlobal.MySetting.JigIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        public bool To_CSV_File(VnptAsmTestFunctionLogInfo logInfo) {
            try {
                string title = "Date_Time_Create,MacAddress,ProductCode,NhanVien,TestSubject,LowerLimit,UpperLimit,GiaTriDo,PhanDinh";
                logInfo.Mac_Address = logInfo.Mac_Address == null || logInfo.Mac_Address == "" || logInfo.Mac_Address == string.Empty ? "NULL" : logInfo.Mac_Address.Replace(":", "");
                this._file_Name = string.Format("{0}_{1}_{2}_{3}_{4}.csv", this._file_Name, logInfo.Mac_Address, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), logInfo.Total_Result);
                string fileFullName = Path.Combine(_dir_Log_Single, _file_Name);

                //write data to file
                using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.Unicode)) {
                    //write title
                    sw.WriteLine(title);

                    foreach (PropertyInfo propertyInfo in logInfo.GetType().GetProperties()) {
                        if (propertyInfo.PropertyType == typeof(VnptTestItemInfo)) {
                            VnptTestItemInfo itemInfo = (VnptTestItemInfo)propertyInfo.GetValue(logInfo, null);

                            string content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                                                           DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff"),
                                                           logInfo.Mac_Address.Replace(":", "").ToUpper().Replace(",", ";"),
                                                           logInfo.Product_Serial.Replace(",", ";"),
                                                           logInfo.Operator.Replace(",", ";"),
                                                           propertyInfo.Name.Replace(",", ";"),
                                                           itemInfo.Lower_Limit.Replace(",", ";"),
                                                           itemInfo.Upper_Limit.Replace(",", ";"),
                                                           itemInfo.Actual_Value.Replace(",", ";"),
                                                           itemInfo.Result.Replace(",", ";")
                                                           );

                            //write content
                            sw.WriteLine(content);
                        }
                    }
                }

                return true;
            } catch {
                return false;
            }
        }
    }
}
