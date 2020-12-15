using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day05 : IDay
	{
		public void Run()
		{
			string[] lines = File.ReadAllLines("inputs/day05.txt");

			Console.WriteLine("--> Part 1");
			List<int> seatIds = lines.Select(line =>
			{
				string rowPart = line.Substring(0, 7);
				string columnPart = line.Substring(7);

				int minRow = 0;
				int maxRow = 128;
				int minColumn = 0;
				int maxColumn = 8;

				foreach (char c in rowPart)
				{
					if (c == 'F')
					{
						maxRow -= (maxRow - minRow) / 2;
					}
					else if(c == 'B')
					{
						minRow += (maxRow - minRow) / 2;
					}
				}
				int row = minRow;
				foreach (char c in columnPart)
				{
					if (c == 'L')
					{
						maxColumn -= (maxColumn - minColumn) / 2;
					}
					else if(c == 'R')
					{
						minColumn += (maxColumn - minColumn) / 2;
					}
				}

				int column = minColumn;

				return (row * 8) + column;
			}).ToList();
			Console.WriteLine($"Max seat id: {seatIds.Max()}");

			Console.WriteLine("--> Part 2");
			seatIds.Sort();
			for (int i = 1; i < seatIds.Count; i++)
			{
				if (seatIds[i] == seatIds[i - 1] + 2)
				{
					Console.WriteLine($"Your seat it = {seatIds[i-1]+1}");
				}
			}
		}
	}
}