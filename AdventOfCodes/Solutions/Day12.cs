using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day12 : IDay
	{
		private enum BoatMoves
		{
			North, East, South, West, TurnLeft, TurnRight, Forward
		}

		public void Run()
		{
			List<(BoatMoves instruction, int arg)> instructions = File.ReadAllLines("inputs/day12.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.Select(l =>
				{
					return (l[0] switch
					{
						'N' => BoatMoves.North,
						'E' => BoatMoves.East,
						'S' => BoatMoves.South,
						'W' => BoatMoves.West,
						'L' => BoatMoves.TurnLeft,
						'R' => BoatMoves.TurnRight,
						'F' => BoatMoves.Forward,
					}, int.Parse(l.Substring(1)));
				}).ToList();

			Console.WriteLine("--> Part 1");
			Part1(instructions);

			Console.WriteLine("--> Part 2");
			Part2(instructions);
		}

		private static void Part1(List<(BoatMoves instruction, int arg)> instructions)
		{
			int x = 0;
			int y = 0;
			(int dx, int dy)[] directions = new[]
			{
				(1, 0),
				(0, -1),
				(-1, 0),
				(0, 1),
			};
			(int dx, int dy) direction = directions[0];
			int directionIndex = 0;
			foreach ((BoatMoves instruction, int inc) in instructions)
			{
				switch (instruction)
				{
					case BoatMoves.North:
						y += inc;
						break;
					case BoatMoves.East:
						x += inc;
						break;
					case BoatMoves.South:
						y -= inc;
						break;
					case BoatMoves.West:
						x -= inc;
						break;
					case BoatMoves.TurnLeft:
						directionIndex = (directionIndex + 4 - (inc / 90)) % 4;
						direction = directions[directionIndex];
						break;
					case BoatMoves.TurnRight:
						directionIndex = (directionIndex + 4 + (inc / 90)) % 4;
						direction = directions[directionIndex];
						break;
					case BoatMoves.Forward:
						x += direction.dx * inc;
						y += direction.dy * inc;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			Console.WriteLine($"Distance: {Math.Abs(x) + Math.Abs(y)}");
		}

		private static void Part2(List<(BoatMoves instruction, int arg)> instructions)
		{
			int x = 0;
			int y = 0;
			int wx = 10;
			int wy = 1;

			foreach ((BoatMoves instruction, int inc) in instructions)
			{
				switch (instruction)
				{
					case BoatMoves.North:
						wy += inc;
						break;
					case BoatMoves.East:
						wx += inc;
						break;
					case BoatMoves.South:
						wy -= inc;
						break;
					case BoatMoves.West:
						wx -= inc;
						break;
					case BoatMoves.TurnLeft:
						(wx, wy) = Rotate(wx, wy, inc);
						break;
					case BoatMoves.TurnRight:
						(wx, wy) = Rotate(wx, wy, -inc);
						break;
					case BoatMoves.Forward:
						x += wx * inc;
						y += wy * inc;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			Console.WriteLine($"Distance: {Math.Abs(x) + Math.Abs(y)}");

			static (int x, int y) Rotate(int x, int y, int angle)
			{
				double sin = Math.Sin(ToRadian(angle));
				double cos = Math.Cos(ToRadian(angle));

				double dx = (x * cos) - (y * sin);
				double dy = (x * sin) + (y * cos);

				return ((int)Math.Round(dx), (int)Math.Round(dy));
			}

			static double ToRadian(int angle)
			{
				return angle * Math.PI / 180;
			}
		}
	}
}