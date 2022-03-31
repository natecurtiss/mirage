using System.IO;
using System.Runtime.CompilerServices;
using static System.IO.Directory;
using static System.IO.Path;

namespace Mirage.Utils;

/// <summary>
/// Helper functions for searching paths and directories.
/// </summary>
public static class PathHelper
{
    /// <summary>
    /// Looks upwards from the current directory for the specified path.
    /// </summary>
    /// <param name="path">The path to look for.</param>
    /// <param name="caller">Leave this alone.</param>
    /// <param name="iterations">The number of directories to check upwards.</param>
    /// <returns>The absolute path to the file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file cannot be found after searching upwards the specific number of iterations.</exception>
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