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

            //Get exact time of each note, events
            CalcBMSTime(bms);

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
                    bms.wavList.Insert(index, args[1]);
                }
                else if (args[0].StartsWith("#BMP"))
                {
                    int index = Util.HexToInt(args[0].Substring(4, 2));
                    bms.bmpList[index] = args[1];
                }
                else if (args[0] == "#BPM")
                {
                    bms.BPM = Convert.ToDouble(args[1]);
                }
                else if (args[0].StartsWith("#BPM"))
                {
                    int index = Util.HexToInt(args[0].Substring(4, 2));
                    bms.bpmList.Insert(index, Convert.ToDouble(args[1]));
                }
                else if (args[0].StartsWith("#STOP"))
                {
                    int index = Util.HexToInt(args[0].Substring(5, 2));
                    bms.stopList.Insert(index, Convert.ToInt64(args[1]));
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
                else if(args[0] == "#LNOBJ")
                {
                    bms.lnObj = Util.HexToInt(args[1]);
                }
                else if(args[0] == "#BACKBMP")
                {
                    bms.backBmp = args[1];
                }
                //main data field
                else
                {
                    args = line.Split(':');

                    int measure = Convert.ToInt32(args[0].Substring(1, 3));
                    int channel = Util.HexToInt(args[0].Substring(4, 2));



                    //measure length channel
                    if(channel == 2)
                    {
                        bms.measureList[measure] = Convert.ToDouble(args[1]);
                        return;
                    }

                    IEnumerable<string> notes = Util.Split(args[1], 2);


                    IEnumerator<string> noteEnum = notes.GetEnumerator();
                    int argsLength = notes.Count<string>();
                    int argIndex = 0;
                    double measureDiv;

                    //BPM Change channel
                    if (channel == 3)
                    {
                        while(noteEnum.MoveNext())
                        {
                            if(noteEnum.Current != "00")
                            {
                                measureDiv = (double)argIndex/argsLength;
                                //channel 03 use plain hex
                                int bpm = Convert.ToInt32(noteEnum.Current, 16);
                                bms.bpmEvents.Add(new BpmEvent( Convert.ToDouble(bpm), measure, measureDiv));
                            }
                            argIndex++;
                        }
                    }
                    //exBPM Change channel
                    else if (channel == 8)
                    {
                        while (noteEnum.MoveNext())
                        {
                            if (noteEnum.Current != "00")
                            {
                                measureDiv = (double)argIndex/argsLength;
                                double bpm = bms.bpmList[Util.HexToInt(noteEnum.Current)];
                                bms.bpmEvents.Add(new BpmEvent(bpm, measure, measureDiv));
                            }
                            argIndex++;
                        }
                    }
                    //Stop Event Channel
                    else if (channel == 9)
                    {
                        while (noteEnum.MoveNext())
                        {
                            if (noteEnum.Current != "00")
                            {
                                measureDiv = (double)argIndex / argsLength;
                                long stop = bms.stopList[Util.HexToInt(noteEnum.Current)];
                                bms.stopEvents.Add(new StopEvent(stop, measure, measureDiv));
                            }
                            argIndex++;
                        }
                    }
                    //Notes channels
                    else
                    {
                        while (noteEnum.MoveNext())
                        {
                            if (noteEnum.Current != "00")
                            {
                                measureDiv = (double)argIndex / argsLength;
                                bms.noteList.Add(new Note(channel, Util.HexToInt(noteEnum.Current), measure, measureDiv));
                            }
                            argIndex++;
                        }
                    }
                    
                }
                
            }
        }

        private void CalcBMSTime(BMS bms)
        {
            double sum = 0;
            for(int i=0; i<999; i++)
            {
                sum += bms.measureList[i];
                bms.measureList[i] = sum;
            }

            foreach(Note note in bms.noteList)
            {
                double accumulatedTime = 0;
                if (note.measure == 0)
                    accumulatedTime = 0;
                else
                    accumulatedTime = bms.measureList[note.measure - 1];

                note.time = (ulong)((accumulatedTime + note.measureDiv * bms.measureList[note.measure]) * bms.pulse);
            }

            foreach(BpmEvent bpmEvent in bms.bpmEvents)
            {
                double accumulatedTime = 0;
                if (bpmEvent.measure == 0)
                    accumulatedTime = 0;
                else
                    accumulatedTime = bms.measureList[bpmEvent.measure - 1];

                bpmEvent.time = (ulong)((accumulatedTime + bpmEvent.measureDiv * bms.measureList[bpmEvent.measure]) * bms.pulse);

            }

            foreach (StopEvent stopEvent in bms.stopEvents)
            {
                double accumulatedTime = 0;
                if (stopEvent.measure == 0)
                    accumulatedTime = 0;
                else
                    accumulatedTime = bms.measureList[stopEvent.measure - 1];

                stopEvent.time = (ulong)((accumulatedTime + stopEvent.measureDiv * bms.measureList[stopEvent.measure]) * bms.pulse);

            }
        }
    }
}
