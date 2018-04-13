using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Answer.Q2.Solution2;
namespace Answer
{
	class Program
	{
		static void Main(string[] args)
		{
			StandardGuitar myGuitar = new StandardGuitar();

			string n1 =  myGuitar.String1.Play();
			string n2 = myGuitar.String4.Play(14);

			Console.WriteLine($"N1: {n1} vs N2: {n2}, and they are equal {n1 == n2}");

			string n3 = myGuitar.String2.Play(1);
			string n4 = myGuitar.String6.Play(20);

			Console.WriteLine($"N1: {n3} vs N2: {n4}, and they are equal {n3 == n4}");

			double f1 = myGuitar.String6.PlayFrequency(4);
			double f2 = myGuitar.String6.PlayFrequency(13);
			double f3 = myGuitar.String5.PlayFrequency(8);

			Console.WriteLine($"F1: {f1} - F2: ${f2} - F3: {f3}");


			Console.ReadLine();

		}
	}
}
