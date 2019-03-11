using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class GetRecentProductionLot {
        static string _file_productionlot = string.Format("{0}tmp_\\Recent.dll", AppDomain.CurrentDomain.BaseDirectory);
        static string tmpData = ""; //Lot Name, Lot Progress

        //get all text from file production lot
        static GetRecentProductionLot() {
            if (File.Exists(_file_productionlot) == true) {
                tmpData = File.ReadAllText(_file_productionlot);
            }
        }

        //get recent lot
        public static void GetData() {
            try {
                if (string.IsNullOrEmpty(tmpData)) {
                    MyGlobal.MyTesting.LotCount = "0";
                    MyGlobal.MyTesting.LotName = new GenerateProductionLot().Gererate();
                }
                else {
                    string[] buffer = tmpData.Split(',');
                    MyGlobal.MyTesting.LotCount = buffer[1].Split('/')[0];
                    MyGlobal.MyTesting.LotName = buffer[0];
                }
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        //write lot info to file
        public static bool SetData() {
            try {
                if (string.IsNullOrEmpty(MyGlobal.MyTesting.LotName) == false && string.IsNullOrEmpty(MyGlobal.MyTesting.LotProgress) == false) {
                    File.WriteAllText(_file_productionlot, string.Format("{0},{1}", MyGlobal.MyTesting.LotName, MyGlobal.MyTesting.LotProgress));
                }
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
