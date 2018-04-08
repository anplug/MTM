using System.Threading;
using Midi;

namespace Music_Turing_Machine
{
    class Note : Sound
    {
        private const double ETALON_FREQ = 440; // 1st Octave A
        public const Octaves LAST_OCTAVE = Octaves.Four;
        public const Octaves FIRST_OCTAVE = Octaves.Great;

        public static Letters ETALON_LETTER = Letters.A;
        public static Octaves ETALON_OCTAVE = Octaves.One;

        private Pitch note;
        private Octaves oct;
        private Letters let;

        public enum Letters { C = 1, Cd, D, Dd, E, F, Fd, G, Gd, A, Ad, B };
        public enum Octaves { Contra = 1, Great, Small, One, Two, Three, Four, Five };

        private void Init(Letters letter, Octaves octave, int duration)
        {
            note = (Pitch)(((int)octave - 1) * 12 + (int)letter);

            dur = duration;
            let = letter;
            oct = octave;
        }

        public Note() => Init(ETALON_LETTER, ETALON_OCTAVE, DEFAULT_DUR);
        public Note(Note note) => Init(note.Letter, note.Octave, note.Duration);
        public Note(Letters l, Octaves o) => Init(l, o, DEFAULT_DUR);
        public Note(Letters l, Octaves o, int d) => Init(l, o, d);
        public Letters Letter => let;
        public Octaves Octave => oct;
        public override void Play(OutputDevice outputDevice)
        {
            outputDevice.SendNoteOn(Channel.Channel1, note, 80);
            Thread.Sleep((int)dur);
        }
        public override Sound ToneUp()    // returns true when state is changing to "Pause"
        {
            Letters l = let;
            Octaves o = oct;
            if ((int)let != 12)
            {
                l = let + 1;
            }
            else
            {
                if (oct != LAST_OCTAVE)
                {
                    l = Letters.C;
                    o = oct + 1;
                }
                else
                {
                    return new Pause(dur);
                }

            }
            Init(l, o, dur);
            return null;
        }
        public override Sound ToneDown()  // returns true when state is changing to "Pause"
        {
            Letters l = let;
            Octaves o = oct;
            if ((int)let != 1)
            {
                l = let - 1;
            }
            else
            {
                if (oct != FIRST_OCTAVE)
                {
                    l = Letters.B;
                    o = oct - 1;
                }
                else
                {
                    return new Pause(dur);
                }
            }
            Init(l, o, dur);
            return null;
        }
        public override Sound OctaveUp()  // returns true when state is changing to "Pause"
        {
            for (int i = 0; i < 12; i++)
            {
                if (this.ToneUp() != null)
                {
                    return new Pause(dur);
                }
            }
            return null;
        }
        public override Sound OctaveDown()    // returns true when state is changing to "Pause"
        {
            for (int i = 0; i < 12; i++)
            {
                if (this.ToneDown() != null)
                {
                    return new Pause(dur);
                }
            }
            return null;
        }
    }
}