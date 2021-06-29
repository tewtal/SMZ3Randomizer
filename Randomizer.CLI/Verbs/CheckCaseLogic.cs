using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using static System.Linq.Enumerable;

namespace Randomizer.CLI.Verbs {

    [Verb("checkcaselogic", HelpText = "Check a file with space separated case logic lists for duplicates and subsets")]
    class CheckCaseLogicOptions {

        [Value(0, Required = true, MetaName = "File",
            HelpText = "Specify the path to a text file containing one logic case per line")]
        public string File { get; set; }

    }

    static class CheckCaseLogic {

        public static void Run(CheckCaseLogicOptions opts) {
            if (!File.Exists(opts.File)) {
                Console.Error.WriteLine($"The file {opts.File} does not exist");
                return;
            }

            var lines = ReadLines(opts.File);
            var cases = ParseLines(lines);

            cases = CheckDuplicates(cases);
            if (cases.Any(x => x.Duplicate)) {
                Console.WriteLine("Found case line(s) with duplicate items, marked in the source file");
                WriteWithDuplicatePrefix(opts.File, cases);
                return;
            }

            cases = ExpandCaseItems(cases);
            cases = CheckProperSubsets(cases);
            if (cases.Any(x => x.SetNumber is not null)) {
                Console.WriteLine("Found case lines that are properly contained by others, marked in the source file");
                WriteWithSubsetPrefix(opts.File, cases);
                return;
            }

            Console.WriteLine("No issues found");
            WriteWithoutPrefix(opts.File, cases);
        }

        static IEnumerable<string> ReadLines(string filename) {
            return from line in File.ReadLines(filename)
                   where !string.IsNullOrWhiteSpace(line)
                   select line;
        }

        static readonly Regex PrefixPattern = new(@"^(x|[<>]\d*)? *");

        static IList<Case> ParseLines(IEnumerable<string> lines) {
            var cases = from line in lines
                        select PrefixPattern.Replace(line, "") into line
                        select new Case(line, Item.Parse(line));
            return cases.ToList();
        }

        static IList<Case> CheckDuplicates(IList<Case> cases) {
            var duplicates = from @case in cases
                             select @case with { Duplicate = HasDuplicate(@case.List) };
            return duplicates.ToList();

            static bool HasDuplicate(IList<Item> items) {
                return items.Distinct().Count() < items.Count;
            }
        }

        static IList<Case> ExpandCaseItems(IList<Case> cases) {
            var expanded = from @case in cases
                           select @case with { List = Expand(@case.List) };
            return expanded.ToList();

            static IList<Item> Expand(IList<Item> items) {
                var expanded = items.SelectMany(item => item.Name switch {
                    "Mitt" => Repeat(new Item("Glove"), 2),
                    "MasterSword" => Repeat(new Item("Sword"), 2),
                    "TwoPowerBombs" => Repeat(new Item("PowerBomb"), 2),
                    _ => Repeat(new Item(item.Name), item.Count),
                });
                return expanded.ToList();
            }
        }

        static IList<Case> CheckProperSubsets(IList<Case> cases) {
            var n = 0;
            foreach (var i in Range(0, cases.Count)) {
                if (cases[i].SetNumber is not null)
                    continue;
                
                var a = cases[i];
                var k = from x in cases.Select((b, j) => (b, j)).Skip(i + 1)
                        where x.b.SetNumber is null
                        select (x.b, x.j,
                                aIsSubset: IsProperSubset(a.List, x.b.List),
                                bIsSubset: IsProperSubset(x.b.List, a.List));
                var (b, j, aIsSubset, bIsSubset) = k.FirstOrDefault(x => x.aIsSubset || x.bIsSubset);
                
                if (b is not null) {
                    n += 1;
                    cases[i] = cases[i] with { SetNumber = n, IsSubset = aIsSubset };
                    cases[j] = cases[j] with { SetNumber = n, IsSubset = bIsSubset };
                }
            }
            return cases;

            static bool IsProperSubset(IList<Item> a, IList<Item> b) {
                var n = a.Intersect(b).Count();
                return n == a.Count && n < b.Count;
            }
        }

        static void WriteWithDuplicatePrefix(string filename, IList<Case> cases) {
            File.WriteAllLines(filename,
                from @case in cases
                let prefix = @case.Duplicate ? "x" : ""
                select $"{prefix,-2}{@case.Line}"
            );
        }

        static void WriteWithSubsetPrefix(string filename, IList<Case> cases) {
            var pad = cases.Max(x => x.SetNumber ?? 0).ToString().Length + 2;
            File.WriteAllLines(filename,
                from @case in cases
                let prefix = @case.SetNumber is not null
                    ? $"{(@case.IsSubset == true ? "<" : ">")}{@case.SetNumber}"
                    : ""
                select $"{prefix.PadRight(pad)}{@case.Line}"
            );
        }

        static void WriteWithoutPrefix(string filename, IList<Case> cases) {
            File.WriteAllLines(filename, from @case in cases select @case.Line);
        }

        record Case(string Line, IList<Item> List) {
            public bool Duplicate { get; init; }
            public int? SetNumber { get; init; }
            public bool? IsSubset { get; init; }
        }

        class Item : IEquatable<Item> {
            
            public string Name { get; init; }
            public int Count { get; init; }

            public Item(string name) : this(name, 1) { }
            public Item(string name, int count) {
                Name = name;
                Count = count;
            }

            static readonly Regex ItemPattern = new(@"(?<name>\w+)(?:\((?<count>\d+)\))?");

            public static IList<Item> Parse(string text) {
                var items = from match in ItemPattern.Matches(text)
                            let name = match.Groups["name"]
                            let count = match.Groups["count"]
                            select count.Success
                                ? new Item(name.Value, int.Parse(count.Value))
                                : new Item(name.Value);
                return items.ToList();
            }

            public bool Equals(Item other) {
                return other is not null && (
                    ReferenceEquals(this, other)
                    || Name == other.Name
                    || Name switch {
                        "Mitt" => other.Name == "Glove",
                        "Glove" => other.Name == "Mitt",
                        "MasterSword" => other.Name == "Sword",
                        "Sword" => other.Name == "MasterSword",
                        "TwoPowerBombs" => other.Name == "PowerBomb",
                        "PowerBomb" => other.Name == "TwoPowerBombs",
                        _ => false,
                    }
                );
            }

            public override bool Equals(object obj) => Equals(obj as Item);
            public override int GetHashCode() {
                return (Name switch {
                    "Mitt" => "Glove",
                    "MasterSword" => "Sword",
                    "TwoPowerBombs" => "PowerBomb",
                    _ => Name,
                }).GetHashCode();
            }
        }

    }

}
