﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UtilityPack.Protocol;
using UtilityPack.Converter;
using System.IO.Ports;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Lamp {

    public enum LampStatus { AllOFF = -1, GreenON = 0, RedON = 1, YellowON=2 };

    public class VNPT_Lamp : UART {
        public static UART device = null;

        public static bool Output(LampStatus lampstatus) {
            try {
                if (device != null) device.Close();
                device = new UART();
                device.Open(
                    MyGlobal.MySetting.LampPortName,
                    MyGlobal.MySetting.LampBaudRate,
                    MyGlobal.MySetting.LampDataBits,
                    myConverter.FromStringToSerialParity(MyGlobal.MySetting.LampParity),
                    myConverter.FromStringToSerialStopBits(MyGlobal.MySetting.LampStopBits)
                    );

                if (device == null) return false;
                if (device.IsConnected() == false) return false;

                int _ref = (int)lampstatus;
                switch (_ref) {
                    case -1: {
                            MyGlobal.OutputLamp = "0"; // off all lamp
                            break;
                        }
                    case 0: {
                            MyGlobal.OutputLamp = "p"; //on led green
                            break;
                        }
                    case 1: {
                            MyGlobal.OutputLamp = "f"; //on led red
                            break;
                        }
                    case 2: {
                            MyGlobal.OutputLamp = "w"; //on led yellow
                            break;
                        }
                    default: {
                            MyGlobal.OutputLamp = "0";
                            break;
                        }

                }

                Thread t = new Thread(new ThreadStart(() => {
                    device.Write(MyGlobal.OutputLamp);
                    device.Write(MyGlobal.OutputLamp);
                    device.Write(MyGlobal.OutputLamp);
                    int delay = MyGlobal.OutputLamp == "p" ? 1000 : 5000;
                    Thread.Sleep(delay);
                    device.Write("0");
                    Thread.Sleep(100);
                    device.Close();
                }));
                t.IsBackground = true;
                t.Start();
                
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
