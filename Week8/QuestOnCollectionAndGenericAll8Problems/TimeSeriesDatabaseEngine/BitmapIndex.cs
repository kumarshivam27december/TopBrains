using System.Collections.Generic;

namespace TimeSeriesDatabaseEngine;

public class BitmapIndex
{
    public HashSet<int> Indices { get; } = new();
}
