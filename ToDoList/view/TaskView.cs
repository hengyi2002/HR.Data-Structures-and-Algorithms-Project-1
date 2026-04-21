using Spectre.Console;

public static class TaskView
{
    public static void DisplayAll()
    {
        bool exit = false;
        while (!exit) {
            //Clear the console
            AnsiConsole.Clear();
            IMyCollection<TaskItem> taskItems = Utilities.ConvertToMyCollection(Program.TaskItemRepo.Read(), eIMycollectionType.Array);
            PrintTasks(taskItems, "All tasks");

            exit = TaskOptions(taskItems, true);
        }
    }

    public static void DisplayCurrentlyDoing()
    {
        bool exit = false;
        while (!exit)
        {
            //Clear the console
            AnsiConsole.Clear();
            IMyCollection<TaskItem> taskItems = Utilities.ConvertToMyCollection(Program.TaskItemRepo.Read(), eIMycollectionType.Array);
            PrintTasks(taskItems.Filter(t => t.Status == new TaskStatus("Doing")), "Currently doing tasks");

            SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>();
            reqPrompt.AddChoices("Exit");

            if(AnsiConsole.Prompt(reqPrompt) == "Exit") exit = true;
        }
    }

    public static void SelectBoard()
    {
        bool exit = false;
        while (!exit) {
            //Clear the console
            AnsiConsole.Clear();
            IMyCollection<TaskItem> taskItems = Utilities.ConvertToMyCollection(Program.TaskItemRepo.Read(), eIMycollectionType.Array);

            SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>();
            reqPrompt.Title("Which board would you like to view?");
            reqPrompt.AddChoices("ToDo", "Doing", "Review", "Complete", "Exit");
            switch (AnsiConsole.Prompt(reqPrompt))
            {
                case "ToDo":
                    IMyCollection<TaskItem> ToDoItems = taskItems.Filter(t => t.Status == new TaskStatus("ToDo"));
                    PrintTasks(ToDoItems, "ToDo tasks");
                    exit = TaskOptions(ToDoItems, true);
                    break;
                case "Doing":
                    IMyCollection<TaskItem> DoingItems = taskItems.Filter(t => t.Status == new TaskStatus("Doing"));
                    PrintTasks(DoingItems, "Doing tasks");
                    exit = TaskOptions(DoingItems, true);
                    break;
                case "Review":
                    IMyCollection<TaskItem> ReviewItems = taskItems.Filter(t => t.Status == new TaskStatus("Review"));
                    PrintTasks(ReviewItems, "Review tasks");
                    exit = TaskOptions(ReviewItems, true);
                    break;
                case "Complete":
                    IMyCollection<TaskItem> CompleteItems = taskItems.Filter(t => t.Status == new TaskStatus("Complete"));
                    PrintTasks(CompleteItems, "Complete tasks");
                    exit = TaskOptions(CompleteItems, true);
                    break;
                default: case "Exit":
                    exit = true;
                    break;
            }
        }
    }

    private static bool TaskOptions(IMyCollection<TaskItem> taskItems, bool statusChangeOption = false)
    {
        bool isAdmin = Program.appUser is not null && Program.appUser.Role.Name == "Admin";

        SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>().Title("What would you like to do?");
        if (isAdmin)reqPrompt.AddChoice("Create ticket");
        if (taskItems.Count > 0 || isAdmin) reqPrompt.AddChoice("Update ticket");
        if (taskItems.Count > 0 || isAdmin) reqPrompt.AddChoice("Delete ticket");
        if (taskItems.Count > 0 || statusChangeOption) reqPrompt.AddChoice("Change ticket status");
        reqPrompt.AddChoice("Exit");

        switch (AnsiConsole.Prompt(reqPrompt))
        {
            case "Create ticket":
                CreateTicket();
                return false;
            case "Update ticket":
                TaskItem? selectedTicket = selectPrompt(taskItems);
                if (selectedTicket != null)
                {
                    UpdateTicket(selectedTicket);
                }
                return false;
            case "Delete ticket":
                TaskItem? ticketToDelete = selectPrompt(taskItems);
                if (ticketToDelete != null)
                {
                    DeleteTicket(ticketToDelete);
                }
                return false;
            case "Change ticket status":
                TaskItem? selectedChangeTicket = selectPrompt(taskItems);
                if (selectedChangeTicket != null)
                {
                    UpdateStatusTicket(selectedChangeTicket);
                }
                return false;
            case "Exit":
                //Exit loop
                return true;
        }

        return false;
    }

