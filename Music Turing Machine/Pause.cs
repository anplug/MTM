using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music_Turing_Machine
{
    class Pause : Sound
    {
        public Pause()
        {
            Init(DEFAULT_DUR);
        }
        public Pause(int d)
        {
            Init(d);
        }
        private void Init(int d)
        {
            dur = d;
        }
        public Pause(Pause pause)        
        {
            Init(pause.getDuration());
        }
        public override void Play()
        {
            System.Threading.Thread.Sleep(dur);
        }
        public override Sound ToneUp()
        {
            return new Note(Note.Letters.C, Note.FIRST_OCTAVE, dur);
        }
        public override Sound ToneDown()
        {
            return new Note(Note.Letters.B, Note.LAST_OCTAVE, dur);
        }
        public override Sound OctaveUp()
        {
            return new Note(Note.Letters.C, Note.Octaves.Small, dur);
        }
        public override Sound OctaveDown()
        {
            return new Note(Note.Letters.B, Note.Octaves.Three, dur);
        }
    }
}
