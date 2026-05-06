using Microsoft.EntityFrameworkCore;
using Spectre.Console;

public enum eTaskPriority
{
    Low,
    Medium,
    High
}

[Keyless]
public class TaskPriority
{
    public eTaskPriority eTaskPriority { get; }

    //Color value
    public Color Color
    {
        get
        {
            switch (eTaskPriority)
            {
                case eTaskPriority.Medium:
                    return MediumColor;
                case eTaskPriority.High:
                    return HighColor;
                default:
                    return LowColor;
            }
        }
    }
    
    //Constructors
    public TaskPriority(): this(FromInt(0)) { }
    public TaskPriority(int prio=0): this(FromInt(prio)) { }

    public TaskPriority(string prio="low"): this(FromString(prio)) { }

    private TaskPriority(eTaskPriority prio) => eTaskPriority = prio;

    //Static values for TaskPriorities
    public static Color LowColor => Color.Yellow;
    public static Color MediumColor => Color.OrangeRed1;
    public static Color HighColor => Color.Red;

    //Static methods for data conversion
    public static eTaskPriority FromInt(int value)
    {
        switch (value)
        {
            case 1:
                return eTaskPriority.Medium;
            case 2:
                return eTaskPriority.High;
            case 0: default:
                return eTaskPriority.Low;
        }
    }

    public static eTaskPriority FromString(string value)
    {
        switch (value.ToLower())
        {
            case "high":
                return eTaskPriority.Medium;
            case "medium":
                return eTaskPriority.High;
            case "low": default:
                return eTaskPriority.Low;
        }
    }

    //Override the ToString()
    public override string ToString() => eTaskPriority.ToString();
    public int ToInt() => (int) eTaskPriority;
}