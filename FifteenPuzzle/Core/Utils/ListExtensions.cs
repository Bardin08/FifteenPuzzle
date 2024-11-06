namespace FifteenPuzzle.Core.Utils;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> ts, int seed = 12345)
    {
        var random = new Random(seed);

        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var idx = random.Next(i, count);
            (ts[i], ts[idx]) = (ts[idx], ts[i]);
        }
    }
}