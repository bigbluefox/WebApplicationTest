using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsp.Test.Common
{
    /// <summary>
    /// 
    /// </summary>
   public interface ISoundPlayer
    {
        bool Inited { get; }
        bool Init();
        void PreLoad(ISoundFile sound);
        void UnLoad();
        void Play();
        void Pause();
        void Resume();
        void Stop();
        bool AutoPlay { get; set; }
        int Volume { get; set; }
        double SoundPosition { get; set; }
        double SoundLength { get; }
        PlayState State { get; } 
    }

   public enum PlayState
   {
       Stopped = 0,
       Playing,
       Paused
   }
   public interface ISoundFile
   {
       string FileName { get; }
       int StartPosition { get; }
       int Length { get; }
   }
}
