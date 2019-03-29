using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class GenerateProductionLot {

        string _file_production = string.Format("{0}tmp_\\Production.dll", AppDomain.CurrentDomain.BaseDirectory);
        string _file_recent = string.Format("{0}tmp_\\Recent.dll", AppDomain.CurrentDomain.BaseDirectory);

        string tmpData = ""; //Factory,Year,Line,LOT Index
        string tmpRecent = ""; //LotName,LotProgress
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
            if (File.Exists(_file_recent) == true) {
                tmpRecent = File.ReadAllText(_file_recent);
            }
        }

        //generate lot
        public string Gererate() {
            try {
                string _new_lot = "";
                string _new_lot_index = "";

                if (string.IsNullOrEmpty(tmpData) == true) {
                    //reset lot
                    _new_lot = _gen(null, null, out _new_lot_index);
                }
                else {
                    string[] buffer = tmpData.Split(',');
                    string factory = buffer[0], year = buffer[1], line = buffer[2], lot = buffer[3];

                    buffer = tmpRecent.Split(',');
                    string _recent_lot = buffer[0], _recent_lot_progress = buffer[1];

                    if (factory.ToLower().Equals(_place.ToLower()) && year.ToLower().Equals(_year.ToLower()) && line.ToLower().Equals(_line.ToLower())) {
                        //increment lot or not
                        string _curr_qty = _recent_lot_progress.Split('/')[0];
                        _new_lot = _gen(lot, _curr_qty, out _new_lot_index);
                    }
                    else {
                        //reset lot
                        _new_lot = _gen(null, null, out _new_lot_index);
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


        private string _gen(string lot, string curr_qty, out string lot_index) {
            lot_index = lot == null ? "000001" : (curr_qty=="0" ? lot : _increment_lot(lot));
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
            new io_msaccdb_tbIMEISerialPrint().DeleteAll(); //delete all row in table imei print
            return r.ToString().PadLeft(6, '0');
        }

    }
}
