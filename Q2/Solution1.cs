using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer.Q2.Solution1
{

	public class MusicConvention {		
		public static string[] musicAlphabet = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };	
	}

	public class GuitarString {
		

		internal string _tuneNote;
		internal int _octave;
		public GuitarString(string tuneNote,int octave=1) {
			SetTuneNote(tuneNote, octave);
		}

		public string Note { get => _tuneNote; }
		public int Octave { get => _octave; }

		public string TuneNote { get => Octave > 1 ? (Note + Octave) : Note; }

		internal void SetTuneNote(string tuneNote, int octave) {
			if (!MusicConvention.musicAlphabet.Any<string>(x => x == tuneNote))
				throw new ArgumentException("Invalid tune note");

			_tuneNote = tuneNote;
			_octave = octave;
		}

		public string Play(int fret = 0) {
			int idx = Array.FindIndex<string>(MusicConvention.musicAlphabet, x => _tuneNote == x);
			int octaveInc = (idx + fret) / 12;
			string nextNote = MusicConvention.musicAlphabet[(idx + fret) % 12];
			return nextNote + (_octave + octaveInc);
		}

	}

	public class BassGuitar {

		private GuitarString s1, s2, s3, s4;
		
		public BassGuitar() {
			s1 = new GuitarString("G");
			s2 = new GuitarString("D");
			s3 = new GuitarString("A");
			s4 = new GuitarString("E");
		}

		public GuitarString String1 { get => s1; }
		public GuitarString String2 { get => s2; }
		public GuitarString String3 { get => s3; }
		public GuitarString String4 { get => s4; }
	}

	public class StandardGuitar: BassGuitar
	{
		GuitarString s5, s6;

		public StandardGuitar():base() {

			String1.SetTuneNote("E", 4);
			String2.SetTuneNote("B", 3);
			String3.SetTuneNote("G", 3);
			String4.SetTuneNote("D", 3);
			s5 = new GuitarString("A", 2);
			s6 = new GuitarString("E", 2);
		}

		public GuitarString String5 { get => s5; }
		public GuitarString String6 { get => s6; }
	}
}
