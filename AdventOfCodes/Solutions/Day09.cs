using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day09 : IDay
	{
		private const int PREAMBlE_SIZE = 25;

		public void Run()
		{
			List<long> numbers = File.ReadAllLines("inputs/day09.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.Select(long.Parse)
				.ToList();

			Console.WriteLine("--> Part 1");
			Dictionary<long, long> validTimes = new();
			foreach (long number in numbers)
			{
				if (!validTimes.ContainsKey(number))
				{
					validTimes.Add(number, 0);
				}
			}

			for (int i = 0 ; i < PREAMBlE_SIZE ; i++)
			{
				validTimes[numbers[i]] = 1;
			}

			long issueNumber = 0;
			for (int i = PREAMBlE_SIZE ; i < numbers.Count ; ++i)
			{
				bool found = false;
				long targetNumber = numbers[i];
				for (int j = i - PREAMBlE_SIZE ; j < i ; ++j)
				{
					if (numbers[j] < targetNumber)
					{
						long secondPart = targetNumber - numbers[j];
						if (secondPart != numbers[j] && validTimes.GetValueOrDefault(secondPart, 0) != 0)
						{
							found = true;
							break;
						}
					}
				}

				if (!found)
				{
					issueNumber = targetNumber;
					Console.WriteLine($"First invalid number in XMAS protocoal: {targetNumber}");
					break;
				}

				validTimes[numbers[i - PREAMBlE_SIZE]]--;
				validTimes[numbers[i]]++;
			}

			Console.WriteLine("--> Part 2");

			int startOffset = 0;
			long sum = numbers[0];
			Queue<long> contiguousNumbers = new();
			contiguousNumbers.Enqueue(sum);

			for (int i = 1; i < numbers.Count; i++)
			{
				while (sum > issueNumber)
				{
					sum -= contiguousNumbers.Dequeue();
				}

				if (sum == issueNumber)
				{
					break;
				}

				contiguousNumbers.Enqueue(numbers[i]);
				sum += numbers[i];

				if (sum == issueNumber)
				{
					break;
				}
			}

			List<long> foundSolutionRange = contiguousNumbers.ToList();
			Console.WriteLine($"Found contiguous range : {string.Join(", ", foundSolutionRange)}");
			long min = foundSolutionRange.Min();
			long max = foundSolutionRange.Max();
			Console.WriteLine($"Min: {min} / Max: {max}");
			Console.WriteLine($"Answer: {min+max}");
		}
	}
}