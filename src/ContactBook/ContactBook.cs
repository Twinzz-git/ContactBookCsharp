
using System.Drawing;
using System.Runtime.CompilerServices;
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
    private int page;
    private int size;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
        page = 1;
        size = 10;
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
        while (input != EXIT || !ConfirmExit());

        ShowExitScreen();
    }

    private void ProcessInput(string input)
    {
        switch (input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE: PrevPage(); break;
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
        if (!Console.IsOutputRedirected) Console.Clear();
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
        ShowContacts(allContacts, page, size);
    }
    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        if (!Console.IsOutputRedirected) Console.Clear();
        if (contacts.Count <= 0)
        {
            Console.WriteLine("No contact found.");
        }

        else
        {
            int indexCol = -contacts.Count.ToString().Length;
            int fnameCol = -Math.Max(contacts.Max(c => c.GetFName()?.Length ?? 0), "First Name".Length);
            int lnameCol = -Math.Max(contacts.Max(c => c.GetLName()?.Length ?? 0), "Last Name".Length);
            int phoneCol = -Math.Max(contacts.Max(c => c.GetPhone()?.Length ?? 0), "Phone".Length);
            int emailCol = -Math.Max(contacts.Max(c => c.GetEmail()?.Length ?? 0), "Email".Length);

            Console.WriteLine(string.Format(""
                + "{0," + indexCol + "}  "
                + "{1," + fnameCol + "}  "
                + "{2," + lnameCol + "}  "
                + "{3," + phoneCol + "}  "
                + "{4," + emailCol + "}",
                "#", "First Name", "Last Name", "Phone", "Email"));

            Console.WriteLine(new string('-', Math.Abs(indexCol) + Math.Abs(fnameCol) + Math.Abs(lnameCol) + Math.Abs(phoneCol) + Math.Abs(emailCol) + 8));

            int n = contacts.Count;
            int pagecount = (int)Math.Max(1, Math.Ceiling(n / (double)size));
            int s = Math.Clamp((page - 1) * size, 0, n); ;
            int e = Math.Clamp(s + size, 0, n);

            for (int i = s; i < e; i++)
            {
                Contact c = contacts[i];
                Console.WriteLine(string.Format(""
                    + "{0," + indexCol + "}  "
                    + "{1," + fnameCol + "}  "
                    + "{2," + lnameCol + "}  "
                    + "{3," + phoneCol + "}  "
                    + "{4," + emailCol + "}",
                    (i + 1), c.GetFName(), c.GetLName(), c.GetPhone(), c.GetEmail()));
            }

            Console.WriteLine();
            for (int i = 0; i < size - (e - s); i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine($"Page {page} of {pagecount} ({s + 1}-{e} of {n})");
        }
    }

    private void PressEnterToContinue()
    {
        Console.Write("Press ENTER to continue.");
        if (Console.IsInputRedirected)
        {
            Console.ReadLine();
            return;
        }
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
    }



    private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welconme to Eli Samuel Contact Book!");
        PressEnterToContinue();
    }

    private void NextPage()
    {
        int pagecount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
        if (page < pagecount)
            page++;
    }

    private void PrevPage()
    {
        if (page > 1)
            page--;
    }

    private void GotoPage()
    {
        int pagecount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
        page = Math.Clamp(GetInt("Go to page", 1, pagecount), 1, pagecount);
    }

    private void PageSize()
    {
        size = GetInt("Contacts per page", 1, 100);
        int pagecount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
        page = Math.Clamp(page, 1, pagecount);
    }

    private void CreateContacts()
    {
        Console.WriteLine("--- New Contact ---");
        string fname = GetString("First name");
        string lname = GetString("Last name");
        string phone = GetString("Phone");
        string email = GetString("Email");

        Contact contact = new Contact(fname, lname, phone, email);

        if (Confirm("Save contact?", YES))
        {
            allContacts.Add(contact);
            int pagecount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
            page = pagecount;
        }

        else
        {
            Console.WriteLine("Contact not created");
        }
    }

    private void ReviewContacts()
    {
        if (allContacts.Count == 0)
        {
            Console.WriteLine("No contacts to review.");
            PressEnterToContinue();
            return;
        }
        int index = GetInt("Contact number", 1, allContacts.Count);
        ReviewContact(allContacts[index - 1]);
    }

    private void ReviewContact(Contact contact)
    {
        Console.WriteLine();
        Console.WriteLine("--- Contact Details ---");
        Console.WriteLine($"First Name : {contact.GetFName()}");
        Console.WriteLine($"Last Name  : {contact.GetLName()}");
        Console.WriteLine($"Phone      : {contact.GetPhone()}");
        Console.WriteLine($"Email      : {contact.GetEmail()}");
        Console.WriteLine();
        PressEnterToContinue();
    }

    private void UpdateContacts()
    {
        if (allContacts.Count == 0)
        {
            Console.WriteLine("No contacts to update.");
            PressEnterToContinue();
            return;
        }
        int index = GetInt("Contact number", 1, allContacts.Count);
        UpdateContact(allContacts[index - 1]);
    }

    private void UpdateContact(Contact contact)
    {
        ReviewContact(contact);

        Console.WriteLine("--- Update Contact (press ENTER to keep current value) ---");
        string fname = GetString("First name", contact.GetFName());
        string lname = GetString("Last name", contact.GetLName());
        string phone = GetString("Phone",      contact.GetPhone());
        string email = GetString("Email",      contact.GetEmail());

        if (Confirm("Save changes?", YES))
        {
            contact.SetFName(fname);
            contact.SetLName(lname);
            contact.SetPhone(phone);
            contact.SetEmail(email);
        }
    }

    private void DeleteContacts()
    {
        if (allContacts.Count == 0)
        {
            Console.WriteLine("No contacts to delete.");
            PressEnterToContinue();
            return;
        }
        int index = GetInt("Contact number", 1, allContacts.Count);
        DeleteContact(index - 1);
    }

    private void DeleteContact(int index)
    {
        Contact contact = allContacts[index];
        ReviewContact(contact);

        if (Confirm("Delete this contact?", NO))
        {
            allContacts.RemoveAt(index);
            int pagecount = (int)Math.Max(1, Math.Ceiling(allContacts.Count / (double)size));
            page = Math.Clamp(page, 1, pagecount);
        }
    }

    private void FindContacts()
    {
        string term = GetString("Search");
        List<Contact> results = FindContact(term);

        if (results.Count == 0)
        {
            Console.WriteLine($"No contacts found for '{term}'.");
            PressEnterToContinue();
            return;
        }

        ShowContacts(results, 1, results.Count);
        PressEnterToContinue();
    }

    private List<Contact> FindContact(string term)
    {
        string t = term.ToLower();
        return allContacts.Where(c =>
            (c.GetFName()?.ToLower().Contains(t) ?? false) ||
            (c.GetLName()?.ToLower().Contains(t) ?? false) ||
            (c.GetPhone()?.Contains(t) ?? false) ||
            (c.GetEmail()?.ToLower().Contains(t) ?? false)
        ).ToList();
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

    private string GetOption(string prompt, string[] validOption, string defaultOption)
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

            option = Console.ReadLine()!.ToUpper();

            if (string.IsNullOrWhiteSpace(option)) { option = defaultOption; }

        }

        return option;
    }

    private string GetString(string prompt)
    {
        Console.Write(prompt + ": ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    private string GetString(string prompt, string current)
    {
        Console.Write($"{prompt} [{current}]: ");
        string input = Console.ReadLine()?.Trim() ?? "";
        return string.IsNullOrEmpty(input) ? current : input;
    }

    private int GetInt(string prompt, int min, int max)
    {
        Console.Write(prompt + $" [{min}-{max}]: ");

        string raw = Console.ReadLine()?.Trim() ?? "";

        while (!int.TryParse(raw, out int value) || value < min || value > max)
        {
            Console.WriteLine($"ERROR: Please enter a number between {min} and {max}.");
            Console.Write(prompt + $" [{min}-{max}]: ");
            raw = Console.ReadLine()?.Trim() ?? "";
        }

        return int.Parse(raw);
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOption(prompt, YES_NO, defaultOption) == YES;
    }
}