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
                return box.Input_New_DataRow_To_Access_DB_Table<msaccdb_tbDataProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", this.tbDataProductionLot, "tb_ID");
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ReworkInformation"></param>
        /// <returns></returns>
        public bool WriteData(Proj_ReworkInformation _ReworkInformation) {
            try {
                var box = MyGlobal.MasterBox;
                this.tbDataProductionLot = new msaccdb_tbDataProductionLOT();
                tbDataProductionLot.DateTimeCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                tbDataProductionLot.Line = _ReworkInformation.Line;
                tbDataProductionLot.Lot = _ReworkInformation.ProductionLot;
                tbDataProductionLot.LotProgress = _ReworkInformation.LotProgress;
                tbDataProductionLot.LotQuantity = _ReworkInformation.LotQuantity ;
                tbDataProductionLot.LotStatus = "";
                tbDataProductionLot.ProductName = _ReworkInformation.ProductName;
                tbDataProductionLot.ProductSerial = _ReworkInformation.NewProductSerial;
                tbDataProductionLot.Rework = "-";

                return box.Input_New_DataRow_To_Access_DB_Table<msaccdb_tbDataProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", this.tbDataProductionLot, "tb_ID");
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lot"></param>
        /// <param name="oldSN"></param>
        /// <param name="newSN"></param>
        /// <param name="reason"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool UpdateReworkData(string lot, string oldSN, string newSN, string reason, string person) {
            try {
                var box = MyGlobal.MasterBox;
                string rw_reason = string.Format("[Rework date::{0}][Reason::{1}][Person::{2}][New Product::{3}]", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), reason, person, newSN);
                return box.QueryData(string.Format("UPDATE {3} SET Rework='1',ReworkReason='{0}' WHERE Lot='{1}' AND ProductSerial='{2}'", rw_reason, lot, oldSN, MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk"));
            }
            catch {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll() {
            try {
                var box = MyGlobal.MasterBox;
                return box.Delete_All_DataRow_From_Access_DB_Table(MyGlobal.MySetting.ProductionStatus == "Normal" ? "" : "tb_DataProductionLOT_Bulk");
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
            return MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "tb_ID", "ProductSerial", "", "Line", "", "Lot", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_tbDataProductionLOT> ReadProduct(string lot_name) {
            return MyGlobal.MasterBox.Get_Specified_DataRow_From_Access_DB_Table<msaccdb_tbDataProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", 1000, "tb_ID", "Rework", "-", "Line", "", "Lot", lot_name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_ProductionLOT> ReadProductionLot() {
            return MyGlobal.MasterBox.Get_Distinct_Newest_DataRow_From_Access_DB_Table<msaccdb_ProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "Lot");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<msaccdb_ProductionLOT> ReadProductionLot(string lot_name) {
            return MyGlobal.MasterBox.Get_Distinct_Newest_DataRow_From_Access_DB_Table<msaccdb_ProductionLOT>(MyGlobal.MySetting.ProductionStatus == "Normal" ? "tb_DataProductionLOT" : "tb_DataProductionLOT_Bulk", int.Parse(MyGlobal.MySetting.VisibleLogQuantity), "Lot", lot_name);
        }
        
    }
}
