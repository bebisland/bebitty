internal static class Codepoints
{
    public static readonly int[] Array;
    public static readonly int   Count;

    private static readonly List<int> List = [];

    static Codepoints()
    {
        AddRange(32, 126); // ASCII

        Array = [.. List];
        Count = Array.Length;
    }

    private static void AddRange(int first, int last)
    {
        for (var codepoint = first; codepoint <= last; codepoint++)
            List.Add(codepoint);
    }
}