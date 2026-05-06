public class Program
{
    public static AppContext AppDB = new();
    public static Repository<Role> RoleRepo = new(AppDB);
    public static Repository<TaskItem> TaskItemRepo = new(AppDB);
    public static Repository<Team> TeamRepo = new(AppDB);
    public static Repository<User> UserRepo = new(AppDB);

    public static User? appUser = null;

    public static eIMycollectionType collectionType;

    public static void Main()
    {
        int implementation = MenuView.SelectImplementation();
        collectionType = (eIMycollectionType)implementation;

        bool ExitProgram = false;
        int attempts = 0;
        
        while (attempts < 3 && appUser == null)
        {
            appUser = MenuView.Login(attempts);
            if (appUser == null) attempts++;
        }
        if (attempts == 3 && appUser == null) ExitProgram = true;

        while (!ExitProgram)
        {
            ExitProgram = MenuView.Main();
        }

        MenuView.GoodByeMessage();
    }
}