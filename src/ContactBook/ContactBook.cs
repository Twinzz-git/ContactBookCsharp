
using System.Drawing;
using System.Text.RegularExpressions;

namespace ContactBook;

public class ContactBook
{
    public const string YES = "Y";
    public const string NO = "N";

    public readonly string[] YES_NO = new string[] { YES, NO };
    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
    public const string GOTO_PAGE = "G";
    public const string PAGE_SIZE = "S";
    public const string CREATE_CONTACT = "C";
    public const string REVIEW_CONTACT = "R";
    public const string UPDATE_CONTACT = "U";
    public const string DELETE_CONTACT = "D";
    public const string FIND_CONTACTS = "F";
    public const string ORDER_CONTACTS = "O";
    public const string DEDUPLICATE_CONTACTS = "M";
    public const string EXIT = "X";

    public readonly string[] COMMANDS = new string[]
    {
    NEXT_PAGE ,PREV_PAGE,
    GOTO_PAGE ,PAGE_SIZE ,CREATE_CONTACT ,
    REVIEW_CONTACT ,UPDATE_CONTACT,
    DELETE_CONTACT,FIND_CONTACTS ,ORDER_CONTACTS ,
    DEDUPLICATE_CONTACTS,EXIT ,

    };

    private List<Contact> allContacts;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
    }

    public void Start()
    {
        ShowWelcomeScreen();
        string input;

        do
        {
            ShowContact();
            ShowInputOptions();
            input = GetInput();

            if (IsValidInput(input))
                ProcessInput(input);
        }
        while (!ConfirmExit());

        ShowExitScreen();
    }

    private void ProcessInput(string input)
    {
        switch (input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE: PrePage(); break;
            case GOTO_PAGE: GotoPage(); break;
            case PAGE_SIZE: PageSize(); break;
            case CREATE_CONTACT: CreateContacts(); break;
            case REVIEW_CONTACT: ReviewContacts(); break;
            case UPDATE_CONTACT: UpdateContacts(); break;
            case DELETE_CONTACT: DeleteContacts(); break;
            case FIND_CONTACTS: FindContacts(); break;
            case ORDER_CONTACTS: OrderContacts(); break;
            case DEDUPLICATE_CONTACTS: DuplicateContacts(); break;
            case EXIT: Exit(); break;
            default: break;


        }
    }


    private bool ConfirmExit()
    {
        return Confirm("Do you want to exit?", NO);
    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine(" Thank you for using Samuel Contact Book");
    }

    private bool IsValidInput(string input) => COMMANDS.Contains(input.ToUpper());

    private void ShowInputOptions()
    {
        Console.WriteLine();
        Console.WriteLine($"[{NEXT_PAGE}] Next  [{PREV_PAGE}] Prev  [{GOTO_PAGE}] Go to page  [{PAGE_SIZE}] Page size  [{CREATE_CONTACT}] Create");
        Console.WriteLine($"[{REVIEW_CONTACT}] Review  [{UPDATE_CONTACT}] Update  [{DELETE_CONTACT}] Delete  [{FIND_CONTACTS}] Find  [{ORDER_CONTACTS}] Order  [{DEDUPLICATE_CONTACTS}] Merge  [{EXIT}] Exit");

        Console.Write("Command: ");
    }

    private string GetInput()
    {
        string input = Console.ReadLine()?.Trim().ToUpper() ?? "";
        if (IsValidInput(input))
            return input;
        else
        {
            Console.WriteLine($"'{input}' is not a valid command. Please try again.");
            return input;
        }
    }

    private void ShowContact()
    {
        if (allContacts.Count <= 0)
        {
            Console.WriteLine("No contact found.");
        }

        else
        {
            int indexCol = -allContacts.Count.ToString().Length;
            int fnameCol = -Math.Max(allContacts.Max(c => c.GetFName()?.Length ?? 0), "First Name".Length);
            int lnameCol = -Math.Max(allContacts.Max(c => c.GetLName()?.Length ?? 0), "Last Name".Length);
            int phoneCol = -Math.Max(allContacts.Max(c => c.GetPhone()?.Length ?? 0), "Phone".Length);
            int emailCol = -Math.Max(allContacts.Max(c => c.GetEmail()?.Length ?? 0), "Email".Length);

            Console.WriteLine(string.Format(""
                + "{0," + indexCol + "}  "
                + "{1," + fnameCol + "}  "
                + "{2," + lnameCol + "}  "
                + "{3," + phoneCol + "}  "
                + "{4," + emailCol + "}",
                "#", "First Name", "Last Name", "Phone", "Email"));

            Console.WriteLine(new string('-', Math.Abs(indexCol) + Math.Abs(fnameCol) + Math.Abs(lnameCol) + Math.Abs(phoneCol) + Math.Abs(emailCol) + 8));
            int page = 1;
            int size = 10;
            int n = allContacts.Count;
            int pagecount = (int)Math.Max(1, Math.Ceiling(n / (double)size));
            int s = Math.Clamp((page - 1) * size, 0, n); ;
            int e = Math.Clamp(s + size, 0, n);

            for (int i = s; i < e; i++)
            {
                Contact c = allContacts[i];
                Console.WriteLine(string.Format(""
                    + "{0," + indexCol + "}  "
                    + "{1," + fnameCol + "}  "
                    + "{2," + lnameCol + "}  "
                    + "{3," + phoneCol + "}  "
                    + "{4," + emailCol + "}",
                    (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail()));
            }

            Console.WriteLine();
            Console.WriteLine($"Page {page} of {pagecount} ({s + 1}-{e} of {n})");
        }
    }

    private void PressEnterToContinue()
    {
        Console.Write("Press ENTER to continue.");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

    }



    private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welconme to Eli Samuel Contact Book!");
        PressEnterToContinue();
    }

    private void NextPage()
    {
        Console.WriteLine("Next page.");
    }

    private void PrePage()
    {
        Console.WriteLine("Previous page.");
    }

    private void GotoPage()
    {
        Console.WriteLine("Go to page.");
    }

    private void PageSize()
    {
        Console.WriteLine("Page size.");
    }

    private void CreateContacts()
    {
        Console.WriteLine("Create contact.");
    }

    private void ReviewContacts()
    {
        Console.WriteLine("Review contact.");
    }

    private void UpdateContacts()
    {
        Console.WriteLine("Update contact.");
    }

    private void DeleteContacts()
    {
        Console.WriteLine("Delete contact.");
    }

    private void FindContacts()
    {
        Console.WriteLine("Find contacts.");
    }

    private void OrderContacts()
    {
        Console.WriteLine("Order contacts.");
    }

    private void DuplicateContacts()
    {
        Console.WriteLine("Merge duplicate contacts.");
    }

    private void Exit()
    {
        Console.WriteLine("Exit.");
    }

    private string GetOptions(string prompt, string[] validOption, string defaultOption)
    {
        string options = String.Join('/', validOption);

        Console.Write(prompt + $" [{options}] ({defaultOption})");

        string option = Console.ReadLine()!.ToUpper();

        if (string.IsNullOrWhiteSpace(option))
        {
            option = defaultOption;

        }
        while (!validOption.Contains(option))
        {
            Console.WriteLine("ERROR: Invalid option. Please try again.");
            Console.Write(prompt + $" [{options}] ({defaultOption})");
            Console.Write(prompt);

            option = Console.ReadLine()!.ToUpper();

            if (string.IsNullOrWhiteSpace(option)) { option = defaultOption; }

        }

        return option;
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOptions(prompt, YES_NO, defaultOption) == YES;
    }
}