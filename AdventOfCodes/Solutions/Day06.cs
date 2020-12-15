using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day06 : IDay
	{
		public void Run()
		{
			string[] lines = File.ReadAllLines("inputs/day06.txt");
			// parse inputs
			List<List<string>> answers = new();
			List<string> current = new();
			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					if (current.Count > 0)
					{
						answers.Add(current);
						current = new();
					}
				}
				else
				{
					current.Add(line);
				}
			}

			if (current.Count > 0)
			{
				answers.Add(current);
			}

			Console.WriteLine("--> Part 1");
			List<int> answerCount = answers.Select(group => string.Join("", group).Where(char.IsLetter).ToHashSet().Count).ToList();
			Console.WriteLine($"Sum: {answerCount.Sum()}");

			Console.WriteLine("--> Part 2");

			List<int> answerCount2 = answers.Select(group =>
			{
				List<HashSet<char>> sets = group.Select(x => x.Where(char.IsLetter).ToHashSet()).ToList();

				for (int i = 1; i < sets.Count; i++)
				{
					sets[0].IntersectWith(sets[i]);
				}

				return sets[0].Count;
			}).ToList();
			Console.WriteLine($"Sum: {answerCount2.Sum()}");
		}
	}
}