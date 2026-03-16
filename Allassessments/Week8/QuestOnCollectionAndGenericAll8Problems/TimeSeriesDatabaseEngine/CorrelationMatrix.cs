using System.Collections.Generic;

namespace TimeSeriesDatabaseEngine;

public class CorrelationMatrix
{
    private readonly Dictionary<(string, string), double> _matrix = new();

    public double this[string a, string b]
    {
        get => _matrix.TryGetValue((a, b), out var v) ? v : _matrix.TryGetValue((b, a), out v) ? v : 0;
        set { _matrix[(a, b)] = value; _matrix[(b, a)] = value; }
    }
}
