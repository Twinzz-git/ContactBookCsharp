namespace ContactBook;

public class ContactMerger
{
    public static List<List<Contact>> FindDuplicates(List<Contact> contacts)
    {
        var phoneIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var emailIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        var ds = new DuplicateSet(contacts.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            Contact c = contacts[i];
            string phone = c.GetPhone();
            string email = c.GetEmail();

            if (!string.IsNullOrWhiteSpace(phone))
            {
                if (phoneIndex.TryGetValue(phone, out int existingPhone))
                    ds.Union(existingPhone, i);
                else
                    phoneIndex[phone] = i;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (emailIndex.TryGetValue(email, out int existingEmail))
                    ds.Union(existingEmail, i);
                else
                    emailIndex[email] = i;
            }
        }

        var groups = new Dictionary<int, List<int>>();

        for (int j = 0; j < contacts.Count; j++)
        {
            int root = ds.FindRoot(j);

            if (!groups.TryGetValue(root, out var list))
            {
                list = new List<int>();
                groups[root] = list;
            }
            list.Add(j);
        }

        return groups.Values
            .Select(g => g.Select(i => contacts[i]).ToList())
            .ToList();
    }

    private class DuplicateSet
    {
        private int[] parents;

        public DuplicateSet(int n)
        {
            parents = new int[n];
            for (int i = 0; i < n; i++)
                parents[i] = i;
        }

        public int FindRoot(int i)
        {
            if (parents[i] != i)
                parents[i] = FindRoot(parents[i]);
            return parents[i];
        }

        public void Union(int a, int b)
        {
            int rootA = FindRoot(a);
            int rootB = FindRoot(b);

            if (rootA != rootB)
                parents[rootB] = rootA;
        }
    }
}
