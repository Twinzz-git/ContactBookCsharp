using ContactBook;

public class Program
{
    public static void Main()
    {

var cb = new ContactBook.ContactBook(ContactSeed.Contacts);
cb.Start();
    }
}