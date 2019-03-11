using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom {

    public class msaccdb_SerialPrintLog {

        public string ID { get; set; }
        public string SN { get; set; } //serial number cua san pham
        public string TimeScan { get; set; }
        public string LoSX { get; set; }
        public string Colour { get; set; }
        public string NoLine { get; set; }
        public string NoLine1 { get; set; }
        public string SNResult { get; set; }
        public string WeightLL { get; set; }
        public string WeightAct { get; set; }
        public string WeightUL { get; set; }
        public string WeightUOM { get; set; }
        public string WeightResult { get; set; }
        public string LOTProgress { get; set; }
        public string TotalResult { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
