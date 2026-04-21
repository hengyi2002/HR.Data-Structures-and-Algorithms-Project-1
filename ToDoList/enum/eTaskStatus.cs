using Spectre.Console;

public enum eTaskStatus
{
    ToDo,
    Doing,
    Review,
    Complete
}

public class TaskStatus {
    public eTaskStatus eTaskStatus;

    //Color value
    public Color Color
    {
        get
        {
            switch (eTaskStatus)
            {
                case eTaskStatus.Doing:
                    return DoingColor;
                case eTaskStatus.Review:
                    return ReviewColor;
                case eTaskStatus.Complete:
                    return CompleteColor;
                default:
                    return ToDoColor;
            }
        }
    }

    //Constructors
    public TaskStatus(int status=0): this(FromInt(status)) { }

    public TaskStatus(string status="low"): this(FromString(status)) { }

    private TaskStatus(eTaskStatus status) => eTaskStatus = status;

    //Object methods for updating the state
    public void NextState()
    {
        if (((int) eTaskStatus) + 1 <= 3)
        {
            switch (eTaskStatus)
            {
                case eTaskStatus.ToDo:
                    eTaskStatus = eTaskStatus.Doing;
                    break;
                case eTaskStatus.Doing:
                    eTaskStatus = eTaskStatus.Review;
                    break;
                case eTaskStatus.Review:
                    eTaskStatus = eTaskStatus.Complete;
                    break;
            }
        }
    }

    public void PrevState()
    {
        if (((int) eTaskStatus) - 1 >= 0)
        {
            switch (eTaskStatus)
            {
                case eTaskStatus.Complete:
                    eTaskStatus = eTaskStatus.Review;
                    break;
                case eTaskStatus.Review:
                    eTaskStatus = eTaskStatus.Doing;
                    break;
                case eTaskStatus.Doing:
                    eTaskStatus = eTaskStatus.ToDo;
                    break;
            }
        }
    }

    //Static values for TaskPriorities
    public static Color ToDoColor => Color.Aqua;
    public static Color DoingColor => Color.Yellow;
    public static Color ReviewColor => Color.OrangeRed1;
    public static Color CompleteColor => Color.Green;

    //Static methods that can be used for convertion
    private static eTaskStatus FromInt(int status)
    {
        switch (status)
        {
            case 1:
                return eTaskStatus.Doing;
            case 2:
                return eTaskStatus.Review;
            case 3:
                return eTaskStatus.Complete;
            case 0: default:
                return eTaskStatus.ToDo;
        }
    }

    private static eTaskStatus FromString(string status)
    {
        switch (status.ToLower())
        {
            case "doing":
                return FromInt(1);
            case "review":
                return FromInt(2);
            case "complete":
                return FromInt(3);
            case "todo": default:
                return FromInt(0);
        }
    }

    //Override the ToString()
    public override string ToString() => eTaskStatus.ToString();

    public int ToInt() => (int) eTaskStatus;
}