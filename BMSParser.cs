using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class BMSParser
    {

        public BMS Parse(String path)
        {
            BMS bms = new BMS();
            Encoding shiftjis = Encoding.GetEncoding(932);
            string line;

            using (StreamReader sr = new StreamReader(path, shiftjis))
            {
                while( (line = sr.ReadLine()) != null )
                {
                    ProcessBMSLine(line.Trim(), bms);
                }
            }

            return bms;
        }

        private void ProcessBMSLine(String line, BMS bms)
        {
            char[] seperators = { ' ', '\t', '　' };
            String[] args = line.Split(seperators, 2);
            if(args[0].StartsWith("#"))
            {
                args[0] = args[0].ToUpper();

                if (args[0].StartsWith("#WAV"))
                {
                    int index = Util.HexToInt(args[0].Substring(4, 2));
                    bms.wavList[index] = args[1];
                }
                else if (args[0].StartsWith("BMP"))
                {
                    int index = Util.HexToInt(args[0].Substring(4, 2));
                    bms.bmpList[index] = args[1];
                }
                else if (args[0] == "#TITLE")
                {
                    bms.title = args[1];
                }
                else if (args[0] == "#SUBTITLE")
                {
                    bms.subtitle = args[1];
                }
                else if (args[0] == "#PLAYER")
                {
                    bms.player = Convert.ToInt32(args[1]);
                }
                else if (args[0] == "#GENRE")
                {
                    bms.genre = args[1];
                }
                else if (args[0] == "#ARTIST")
                {
                    bms.artist = args[1];
                }
                else if (args[0] == "#BPM")
                {
                    bms.BPM = Convert.ToDouble(args[1]);
                }
                else if (args[0] == "#PLAYLEVEL")
                {
                    bms.playLevel = Convert.ToInt32(args[1]);
                }
                else if (args[0] == "#RANK")
                {
                    bms.rank = Convert.ToInt32(args[1]);
                }
                else if (args[0] == "#STAGEFILE")
                {
                    bms.stageFile = args[1];
                }
                else if (args[0] == "#TOTAL")
                {
                    bms.total = Convert.ToInt32(args[1]);
                }
                else if (args[0] == "#VOLWAV")
                {
                    bms.volwav = Convert.ToInt32(args[1]);
                }
                else if (args[0] == "#SUBARTIST")
                {
                    bms.subartist = args[1];
                }
                else if (args[0] == "#BANNER")
                {
                    bms.banner = args[1];
                }
                else if (args[0] == "#DIFFICULTY")
                {
                    bms.difficulty = Convert.ToInt32(args[1]);
                }
                //main data field
                else
                {
                    args = line.Split(':');

                    int measure = Convert.ToInt32(args[0].Substring(1, 3));
                    int channel = Convert.ToInt32(args[0].Substring(4, 2));


                }
                
            }
        }
    }
}
