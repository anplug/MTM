using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music_Turing_Machine
{
    class RuntimeEnvironment
    {
        private static int instances = 0;

        private List<Char> source;
        private List<Sound> tape;
        private int current;

        private const int tapeSize = 10;

        private const char toneUpT = '+';
        private const char toneDownT = '-';
        private const char octaveUpT = '*';
        private const char octaveDownT = '/';
        private const char beginWhileT = '[';
        private const char endWhileT = ']';
        private const char leftT = '<';
        private const char rightT = '>';
        private const char playT = '.';

        private RuntimeEnvironment()
        {
            initiateEnvironment();
        }
        private void initiateEnvironment()
        {
            source = new List<Char>();
            tape = new List<Sound>();
            for (int i = 0; i < tapeSize; i++)
            {
                tape.Add(new Pause(Sound.DEFAULT_DUR));
            }
            current = 0;
        }
        public static RuntimeEnvironment getInstance()
        {
            if (instances == 0)
            {
                instances = 1;
                return new RuntimeEnvironment();
            }
            return null;
        }
        public void putSource(char[] preSource)
        {
            for (int i = 0; i < preSource.Length; i++)
            {
                if (preSource[i] == toneUpT || preSource[i] == toneDownT ||
                    preSource[i] == octaveUpT || preSource[i] == octaveDownT ||
                    preSource[i] == leftT || preSource[i] == rightT)
                {
                    int numberOfTokens = 1;
                    if (i != preSource.Length - 1)
                    {
                        if (Char.IsNumber(preSource[i + 1]))
                        {
                            numberOfTokens = int.Parse(new String(preSource[i + 1], 1));
                        }
                    }
                    for (int j = 0; j < numberOfTokens; j++)
                    {
                        source.Add(preSource[i]);
                    }
                }
                if (preSource[i] == beginWhileT || preSource[i] == endWhileT || preSource[i] == playT)
                {
                    source.Add(preSource[i]);
                }
            }
        }
        public char[] getSource()
        {
            return source.ToArray();
        }
        public void clear()
        {
            initiateEnvironment();
        }
        public void start(ref bool isAborted)
        {
            Sound temp;
            for (int srcCharCurrent = 0; srcCharCurrent != (int)source.LongCount(); srcCharCurrent++)
            {
                char ch = source[srcCharCurrent];
                switch (ch)
                {
                    case (playT):
                    {
                        tape[current].Play();
                        break;
                    }
                    case (toneUpT):
                    {
                        temp = tape[current].ToneUp();
                        if (temp != null)
                        {
                            tape[current] = temp;
                        }
                        break;
                    }
                    case (toneDownT):
                    {
                        temp = tape[current].ToneDown();
                        if (temp != null)
                        {
                            tape[current] = temp;
                        }
                        break;
                    }
                    case (octaveUpT):
                    {
                        temp = tape[current].OctaveUp();
                        if (temp != null)
                        {
                            tape[current] = temp;
                        }
                        break;
                    }
                    case (octaveDownT):
                    {
                        temp = tape[current].OctaveDown();
                        if (temp != null)
                        {
                            tape[current] = temp;
                        }
                        break;
                    }
                    case (rightT):
                    {
                        if (current == tapeSize - 1)
                        {
                            current = 0;
                        }
                        else
                        {
                            ++current;
                        }
                        break;
                    }
                    case (leftT):
                    {
                        if (current == 0)
                        {
                            current = tapeSize - 1;
                        }
                        else
                        {
                            --current;
                        }
                        break;
                    }
                    case (beginWhileT):
                    {
                        if (tape[current] is Pause)
                        {
                            do
                            {
                                if (source[srcCharCurrent] == endWhileT && (srcCharCurrent + 1 != (int)source.LongCount()))
                                {
                                    break;
                                }
                            }
                            while (srcCharCurrent != (int)source.LongCount());
                        }
                        break;
                    }
                    case (endWhileT):
                    {
                        if (tape[current] is Note)
                        {
                            int numberOfWhiles = 0;
                            do
                            {
                                --srcCharCurrent;
                                if (source[srcCharCurrent] == endWhileT)
                                {
                                    numberOfWhiles++;
                                }
                                if (source[srcCharCurrent] == beginWhileT)
                                {
                                    if (numberOfWhiles == 0)
                                        break;
                                    numberOfWhiles--;
                                }

                            }
                            while (srcCharCurrent != 0);
                        }
                        break;
                    }
                }
                if (isAborted)
                    break;
            }
        }
        public void setDuration(int dur)
        {
            foreach (Sound s in tape)
            {
                s.setDuration(dur);
            }
        }
    }
}
