using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using UtilityPack.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.IO;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti {

    public class GenerateLOT {

        string line = "", place = "", year = "", number = "", lotindex = "";

        //get all text from file production
        public GenerateLOT(string _line, string _place, string _year, string _number, string _lotindex, bool _flag_inc) {
            this.line = _line;
            this.place = _place;
            this.year = _year;
            this.number = _number;
            this.lotindex = _flag_inc == true ? _increment_lotindex(_lotindex)  : _lotindex;

            MyGlobal.MySetting.LOTIndex = this.lotindex; //update setting lot index
            XmlHelper<Proj_SettingInformation>.ToXmlFile(MyGlobal.MySetting, MyGlobal.Setting_FileFullName); //save to xml file
        }

        //generate lot
        public string Gererate() {
            string str = string.Format("{0}{1}{2}{3}_{4}", this.number, this.place, this.year, this.line, this.lotindex);
            io_dll_Recent.ToFile(str, MyGlobal.MyTesting.LotProgress); //save recent file
            return str;
        }

        //increase lot index
        string _increment_lotindex(string lotindex) {
            int r = int.Parse(lotindex) + 1;
            return r.ToString().PadLeft(6, '0');
        }

    }
}
