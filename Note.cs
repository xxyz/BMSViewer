﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSViewer
{
    //BMS each note
    class Note
    {
        //script's channel
        public int channel;

        //WAV
        public int sound;

        //measure-time acculated + (measure + measure Div)*pulse = time
        public int measure;
        public double measureDiv;
        public ulong time;

        public Note(int channel, int sound, int measure, double measureDiv)
        {
            this.sound = sound;
            this.channel = channel;
            this.measure = measure;
            this.measureDiv = measureDiv;
        }
    }
}