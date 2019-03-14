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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteData() {
            try {
                var box = MyGlobal.MasterBox;
                return box.Input_New_DataRow_To_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", this.tbDataProductionLot, "tb_ID");
            }
            catch {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_tbDataProductionLOT> ReadData() {
            return MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", "", "Line", "", "Lot", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_tbDataProductionLOT> ReadProduct(string lot_name) {
            return MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>("tb_DataProductionLOT", 1000, "tb_ID", "ProductSerial", "", "Line", "", "Lot", lot_name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_ProductionLOT> ReadProductionLot() {
            return MyGlobal.MasterBox.Get_Distinct_Newest_DataRow_From_Access_DB_Table<msaccdb_ProductionLOT>("tb_DataProductionLOT", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "Lot");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_ProductionLOT> ReadProductionLot(string lot_name) {
            return MyGlobal.MasterBox.Get_Distinct_Newest_DataRow_From_Access_DB_Table<msaccdb_ProductionLOT>("tb_DataProductionLOT", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "Lot", lot_name);
        }
    }
}
