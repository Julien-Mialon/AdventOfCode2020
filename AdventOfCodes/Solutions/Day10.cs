using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day10 : IDay
	{
		public void Run()
		{
			List<int> numbers = File.ReadAllLines($"inputs/day10.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.Select(int.Parse)
				.OrderBy(x => x)
				.ToList();
			//Console.WriteLine("Input: " + string.Join(", ", numbers));

			Console.WriteLine("--> Part 1");
			int[] differences =
			{
				0,
				0,
				0,
				1
			}; // always 3 jolt difference between last adapter and device
			int previous = 0;
			foreach (int number in numbers)
			{
				int diff = number - previous;

				if (diff < 0 || diff > 3)
				{
					Console.WriteLine($"ERROR: {previous} => {number}, too much difference");
					break;
				}

				differences[diff]++;

				previous = number;
			}

			//Console.WriteLine($"differences : {string.Join(", ", differences)}");
			Console.WriteLine($"Answer {differences[1] * differences[3]}");

			Console.WriteLine("--> Part 2");
			bool[] isRemovable = Enumerable.Repeat(true, numbers.Count).ToArray();
			isRemovable[^1] = false;
			previous = 0;
			for (int i = 0; i < numbers.Count; i++)
			{
				if (numbers[i] - previous == 3)
				{
					if (i > 0)
					{
						isRemovable[i - 1] = false;
					}

					isRemovable[i] = false;
				}

				previous = numbers[i];
			}

			//Console.WriteLine(string.Join(", ", isRemovable));
			int contiguousCount = 0;
			List<int> contiguousRemovable = new();
			foreach (bool b in isRemovable)
			{
				if (b)
				{
					contiguousCount++;
				}
				else if (contiguousCount > 0)
				{
					//time to calculate
					contiguousRemovable.Add(contiguousCount);
					contiguousCount = 0;
				}
			}
			if (contiguousCount > 0)
			{
				contiguousRemovable.Add(contiguousCount);
			}

			//Console.WriteLine("Contiguous: " + string.Join(", ", contiguousRemovable));
			//Console.WriteLine($"Max: {contiguousRemovable.Max()}");
			Dictionary<int, int> multipliers = new();
			List<int> multipliersPerGroups = new();
			foreach (int count in contiguousRemovable)
			{
				if (multipliers.TryGetValue(count, out int multiplier))
				{
					multipliersPerGroups.Add(multiplier);
					continue;
				}

				if (count < 3)
				{
					multipliers.Add(count, (int)Math.Pow(2, count));
				}
				else
				{
					multipliers.Add(count, (int)Math.Pow(2, count) - 1);
				}

				multipliersPerGroups.Add(multipliers[count]);
			}

			// Console.WriteLine("Multipliers: " + string.Join(", ", multipliersPerGroups));
			Console.WriteLine($"Answer: {multipliersPerGroups.Aggregate(1L, (acc, item) => acc * item)}");
		}
	}
}