    private static void CreateTicket()
    {
        TaskItem newTicket = createPrompt();
        PrintTasks(newTicket);
        
        if(AreYouSurePrompt("Create"))
        {
            //TODO, Implement create to database
        }
    }

    private static void UpdateTicket(TaskItem TicketToUpdate)
    {
        TaskItem updatedTicket = updatePrompt(TicketToUpdate);
        PrintTasks(updatedTicket);

        if(AreYouSurePrompt("Update"))
        {
            
        }
    }

    private static void UpdateStatusTicket(TaskItem TicketToUpdate)
    {
        TaskItem updatedTicket = updateStatusPrompt(TicketToUpdate);
        PrintTasks(updatedTicket);

        if(AreYouSurePrompt("Update status"))
        {
            
        }
    }

    private static TaskItem updatePrompt(TaskItem toUpdateTI)
    {
        Console.Clear();

        toUpdateTI.Title = AnsiConsole.Ask<string>($"Title ({toUpdateTI.Title}): ", toUpdateTI.Title);
        toUpdateTI.Description = AnsiConsole.Ask<string>($"Description ({toUpdateTI.Description}): ", toUpdateTI.Description);
        toUpdateTI.Priority = GetTaskPriority(toUpdateTI.Priority);
        
        return toUpdateTI;
    }

    private static TaskItem updateStatusPrompt(TaskItem toUpdateTI)
    {
        Console.Clear();

        string updateValue = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title($"Status ({toUpdateTI.Status}): ")
            .AddChoices("ToDo", "Doing", "Review", "Complete"));
        
        switch (updateValue)
        {
            case "ToDo":
                toUpdateTI.Status = new TaskStatus("ToDo");
                break;
            case "Doing":
                toUpdateTI.Status = new TaskStatus("Doing");
                break;
            case "Review":
                toUpdateTI.Status = new TaskStatus("Review");
                break;
            case "Complete":
                toUpdateTI.Status = new TaskStatus("Complete");
                break;
        }

