using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day01 : IDay
	{
		public void Run()
		{
			HashSet<int> set = File.ReadAllLines("inputs/day01.txt")
				.Select(int.Parse)
				.ToHashSet();

			Console.WriteLine("--> Part 1");
			foreach (int n in set)
			{
				if (set.Contains(2020 - n) && 2020 - n != n)
				{
					Console.WriteLine($"Found the two numbers : {n} ; {2020-n}");
					Console.WriteLine($"Result: {n*(2020-n)}");
					break;
				}
			}

			Console.WriteLine("--> Part 2");
			foreach (int n in set)
			{
				foreach (int m in set)
				{
					if (n == m || n + m > 2020)
					{
						continue;
					}

					int expected = 2020 - (n + m);
					if (set.Contains(expected) && expected != n && expected != m)
					{
						Console.WriteLine($"Found the three numbers : {n} ; {m} ; {expected}");
						Console.WriteLine($"Result: {n*m*expected}");
						break;
					}
				}
			}
		}
	}
}