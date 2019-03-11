using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class GenerateProductionLot {

        string _file_production = string.Format("{0}tmp_\\Production.dll", AppDomain.CurrentDomain.BaseDirectory);
        string tmpData = ""; //Factory,Year,Line,LOT Index

        //get all text from file production
        public GenerateProductionLot() {
            if (File.Exists(_file_production) == true) {
                tmpData = File.ReadAllText(_file_production);
            }
        }

        //generate lot
        public string Gererate() {
            try {
                string _new_lot = "";
                string _new_lot_index = "";

                if (string.IsNullOrEmpty(tmpData) == true) {
                    //reset lot
                    _new_lot = _gen(null, out _new_lot_index);
                }
                else {
                    string[] buffer = tmpData.Split(',');
                    string factory = buffer[0], year = buffer[1], line = buffer[2], lot = buffer[3];

                    if (factory.ToLower().Equals(MyGlobal.MySetting.ProductionPlace.ToLower()) && year.ToLower().Equals(MyGlobal.MySetting.ProductionYear.ToLower()) && line.ToLower().Equals(MyGlobal.MySetting.LineIndex.ToLower())) {
                        //increment lot
                        _new_lot = _gen(lot, out _new_lot_index);
                    }
                    else {
                        //reset lot
                        _new_lot = _gen(null, out _new_lot_index);
                    }
                }

                //save to file
                if (_new_lot != null) {
                    File.WriteAllText(_file_production, string.Format("{0},{1},{2},{3}", MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.LineIndex, _new_lot_index));
                }
                
                //return lot
                return _new_lot;
            } catch {
                return null;
            }
        }


        private string _gen(string lot, out string lot_index) {
            lot_index = lot == null ? "000001" : _increment_lot(lot);
            if (string.IsNullOrEmpty(MyGlobal.MySetting.ProductCode) ||
                string.IsNullOrEmpty(MyGlobal.MySetting.ProductionPlace) ||
                string.IsNullOrEmpty(MyGlobal.MySetting.ProductionYear) ||
                string.IsNullOrEmpty(MyGlobal.MySetting.LineIndex)) 
                {
                return null;
            }
            else return string.Format("{0}{1}{2}{3}_{4}", MyGlobal.MySetting.ProductCode, MyGlobal.MySetting.ProductionPlace, MyGlobal.MySetting.ProductionYear, MyGlobal.MySetting.LineIndex, lot_index);
        }
        private string _increment_lot(string lot) {
            int r = int.Parse(lot);
            r++;
            return r.ToString().PadLeft(6, '0');
        }

    }
}
