using static System.Console;
using DevTeams.Repository.DeveloperRepository;
using DevTeams.Repository.DeveloperTeamRepository;
using DevTeams.Data.Entities;
using System.Drawing;

public class ProgramUI
{
    private readonly DevRepository _devRepo = new DevRepository();
    private readonly DevTeamRepository _devTeamRepo = new DevTeamRepository();
    private bool IsRunning = true;
    public void RunApplication()
    {
        Seed();
        Run();
    }

    private void Run()
    {
        while (IsRunning)
        {
            Clear();
            WriteLine("Welcome to Dev-Teams\n" +
                      "Please make a selection\n" +
                      "1.  Add Developer\n" +
                      "2.  Get All Developers\n" +
                      "3.  Get Developer By Id\n" +
                      "4.  Edit Existing Developer\n" +
                      "5.  Delete Existing Developer\n" +
                      "===============================\n" +
                      "6.  Add Developer Team\n" +
                      "7.  Add Developer to Existing Team\n" +
                      "8.  Get All Developer Teams\n" +
                      "9.  Get Developer Team By Id\n" +
                      "10. Edit Existing Developer Team \n" +
                      "11. Delete Existing Developer Team \n" +
                      "==============  Challenge(s) =============\n" +
                      "12. Developers That need a Pluralsight license \n" +
                      "13. Add Multiple Developers to a Team\n" +
                      "0.  Close Application\n");

            var userInput = int.Parse(ReadLine()!);

            switch (userInput)
            {
                case 1:
                    AddDeveloper();
                    break;
                case 2:
                    GetAllDevelopers();
                    break;
                case 3:
                    GetDeveloperById();
                    break;
                case 4:
                    EditExistingDeveloper();
                    break;
                case 5:
                    DeleteExistingDeveloper();
                    break;
                case 6:
                    AddDeveloperTeam();
                    break;
                case 7:
                    AddDeveloperToExistingTeam();
                    break;
                case 8:
                    GetAllDeveloperTeams();
                    break;
                case 9:
                    GetDeveloperTeamById();
                    break;
                case 10:
                    EditExistingDeveloperTeam();
                    break;
                case 11:
                    DeleteExistingDeveloperTeam();
                    break;
                case 12:
                    DevelopersThatNeedAPluralsightLicense();
                    break;
                case 13:
                    AddMultipleDevelopersToATeam();
                    break;
                case 0:
                    IsRunning = CloseApplication();
                    break;
                default:
                    WriteLine("Invalid Selection, Press any key to continue.");
                    ReadKey();
                    break;
            }
        }
    }

    private void GetDeveloperById()
    {
        Clear();

        RetriveDevListingData();

        WriteLine("Please Select a Developer By Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        Developer dev = RetriveDevData(userInputDevId);

        if (dev == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            Clear();
            DisplayDevInfo(dev!);
        }

        PressAnyKeyToContinue();
    }

    private Developer RetriveDevData(int userInputDevId)
    {
        //* access the database to get a single developer
        Developer dev = _devRepo.GetDeveloperById(userInputDevId);
        return dev;
    }
    private DeveloperTeam RetriveDevTeamData(int userInputDevId)
    {
        //* access the database to get a single developer
        DeveloperTeam devTeam = _devTeamRepo.GetDeveloperTeamById(userInputDevId);
        return devTeam;
    }
    private void DisplayDataValidationError(int userInputIdValue)
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine($"Invalid Id Entry: {userInputIdValue}!");
        ResetColor();
        return; //* this is to prevent any exceptions from being thrown! we will "jump out of this method"
    }

