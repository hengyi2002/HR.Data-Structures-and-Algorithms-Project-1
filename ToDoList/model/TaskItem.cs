using Spectre.Console;

public class TaskItem : iDatabase, IComparable<TaskItem>
{
    public int ID {get; set;}
    public string Title {get; set;}
    public string Description {get; set;}
    public TaskPriority Priority {get; set;}
    public int Priority_Int => Priority.ToInt();
    public TaskStatus Status {get; set;}
    public int Status_Int => Status.ToInt();
    public String Priority_String => Priority.ToString();
    public String Status_String => Status.ToString(); 
    public Style Priority_Color {
        get { return Priority.Color; }
    }

    public Style Status_Color
    {
        get { return Status.Color; }
    }
    public int? TeamID {get; set;}
    public int? UserID {get; set;}
    public DateTime CreateDateTime {get; set;}
    public DateTime UpdateDateTime {get; set;}

    //Constructors
    // public TaskItem(string title, string description, int priority, int? teamID, int? userID) 
    // : this(0, title, description, new TaskPriority(priority), new TaskStatus("ToDo"), teamID, userID, DateTime.Now, DateTime.Now)
    // {
        
    // }

    public TaskItem(int id, string title, string description, int priority, int status, int? teamID, int? userID, string createDateTime, string updateDateTime) 
    : this(id, title, description, new TaskPriority(priority), new TaskStatus(status), teamID, userID, Utilities.DTFromSTR(createDateTime), Utilities.DTFromSTR(updateDateTime))
    {
        
    }

    public TaskItem(int id, string title, string description, int priority, int status, int? teamID, int? userID, DateTime createDateTime, DateTime updateDateTime) 
    : this(id, title, description, new TaskPriority(priority), new TaskStatus(status), teamID, userID, createDateTime, updateDateTime)
    {
        
    }

    public TaskItem(int id, string title, string description, TaskPriority priority, TaskStatus status, int? teamID, int? userID, DateTime createDateTime, DateTime updateDateTime)
    {
        ID = id;
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        TeamID = teamID;
        UserID = userID;
        CreateDateTime = createDateTime;
        UpdateDateTime = updateDateTime;
    }

    public string ToSQLInsert() =>
        $"";
    
    public string ToSQLUpdate() =>
        $"";

    public string ToSQLDelete() =>
        $"";

    public static string GetAllSQL() =>
        $"";

    public static string GetByIDSQL() =>
        $"";

    public override string ToString()
    {
        return $"Taskitem: {Title}";
    }
    public override int GetHashCode()
    {
        return ID.GetHashCode(); 
    }

    public int CompareTo(TaskItem? other)
    {
        if (other == null) return 1;
        int cmp = Priority_Int.CompareTo(other.Priority_Int);
        if (cmp != 0) return cmp;
        cmp = Status_Int.CompareTo(other.Status_Int);
        if (cmp != 0) return cmp;
        return CreateDateTime.CompareTo(other.CreateDateTime);
    }
}