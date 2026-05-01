namespace ContactBook;

public class Contact : IEquatable<Contact>
{
    private string fname = default!;
    private string lname = default!;
    private string phone = default!;
    private string email = default!;

    public Contact(string fname = "", string lname = "", string phone = "", string email = "")
    {
        this.fname = fname;
        this.lname = lname;
        this.phone = phone;
        this.email = email;
    }

    public string GetFName() => fname;
    public string GetLName() => lname;
    public string GetPhone() => phone;
    public string GetEmail() => email;

    public void SetFName(string fname) => this.fname = fname;
    public void SetLName(string lname) => this.lname = lname;
    public void SetPhone(string phone) => this.phone = phone;
    public void SetEmail(string email) => this.email = email;

    public override string ToString()
    {
        return $"Contact[fname= {fname}, lname= {lname}, phone= {phone}, email= {email}]";
    }

    public bool Equals(Contact? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return string.Equals(fname, other.fname)
            && string.Equals(lname, other.lname)
            && string.Equals(phone, other.phone)
            && string.Equals(email, other.email);
    }

    public override bool Equals(object? obj) => Equals(obj as Contact);

    public override int GetHashCode() => HashCode.Combine(fname, lname, phone, email);

    public static bool operator ==(Contact? x, Contact? y) => (x is null) ? (y is null) : x.Equals(y);
    public static bool operator !=(Contact? x, Contact? y) => !(x == y);
}