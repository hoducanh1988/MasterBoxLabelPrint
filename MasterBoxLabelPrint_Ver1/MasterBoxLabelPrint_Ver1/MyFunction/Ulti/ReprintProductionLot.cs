using MasterBoxLabelPrint_Ver1.MyFunction.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using MasterBoxLabelPrint_Ver1.MyFunction.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti
{
    public class ReprintProductionLot
    {
        string tbtemp = "tb_Template";
        string tbprint = "IMEI_SN_Print";
        string lot = "";

        public ReprintProductionLot(string lot_name) {
            this.lot = lot_name;
        }


        public bool Print(out string message) {
            message = "";
            try {
                bool r = false;
                
                //delete all data row in tb_Template
                r = _delete_All_DataRow_In_Table_(tbtemp);
                if (!r) { message = "Cant delete all data row in tb_Template."; return r; }

                //copy all data row from IMEI_SN_Print to tb_Template
                r = _copy_All_DataRow_From_Table1_To_Table2_(tbtemp, tbprint);
                if (!r) { message = "Cant copy all data row from IMEI_SN_Print to tb_Template."; return r; }

                //get all product in lot name from tb_DataProductionLOT
                List<msaccdb_tbDataProductionLOT> productlot = new io_msaccdb_tbDataProductionLot().ReadProduct(this.lot);
                if (productlot == null) { message = "Lot status is in progress."; return false; }
                if (productlot.Count == 0) { message = "Lot status is in progress."; return false; }
                if (productlot.Count != int.Parse(productlot[0].LotQuantity)) { message = string.Format("Lot status is in progress: qty {0}, actual {1}.", productlot[0].LotQuantity, productlot.Count); return false; }

                //delete all data row in IMEI_SN_Print
                r = _delete_All_DataRow_In_Table_(tbprint);
                if (!r) { message = "Cant delete all data row in IMEI_SN_Print."; return r; }

                //insert all product in lot name into IMEI_SN_Print
                foreach (var product in productlot) {
                    new io_msaccdb_tbIMEISerialPrint(product).WriteData();
                }

                //reprint label
                MyGlobal.MasterBox.Print_Access_Report("IMEI_SN_fPrint");
                Thread.Sleep(100);


                //delete all data row in IMEI_SN_Print
                r = _delete_All_DataRow_In_Table_(tbprint);
                if (!r) { message = "Cant delete all data row in IMEI_SN_Print."; return r; }

                //copy all data row from tb_Template to IMEI_SN_Print
                r = _copy_All_DataRow_From_Table1_To_Table2_(tbprint, tbtemp);
                if (!r) { message = "Cant copy all data row from tb_Template to IMEI_SN_Print."; return r; }

                return true;
            } catch (Exception ex) {
                message = ex.ToString();
                return false;
            }
        }




        private bool _delete_All_DataRow_In_Table_(string table_name) {
            try {
                var box = MyGlobal.MasterBox;
                return box.Delete_All_DataRow_From_Access_DB_Table(table_name);
            } catch {
                return false;
            }
        }
        private bool _copy_All_DataRow_From_Table1_To_Table2_(string new_table, string old_table) {
            try {
                var box = MyGlobal.MasterBox;
                return box.QueryData(string.Format("INSERT INTO {0} SELECT * FROM {1}", new_table, old_table));
            } catch {
                return false;
            }
        }


    }
}
