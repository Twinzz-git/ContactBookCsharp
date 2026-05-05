namespace ContactBook;


public class ContactComparer : IComparer<Contact>
{
    public enum SortType
    {
        FName,
        LName,
        Phone,
        Email
    }

    private SortType sorType;

    public ContactComparer(SortType sortType)
    {
        SetSortType(sortType);
    }

    public SortType GetSortType()
    {
        return sorType;
    }

    public void SetSortType(SortType sortType)
    {
        this.sorType = sortType;
    }

    public int Compare(Contact? x, Contact? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        int r = 0;
        switch (sorType)
        {
            case SortType.FName: r = string.Compare(x.GetFName(), y.GetFName(), StringComparison.OrdinalIgnoreCase); break;
            case SortType.LName: r = string.Compare(x.GetLName(), y.GetLName(), StringComparison.OrdinalIgnoreCase); break;
            case SortType.Phone: r = string.Compare(x.GetPhone(), y.GetPhone(), StringComparison.OrdinalIgnoreCase); break;
            case SortType.Email: r = string.Compare(x.GetEmail(), y.GetEmail(), StringComparison.OrdinalIgnoreCase); break;
        }
        return r;
    }

}
