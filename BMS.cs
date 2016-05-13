using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class BMS
    {
        public int player;
        public String genre;
        public String title;
        public String subtitle;
        public String artist;
        public String subartist;
        public String banner;
        
        public double BPM;
        public int difficulty;
        public int rank;
        public int volwav;
        public int playLevel;
        public double total;
        public String stageFile;

        public List<String> wavList = new List<String>(1322);
        public List<String> bmpList = new List<String>(1322);

        public List<Measure> measureList = new List<Measure>(999);
        
    }
}
