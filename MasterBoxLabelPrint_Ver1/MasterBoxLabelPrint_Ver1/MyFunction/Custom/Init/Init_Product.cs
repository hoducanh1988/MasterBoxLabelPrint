using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Custom {
    public class Init_Product {
        public string name { get; set; }
        public string code { get; set; }
        public string color { get; set; }
        public string weight { get; set; }
        public string tolerance { get; set; }
        public string lotqty { get; set; }

        public override string ToString() {
            return string.Format("< {0},   {1},   {2},   {3},   {4} >", code, color, weight, tolerance, lotqty);
        }
    }
}
