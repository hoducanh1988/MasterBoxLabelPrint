using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {
    public class LogDetail : AbstractLogger {

        private string _dir_Log_Detail;
        private string _file_Name = "";

        /// <summary>
        /// 
        /// </summary>
        public LogDetail() : base() {
            _dir_Log_Detail = Path.Combine(base.dir_Jig_Index, "LogDetail");
            if (!Directory.Exists(_dir_Log_Detail)) Directory.CreateDirectory(_dir_Log_Detail);

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
        public bool To_TXT_File(VnptAsmTestFunctionLogInfo logInfo) {
            try {
                logInfo.Mac_Address = logInfo.Mac_Address == null || logInfo.Mac_Address == "" || logInfo.Mac_Address == string.Empty ? "NULL" : logInfo.Mac_Address.Replace(":", "");
                this._file_Name = string.Format("{0}_{1}_{2}_{3}_{4}.txt", this._file_Name, logInfo.Mac_Address, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), logInfo.Total_Result);
                string fileFullName = Path.Combine(_dir_Log_Detail, _file_Name);

                using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.Unicode)) {
                    sw.WriteLine(logInfo.Software_Version);
                    sw.WriteLine(logInfo.System_Log);
                }

                return true;
            } catch {
                return false;
            }
        }
    }
}
