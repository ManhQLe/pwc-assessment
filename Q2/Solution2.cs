using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer.Q2.Solution2
{
	public class MusicConvention
	{

		public static string[] musicAlphabet = new string[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

		public static double[] baseFrequencies = new double[] { 32.70, 34.65, 36.71, 38.89, 41.20, 43.65, 46.25, 49.00, 51.91, 55.00, 58.27, 61.74 };

		public static double GetFrequency(string note, int octave = 1) {
			int idx = Array.FindIndex<string>(musicAlphabet, x => x == note);
			octave = Math.Max(octave, 1);

			double f1 = baseFrequencies[idx];
			return f1 * Math.Pow(2, octave - 1);
		}

		public static void InterpretNote(string tuneNote, out string note, out int oct) {
			note = "";
			oct = 0;
			for (int i = 0; i < tuneNote.Length; i++)
			{
				char c = tuneNote[i];
				if (30 <= c && c <= 39)
					oct += oct * 10 + c;
				else
					note += c;
			}
			if (String.IsNullOrEmpty(note))
				throw new ArgumentException($"Invalid tune note {tuneNote}");

			oct = oct > 0 ? oct : 1;
		}
	}

	public class GuitarString
	{

		internal string _note;
		internal int _octave;

		public GuitarString():this("C") {
		}

		public GuitarString(string tuneNote) {
			SetTuneNote(tuneNote);
		}

		public GuitarString(string note, int octave = 1)
		{
			SetTuneNote(note, octave);
		}

		public string Note { get => _note; }
		public int Octave { get => _octave; }

		public string TuneNote { get => Octave > 1 ? (Note + Octave) : Note; }		

		public string Play(int fret = 0)
		{
			int idx = Array.FindIndex<string>(MusicConvention.musicAlphabet, x => _note == x);
			int octaveInc = (idx + fret) / 12;
			string nextNote = MusicConvention.musicAlphabet[(idx + fret) % 12];
			return nextNote + (_octave + octaveInc);
		}

		public double PlayFrequency(int fret = 0) {
			int idx = Array.FindIndex<string>(MusicConvention.musicAlphabet, x => _note == x);
			int octaveInc = (idx + fret) / 12;
			string nextNote = MusicConvention.musicAlphabet[(idx + fret) % 12];
			return MusicConvention.GetFrequency(nextNote, _octave + octaveInc);
		}

		#region Internal Methods
		internal void SetTuneNote(string tuneNote)
		{
			MusicConvention.InterpretNote(tuneNote, out string note, out int oct);

			this.SetTuneNote(note, oct > 0 ? oct : 1);
		}

		internal void SetTuneNote(string note, int octave)
		{
			if (octave < 1)
				throw new ArgumentException("Invalid octave, must be greater than 0");

			if (!MusicConvention.musicAlphabet.Any<string>(x => x == note))
				throw new ArgumentException("Invalid tune note");

			_note = note;
			_octave = octave;
		}
		#endregion
	}

	public class Guitar {
		protected List<GuitarString> _guitarStrings;

		public Guitar() {
			_guitarStrings = new List<GuitarString>();
		}

		public string Play(int stringNumber, int atFret = 0) {
			GuitarString targetString = _guitarStrings[stringNumber];
			return targetString.Play(atFret);
		}
	}

	public class BassGuitar:Guitar
	{

		public BassGuitar():base()
		{
			_guitarStrings.Add(new GuitarString("G"));
			_guitarStrings.Add(new GuitarString("D"));
			_guitarStrings.Add(new GuitarString("A"));
			_guitarStrings.Add(new GuitarString("E"));
		}
		public GuitarString String1 { get => _guitarStrings[0]; }
		public GuitarString String2 { get => _guitarStrings[1]; }
		public GuitarString String3 { get => _guitarStrings[2]; }
		public GuitarString String4 { get => _guitarStrings[3]; }
	}

	public class StandardGuitar : BassGuitar
	{		
		public StandardGuitar() : base()
		{
			String1.SetTuneNote("E", 4);
			String2.SetTuneNote("B", 3);
			String3.SetTuneNote("G", 3);
			String4.SetTuneNote("D", 3);
			_guitarStrings.Add(new GuitarString("A", 2));
			_guitarStrings.Add(new GuitarString("E", 2));
		}
		public GuitarString String5 { get => _guitarStrings[4]; }
		public GuitarString String6 { get => _guitarStrings[5]; }
	}
}
