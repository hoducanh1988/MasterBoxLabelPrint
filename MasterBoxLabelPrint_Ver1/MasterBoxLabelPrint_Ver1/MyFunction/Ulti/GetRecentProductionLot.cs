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
        string _line = "", _place = "", _year = "", _code = "";

        //get all text from file production lot
        public GetRecentProductionLot() { }

        public GetRecentProductionLot(string line, string place, string year, string code) {
            this._line = line;
            this._place = place;
            this._year = year;
            this._code = code;

            if (File.Exists(_file_productionlot) == true) {
                tmpData = File.ReadAllText(_file_productionlot);
            }
        }

        //get recent lot
        public void GetData() {
            try {
                if (string.IsNullOrEmpty(tmpData)) {
                    MyGlobal.MyTesting.LotCount = "0";
                    MyGlobal.MyTesting.LotName = new GenerateProductionLot(_line, _place, _year, _code).Gererate();
                    this.SetData();
                }
                else {
                    string[] buffer = tmpData.Split(',');
                    string lotname = buffer[0];

                    string pd_number = lotname.Substring(0, 3);
                    string pd_place = lotname.Substring(3, 1);
                    string pd_year = lotname.Substring(4, 2);
                    string pd_line = lotname.Substring(6, 1);

                    bool _ischangelot = pd_number != _code || pd_place != _place || pd_year != _year || pd_line != _line;

                    if (_ischangelot) {
                        MyGlobal.MyTesting.LotCount = "0";
                        MyGlobal.MyTesting.LotName = new GenerateProductionLot(_line, _place, _year, _code).Gererate();
                        this.SetData();
                    }
                    else {
                        MyGlobal.MyTesting.LotCount = buffer[1].Split('/')[0];
                        MyGlobal.MyTesting.LotName = buffer[0];
                    }
                }
            }
            catch (Exception ex) {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        //write lot info to file
        public bool SetData() {
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
