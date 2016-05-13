using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class BMS
    {
        //default pulse = 1/192
        //todo: change pulse when needed
        public int pulse = 192;

        public int player;
        public String genre;
        public String title;
        public String subtitle;
        public String artist;
        public String subartist;
        public String banner;
        public String path;
        public String backBmp;

        public int lnObj;
        public double BPM;
        public int difficulty;
        public int rank;
        public int volwav;
        public int playLevel;
        public double total;
        public String stageFile;

        public List<String> wavList = new List<String>(new String[1322]);
        public List<String> bmpList = new List<String>(new String[1322]);
        //list of exBPMs
        public List<double> bpmList = new List<double>(new double[1322]);
        public List<long> stopList = new List<long>(new long[1322]);

        public List<double> measureList = new List<double>(Enumerable.Repeat(1.0, 999));
        public List<Note> noteList = new List<Note>();

        public List<BpmEvent> bpmEvents = new List<BpmEvent>();
        public List<StopEvent> stopEvents = new List<StopEvent>();
    }
}