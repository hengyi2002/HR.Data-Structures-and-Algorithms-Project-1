using Spectre.Console;

public static class UserView
{
    public static void Display()
    {
        bool exit = false;

        while (!exit)
        {
            AnsiConsole.Clear();
            PrintUser();
            exit = UserOptions();
        }
    }

    private static void PrintUser()
    {
        User[] users = Program.UserRepo.Read();
        Role[] roles = Program.RoleRepo.Read();

        //Create a new table
        Table table = new Table().Expand();

        //Add columns to the table
        table.AddColumn(new TableColumn("TH_Username").Header("Username"));
        table.AddColumn(new TableColumn("TH_Role").Header("Description"));

        foreach (User user in users)
        {
            Role? role = roles.FirstOrDefault(r => r.ID == user.RoleID);
            table.AddRow(user.Username, role is not null ? role.Name : "No role assigned");
        }

        AnsiConsole.Write(table);
    }

    private static bool UserOptions()
    {
        SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>().Title("What would you like to do?");
        reqPrompt.AddChoice("Create user");
        reqPrompt.AddChoice("Delete user");
        reqPrompt.AddChoice("Update user role");
        reqPrompt.AddChoice("Exit");
    
        switch (AnsiConsole.Prompt(reqPrompt))
        {
            case "Create user":
                // CreateUser();
                return false;
            case "Delete user":
                // DeleteUser();
                return false;
            case "Update user role":
                // UpdateUserRole();
                return false;
            case "Exit":
                return true;
            default:
                return false;
        }
    }

    private static void CreateUser()
    {
        // Implementation for creating a user
    }

    private static void DeleteUser()
    {
        // Implementation for deleting a user
    }

    private static void UpdateUserRole()
    {
        // Implementation for updating a user's role
    }

    
}