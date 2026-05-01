namespace ContactBook.test;

public class ContactTests
{
    // ── Constructor ──────────────────────────────────────────────────────────

    [Fact]
    public void Constructor_DefaultValues_AllFieldsEmpty()
    {
        var contact = new Contact();

        Assert.Equal("", contact.GetFName());
        Assert.Equal("", contact.GetLName());
        Assert.Equal("", contact.GetPhone());
        Assert.Equal("", contact.GetEmail());
    }

    [Fact]
    public void Constructor_WithValues_SetsAllFields()
    {
        var contact = new Contact("John", "Doe", "555-1234", "john@example.com");

        Assert.Equal("John", contact.GetFName());
        Assert.Equal("Doe", contact.GetLName());
        Assert.Equal("555-1234", contact.GetPhone());
        Assert.Equal("john@example.com", contact.GetEmail());
    }

    [Fact]
    public void Constructor_PartialValues_SetsProvidedFields()
    {
        var contact = new Contact(fname: "Jane", email: "jane@example.com");

        Assert.Equal("Jane", contact.GetFName());
        Assert.Equal("", contact.GetLName());
        Assert.Equal("", contact.GetPhone());
        Assert.Equal("jane@example.com", contact.GetEmail());
    }

    // ── Getters ──────────────────────────────────────────────────────────────

    [Theory]
    [InlineData("Alice")]
    [InlineData("")]
    [InlineData("John-Paul")]
    public void GetFName_ReturnsCorrectValue(string fname)
    {
        var contact = new Contact(fname: fname);
        Assert.Equal(fname, contact.GetFName());
    }

    [Theory]
    [InlineData("Smith")]
    [InlineData("")]
    [InlineData("O'Brien")]
    public void GetLName_ReturnsCorrectValue(string lname)
    {
        var contact = new Contact(lname: lname);
        Assert.Equal(lname, contact.GetLName());
    }

    [Theory]
    [InlineData("555-0000")]
    [InlineData("+1-800-123-4567")]
    [InlineData("")]
    public void GetPhone_ReturnsCorrectValue(string phone)
    {
        var contact = new Contact(phone: phone);
        Assert.Equal(phone, contact.GetPhone());
    }

    [Theory]
    [InlineData("user@domain.com")]
    [InlineData("")]
    [InlineData("no-at-sign")]
    public void GetEmail_ReturnsCorrectValue(string email)
    {
        var contact = new Contact(email: email);
        Assert.Equal(email, contact.GetEmail());
    }

    // ── Setters ──────────────────────────────────────────────────────────────

    [Fact]
    public void SetFName_UpdatesFirstName()
    {
        var contact = new Contact("Old", "Doe", "555", "a@b.com");
        contact.SetFName("New");
        Assert.Equal("New", contact.GetFName());
    }

    [Fact]
    public void SetLName_UpdatesLastName()
    {
        var contact = new Contact("John", "Old", "555", "a@b.com");
        contact.SetLName("New");
        Assert.Equal("New", contact.GetLName());
    }

    [Fact]
    public void SetPhone_UpdatesPhone()
    {
        var contact = new Contact("John", "Doe", "000", "a@b.com");
        contact.SetPhone("999-9999");
        Assert.Equal("999-9999", contact.GetPhone());
    }

    [Fact]
    public void SetEmail_UpdatesEmail()
    {
        var contact = new Contact("John", "Doe", "555", "old@old.com");
        contact.SetEmail("new@new.com");
        Assert.Equal("new@new.com", contact.GetEmail());
    }

    [Fact]
    public void Setters_CanChainMultipleUpdates()
    {
        var contact = new Contact();
        contact.SetFName("Jane");
        contact.SetLName("Smith");
        contact.SetPhone("123-4567");
        contact.SetEmail("jane@smith.com");

        Assert.Equal("Jane", contact.GetFName());
        Assert.Equal("Smith", contact.GetLName());
        Assert.Equal("123-4567", contact.GetPhone());
        Assert.Equal("jane@smith.com", contact.GetEmail());
    }

    // ── ToString ─────────────────────────────────────────────────────────────

    [Fact]
    public void ToString_ContainsAllFields()
    {
        var contact = new Contact("John", "Doe", "555-1234", "john@example.com");
        var result = contact.ToString();

        Assert.Contains("John", result);
        Assert.Contains("Doe", result);
        Assert.Contains("555-1234", result);
        Assert.Contains("john@example.com", result);
    }

    [Fact]
    public void ToString_DefaultContact_ContainsEmptyValues()
    {
        var contact = new Contact();
        var result = contact.ToString();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    // ── Equals ───────────────────────────────────────────────────────────────

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("John", "Doe", "555", "j@d.com");

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentFName_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("Jane", "Doe", "555", "j@d.com");

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentLName_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("John", "Smith", "555", "j@d.com");

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentPhone_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "111", "j@d.com");
        var b = new Contact("John", "Doe", "999", "j@d.com");

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentEmail_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "a@a.com");
        var b = new Contact("John", "Doe", "555", "b@b.com");

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");

        Assert.False(a.Equals(null));
    }

    [Fact]
    public void Equals_SameReference_ReturnsTrue()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");

        Assert.True(a.Equals(a));
    }

    [Fact]
    public void Equals_ObjectOverload_SameValues_ReturnsTrue()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        object b = new Contact("John", "Doe", "555", "j@d.com");

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_ObjectOverload_DifferentType_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");

        Assert.False(a.Equals("not a contact"));
    }

    // ── Operators == and != ──────────────────────────────────────────────────

    [Fact]
    public void OperatorEqual_SameValues_ReturnsTrue()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("John", "Doe", "555", "j@d.com");

        Assert.True(a == b);
    }

    [Fact]
    public void OperatorEqual_DifferentValues_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("Jane", "Doe", "555", "j@d.com");

        Assert.False(a == b);
    }

    [Fact]
    public void OperatorEqual_BothNull_ReturnsTrue()
    {
        Contact? a = null;
        Contact? b = null;

        Assert.True(a == b);
    }

    [Fact]
    public void OperatorEqual_OneNull_ReturnsFalse()
    {
        Contact? a = new Contact("John", "Doe", "555", "j@d.com");
        Contact? b = null;

        Assert.False(a == b);
        Assert.False(b == a);
    }

    [Fact]
    public void OperatorNotEqual_DifferentValues_ReturnsTrue()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("Jane", "Doe", "555", "j@d.com");

        Assert.True(a != b);
    }

    [Fact]
    public void OperatorNotEqual_SameValues_ReturnsFalse()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("John", "Doe", "555", "j@d.com");

        Assert.False(a != b);
    }

    // ── GetHashCode ──────────────────────────────────────────────────────────

    [Fact]
    public void GetHashCode_EqualContacts_SameHashCode()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("John", "Doe", "555", "j@d.com");

        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentContacts_DifferentHashCode()
    {
        var a = new Contact("John", "Doe", "555", "j@d.com");
        var b = new Contact("Jane", "Smith", "999", "x@y.com");

        Assert.NotEqual(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ConsistentAcrossCalls()
    {
        var contact = new Contact("John", "Doe", "555", "j@d.com");

        Assert.Equal(contact.GetHashCode(), contact.GetHashCode());
    }
}
