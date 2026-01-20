
public class SortedArrays
{
    public static void Main(string[] args)
    {
        int length1 = Convert.ToInt32(Console.ReadLine());
        int[] arr1 = new int[length1];
        for(int i = 0; i < length1; i++)
        {
            arr1[i] = Convert.ToInt32(Console.ReadLine());
        }
        int length2 = Convert.ToInt32(Console.ReadLine());
        int[] arr2 = new int[length2];
        for(int i = 0; i < length2; i++)
        {
            arr2[i] = Convert.ToInt32(Console.ReadLine()); 
        }

        int[] answer = mergeSortedArr(arr1,arr2);
        foreach (var item in answer)
        {
            Console.WriteLine(item);
        }
    }

    public static T[] mergeSortedArr<T>(T[] arr1,T[] arr2) where T : IComparable<T> 
    {
        T[] mergeArr = new T[arr1.Length+arr2.Length];
        int i = 0;
        int j = 0;
        int k = 0;
        while(i<arr1.Length && j < arr2.Length)
        {
            if (arr1[i].CompareTo(arr2[j]) <= 0)
            {
                mergeArr[k++] = arr1[i++];
            }
            else
            {
                mergeArr[k++] = arr2[j++];
            }
        }
        while (i < arr1.Length)
        {
            mergeArr[k++] = arr1[i++];
        }

        while (j < arr2.Length)
        {
            mergeArr[k++] = arr2[j++];
        }

        return mergeArr;
    }
}