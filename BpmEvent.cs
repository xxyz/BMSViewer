using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class BpmEvent
    {
        //bpm change
        public double bpm;

        //measure-time acculated + (measure + measure Div)*pulse = time
        public int measure;
        public double measureDiv;
        public ulong time;

        public BpmEvent(double bpm, int measure, double measureDiv)
        {
            this.bpm = bpm;
            this.measure = measure;
            this.measureDiv = measureDiv;
        }
    }
}