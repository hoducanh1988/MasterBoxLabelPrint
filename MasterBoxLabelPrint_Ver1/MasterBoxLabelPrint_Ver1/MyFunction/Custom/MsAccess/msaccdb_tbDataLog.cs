using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom {

    public class msaccdb_tbDataLog {

        public string tb_ID { get; set; }
        public string DateTimeCreated { get; set; }
        public string ProductSerial { get; set; }
        public string Lot { get; set; }
        public string LotProgress { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public string ProductionCommand { get; set; }
        public string Factory { get; set; }
        public string Line { get; set; }
        public string Station { get; set; }
        public string StationIndex { get; set; }
        public string JigIndex { get; set; }
        public string Operator { get; set; }
        public string WeightLower { get; set; }
        public string WeightAct { get; set; }
        public string WeightUpper { get; set; }
        public string TotalResult { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Rework { get; set; }

    }
}
