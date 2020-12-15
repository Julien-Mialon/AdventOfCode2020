using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day08 : IDay
	{
		private enum Instruction
		{
			Nop,
			Jump,
			Accumulate
		}

		public void Run()
		{
			List<(Instruction instruction, int arg)> instructions = File.ReadAllLines("inputs/day08.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.Select(l =>
				{
					string[] parts = l.Split();
					return (parts[0] switch
					{
						"nop" => Instruction.Nop,
						"acc" => Instruction.Accumulate,
						"jmp" => Instruction.Jump
					}, int.Parse(parts[1]));
				}).ToList();

			Console.WriteLine("--> Part 1");
			long accumulator = Execute(instructions, out _);
			Console.WriteLine($"Accumulator before repetition: {accumulator}");

			Console.WriteLine("--> Part 2");
			(Instruction instruction, int arg)[] instructionsCopy = instructions.ToArray();
			for (int i = 0; i < instructions.Count; i++)
			{
				(Instruction instruction, int arg) = instructions[i];

				if (instruction == Instruction.Accumulate)
				{
					continue;
				}

				if (instruction == Instruction.Nop && arg != 0)
				{
					instructionsCopy[i] = (Instruction.Jump, arg);
				}
				else if (instruction == Instruction.Jump)
				{
					instructionsCopy[i] = (Instruction.Nop, arg);
				}

				long result = Execute(instructionsCopy, out bool isInfinite);
				if (isInfinite)
				{
					instructionsCopy[i] = (instruction, arg);
					continue;
				}

				Console.WriteLine($"Changed instruction at line {i+1}, result={result}");
				break;
			}
		}

		private static long Execute(IReadOnlyList<(Instruction instruction, int arg)> instructions, out bool infiniteLoop)
		{
			bool[] visitedInstructions = new bool[instructions.Count];
			long accumulator = 0;
			infiniteLoop = false;
			for (int i = 0; i < instructions.Count;)
			{
				if (visitedInstructions[i])
				{
					infiniteLoop = true;
					break;
				}

				visitedInstructions[i] = true;
				(Instruction instruction, int arg) = instructions[i];

				switch (instruction)
				{
					case Instruction.Nop:
						i++;
						break;
					case Instruction.Jump:
						i += arg;
						break;
					case Instruction.Accumulate:
						accumulator += arg;
						i++;
						break;
				}
			}

			return accumulator;
		}
	}
}