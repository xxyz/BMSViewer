using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            BMSParser bmsParser = new BMSParser();
            BMS bms = bmsParser.Parse("D:/BMS/BOF/BOFU2015/Glitch Throne/GlitchThrone_eFeL_Engine_ogg/engine_XYZ.bms");
        }
    }
}