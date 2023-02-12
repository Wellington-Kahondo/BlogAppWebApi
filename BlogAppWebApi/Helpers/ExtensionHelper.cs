using System.Linq;

namespace BlogAppWebApi.Helpers
{
    public static class ExtensionHelper
    {
        public static bool In<T>(T value, params T[] values) => values.Any(x => x.Equals(value));
        public static bool ValidateProperties<T>(T value, params T[] values) => values.Any(x => x.Equals(value));

        public static bool NullCheck<T>(T value) => value == null;
    }
}
