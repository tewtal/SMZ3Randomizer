using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static Randomizer.SMZ3.ItemType;

namespace Randomizer.SMZ3.Tests.Logic {

    public class Case : IEnumerable<Preparation> {

        public class Builder : DynamicObject {

            readonly IList<Preparation> list = new List<Preparation>();

            bool has;
            string atLocation;

            bool assumed;

            Skip skip = Skip.None;
            List<InventoryItem> subjects;

            enum Skip {
                None,
                Which,
                Also,
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result) {
                result = Add(binder.Name);
                return true;
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {
                result = Add(binder.Name, count: (int) args[0]);
                return true;
            }

            dynamic Add(string type, int count = 1) {
                if (has) {
                    list.Add(NewHasItem());
                } else if (skip == Skip.Which) {
                    subjects ??= new();
                    subjects.Add(NewInventoryItem());
                } else if (skip == Skip.Also) {
                    foreach (var subject in subjects) {
                        list.Add(subject);
                        subject.AlsoSkip = NewInventoryItem();
                    }
                    subjects = null;
                    skip = Skip.None;
                } else {
                    list.Add(NewInventoryItem());
                }
                has = false;
                atLocation = null;
                assumed = false;
                return this;

                LocationItem NewHasItem() => new() { ItemName = type, At = atLocation };
                InventoryItem NewInventoryItem() => new() { ItemName = type, Count = count, Assumed = assumed };
            }

            public dynamic HasAt(string location) {
                atLocation = location;
                return Has;
            }

            public dynamic Has {
                get {
                    has = true;
                    return this;
                }
            }

            public dynamic Assume {
                get {
                    assumed = true;
                    return this;
                }
            }

            public dynamic IfSkipping {
                get {
                    skip = Skip.Which;
                    return this;
                }
            }

            public dynamic AlsoSkip {
                get {
                    skip = Skip.Also;
                    return this;
                }
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

        internal IEnumerable<int> IndicesToSkip
            => from preparation in preparations.Select((v, i) => (v, i))
               where preparation.v is not InventoryItem x || x.Assumed == false
               select preparation.i;

        internal Progression Prepare(World world, string name, int? skip = null) {
            var pool = new List<Item>();
            foreach (var preparation in preparations) {
                preparation.Implement(world, name, pool);
            }
            if (skip != null) {
                preparations.ElementAt((int) skip).Skip(world, name, pool);
            }
            return new Progression(pool);
        }

        public IEnumerator<Preparation> GetEnumerator() => preparations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    public abstract class Preparation {

        public string ItemName { get; set; }

        internal abstract void Implement(World world, string location, List<Item> pool, bool skipOne = false);
        internal abstract void Skip(World world, string location, List<Item> pool);
        public abstract string DescribeSkipped();

    }

    public class LocationItem : Preparation {

        public string At { get; set; }

        internal override void Implement(World world, string location, List<Item> pool, bool skipOne = false) {
            var type = Enum.Parse<ItemType>(ItemName);
            world.Locations.Get(At ?? location).Item = new Item(type, world);
        }

        internal override void Skip(World world, string location, List<Item> pool) {
            world.Locations.Get(At ?? location).Item = null;
        }

        public override string DescribeSkipped() {
            return $"Without a {ItemName} at {(At is null ? "this location" : $"\"{At}\"")}";
        }

        public override string ToString() {
            return At is null ? $"Has {ItemName}" : $"Has {ItemName} At \"{At}\"";
        }

    }

    public class InventoryItem : Preparation {

        public int Count { get; set; } = 1;
        public bool Assumed { get; set; }
        public InventoryItem AlsoSkip { get; set; }

        static readonly Regex showCountPattern = new(@"^(Key|ETank)");

        internal override void Implement(World world, string location, List<Item> pool, bool skipOne = false) {
            var type = ItemType;
            pool.AddRange(
                from itemType in Repeat(type, ItemCount.all)
                select new Item(itemType, world)
            );
        }

        internal override void Skip(World world, string location, List<Item> pool) {
            var type = ItemType;
            pool.RemoveAll(x => x.Type == type);
            pool.AddRange(
                from itemType in Repeat(type, ItemCount.skipOne)
                select new Item(itemType, world)
            );
            if (AlsoSkip != null) {
                AlsoSkip.SkipAll(pool);
            }
        }

        void SkipAll(List<Item> pool) {
            var type = ItemType;
            var items = pool.Where(x => x.Type == type).Take(Count).ToList();
            foreach (var item in items) {
                pool.Remove(item);
            }
        }

        ItemType ItemType => ItemName switch {
            "Glove" => ProgressiveGlove,
            "Mitt" => ProgressiveGlove,
            "Sword" => ProgressiveSword,
            "MasterSword" => ProgressiveSword,
            "MirrorShield" => ProgressiveShield,
            "TwoPowerBombs" => PowerBomb,
            _ => Enum.Parse<ItemType>(ItemName),
        };

        (int all, int skipOne) ItemCount => ItemName switch {
            "Glove" => (1, 0),
            "Mitt" => (2, 1),
            "Sword" => (1, 0),
            "MasterSword" => (2, 1),
            "MirrorShield" => (3, 0),
            "TwoPowerBombs" => (2, 1),
            _ => (Count, Count - 1),
        };

        public override string DescribeSkipped() {
            var parts = new[] {
                ItemName switch {
                    "Mitt" => "With only Glove",
                    "MasterSword" => "With only Sword",
                    "TwoPowerBombs" => "With only one PB pack",
                    _ when Count > 1 => $"With only {Count - 1} {ItemName}",
                    _ => $"Without {ItemName}",
                },
                AlsoSkip is null ? null : $"and {AlsoSkip.Count} fewer {AlsoSkip.ItemName}",
            };
            return string.Join(" ", parts.Where(x => x != null));
        }

        public override string ToString() {
            var parts = new[] {
                Assumed ? "Assumed" : null,
                ItemName,
                Count > 1 || showCountPattern.IsMatch(ItemName) ? $"{Count}" : null,
            };
            return string.Join(" ", parts.Where(x => x != null));
        }

    }

}
