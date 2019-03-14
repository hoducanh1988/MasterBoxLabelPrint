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
        string _line = "", _place = "", _year = "", _code = "";

        //get all text from file production
        public GenerateProductionLot(string line, string place, string year, string code) {
            this._line = line;
            this._place = place;
            this._year = year;
            this._code = code;

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

                    if (factory.ToLower().Equals(_place.ToLower()) && year.ToLower().Equals(_year.ToLower()) && line.ToLower().Equals(_line.ToLower())) {
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
                    File.WriteAllText(_file_production, string.Format("{0},{1},{2},{3}", _place, _year, _line, _new_lot_index));
                }
                
                //return lot
                return _new_lot;
            } catch {
                return null;
            }
        }


        private string _gen(string lot, out string lot_index) {
            lot_index = lot == null ? "000001" : _increment_lot(lot);
            if (string.IsNullOrEmpty(_code) ||
                string.IsNullOrEmpty(_place) ||
                string.IsNullOrEmpty(_year) ||
                string.IsNullOrEmpty(_line)) 
                {
                return null;
            }
            else return string.Format("{0}{1}{2}{3}_{4}", _code, _place, _year, _line, lot_index);
        }
        private string _increment_lot(string lot) {
            int r = int.Parse(lot);
            r++;
            return r.ToString().PadLeft(6, '0');
        }

    }
}
