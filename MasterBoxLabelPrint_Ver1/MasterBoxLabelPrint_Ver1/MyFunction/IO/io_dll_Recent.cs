using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Global;

namespace MasterBoxLabelPrint_Ver1.MyFunction.IO {
    public class io_dll_Recent {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lot_name"></param>
        /// <param name="lot_progress"></param>
        /// <returns></returns>
        public static bool ToFile(string lot_name, string lot_progress) {
            try {
                string text = string.Format("{0},{1}", lot_name, lot_progress);
                File.WriteAllText(MyGlobal.Recent_FileFullName, text);
                return true;
            } catch {
                return false;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lot_name"></param>
        /// <param name="lot_progress"></param>
        /// <returns></returns>
        public static bool FromFile(out string lot_name, out string lot_progress) {
            lot_name = lot_progress = null;
            if (!File.Exists(MyGlobal.Recent_FileFullName)) return false;

            try {
                string text = File.ReadAllLines(MyGlobal.Recent_FileFullName)[0];
                string[] buffer = text.Split(',');
                lot_name = buffer[0];
                lot_progress = buffer[1];
                return true;
            } catch {
                return false;
            }
        }

    }
}
