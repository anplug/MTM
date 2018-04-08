using Midi;

namespace Music_Turing_Machine
{
    abstract class Sound
    {
        public static int DEFAULT_DUR = 200;

        protected int dur;
        public int Duration { get => dur; set => dur = value; }

        public abstract void Play(OutputDevice outputDevice);
        public abstract Sound ToneUp();
        public abstract Sound ToneDown();
        public abstract Sound OctaveUp();
        public abstract Sound OctaveDown();
    }
}
