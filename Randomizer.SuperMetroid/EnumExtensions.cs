using System;
using System.ComponentModel;

namespace Randomizer.SuperMetroid {

    public static class EnumExtensions {

        public static string GetDescription(this Enum anEnum) {
            var enumType = anEnum.GetType();
            var members = enumType.GetMember(anEnum.ToString());
            if ((members?.Length ?? 0) > 0) {
                var attrs = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((attrs?.Length ?? 0) > 0) {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return anEnum.ToString();
        }

        public static string ToLowerString(this Enum anEnum) {
            return anEnum.ToString().ToLower();
        }

    }

}
