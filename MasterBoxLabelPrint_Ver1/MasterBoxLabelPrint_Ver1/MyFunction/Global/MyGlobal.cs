using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterBoxLabelPrint_Ver1.MyFunction.Custom;

namespace MasterBoxLabelPrint_Ver1.MyFunction.Global {

    public class MyGlobal {

        //variables
        public static string Setting_FileFullName = string.Format("{0}Setting.xml", AppDomain.CurrentDomain.BaseDirectory);


        //objects
        public static Proj_TestingInformation MyTesting = new Proj_TestingInformation();
        public static Proj_SettingInformation MySetting = new Proj_SettingInformation();
        public static Proj_DebugInformation myDebug = new Proj_DebugInformation();
        public static Proj_MainWindowInfo myWindow = new Proj_MainWindowInfo();

        public static AccessDatabase.MasterBoxAccessDB MasterBox = null;

        public static List<Init_Guideline> Guidelines = null;
        public static List<Init_SuggestionText> SuggestionTexts = null;
        public static List<Init_Product> Products = null;

        

        
    }
}
