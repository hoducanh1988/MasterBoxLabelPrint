using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom {

    public class msaccdb_tbDataProductionLOT {

        public string tb_ID { get; set; }
        public string DateTimeCreated { get; set; }
        public string Line { get; set; }
        public string Lot { get; set; }
        public string ProductSerial { get; set; }
        public string ProductName { get; set; }
        public string LotProgress { get; set; }
        public string LotQuantity { get; set; }
        public string LotStatus { get; set; }
        public string Rework { get; set; }
        public string ReworkReason { get; set; }

    }

    public class msaccdb_ProductionLOT {
        public string Lot { get; set; }
    }

}
