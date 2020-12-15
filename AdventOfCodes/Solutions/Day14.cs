using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodes.Solutions
{
	public class Day14 : IDay
	{
		private class Instruction
		{
			public bool IsMaskSet { get; }

			public long AndMask { get; }

			public long OrMask { get; }

			public long Value { get; }

			public long Address { get; }

			public List<int> FloatingBits { get; }

			public long ResetFloatingBitsMask { get; }

			public Instruction(string l)
			{
				if (l.StartsWith("mask = "))
				{
					string mask = l.Substring("mask = ".Length);
					IsMaskSet = true;
					FloatingBits = new();
					long or = 0;
					long and = 0;
					long resetFloatingBits = 0;

					for (int i = 0 ; i < mask.Length ; i++)
					{
						char c = mask[i];
						or <<= 1;
						and <<= 1;
						resetFloatingBits <<= 1;

						if (c != '0')
						{
							and |= 1;
						}

						if (c == '1')
						{
							or |= 1;
						}

						if (c == 'X')
						{
							FloatingBits.Add(mask.Length - i - 1);
						}
						else
						{
							resetFloatingBits |= 1;
						}
					}

					AndMask = and;
					OrMask = or;
					ResetFloatingBitsMask = resetFloatingBits;
				}
				else if (l.StartsWith("mem"))
				{
					string memAddress = l.Substring("mem[".Length, l.IndexOf(']') - 4);
					string value = l.Substring(l.IndexOf('=') + 1);

					Address = int.Parse(memAddress);
					Value = int.Parse(value);
				}
			}
		}

		public void Run()
		{
			List<Instruction> lines = File.ReadAllLines($"inputs/day14.txt")
				.Where(x => !string.IsNullOrEmpty(x))
				.Select(x => new Instruction(x))
				.ToList();
			Console.WriteLine("--> Part 1");
			Part1(lines);

			Console.WriteLine("--> Part 2");
			Part2(lines);

		}

		private void Part1(List<Instruction> lines)
		{
			Dictionary<long, long> memory = new();

			Instruction mask = null;
			foreach (Instruction instruction in lines)
			{
				if (instruction.IsMaskSet)
				{
					mask = instruction;
					continue;
				}

				long value = instruction.Value;
				if (mask is not null)
				{
					value &= mask.AndMask;
					value |= mask.OrMask;
				}

				memory[instruction.Address] = value;
			}

			Console.WriteLine($"Result: {memory.Values.Sum()}");
		}

		private void Part2(List<Instruction> lines)
		{
			Dictionary<long, long> memory = new();

			Instruction mask = lines.First(x => x.IsMaskSet);
			foreach (Instruction instruction in lines)
			{
				if (instruction.IsMaskSet)
				{
					mask = instruction;
					continue;
				}

				long address = (instruction.Address | mask.OrMask) & mask.ResetFloatingBitsMask;
				Set(memory, address,  instruction.Value, mask.FloatingBits, 0);
			}

			Console.WriteLine($"Result: {memory.Values.Select(x => (double)x).Sum()}");

			static void Set(Dictionary<long, long> memory, long address, long value, List<int> bits, int offset)
			{
				if (offset + 1 < bits.Count)
				{
					int index = bits[offset];
					long bitShift = 1L << index;
					Set(memory, address, value, bits, offset + 1);
					address |= bitShift;
					Set(memory, address | bitShift, value, bits, offset + 1);
				}
				else
				{
					int index = bits[offset];
					long bitShift = 1L << index;
					memory[address] = value;
					address |= bitShift;
					memory[address] = value;
				}
			}
		}
	}
}