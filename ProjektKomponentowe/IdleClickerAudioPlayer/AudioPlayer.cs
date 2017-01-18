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
        static WaveChannel32 soundWaveChannel = null;
        static List<string> musicPaths = new List<string>();
        static WaveStream soundStream = null;
        static float soundVolume = 1.0f;
        static bool isPlayed = false;
        private static DirectSoundOut musicOutput = new DirectSoundOut();
        private static DirectSoundOut soundOutput = new DirectSoundOut();
        public static float MusicVolume
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
        public static float SoundVolume
        {
            get
            {
                if (soundWaveChannel == null)
                    return 1.0f;
                return soundWaveChannel.Volume;
            }
            set
            {
                if(soundWaveChannel != null)
                    soundWaveChannel.Volume = value;
            }
        }


        public static void AddMusic(string path)
        {
            if (!musicPaths.Contains(path)) { 
                musicPaths.Add(path);
            }
        }
        public static void RemoveMusic(string path)
        {
            soundOutput.Stop();
            musicPaths.Remove(path);
        }
        public static void RemoveAllMusic()
        {
            soundOutput.Stop();
            musicPaths.Clear();
            MusicClips.Clear();
        }
        public static void PlayMusic()
        {
            if (isPlayed) return;

            foreach (string path in musicPaths)
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
            musicOutput.Init(mixer);

            isPlayed = true;
            musicOutput.Play();

        }
        public static void PlaySound(string path)
        {       
            soundStream = getWaveStream(path);

            var mixer = new MixingWaveProvider32();
            soundWaveChannel = new WaveChannel32(soundStream);
            soundWaveChannel.PadWithZeroes = false;
            // set the volume of background file
            soundWaveChannel.Volume = soundVolume;
            //add stream into the mixer
            mixer.AddInputStream(soundWaveChannel);
            
            soundOutput.Init(mixer);

            soundOutput.Play();
        }

        private static WaveStream getWaveStream(string path)
        {
            if (path.EndsWith(".mp3"))
                return WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(path));
            else
                return new WaveFileReader(path);
        }

        public static void StopMusic()
        {
            musicOutput.Stop();
            isPlayed = false;
        }
        public static void PauseMusic()
        {
            musicOutput.Pause();
            isPlayed = false;
        }
        
    }
}
