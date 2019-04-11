﻿using System;
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
            this._file_Name = string.Format("{0}_{1}_Jig{3}_{4}.csv", 
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
                string title = "DATE_TIME,WORK_ORDER,OPERATOR,PN,UID1,UID2,TESTNAME,L_LIMIT,U_LIMIT,MEASURE_VAL,RESULT,INFO1,INFO2,INFO3,INFO4,INFO5,INFO6,INFO7,INFO8,INFO9,INFO10";
                string fileFullName = Path.Combine(_dir_Log_Total, _file_Name);
                bool IsCreateTitle = !File.Exists(fileFullName);

                //write data to file
                using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.Unicode)) {
                    //write title
                    if (IsCreateTitle == true) sw.WriteLine(title);

                    foreach (PropertyInfo propertyInfo in logInfo.GetType().GetProperties()) {
                        if (propertyInfo.PropertyType == typeof(VnptTestItemInfo)) {
                            VnptTestItemInfo itemInfo = (VnptTestItemInfo)propertyInfo.GetValue(logInfo, null);

                            //check save log NONE
                            if (MyGlobal.MySetting.SaveLogNoMeaning == "No") { if (itemInfo.Result.ToLower().Contains("none") == true) continue; }

                            //check save log format
                            if (MyGlobal.MySetting.IsSaveLogFormat == false) { if (propertyInfo.Name.ToLower().Equals("format") == true) continue; }

                            //check save log printed
                            if (MyGlobal.MySetting.IsSaveLogPrinted == false) { if (propertyInfo.Name.ToLower().Equals("printed") == true) continue; }

                            //check save log weight
                            if (MyGlobal.MySetting.IsSaveLogWeight == false) { if (propertyInfo.Name.ToLower().Equals("weight") == true) continue; }
                            
                            //save log
                            string content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20}",
                                                           DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff"),
                                                           logInfo.Production_Command.Replace(",", ";"),
                                                           logInfo.Operator.Replace(",", ";"),
                                                           logInfo.ProductCode.Replace(",", ";"),
                                                           logInfo.Mac_Address.Replace(":", "").ToUpper().Replace(",", ";"),
                                                           logInfo.Product_Serial.Replace(",", ";"),
                                                           propertyInfo.Name.Replace(",", ";"),
                                                           itemInfo.Lower_Limit.Replace(",", ";"),
                                                           itemInfo.Upper_Limit.Replace(",", ";"),
                                                           itemInfo.Actual_Value.Replace(",", ";"),
                                                           itemInfo.Result.Replace(",", ";"),
                                                           logInfo.ProductionLot.Replace(",", ";"),
                                                           logInfo.LotProgress.Replace(",", ";"),
                                                           moreInfo.Info3,
                                                           moreInfo.Info4,
                                                           moreInfo.Info5,
                                                           moreInfo.Info6,
                                                           moreInfo.Info7,
                                                           moreInfo.Info8,
                                                           moreInfo.Info9,
                                                           moreInfo.Info10
                                                           );

                            //write content
                            sw.WriteLine(content);
                        }
                    }
                }
                return true;
            }
            catch {
                return false;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="logInfo"></param>
        ///// <param name="moreInfo"></param>
        ///// <returns></returns>
        //public bool To_CSV_File(VnptAsmTestFunctionLogInfo logInfo, VnptLogMoreInfo moreInfo) {
        //    try {
        //        string title = "Date_Time_Create,MacAddress,ProductCode,ProductionOrder,NhanVien,SerialNumber,LOT,LotProgress,Rework,Infor3,Infor4,Infor5,TestSubject,LowerLimit,UpperLimit,GiaTriDo,PhanDinh";
        //        string fileFullName = Path.Combine(_dir_Log_Total, _file_Name);
        //        bool IsCreateTitle = !File.Exists(fileFullName);

        //        //write data to file
        //        using (StreamWriter sw = new StreamWriter(fileFullName, true, Encoding.Unicode)) {
        //            //write title
        //            if (IsCreateTitle == true) sw.WriteLine(title);

        //            foreach (PropertyInfo propertyInfo in logInfo.GetType().GetProperties()) {
        //                if (propertyInfo.PropertyType == typeof(VnptTestItemInfo)) {
        //                    VnptTestItemInfo itemInfo = (VnptTestItemInfo)propertyInfo.GetValue(logInfo, null);

        //                    string content = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}",
        //                                                   DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff"),
        //                                                   logInfo.Mac_Address.Replace(":", "").ToUpper().Replace(",", ";"),
        //                                                   logInfo.ProductCode.Replace(",", ";"),
        //                                                   logInfo.Production_Command.Replace(",", ";"),
        //                                                   logInfo.Operator.Replace(",", ";"),
        //                                                   logInfo.Product_Serial.Replace(",", ";"),
        //                                                   logInfo.ProductionLot.Replace(",", ";"),
        //                                                   logInfo.LotProgress.Replace(",", ";"),
        //                                                   logInfo.Rework.Replace(",", ";"),
        //                                                   moreInfo.Info3,
        //                                                   moreInfo.Info4,
        //                                                   moreInfo.Info5,
        //                                                   propertyInfo.Name.Replace(",", ";"),
        //                                                   itemInfo.Lower_Limit.Replace(",", ";"),
        //                                                   itemInfo.Upper_Limit.Replace(",", ";"),
        //                                                   itemInfo.Actual_Value.Replace(",", ";"),
        //                                                   itemInfo.Result.Replace(",", ";")
        //                                                   );

        //                    //write content
        //                    sw.WriteLine(content);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch {
        //        return false;
        //    }
        //}

    }
}
