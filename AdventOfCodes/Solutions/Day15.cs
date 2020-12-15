using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day15 : IDay
	{
		public void Run()
		{
			string input = File.ReadAllText($"inputs/day15.txt");
			List<int> numbers = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

			Console.WriteLine("--> Part 1");
			Dictionary<int, List<int>> occurences = new();
			for (int turn = 0 ; turn < numbers.Count ; turn++)
			{
				int number = numbers[turn];
				occurences[number] = occurences.GetValueOrDefault(number) ?? new List<int>();
				occurences[number].Add(turn + 1);
			}

			while (numbers.Count < 2020)
			{
				int lastNumber = numbers[^1];
				if (occurences[lastNumber].Count > 1)
				{
					List<int> previous = occurences[lastNumber];
					numbers.Add(previous[^1] - previous[^2]);
				}
				else
				{
					numbers.Add(0);
				}

				occurences[numbers[^1]] = occurences.GetValueOrDefault(numbers[^1]) ?? new List<int>();
				occurences[numbers[^1]].Add(numbers.Count);
			}
			Console.WriteLine($"Result: {numbers[^1]}");
			Console.WriteLine("--> Part 2");

			while (numbers.Count < 30000000)
			{
				int lastNumber = numbers[^1];
				if (occurences[lastNumber].Count > 1)
				{
					List<int> previous = occurences[lastNumber];
					numbers.Add(previous[^1] - previous[^2]);
				}
				else
				{
					numbers.Add(0);
				}

				occurences[numbers[^1]] = occurences.GetValueOrDefault(numbers[^1]) ?? new List<int>();
				occurences[numbers[^1]].Add(numbers.Count);
			}
			Console.WriteLine($"Result: {numbers[^1]}");
		}
	}
}