using System.ComponentModel.DataAnnotations;
public class Role : iDatabase, IComparable<Role>
{
    [Key]
    public int ID { get; set; }

    public string Name { get; set; }
    public bool CreatePremission { get; set; }
    public bool ClosePremission { get; set; }

    public int CompareTo(Role? other)
    {
        if (other == null) return 1;
        int compare = CreatePremission.CompareTo(other.CreatePremission);
        if (compare != 0) return compare;
        return ClosePremission.CompareTo(other.ClosePremission);
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