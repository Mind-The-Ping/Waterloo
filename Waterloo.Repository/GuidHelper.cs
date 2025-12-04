using System.Security.Cryptography;
using System.Text;

namespace Waterloo.Repository;

public static class GuidHelper
{
    public static Guid GuidFromString(string input)
    {
        input ??= string.Empty;
        var normalized = input.Trim().ToLowerInvariant();
        var bytes = Encoding.UTF8.GetBytes(normalized);
        var hash = MD5.HashData(bytes);

        return new Guid(hash);
    }
}
