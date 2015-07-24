using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music_Turing_Machine
{
    class Note : Sound
    {
        private const double ETALON_FREQ = 440; // Ля 1й-октавы
        public const Octaves LAST_OCTAVE = Octaves.Four;
        public const Octaves FIRST_OCTAVE = Octaves.Great;

        public static Letters ETALON_LETTER = Letters.A;
        public static Octaves ETALON_OCTAVE = Octaves.One;

        private double freq;
        private Octaves oct;
        private Letters let;

        public enum Letters { C = 1, Cd, D, Dd, E, F, Fd, G, Gd, A, Ad, B };
        public enum Octaves { Contra = 1, Great, Small, One, Two, Three, Four, Five };

        private void Init(Letters l, Octaves o, int d)
        {
            int letter_shift = (int)l - (int)Letters.A;
            int octave_shift = (int)o - (int)Octaves.One;
            int shift = letter_shift + octave_shift * 12;
            double f = ETALON_FREQ * Math.Pow(2.0, (double)shift / 12);

            freq = f;
            dur = d;
            let = l;
            oct = o;
        }
        public Note()
        {
            Init(ETALON_LETTER, ETALON_OCTAVE, DEFAULT_DUR);
        }
        public Note(Note note)
        {
            Init(note.getLetter(), note.getOctave(), note.getDuration());
        }
        public Note(Letters l, Octaves o)
        {
            Init(l, o, DEFAULT_DUR);
        }
        public Note(Letters l, Octaves o, int d)
        {
            Init(l, o, d);
        }
        public double getFrequency()
        {
            return freq;
        }
        public Letters getLetter()
        {
            return let;
        }
        public Octaves getOctave()
        {
            return oct;
        }
        public override void Play()
        {
            if (freq >= 37)
                Console.Beep(Convert.ToInt32(freq), dur);
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