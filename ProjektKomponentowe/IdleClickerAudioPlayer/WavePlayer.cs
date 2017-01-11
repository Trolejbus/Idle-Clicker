using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    class WavePlayer
    {
        WaveFileReader Reader;
        public WaveChannel32 Channel { get; set; }
        private LoopStream loop;
        string FileName { get; set; }

        public WavePlayer(string FileName)
        {
            this.FileName = FileName;
            Reader = new WaveFileReader(FileName);
            loop = new LoopStream(Reader);
            Channel = new WaveChannel32(loop) { PadWithZeroes = false };
        }
        public WavePlayer(WaveStream loop)
        {
            Channel = new WaveChannel32(loop) { PadWithZeroes = false };
        }

        public bool EnableLooping
        {
            get
            {
                return loop.EnableLooping;
            }
            set
            {
                loop.EnableLooping = value;
            }
        }

        public void Dispose()
        {
            if (Channel != null)
            {
                Channel.Dispose();
                Reader.Dispose();
            }
        }
    }
}
