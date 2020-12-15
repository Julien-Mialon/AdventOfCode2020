using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day11 : IDay
	{
		public enum State
		{
			Floor,
			Empty,
			Occupied
		}

		public void Run()
		{
			List<string> seats = File.ReadAllLines($"inputs/day11.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.ToList();

			int height = seats.Count;
			int width = seats[0].Length;
			(int dx, int dy)[] around = new[]
			{
				(-1, -1),
				(0, -1),
				(1, -1),
				(-1, 0),
				(1, 0),
				(-1, 1),
				(0, 1),
				(1, 1),
			};
			State[,] states = new State[height, width];
			for (int i = 0 ; i < height ; ++i)
			{
				for (int j = 0; j < width; j++)
				{
					states[i, j] = seats[i][j] switch
					{
						'L' => State.Empty,
						'#' => State.Occupied,
						_ => State.Floor
					};
				}
			}

			Console.WriteLine("--> Part 1");
			State[,] newStates = new State[height, width];
			bool changed;
			do
			{
				changed = false;

				for (int y = 0 ; y < height ; ++y)
				{
					for (int x = 0; x < width; x++)
					{
						if (states[y, x] == State.Floor)
						{
							newStates[y, x] = State.Floor;
							continue;
						}

						int count = 0;
						foreach ((int dx, int dy) in around)
						{
							int x2 = x + dx;
							int y2 = y + dy;

							if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height && states[y2,x2] == State.Occupied)
							{
								count++;
							}
						}

						if (states[y,x] == State.Empty && count == 0)
						{
							changed = true;
							newStates[y, x] = State.Occupied;
						}
						else if (states[y, x] == State.Occupied && count >= 4)
						{
							changed = true;
							newStates[y, x] = State.Empty;
						}
						else
						{
							newStates[y, x] = states[y, x];
						}
					}
				}

				State[,] temp = states;
				states = newStates;
				newStates = temp;
			} while (changed);

			int result = 0;
			for (int y = 0 ; y < height ; ++y)
			{
				for (int x = 0 ; x < width ; x++)
				{
					if (states[y, x] == State.Occupied)
					{
						result++;
					}
				}
			}
			Console.WriteLine($"Result: {result}");

			Console.WriteLine("--> Part 2");

			for (int i = 0 ; i < height ; ++i)
			{
				for (int j = 0; j < width; j++)
				{
					states[i, j] = seats[i][j] switch
					{
						'L' => State.Empty,
						'#' => State.Occupied,
						_ => State.Floor
					};
				}
			}
			do
			{
				changed = false;

				for (int y = 0 ; y < height ; ++y)
				{
					for (int x = 0; x < width; x++)
					{
						if (states[y, x] == State.Floor)
						{
							newStates[y, x] = State.Floor;
							continue;
						}

						int count = 0;
						foreach ((int dx, int dy) in around)
						{
							int x2 = x + dx;
							int y2 = y + dy;

							while (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height && states[y2, x2] == State.Floor)
							{
								x2 += dx;
								y2 += dy;
							}

							if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height && states[y2,x2] == State.Occupied)
							{
								count++;
							}
						}

						if (states[y,x] == State.Empty && count == 0)
						{
							changed = true;
							newStates[y, x] = State.Occupied;
						}
						else if (states[y, x] == State.Occupied && count >= 5)
						{
							changed = true;
							newStates[y, x] = State.Empty;
						}
						else
						{
							newStates[y, x] = states[y, x];
						}
					}
				}

				State[,] temp = states;
				states = newStates;
				newStates = temp;
			} while (changed);

			result = 0;
			for (int y = 0 ; y < height ; ++y)
			{
				for (int x = 0 ; x < width ; x++)
				{
					if (states[y, x] == State.Occupied)
					{
						result++;
					}
				}
			}
			Console.WriteLine($"Result: {result}");
		}
	}
}