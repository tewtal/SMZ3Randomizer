using System;

namespace Randomizer.SMZ3 {

    class HexGuid {
        public Guid Guid { get; } = Guid.NewGuid();
        public override string ToString() => Guid.ToString().Replace("-", "");
        public static implicit operator string(HexGuid hexGuid) => hexGuid.ToString();
    }

}