    private void EditExistingDeveloper()
    {
        Clear();
        RetriveDevListingData();

        WriteLine("Please select a Developer by Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        Developer devDataFromDb = RetriveDevData(userInputDevId);

        if (devDataFromDb == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            Clear();

            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("== Update Developer Form ==");
            ResetColor();

            Developer newDevData = FillOutDeveloperForm();

            if (_devRepo.UpdateDeveloper(userInputDevId, newDevData))
                WriteLine("Success!");
            else
                WriteLine("Fail!");
        }

        PressAnyKeyToContinue();
    }

    private void DeleteExistingDeveloper()
    {
        Clear();
        RetriveDevListingData();

        WriteLine("Please select a Developer by Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        Developer devDataFromDb = RetriveDevData(userInputDevId);

        if (devDataFromDb == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            if (_devRepo.DeleteDeveloper(devDataFromDb))
                WriteLine("Success!");
            else
                WriteLine("Fail!");

        }
        PressAnyKeyToContinue();
    }

    private void AddDeveloperTeam()
    {
        Clear();
        WriteLine("== Developer Team Form ==");
        DeveloperTeam devTeam = FillOutDeveloperTeamForm();
        PressAnyKeyToContinue();
    }

    private DeveloperTeam FillOutDeveloperTeamForm()
    {
        DeveloperTeam team = new DeveloperTeam();

        Write("Please input a Team name: ");
        team.Name = ReadLine();

        return team;
    }

    private void GetAllDeveloperTeams()
    {
        Clear();
        WriteLine("== Developer Team Listing ==");

        RetriveDevTeamListingData();

        PressAnyKeyToContinue();
    }

    //* This is to display a listing of the Team Data that doesn't have all of the team information
    private void RetriveDevTeamListingData()
    {
        var devTeamsInDb = _devTeamRepo.GetDeveloperTeams();  //* the listing is coming from the database
        if (devTeamsInDb.Count > 0)
        {
            foreach (var devTeam in devTeamsInDb)
            {
                DisplayDevTeamInfoListItem(devTeam);
                WriteLine("============================================\n");
            }
        }
        else
        {
            WriteLine("Sorry there are no available Developer Teams.");
        }
    }

    //* This is to display  the Team Data with all of the team information
    // private void RetriveDevTeamData()
    // {
    //     var devTeamsInDb = _devTeamRepo.GetDeveloperTeams();  //* the listing is coming from the database
    //     if (devTeamsInDb.Count > 0)
    //     {
    //         foreach (var devTeam in devTeamsInDb)
    //         {
    //             DisplayDevTeamInfo(devTeam);
    //             WriteLine("============================================\n");
    //         }
    //     }
    //     else
    //     {
    //         WriteLine("Sorry there are no available Developer Teams.");
    //     }
    // }

    private void DisplayDevTeamInfoListItem(DeveloperTeam devTeam)
    {
        WriteLine($" Id: {devTeam.Id} - Name: {devTeam.Name}\n");
    }

    private void DisplayDevTeamInfo(DeveloperTeam devTeam)
    {
        WriteLine($" Id: {devTeam.Id} - Name: {devTeam.Name}\n" +
                  "=========== Members ============");
        foreach (var member in devTeam.DevsOnTeam)
        {
            DisplayDevInfo(member);
        }
    }

    private void AddDeveloperToExistingTeam()
    {
        Clear();
        bool databaseHasDevTeams = ValidateDevTeamsExistance();
        if (databaseHasDevTeams)
        {
            RetriveDevTeamListingData();

            Write("Please select a Team by Id: ");
            int userInputTeamId = int.Parse(ReadLine()!);
            var devTeam = _devTeamRepo.GetDeveloperTeamById(userInputTeamId);

            bool databaseHasDevelopers = ValidateDevelopersExistance();
            if (databaseHasDevelopers)
            {
                Clear();
                List<Developer> devsNotOnTeam = _devRepo.GetDevelopersNotOnATeam();
                if (devsNotOnTeam.Count() > 0)
                {
                    //* Display the Available developers
                    foreach (var developer in devsNotOnTeam)
                    {
                        DisplayDevInfo(developer);
                    }

                    Write("Please select a Developer by Id: ");

                    int userInputDevId = Convert.ToInt32(ReadLine()!);
                    var selectedDeveloper = _devRepo.GetDeveloperById(userInputDevId);

                    devTeam.DevsOnTeam.Add(selectedDeveloper);
                    WriteLine($"{selectedDeveloper.FullName} has been added to: {devTeam.Name}!");
                }
                else
                {
                    WriteLine("All Devs have been assigned to a team.");
                }
            }
            else
            {
                WriteLine("Sorry there are no available Developers at this time.");
            }
        }
        else
            WriteLine("Sorry there are no available Developer Teams at this time.");

        PressAnyKeyToContinue();
    }

    private bool ValidateDevelopersExistance()
    {
        return _devRepo.GetDevelopers().Count() > 0;
    }

    private bool ValidateDevTeamsExistance()
    {
        return _devTeamRepo.GetDeveloperTeams().Count() > 0;
    }

    private void GetDeveloperTeamById()
    {
        Clear();

        RetriveDevTeamListingData();

        WriteLine("Please Select a Developer Team By Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        DeveloperTeam devTeam = RetriveDevTeamData(userInputDevId);

        if (devTeam == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            Clear();
            DisplayDevTeamInfo(devTeam!);
        }

        PressAnyKeyToContinue();
    }

    private void EditExistingDeveloperTeam()
    {
        Clear();

        RetriveDevTeamListingData();

        WriteLine("Please Select a Developer Team By Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        DeveloperTeam devTeam = RetriveDevTeamData(userInputDevId);

        if (devTeam == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            Clear();
            DeveloperTeam newTeamData = FillOutDeveloperTeamForm();

            if (_devTeamRepo.UpdateDevTeam(devTeam.Id, newTeamData))
                WriteLine("Success!");
            else
                WriteLine("Fail!");
        }

        PressAnyKeyToContinue();
    }

    private void DeleteExistingDeveloperTeam()
    {
        Clear();

        if (_devTeamRepo.GetDeveloperTeams().Count() > 0)
        {
            RetriveDevTeamListingData();

            WriteLine("Please Select a Developer Team By Id.");
            int userInputDevId = int.Parse(ReadLine()!);
            DeveloperTeam devTeam = RetriveDevTeamData(userInputDevId);

            if (devTeam == null)
                DisplayDataValidationError(userInputDevId);
            else
            {

                if (_devTeamRepo.DeleteDevTeam(devTeam))
                    WriteLine("Success!");
                else
                    WriteLine("Fail!");
            }
        }
        else
        {
            WriteLine("Sorry, There are no available Developer Teams");
        }

        PressAnyKeyToContinue();
    }

    private void DevelopersThatNeedAPluralsightLicense()
    {
        Clear();

        if (_devTeamRepo.GetDeveloperTeams().Count() > 0)
        {
            List<Developer> devsWithoutPs = _devRepo.GetDevelopersWithoutPluralsight();
            if (devsWithoutPs.Count() > 0)
            {
                foreach (var dev in devsWithoutPs)
                {
                    DisplayDevInfo(dev);
                }
            }
            else
            {
                WriteLine("All Developers have a Pluralsight license.");
            }
        }
        else
        {
            WriteLine("Sorry, There are no available Developer Teams");
        }

        PressAnyKeyToContinue();
    }

    private void AddMultipleDevelopersToATeam()
    {
        Clear();

        RetriveDevTeamListingData();

        WriteLine("Please Select a Developer Team By Id.");
        int userInputDevId = int.Parse(ReadLine()!);
        DeveloperTeam devTeam = RetriveDevTeamData(userInputDevId);

        if (devTeam == null)
            DisplayDataValidationError(userInputDevId);
        else
        {
            Clear();

            //* Display developers that aren't on a team....
            if (_devRepo.GetDevelopers().Count() > 0)
            {
                bool hasCompletedSelections = false;

                while (hasCompletedSelections == false)
                {
                    Clear();
                    List<Developer> devsNotOnTeam = _devRepo.GetDevelopersNotOnATeam();
                    if (devsNotOnTeam.Count() > 0)
                    {
                        foreach (Developer developer in devsNotOnTeam)
                        {
                            DisplayDevInfo(developer);
                        }

                        WriteLine("Please select a Developer by Id.");
                        int devId = int.Parse(ReadLine()!);
                        Developer selectedDev = _devRepo.GetDeveloperById(devId);

                        if (selectedDev == null)
                            DisplayDataValidationError(userInputDevId);
                        else
                        {
                            if (_devTeamRepo.AddDeveloperToTeam(devTeam.Id, selectedDev))
                                WriteLine("Success!");
                            else
                                WriteLine("Fail!");
                        }

                        WriteLine("Do you want to add another Developer? y/n");

                        string userInputYesOrNo = ReadLine()!;

                        if (userInputYesOrNo.ToLower() == "Y".ToLower())
                        {
                            continue;
                        }
                        else
                        {
                            WriteLine("Team Assignment(s) are Complete!");
                            hasCompletedSelections = true;
                        }
                    }
                    else
                    {
                        WriteLine("All Developers are assigned to a team.");
                        break;  //* exit the while loop w/o having to set 'hasCompletedSelections' to true;
                    }
                }
            }
            else
            {
                WriteLine("There are no Developers Available.");
            }
        }

        PressAnyKeyToContinue();
    }

    private bool CloseApplication()
    {
        Clear();
        return false;
    }

    private void GetAllDevelopers()
    {
        Clear();
        //* Need to access the _devRepo to get the devs!
        WriteLine("== Dev Listing ==");

        RetriveDevListingData();

        ReadKey();
    }

    private void RetriveDevListingData()
    {
        var devsInDb = _devRepo.GetDevelopers();  //* the listing is coming from the database
        if (devsInDb.Count > 0)
        {
            foreach (var dev in devsInDb)
            {
                DisplayDevInfo(dev);
            }
        }
        else
        {
            WriteLine("Sorry there are no available devs.");
        }
    }

    private void DisplayDevInfo(Developer dev)
    {
        WriteLine($" Id: {dev.Id} - Name: {dev.FullName} - Has Pluralsight: {dev.HasPluralsight}");
    }

    private void AddDeveloper()
    {
        Clear();
        Developer devFormData = FillOutDeveloperForm();

        //* Need to access the _devRepo to save the devs to the database!
        if (_devRepo.AddDeveloper(devFormData))
            WriteLine("Success!");
        else
            WriteLine("Fail!");

        PressAnyKeyToContinue();
    }

    private Developer FillOutDeveloperForm()
    {
        Clear();
        Developer devFormData = new Developer();

        //* We'll fill out the form from here.
        Write("First Name: ");
        string userInputFirstName = ReadLine()!;
        devFormData.FirstName = userInputFirstName;

        Write("Last Name: ");
        string userInputLastName = ReadLine()!;
        devFormData.LastName = userInputLastName;

        Write("Has Pluralsight (y/n): ");
        string userInputHasPluralsight = ReadLine()!;
        devFormData.HasPluralsight = CheckYesOrNo(userInputHasPluralsight);

        return devFormData;
    }

    private bool CheckYesOrNo(string yesOrNoValue)
    {
        if (yesOrNoValue.ToLower() == "Y".ToLower())
            return true;
        else
            return false;
    }

    private void PressAnyKeyToContinue()
    {
        WriteLine("Press any key to continue...");
        ReadKey();
    }

    private void Seed()
    {
        //* you have to create them here...
        //* you have to add then to the database here as well...
        Developer devA = new Developer("Bill", "Burr", false);
        Developer devB = new Developer("George", "Carlin", true);
        Developer devC = new Developer("Daymon", "Wayans", true);
        Developer devD = new Developer("Bernie", "Mac", false);
        Developer devE = new Developer("Richard", "Pryor", false);
        Developer devF = new Developer("Janet", "Jackson", true);
        Developer devG = new Developer("Rockwell", "null", false);

        //* adding to the database (developer database)
        _devRepo.AddDeveloper(devA);
        _devRepo.AddDeveloper(devB);
        _devRepo.AddDeveloper(devC);
        _devRepo.AddDeveloper(devD);
        _devRepo.AddDeveloper(devE);
        _devRepo.AddDeveloper(devF);
        _devRepo.AddDeveloper(devG);

        //* Seed some developer teams
        DeveloperTeam fire = new DeveloperTeam("Fire");
        devA.DevTeamId = 1;
        fire.DevsOnTeam.Add(devA);

        DeveloperTeam ice = new DeveloperTeam("Ice");
        devB.DevTeamId = 2;
        ice.DevsOnTeam.Add(devB);

        //* adding to the database (developer team database)
        _devTeamRepo.AddDeveloperTeam(fire);
        _devTeamRepo.AddDeveloperTeam(ice);
    }

}
