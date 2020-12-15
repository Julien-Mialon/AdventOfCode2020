using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day07 : IDay
	{
		private const string SHINY = "shiny gold";
		public void Run()
		{
			string[] lines = File.ReadAllLines("inputs/day07.txt");
			// parse inputs
			Dictionary<string, List<(int count, string id)>> dependencies = new();
			Dictionary<string, List<string>> invertDependencies = new();
			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				string[] parts = line.Split(" contain ", StringSplitOptions.RemoveEmptyEntries);
				string colorId = parts[0].Substring(0, parts[0].Length - "bags".Length).Trim();

				if (parts[1] == "no other bags.")
				{
					dependencies.Add(colorId, new());
				}
				else
				{
					List<(int, string)> dependent = parts[1].Trim('.').Split(',', StringSplitOptions.RemoveEmptyEntries)
						.Select(l =>
						{
							string[] p = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);
							return (int.Parse(p[0]), string.Join(" ", p[1..^1]));
						}).ToList();

					foreach ((int _, string color) in dependent)
					{
						if (invertDependencies.TryGetValue(color, out List<string> items))
						{
							items.Add(colorId);
						}
						else
						{
							invertDependencies[color] = new() { colorId };
						}
					}

					dependencies.Add(colorId, dependent);
				}
			}

			Console.WriteLine("--> Part 1");
			HashSet<string> containsAShinyGold = new();
			Queue<string> processingQueue = new();
			processingQueue.Enqueue(SHINY);
			while (processingQueue.Count > 0)
			{
				string item = processingQueue.Dequeue();
				if (invertDependencies.TryGetValue(item, out List<string> values))
				{
					foreach (string value in values)
					{
						if (containsAShinyGold.Add(value))
						{
							processingQueue.Enqueue(value);
						}
					}
				}
			}
			Console.WriteLine($"Number of colors that can contain shiny gold: {containsAShinyGold.Count}");

			Console.WriteLine("--> Part 2");
			int bagCount = CountDependenciesOf(SHINY, dependencies);

			Console.WriteLine($"Bags count in shiny gold: {bagCount}");
		}

		private static int CountDependenciesOf(string color, Dictionary<string, List<(int count, string color)>> dependencies)
		{
			int result = 0;
			if (dependencies.TryGetValue(color, out List<(int count, string color)> innerBags))
			{
				foreach ((int count, string innerColor) in innerBags)
				{
					result += count + (count * CountDependenciesOf(innerColor, dependencies));
				}
			}

			return result;
		}
	}
}