using Novell.Directory.Ldap;

namespace Service;

public static class LdapService
{
    private const int SCOPE_SUB = 1;

    public static async Task<List<Dictionary<string, string>>> SearchAsync(string searchBase, string[] attributes, string searchFilter)
    {
        string connectionString = Environment.GetEnvironmentVariable("AD_CONNECTION_STRING") ?? "";
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("LDAP connection string is missing");
        }

        string[] Login = connectionString.Split("__");
        if (Login.Length != 2)
        {
            throw new Exception("Invalid LDAP connection string format.");
        }

        LdapConnection? connection = ConnectAsync(Login[0], Login[1]);
        List<Dictionary<string, string>> list = [];

        await Task.Run(() =>
        {
            if (connection == null)
                return;

            LdapSearchConstraints searchConstraints = connection.SearchConstraints;
            searchConstraints.ReferralFollowing = true;

            ILdapSearchResults search = connection.Search(
                searchBase,
                SCOPE_SUB,
                searchFilter,
                attributes,
                false,
                searchConstraints
            );

            while (search.HasMore())
            {
                var searchResults = new Dictionary<string, string>();
                try
                {
                    LdapEntry entry = search.Next();
                    LdapAttributeSet attributeSet = entry.GetAttributeSet();

                    foreach (LdapAttribute attr in attributeSet)
                    {
                        searchResults[attr.Name.ToLower()] = attr.StringValue;
                    }
                    list.Add(searchResults);
                }
                catch (LdapException e)
                {
                    Console.WriteLine("Error: " + e.LdapErrorMessage);
                }
            }
        });
        return list;
    }

    public static LdapConnection? ConnectAsync(string username, string password)
    {
        LdapConnection ldapConnection = new()
        {
            SecureSocketLayer = false,
            ConnectionTimeout = 10000,
        };
        try
        {
            string connectionString = Environment.GetEnvironmentVariable("AD_CONNECTION_STRING") ?? "";
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("LDAP connection string is missing");
            }

            string[] connectionParams = connectionString.Split("__");
            if (connectionParams.Length != 2)
            {
                throw new Exception("Invalid LDAP connection string format.");
            }

            string host = connectionParams[0];
            string domainName = connectionParams[1];

            ldapConnection.Connect(host, 389);

            string formattedName = FormatUsername(username, domainName);
            ldapConnection.Bind(formattedName, password);
            return ldapConnection;
        }
        catch (LdapException)
        {
            return null;
        }
    }

    public static bool ValidateAsync(string username, string password)
    {
        LdapConnection? connection = ConnectAsync(username, password);
        connection?.Disconnect();
        return connection != null;
    }

    private static string FormatUsername(string username, string domainName)
    {
        username = username.ToLower();
        if (!username.Contains(domainName))
        {
            username = $"{username}@{domainName}";
        }
        return username;
    }
}
