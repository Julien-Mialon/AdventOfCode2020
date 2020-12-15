using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day13 : IDay
	{
		public void Run()
		{
			string[] lines = File.ReadAllLines($"inputs/day13.txt");
			Console.WriteLine("--> Part 1");
			{
				int startTime = int.Parse(lines[0]);
				int[] buses = lines[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
					.Where(x => x != "x")
					.Select(int.Parse)
					.ToArray();

				int minTime = int.MaxValue;
				int chosenBus = -1;
				foreach (int bus in buses)
				{
					int time = startTime % bus;
					if (time > 0)
					{
						time = bus - time;
					}

					if (time < minTime)
					{
						minTime = time;
						chosenBus = bus;
					}
				}

				Console.WriteLine($"Result: {chosenBus * minTime}");
			}
			Console.WriteLine("--> Part 2");
			{
				string input = lines[1];
				// input = "1789,37,47,1889";
				(int id, int offset)[] constraints = input.Split(',', StringSplitOptions.RemoveEmptyEntries)
					.Select((id, index) => int.TryParse(id, out var x) ? (id: x, index) : (id: -1, index))
					.Where(x => x.id > 0)
					.ToArray();

				List<(int minMultiplier, int bus)> buses = new();
				for (int i = 1; i < constraints.Length; i++)
				{
					for (int t = constraints[0].id ; ; t += constraints[0].id)
					{
						if ((t + constraints[i].offset) % constraints[i].id == 0)
						{
							buses.Add((t / constraints[0].id, constraints[i].id));
							break;
						}
					}
				}

				(int baseCount, int multiplier) = buses.OrderByDescending(x => x.minMultiplier).First();

				long startTime = baseCount * constraints[0].id;
				long loopInc = constraints[0].id * multiplier;
				while (true)
				{
					bool valid = true;
					for (int i = 1; i < constraints.Length; i++)
					{
						if ((startTime + constraints[i].offset) % constraints[i].id > 0)
						{
							valid = false;
							break;
						}
					}

					if (valid)
					{
						break;
					}

					startTime += loopInc;
				}

				Console.WriteLine($"Result: {startTime}");

			}
		}
	}
}