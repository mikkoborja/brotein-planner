namespace BroTeinPlanner.Utils {
    public static class StringFormatter {
        public static string Truncate(string str, int maxLength) {
            if (str.Length <= maxLength) {
                return str;
            } else {
                return str.Substring(0, maxLength - 3) + "...";
            }
        }
    }
}