using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;

namespace IdleClicker
{
    // TODO: zrobić przyciszanie, podgłaśnianie, kontrola czasu kiedy co ma grać.
    public static class AudioPlayer
    {
        private static NAudio.Wave.BlockAlignReductionStream stream = null;
        private static NAudio.Wave.DirectSoundOut output = null;

        public static void Play(string path)
        {
            if (path.EndsWith(".mp3"))
            {
                NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(path));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else if (path.EndsWith(".wav"))
            {
                NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(path));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else throw new InvalidOperationException("Error! Not a correct audio file type!");

            output = new NAudio.Wave.DirectSoundOut();
            output.Init(stream);
            output.Play();
        }

        private static void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        public static void Pause()
        {
            if (output != null)
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
        }

        public static void Stop()
        {
            DisposeWave();
        }
    }
}
