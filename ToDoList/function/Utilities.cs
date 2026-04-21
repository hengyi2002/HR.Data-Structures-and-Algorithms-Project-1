public static class Utilities
{
    public static DateTime DTFromSTR(string datetimeString) =>
        DateTime.ParseExact(datetimeString, "yyyy-MM-dd HH:mm:ss,fff",
                            System.Globalization.CultureInfo.InvariantCulture);

    public static string STRFromDT(DateTime datetime) =>
        datetime.ToString("yyyy-MM-dd HH:mm:ss,fff");

    public static string DTToDisplaySTR(DateTime datetime) =>
        datetime.ToString("yyyy-MM-dd HH:mm:ss");

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