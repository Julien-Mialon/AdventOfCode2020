using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day02 : IDay
	{
		public void Run()
		{
			List<(int min, int max, char letter, string password)> inputs = File.ReadAllLines("inputs/day02.txt")
				.Select(line =>
				{
					string[] parts = line.Split(new char[]
					{
						' ',
						'-',
						':'
					}, StringSplitOptions.RemoveEmptyEntries);

					return (
						min: int.Parse(parts[0]),
						max: int.Parse(parts[1]),
						letter: parts[2][0],
						password: parts[3]
					);
				}).ToList();

			Console.WriteLine("--> Part 1");
			int r1 = inputs.Count(item =>
			{
				int count = item.password.Count(x => x == item.letter);
				return item.min <= count && count <= item.max;
			});
			Console.WriteLine($"Valid count: {r1}");

			Console.WriteLine("--> Part 2");
			int r2 = inputs.Count(item => item.min - 1 < item.password.Length &&
			                              item.max - 1 < item.password.Length &&
			                              (item.letter == item.password[item.min - 1] || item.letter == item.password[item.max - 1]) &&
			                              item.password[item.min - 1] != item.password[item.max - 1]);
			Console.WriteLine($"Valid count: {r2}");
		}
	}
}