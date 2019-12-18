namespace Randomizer.SMZ3 {

    public class Version {

        public int Major { get; }
        public int Minor { get; }

        public Version(int major, int minor) {
            Major = major;
            Minor = minor;
        }

        public override string ToString() => $"{Major}.{Minor}";
        public static implicit operator string(Version version) => version.ToString();

    }

}
