public class Program
{
    public static AppContext AppDB = new();
    public static Repository<Role> RoleRepo = new(AppDB);
    public static Repository<TaskItem> TaskItemRepo = new(AppDB);
    public static Repository<Team> TeamRepo = new(AppDB);
    public static Repository<User> UserRepo = new(AppDB);

    public static void Main()
    {
        // TaskView.PrintTasksDateSplit(DEMO_USER, DEMO_DATA);
        // TaskView.PrintTasksKanBan(DEMO_USER, DEMO_DATA);

        bool ExitProgram = false;

        while (!ExitProgram)
        {
            ExitProgram = MenuView.Main();
        }

        MenuView.GoodByeMessage();
    }
}