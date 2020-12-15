using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCodes.Solutions
{
	public class Day04 : IDay
	{
		public void Run()
		{
			string[] lines = File.ReadAllLines("inputs/day04.txt");
			// parse inputs
			List<Dictionary<string, string>> passports = new();
			Dictionary<string, string> current = new();
			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					if (current.Count > 0)
					{
						passports.Add(current);
						current = new();
					}
				}

				foreach (string s in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
				{
					string[] parts = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
					current[parts[0]] = parts[1];
				}
			}

			if (current.Count > 0)
			{
				passports.Add(current);
			}

			Console.WriteLine("--> Part 1");
			List<string> mandatoryFields1 = new()
			{
				"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
			};
			int validPassport1 = passports.Count(p => mandatoryFields1.All(field => p.ContainsKey(field)));

			Console.WriteLine($"Valid count: {validPassport1}");


			Console.WriteLine("--> Part 2");
			Dictionary<string, Func<string, bool>> validations = new ()
			{
				["byr"] = s => int.TryParse(s, out int n) && n >= 1920 && n <= 2002,
				["iyr"] = s => int.TryParse(s, out int n) && n >= 2010 && n <= 2020,
				["eyr"] = s => int.TryParse(s, out int n) && n >= 2020 && n <= 2030,
				["hgt"] = s => int.TryParse(new (s.TakeWhile(char.IsDigit).ToArray()), out int n) &&
				               ((new string(s.SkipWhile(char.IsDigit).ToArray()) == "cm" && n >= 150 && n <= 193)
				                || (new string(s.SkipWhile(char.IsDigit).ToArray()) == "in" && n >= 59 && n <= 76)),
				["hcl"] = s => new Regex("#[0-9a-f]{6}").IsMatch(s),
				["ecl"] = s => new HashSet<string>{ "amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(s),
				["pid"] = s => s.Length == 9 && s.All(char.IsDigit)
			};

			int validPassport2 = passports.Count(p =>
				mandatoryFields1.All(field => p.ContainsKey(field)) &&
				validations.All(x => x.Value(p[x.Key])));
			Console.WriteLine($"Valid count: {validPassport2}");
		}
	}
}