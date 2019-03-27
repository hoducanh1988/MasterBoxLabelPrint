using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {

    public class LogTotal : AbstractLogger {

        private string _dir_Log_Total = "";
        private string _file_Name = "";


        /// <summary>
        /// 
        /// </summary>
        public LogTotal() : base() {
            _dir_Log_Total = Path.Combine(base.dir_Jig_Index, "LogTotal");
            if (!Directory.Exists(_dir_Log_Total)) Directory.CreateDirectory(_dir_Log_Total);
            //get file name
            this._file_Name = string.Format("{0}_{1}_Station{2}_Jig{3}_{4}.csv", 
                MyGlobal.MySetting.ProductName,
                MyGlobal.MySetting.StationName,
                MyGlobal.MySetting.StationIndex,
                MyGlobal.MySetting.JigIndex, 
                DateTime.Now.ToString("yyyyMMdd"));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="moreInfo"></param>
        /// <returns></returns>
        public bool To_CSV_File(VnptAsmTestFunctionLogInfo logInfo, VnptLogMoreInfo moreInfo) {
            try {
                string title = "Date_Time_Create,ProductionCommand,ProductionLot,LotProgress,MacAddress,ProductSerial,NhanVien,Infor1,Infor2,Infor3,Infor4,Infor5,TestSubject,LowerLimit,UpperLimit,GiaTriDo,DonViDo,PhanDinh,Rework";
                string fileFullName = Path.Combine(_dir_Log_Total, _file_Name);
                bool IsCreateTitle = !File.Exists(fileFullName);

                //write data to file
                using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.Unicode)) {
                    //write title
                    if (IsCreateTitle == true) sw.WriteLine(title);

                    foreach (PropertyInfo propertyInfo in logInfo.GetType().GetProperties()) {
                        if (propertyInfo.PropertyType == typeof(VnptTestItemInfo)) {
                            VnptTestItemInfo itemInfo = (VnptTestItemInfo)propertyInfo.GetValue(logInfo, null);

                            string content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18}",
                                                           DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff"),
                                                           logInfo.Production_Command.Replace(",", ";"),
                                                           logInfo.ProductionLot.Replace(",", ";"),
                                                           logInfo.LotProgress.Replace(",", ";"),
                                                           logInfo.Mac_Address.Replace(":", "").ToUpper().Replace(",", ";"),
                                                           logInfo.Product_Serial.Replace(",", ";"),
                                                           logInfo.Operator.Replace(",", ";"),
                                                           moreInfo.Info1,
                                                           moreInfo.Info2,
                                                           moreInfo.Info3,
                                                           moreInfo.Info4,
                                                           moreInfo.Info5,
                                                           propertyInfo.Name.Replace(",", ";"),
                                                           itemInfo.Lower_Limit.Replace(",", ";"),
                                                           itemInfo.Upper_Limit.Replace(",", ";"),
                                                           itemInfo.Actual_Value.Replace(",", ";"),
                                                           itemInfo.Unit_Of_Measurement.Replace(",", ";"),
                                                           itemInfo.Result.Replace(",", ";"),
                                                           logInfo.Rework.Replace(",", ";")
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
