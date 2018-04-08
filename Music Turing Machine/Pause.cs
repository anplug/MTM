using Midi;

namespace Music_Turing_Machine
{
    class Pause : Sound
    {
        public Pause() => Init(DEFAULT_DUR);
        public Pause(int d) => Init(d);
        private void Init(int d) => dur = d;
        public Pause(Pause pause) => Init(pause.Duration);
        public override void Play(OutputDevice _) => System.Threading.Thread.Sleep(dur);
        public override Sound ToneUp() => new Note(Note.Letters.C, Note.FIRST_OCTAVE, dur);
        public override Sound ToneDown() => new Note(Note.Letters.B, Note.LAST_OCTAVE, dur);
        public override Sound OctaveUp() => new Note(Note.Letters.C, Note.Octaves.Small, dur);
        public override Sound OctaveDown() => new Note(Note.Letters.B, Note.Octaves.Three, dur);
    }
}
