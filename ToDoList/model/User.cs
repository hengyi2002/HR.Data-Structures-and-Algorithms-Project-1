using System.ComponentModel.DataAnnotations;
public class User : iDatabase
{
    [Key]
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get {return $"{FirstName} {LastName}";}
    }
    public string Age { get; set; }
    public Role Role {get; set; }
    public int RoleID { get; set; }
    public TaskItem Task {get; set; }
    public int TaskID { get; set; }
    public Team? Team { get; set; }
    public int? TeamID { get; set; }

    public string ToSQLDelete()
    {
        throw new NotImplementedException();
    }

    public string ToSQLInsert()
    {
        throw new NotImplementedException();
    }

    public string ToSQLUpdate()
    {
        throw new NotImplementedException();
    }
}