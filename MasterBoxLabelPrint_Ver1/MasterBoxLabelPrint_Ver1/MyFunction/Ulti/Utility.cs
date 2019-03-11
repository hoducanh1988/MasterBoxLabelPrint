using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UtilityPack;
using UtilityPack.IO;
using MasterBoxLabelPrint_Ver1.MyFunction.Custom;


namespace MasterBoxLabelPrint_Ver1.MyFunction.Ulti
{
    public class Utility
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Init_Guideline> GetGuidelines() {
            try {
                return CsvHelper<Init_Guideline>.FromCsvFile(string.Format("{0}ref_\\Guideline.csv", AppDomain.CurrentDomain.BaseDirectory), Encoding.Unicode, ",");
            } catch {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Init_SuggestionText> GetSuggestionTexts() {
            try {
                return CsvHelper<Init_SuggestionText>.FromCsvFile(string.Format("{0}ref_\\SuggestionText.csv", AppDomain.CurrentDomain.BaseDirectory), Encoding.Unicode, ",");
            }
            catch {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns></returns>
        public static List<Init_Product> GetProducts(string file_name) {
            try {
                return CsvHelper<Init_Product>.FromCsvFile(string.Format("{0}conf_\\{1}", AppDomain.CurrentDomain.BaseDirectory, file_name), Encoding.Unicode, ",");
            }
            catch {
                return null;
            }
        }



    }
}
