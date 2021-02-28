using System;
using System.Runtime.InteropServices;
using System.Text;
using WMPLib;
using SharpDX.XAudio2;
using SharpDX.IO;
using SharpDX.Multimedia;
using System.IO;

namespace AudioCard
{
    public static class AudioController
    {
        #region PlaySound
        enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        [DllImport("winmm.dll", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        public static void PlayNormal(string file)
        {
            PlaySound(file, new IntPtr(), PlaySoundFlags.SND_ASYNC);
        }
        #endregion

        #region Windows Media Player
        static WindowsMediaPlayer wmp = new WindowsMediaPlayer();

        public static void PlayWMP(string file)
        {
            StopWMP();
            wmp.URL = file;
            wmp.controls.play();
        }

        public static void PauseWMP()
        {
            wmp.controls.pause();
        }

        public static void ResumeWMP()
        {
            wmp.controls.play();
        }

        public static void StopWMP()
        {
            wmp.controls.stop();
        }
        #endregion

        #region Direct Sound
        public static void PlayDirect(string file)
        {
            XAudio2 device = new XAudio2();
            MasteringVoice master = new MasteringVoice(device);
            SoundStream stream = new SoundStream(new NativeFileStream(file, NativeFileMode.Open, NativeFileAccess.Read));
            SourceVoice source = new SourceVoice(device, stream.Format, true);
            source.SubmitSourceBuffer(new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            }, stream.DecodedPacketsInfo);
            source.Start();
        }
        #endregion

        #region WAV Header
        public struct WavHeader
        {
            public int ChunkID;
            public string ChunkFormat;
            public int FileSize;
            public string FileFormat;
            public string Subchunk1ID;
            public int Subchunk1Size;
        }

        public static WavHeader GetWavInfo(string file)
        {
            WavHeader header = new WavHeader();
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                byte[] wave = reader.ReadBytes(24);
                fileStream.Position = 0;
                header.ChunkID = reader.ReadInt32();
                header.FileSize = reader.ReadInt32();
                string formats = Encoding.Default.GetString(wave);
                header.ChunkFormat = formats.Substring(0, 6);
                header.FileFormat = formats.Substring(8, 4);
                header.Subchunk1ID = formats.Substring(12, 4);
                header.Subchunk1Size = reader.ReadInt32();
            }
            return header;
        }
        #endregion
    }
}
