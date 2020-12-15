using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day03 : IDay
	{
		public void Run()
		{
			string[] lines = File.ReadAllLines("inputs/day03.txt")
				.Select(x => x.Trim())
				.ToArray();

			// part 1
			Console.WriteLine("--> Part 1");

			int result = CalculateSlope(lines, 3, 1);
			Console.WriteLine($"Tree: {result}");

			// part 2
			Console.WriteLine("--> Part 2");
			List<(int xOffset, int yOffset)> slopes = new()
			{
				(1, 1),
				(3, 1),
				(5, 1),
				(7, 1),
				(1, 2)
			};
			long result2 = 1;
			foreach ((int xOffset, int yOffset) in slopes)
			{
				result2 *= CalculateSlope(lines, xOffset, yOffset);
			}

			Console.WriteLine($"Result: {result2}");
		}

		private static int CalculateSlope(string[] lines, int xOffset, int yOffset)
		{
			int y = 0;
			int x = 0;
			int result = 0;
			while (y < lines.Length)
			{
				if (lines[y][x] != '.')
				{
					result++;
				}

				x = (x + xOffset) % lines[0].Length;
				y += yOffset;
			}

			return result;
		}
	}
}