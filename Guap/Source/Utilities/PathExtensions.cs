using System.IO;
using System.Runtime.CompilerServices;
using static System.IO.Directory;
using static System.IO.Path;

namespace Guap.Utilities;

public static class PathExtensions
{
    public static string Find(this string path, [CallerFilePath] string caller = "", int iterations = 8)
    {
        var pre = caller;
        var suf = path;
        string full() => Combine(pre, suf);
        if (File.Exists(full()))
            return full();
        for (var i = 0; i < iterations; i++)
        {
            if (GetParent(pre) is null)
                throw new FileNotFoundException($"File {path} not found!");
            pre = GetParent(pre)!.FullName;
            if (File.Exists(full()))
                return full();
        }
        throw new FileNotFoundException($"File {path} not found!");
    }
}