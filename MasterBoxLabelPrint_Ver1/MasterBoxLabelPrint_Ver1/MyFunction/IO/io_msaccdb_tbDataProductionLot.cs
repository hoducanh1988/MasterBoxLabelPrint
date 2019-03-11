using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {
    public class io_msaccdb_tbDataProductionLot {
         msaccdb_tbDataProductionLOT tbDataProductionLot = null;

        public io_msaccdb_tbDataProductionLot() {
            var testing = MyGlobal.MyTesting;
            var setting = MyGlobal.MySetting;

            this.tbDataProductionLot = new msaccdb_tbDataProductionLOT();

            tbDataProductionLot.DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            tbDataProductionLot.Line = setting.LineIndex;
            tbDataProductionLot.Lot = testing.LotName;
            tbDataProductionLot.LotProgress = testing.LotProgress;
            tbDataProductionLot.LotQuantity = setting.LotQuantity;
            tbDataProductionLot.LotStatus = "";
            tbDataProductionLot.ProductName = setting.ProductName;
            tbDataProductionLot.ProductSerial = testing.ProductSerial;
            tbDataProductionLot.Rework = "-";
            
        }
    }
}
