
using System.Drawing;
using System.Text.RegularExpressions;

namespace ContactBook;

public class ContactBook
{
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

            do
            {
                ShowInputOptions();
                input = GetInput();
            }
            while (!IsValidInput(input));

            ProcessInput(input);
        }
        while (!ConfirmExit());

        ShowExitScreen();
    }

    private void ProcessInput(string input)
    {
        throw new NotImplementedException();
    }

    private bool ConfirmExit()
    {
        throw new NotImplementedException();
    }

    private void ShowExitScreen()
    {
    }

    private bool IsValidInput(string input)
    {
        throw new NotImplementedException();
    }

    private void ShowInputOptions()
    {
        throw new NotImplementedException();
    }

    private string GetInput()
    {
        return "";
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
                Console.WriteLine(""
                + "{ 0," + indexCol + " }  "
                + "{ 1," + fnameCol + " }  "
                + "{ 2," + lnameCol + " }  "
                + "{ 3," + phoneCol + " }  "
                + "{ 4," + emailCol + " }  ",
                    (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail());

                Console.WriteLine();
                Console.WriteLine($"Page {page} of {pagecount} ( {s + 1}- {e} of {n})");
            }
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
}