        return toUpdateTI;
    }

    private static TaskItem createPrompt()
    {
        Console.Clear();

        return new TaskItem
        (
            0,
            AnsiConsole.Ask<string>("Title: "),
            AnsiConsole.Ask<string>("Description: "),
            GetTaskPriority().ToInt(),
            0,
            0,
            Program.appUser is not null ? Program.appUser.ID : null,
            DateTime.Now,
            DateTime.Now
        );
    }

    private static void DeleteTicket(TaskItem toDeleteTI)
    {
        Console.Clear();
        PrintTasks(toDeleteTI);

        if (AreYouSurePrompt("Delete"))
        {
            //TODO, Implement delete from database
        }
    }

    private static TaskItem? selectPrompt(IMyCollection<TaskItem> taskItems)
    {
        string[] options = [];
        foreach (TaskItem task in taskItems)        {
            options = options.Append(task.Title).ToArray();
        }
        options = options.Append("Exit").ToArray();

        string? selectedTicket = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a [green]Ticket[/]")
            .PageSize(10)
            .EnableSearch()
            .SearchPlaceholderText("Type to search ticket titles...")
            .AddChoices(options));

        if (selectedTicket == "Exit") return null;
        
        return Utilities.ToArray(taskItems.Filter(t => t.Title == selectedTicket))[0];
    }

    private static bool AreYouSurePrompt(string action)
    {
        SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>();
        reqPrompt.Title($"Are you sure you want to {action} this ticket?");
        reqPrompt.AddChoices(action, "Cancel");

        if(AnsiConsole.Prompt(reqPrompt) == action) return true;
        return false;
    }

    private static TaskPriority GetTaskPriority(TaskPriority? taskPriority = null)
    {
        TaskPriority? returnValue = null;

        SelectionPrompt<string> prompt = new SelectionPrompt<string>()
            .AddChoices("Low", "Medium", "High");

        prompt.Title(taskPriority is null ? "Priority: " : $"Priority ({taskPriority}): ");

        switch(AnsiConsole.Prompt(prompt))
        {
            case "Low":
                returnValue = new TaskPriority(0);
                break;
            case "Medium":
                returnValue = new TaskPriority(1);
                break;
            case "High":
                returnValue = new TaskPriority(2);
                break;
        }
        if (returnValue is not null)
        {
            AnsiConsole.WriteLine(taskPriority is null ? $"Priority: {returnValue}" : $"Priority ({taskPriority.ToString}): {returnValue}");
            return returnValue;
        }

        return new TaskPriority(0);
    }

    private static void PrintTasks(IMyCollection<TaskItem> taskItems, string? headerText = null)
    {
        //Create a new table
        Table table = new Table().Expand();

        //Add columns to the table
        table.AddColumn(new TableColumn("TH_Title").Header("Title"));
        table.AddColumn(new TableColumn("TH_Description").Header("Description"));
        table.AddColumn(new TableColumn("TH_Priority").Header("Priority"));
        table.AddColumn(new TableColumn("TH_Status").Header("Status"));
        table.AddColumn(new TableColumn("TH_Created").Header("Created"));
        table.AddColumn(new TableColumn("TH_Updated").Header("Updated"));

        foreach (TaskItem task in taskItems)
        {
            //DEBUG
            // Console.WriteLine(task.ToString());

            table.AddRow(
                new Text($"{task.Title}"),
                new Text($"{task.Description}"),
                new Text($"{task.Priority_String}", task.Priority_Color),
                new Text($"{task.Status_String}", task.Status_Color),
                new Text($"{Utilities.DTToDisplaySTR(task.CreateDateTime)}"),
                new Text($"{Utilities.DTToDisplaySTR(task.UpdateDateTime)}")
            );
        }

        //Create a new panel in which we will add our table
        Panel panel = new Panel(table);
        if (headerText is not null)
        {
            panel.Header(headerText, Justify.Center);
        }
        
        //Print the new UI
        AnsiConsole.Write(panel);
    }

    private static void PrintTasks(TaskItem taskItems, string? headerText = null)
    {
        //Create a new table
        Table table = new Table().Expand();

        //Add columns to the table
        table.AddColumn(new TableColumn("TH_Title").Header("Title"));
        table.AddColumn(new TableColumn("TH_Description").Header("Description"));
        table.AddColumn(new TableColumn("TH_Priority").Header("Priority"));
        table.AddColumn(new TableColumn("TH_Status").Header("Status"));
        table.AddColumn(new TableColumn("TH_Created").Header("Created"));
        table.AddColumn(new TableColumn("TH_Updated").Header("Updated"));

        table.AddRow(
            new Text($"{taskItems.Title}"),
            new Text($"{taskItems.Description}"),
            new Text($"{taskItems.Priority_String}", taskItems.Priority_Color),
            new Text($"{taskItems.Status_String}", taskItems.Status_Color),
            new Text($"{Utilities.DTToDisplaySTR(taskItems.CreateDateTime)}"),
            new Text($"{Utilities.DTToDisplaySTR(taskItems.UpdateDateTime)}")
        );

        //Create a new panel in which we will add our table
        Panel panel = new Panel(table);
        if (headerText is not null)
        {
            panel.Header(headerText, Justify.Center);
        }
        
        //Print the new UI
        AnsiConsole.Write(panel);
    }

    private static void PrintTasksDateSplit(User? currUser, IMyCollection<TaskItem> taskItems)
    {
        //Clear the console
        AnsiConsole.Clear();

        IMyCollection<TaskItem>[] TaskItems = [
            taskItems.Filter(t => t.Status.eTaskStatus == eTaskStatus.Complete), 
            taskItems.Filter(t => t.Status.eTaskStatus == eTaskStatus.Doing),
            taskItems.Filter(t => t.Status.eTaskStatus == eTaskStatus.ToDo)
        ];
        Table?[] TablesToView = [null, null, null];
        Panel?[] PanelsToView = [null, null, null];
        string[] Headers = ["Title", "Description", "Priority", "Status", "Created", "Updated"];

        if (TaskItems[0].Count > 0) TablesToView[0] = _mBasicTablePrefab(Headers);
        if (TaskItems[1].Count > 0) TablesToView[1] = _mBasicTablePrefab(Headers);
        if (TaskItems[2].Count > 0) TablesToView[2] = _mBasicTablePrefab(Headers);

        int currPos = 0;

        foreach (IMyCollection<TaskItem> tasks in TaskItems)
        {
            if (tasks.Count > 0 && TablesToView[currPos] is not null)
            {
                //Put the tickets into the correct table
                foreach (TaskItem task in tasks)
                {
                    //Create a new row, and add in the correct values to the right spot
                    TablesToView[currPos].AddRow(
                        new Text($"{task.Title}"),
                        new Text($"{task.Description}"),
                        new Text($"{task.Priority_String}", task.Priority_Color),
                        new Text($"{task.Status_String}", task.Status_Color),
                        new Text($"{Utilities.DTToDisplaySTR(task.CreateDateTime)}"),
                        new Text($"{Utilities.DTToDisplaySTR(task.UpdateDateTime)}")
                    );    
                }

                //Create a new panel in which we will add our table
                PanelsToView[currPos] = new Panel(TablesToView[currPos]);

                string tableState = currPos switch
                {
                    0 => "Previouse",
                    1 => "Current (today + 7 days ahead)",
                    2 => "Upcoming (+7 days ahead)"
                };

                PanelsToView[currPos].Header(
                    currUser is null ? $"[{tableState}] - all tasks" : $"[{tableState}] - {currUser.FullName}"
                    ,Justify.Center
                );
            }

            currPos++;
        }

        bool exit = false;
        while (!exit) {
            
            SelectionPrompt<string> reqPrompt = new SelectionPrompt<string>().Title("Which tickets do you wish to see");

            if (PanelsToView[0] is not null) reqPrompt.AddChoice("Previouse");
            if (PanelsToView[1] is not null) reqPrompt.AddChoice("Current");
            if (PanelsToView[2] is not null) reqPrompt.AddChoice("Upcoming");
            reqPrompt.AddChoice("Exit");

            string selectedOption = AnsiConsole.Prompt(reqPrompt);

            switch (selectedOption)
            {
                case "Previous":
                    //Print the new UI
                    AnsiConsole.Write(PanelsToView[0]);
                    break;
                case "Current":
                    //Print the new UI
                    AnsiConsole.Write(PanelsToView[1]);
                    break;
                case "Upcoming":
                    //Print the new UI
                    AnsiConsole.Write(PanelsToView[2]);
                    break;
                case "Exit":
                    //Exit loop
                    exit = true;
                    break;
            }
            if (exit == false) AnsiConsole.Ask<string>("Press enter to continue");
        }   
    }

    public static void PrintTasksKanBan(User? currUser, IMyCollection<TaskItem> taskItems)
    {
        //Clear the console
        AnsiConsole.Clear();

        //Create a new table
        Table table = new Table().Expand();

        //Add columns to the table
        table.AddColumn(new TableColumn("TH_ToDo").Header(new Text("To Do", TaskStatus.ToDoColor)));
        table.AddColumn(new TableColumn("TH_Doing").Header(new Text("Doing", TaskStatus.DoingColor)));
        table.AddColumn(new TableColumn("TH_Review").Header(new Text("Review", TaskStatus.ReviewColor)));
        table.AddColumn(new TableColumn("TH_Complete").Header(new Text("Complete", TaskStatus.CompleteColor)));

        TaskItem[] BacklogItems = Utilities.ToArray(taskItems.Filter(t => t.Status == new TaskStatus("ToDo")));
        TaskItem[] DoingItems = Utilities.ToArray(taskItems.Filter(t => t.Status == new TaskStatus("Doing")));
        TaskItem[] ReviewItems = Utilities.ToArray(taskItems.Filter(t => t.Status == new TaskStatus("Review")));
        TaskItem[] CompleteItems = Utilities.ToArray(taskItems.Filter(t => t.Status == new TaskStatus("Complete")));

        int maxItems = new int[] { BacklogItems.Length, DoingItems.Length, ReviewItems.Length, CompleteItems.Length }.Max();

        for (int i = 0; i < maxItems; i++)
        {
            table.AddRow(
                i < BacklogItems.Length ? new Text($"{BacklogItems[i].Title}\n{BacklogItems[i].Description}", BacklogItems[i].Priority_Color) : new Text(""),
                i < DoingItems.Length ? new Text($"{DoingItems[i].Title}\n{DoingItems[i].Description}", DoingItems[i].Priority_Color) : new Text(""),
                i < ReviewItems.Length ? new Text($"{ReviewItems[i].Title}\n{ReviewItems[i].Description}", ReviewItems[i].Priority_Color) : new Text(""),
                i < CompleteItems.Length ? new Text($"{CompleteItems[i].Title}\n{CompleteItems[i].Description}", CompleteItems[i].Priority_Color) : new Text("")
            );
        }

        //Create a new panel in which we will add our table
        Panel panel = new Panel(table);

        //Set the Header
        if (currUser is not null)
        {
            panel.Header($"{currUser.FirstName} {currUser.LastName} - Tickets", Justify.Center);
        }
        else
        {
            panel.Header("all tasks", Justify.Center);
        }

        //Print the new UI
        AnsiConsole.Write(panel);
    }

    private static Table _mBasicTablePrefab(string[] headers)
    {
        //Create a new table
        Table table = new Table().Expand();

        foreach (string header in headers)
        {
            table.AddColumn(new TableColumn($"TH_{header}").Header(header));
        }

        return table;
    }
}