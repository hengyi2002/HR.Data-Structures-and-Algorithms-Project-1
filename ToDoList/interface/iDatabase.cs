public interface iDatabase
{

    public int ID { get; set; }
    public string ToSQLInsert();
    public string ToSQLUpdate();
    public string ToSQLDelete();
}