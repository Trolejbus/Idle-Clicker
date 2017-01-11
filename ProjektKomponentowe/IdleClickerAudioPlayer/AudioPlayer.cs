using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using System.IO;

namespace IdleClicker
{
    public static class AudioPlayer
    {
        static List<WavePlayer> MusicClips = new List<WavePlayer>();
        static List<string> songsPaths = new List<string>();
        static WaveStream sound;
        static bool played = false;
        private static DirectSoundOut audioOutput = new DirectSoundOut();
        private static DirectSoundOut audioOutputQuickSound = new DirectSoundOut();
        public static float Volume
        {
            get
            {
                if(MusicClips.Count == 0)
                {
                    return 1.0f;
                }
                return MusicClips[0].Channel.Volume;
            }
            set
            {
                foreach (WavePlayer item in MusicClips)
                {
                    item.Channel.Volume = value;
                }
            }
        }


        public static void AddSong(string path)
        {
            if (!songsPaths.Contains(path)) { 
                songsPaths.Add(path);
            }
        }
        public static void RemoveSong(string path)
        {
            songsPaths.Remove(path);
        }
        public static void RemoveAllSongs()
        {
            if(!played)
                songsPaths.Clear();
            else
            {
                StopMusic();
                songsPaths.Clear();
            }
        }
        public static void PlayMusic()
        {
            if (played) return;

            foreach (string path in songsPaths)
            {
                if (path.EndsWith(".mp3"))
                {
                    WaveStream convertedSong = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path));
                    MusicClips.Add(new WavePlayer(convertedSong));
                }
                else
                    MusicClips.Add(new WavePlayer(path));
            }
            var mixer = new MixingWaveProvider32(MusicClips.Select(c => c.Channel));
            audioOutput.Init(mixer);

            played = true;
            audioOutput.Play();

        }
        public static void PlayQuickSound(string path)
        {
            if (path.EndsWith(".mp3"))
            {
                sound = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path));
            }
            else
                sound = new WaveFileReader(path);

            var mixer = new MixingWaveProvider32();
            var sound32 = new WaveChannel32(sound);
            sound32.PadWithZeroes = false;
            // set the volume of background file
            sound32.Volume = 0.8f;
            //add stream into the mixer
            mixer.AddInputStream(sound32);
            
            audioOutputQuickSound.Init(mixer);

            audioOutputQuickSound.Play();
        }
        public static void StopMusic()
        {
            audioOutput.Stop();
            played = false;
        }
        public static void PauseMusic()
        {
            audioOutput.Pause();
            played = false;
        }
        
    }
}
