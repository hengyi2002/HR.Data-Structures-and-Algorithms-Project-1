public static class Utilities
{
    public static DateTime DTFromSTR(string datetimeString) =>
        DateTime.ParseExact(datetimeString, "yyyy-MM-dd HH:mm:ss,fff",
                            System.Globalization.CultureInfo.InvariantCulture);

    public static string STRFromDT(DateTime datetime) =>
        datetime.ToString("yyyy-MM-dd HH:mm:ss,fff");

    public static string DTToDisplaySTR(DateTime datetime) =>
        datetime.ToString("yyyy-MM-dd HH:mm:ss");

    public static IMyCollection<T> ConvertToMyCollection<T>(T[] array, eIMycollectionType collectionType) where T : iDatabase
    {

        IMyCollection<T> collection;
        switch (collectionType)
        {
            case eIMycollectionType.LinkedList:
                collection = new MyLinkedList<T>();
                break;
            case eIMycollectionType.Array:
                collection = new MyArray<T>();
                break;
            case eIMycollectionType.Hashmap:
                collection = new MyHashmap<T>();
                break;
            case eIMycollectionType.BinarySearchTree:
                collection = new MyLinkedList<T>();
                break;
            default:
                throw new ArgumentException("Invalid collection type");
        }

        foreach (T item in array)
        {
            collection.Add(item);
        }
        return collection;
    }

    public static T[] ToArray<T>(IMyCollection<T> values)
    {
        T[] array = new T[values.Count];
        int index = 0;
        foreach (T value in values)
        {
            array[index++] = value;
        }
        return array;
    }
}