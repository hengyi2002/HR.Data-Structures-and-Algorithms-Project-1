using System.ComponentModel.DataAnnotations;
public class Team : iDatabase, IComparable<Team>
{
    [Key]
    public int ID { get; set; }

    public string TeamName { get; set; }
    public User CreateUser { get; set; }
    public int CreateUserID { get; set; }

    public int CompareTo(Team? other)
    {
        if (other == null)
        {
            return 1;
        }

        return string.Compare(TeamName, other.TeamName, StringComparison.OrdinalIgnoreCase);
    }

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