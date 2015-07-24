using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music_Turing_Machine
{
    abstract class Sound
    {
        public static int DEFAULT_DUR = 500;
        protected int dur;
        public abstract void Play();
        public int getDuration()
        {
            return dur;
        }
        public abstract Sound ToneUp();
        public abstract Sound ToneDown();
        public abstract Sound OctaveUp();
        public abstract Sound OctaveDown();
        public void setDuration(int d)
        {
            dur = d;
        }
    }
}
