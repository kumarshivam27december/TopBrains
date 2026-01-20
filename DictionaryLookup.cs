using System.Collections;
using System.Collections.Generic;

public class DictionaryLookup
{
    public static void Main(string[] args)
    {
        int[] ids = {1,4,5};
        Dictionary<int,int> newDictionary = new Dictionary<int,int>()
        {
            {1,20000},
            {2,40000},
            {3,15000}
        };

        int totalSum = 0;
        foreach (KeyValuePair<int,int> keyValuePair in newDictionary)
        {
            totalSum+=(keyValuePair.Value);
        }

        Console.WriteLine(totalSum);

    }
}