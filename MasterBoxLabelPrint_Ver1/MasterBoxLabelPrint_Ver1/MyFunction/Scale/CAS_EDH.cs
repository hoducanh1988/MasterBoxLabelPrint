using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UtilityPack.Protocol;
using UtilityPack.Converter;
using System.IO.Ports;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Scale {
    public class CAS_EDH : UART, Iscale {

        public static UART device = null;

        ////ST, GS,+    0.0 g
        ////ST, GS,+  106.9 g
        ////US, GS,+    1.2 g
       
        /// <summary>
        /// Return null if device is not connect
        /// </summary>
        /// <returns></returns>
        public static string GetWeight() {
            if (device != null) device.Close();
            device = new UART();
            device.Open(
                MyGlobal.MySetting.SerialPortName,
                MyGlobal.MySetting.SerialBaudRate,
                MyGlobal.MySetting.SerialDataBits, 
                myConverter.FromStringToSerialParity(MyGlobal.MySetting.SerialParity), 
                myConverter.FromStringToSerialStopBits(MyGlobal.MySetting.SerialStopBits)
                );

            if (device == null) return null;
            if (device.IsConnected() == false) return null;

            int count = 0;

        REP:
            count++;
            //clear serial receive buffer
            string data = null;
            device.Read();

            int wait_time; bool r = int.TryParse(MyGlobal.MySetting.WaitTime, out wait_time);
            wait_time = r ? wait_time : 100;
            Thread.Sleep(wait_time);

            //get data newest
            data = device.Read();

            //process data
            string value = null;
            string sign = "";

            if (data.Contains("\r\n")) {
                string[] buffer = data.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                value = buffer[buffer.Length - 2].Trim();

                try {
                    buffer = value.Split(',');
                    value = buffer[2].Replace("g", "").Replace("-", "").Replace("+", "").Trim();
                    sign = buffer[2].Contains("-") ? "-" : "";
                    value = sign + value;
                }
                catch {
                    if (count < 3) goto REP;
                }
            }
            device.Close();

            if (count >= 3) return null;
            else return value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDataFromScale() {
            if (device != null) device.Close();
            device = new UART();
            device.Open(
                MyGlobal.MySetting.SerialPortName,
                MyGlobal.MySetting.SerialBaudRate,
                MyGlobal.MySetting.SerialDataBits,
                myConverter.FromStringToSerialParity(MyGlobal.MySetting.SerialParity),
                myConverter.FromStringToSerialStopBits(MyGlobal.MySetting.SerialStopBits)
                );

            if (device == null) return null;
            if (device.IsConnected() == false) return null;

            //clear serial receive buffer
            string data = null;
            device.Read();

            Thread.Sleep(100);
            data = device.Read();
            if (data == null) return null;

            //process data
            string value = null;

            if (data.Contains("\r\n")) {
                string[] buffer = data.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                value = buffer[buffer.Length - 2].Trim();
            }
            device.Close();
            return value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_scalestring"></param>
        /// <returns></returns>
        public static string GetWeightFromScaleString(string _scalestring) {
            string value = null;
            string sign = "";

            try {
                string[] buffer = _scalestring.Split(',');
                value = buffer[2].Replace("g", "").Replace("-", "").Replace("+", "").Trim();
                sign = buffer[2].Contains("-") ? "-" : "";
                value = sign + value;
            }
            catch {
            }
            return value;
        }


    }
}
