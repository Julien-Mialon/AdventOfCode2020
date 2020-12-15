using System;
using System.Collections.Generic;
using AdventOfCodes.Solutions;

namespace AdventOfCodes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Advent of codes 2020");

            List<IDay> run = new()
            {
	            // new Day01(),
	            // new Day02(),
	            // new Day03(),
	            // new Day04(),
	            // new Day05(),
	            // new Day06(),
	            // new Day07(),
	            // new Day08(),
	            // new Day09(),
	            // new Day10(),
	            // new Day11(),
	            // new Day12(),
	            // new Day13(),
	            // new Day14(),
	            new Day15(),
            };

            foreach (IDay day in run)
            {
	            Console.WriteLine($"Start {day.GetType().Name}");
	            day.Run();
            }

            Console.WriteLine("-- end --");
        }
    }
}