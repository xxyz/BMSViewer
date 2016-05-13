using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    class StopEvent
    {
        //stop Duration (1/192)
        public long stop;

        //measure-time acculated + (measure + measure Div)*pulse = time
        public int measure;
        public double measureDiv;
        public ulong time;

        public StopEvent(long stop, int measure, double measureDiv)
        {
            this.stop = stop;
            this.measure = measure;
            this.measureDiv = measureDiv;
        }
    }
}