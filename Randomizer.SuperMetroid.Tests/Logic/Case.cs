using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static Randomizer.SuperMetroid.ItemType;

namespace Randomizer.SuperMetroid.Tests.Logic {

    public class Case : IEnumerable<Preparation> {

        public class Builder : DynamicObject {

            readonly IList<Preparation> list = new List<Preparation>();

            public override bool TryGetMember(GetMemberBinder binder, out object result) {
                result = Add(binder.Name);
                return true;
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {
                result = Add(binder.Name, count: (int) args[0]);
                return true;
            }

            dynamic Add(string type, int count = 1) {
                list.Add(new() { ItemName = type, Count = count });
                return this;
            }

            public override bool TryConvert(ConvertBinder binder, out object result) {
                if (binder.Type != typeof(Case)) {
                    result = null;
                    return false;
                }
                result = new Case(list);
                return true;
            }

        }

        public static Case WithNothing { get; } = new(Empty<Preparation>());

        readonly IEnumerable<Preparation> preparations;

        Case(IEnumerable<Preparation> preparations) => this.preparations = preparations;

        internal IEnumerable<int> IndicesToSkip => Range(0, preparations.Count());

        internal List<Item> Prepare(World world, int? skip = null) {
            var pool = new List<Item>();
            foreach (var preparation in preparations) {
                preparation.Implement(world, pool);
            }
            if (skip != null) {
                preparations.ElementAt((int) skip).Skip(world, pool);
            }
            return pool;
        }

        public IEnumerator<Preparation> GetEnumerator() => preparations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public class Preparation {

        public string ItemName { get; set; }
        public int Count { get; set; } = 1;

        static readonly Regex showCountPattern = new(@"^(Key|ETank)");

        internal void Implement(World world, List<Item> pool) {
            var type = ItemType;
            pool.AddRange(
                from itemType in Repeat(type, ItemCount.all)
                select new Item(itemType, world)
            );
        }

        internal void Skip(World world, List<Item> pool) {
            var type = ItemType;
            pool.RemoveAll(x => x.Type == type);
            pool.AddRange(
                from itemType in Repeat(type, ItemCount.skipOne)
                select new Item(itemType, world)
            );
        }

        ItemType ItemType => ItemName switch {
            "TwoPowerBombs" => PowerBomb,
            _ => Enum.Parse<ItemType>(ItemName),
        };

        (int all, int skipOne) ItemCount => ItemName switch {
            "TwoPowerBombs" => (2, 1),
            _ => (Count, Count - 1),
        };

        public string DescribeSkipped() {
            return ItemName switch {
                "TwoPowerBombs" => "With only one PB pack",
                _ when Count > 1 => $"With only {Count - 1} {ItemName}",
                _ => $"Without {ItemName}",
            };
        }

        public override string ToString() {
            return $"{ItemName}{(Count > 1 || showCountPattern.IsMatch(ItemName) ? $" {Count}" : "")}";
        }

    }

}
