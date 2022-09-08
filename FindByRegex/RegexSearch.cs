using System.Text.RegularExpressions;

namespace FindByRegex;

public class RegexSearch
{
    public static void Main(string[] args)
    {
        Options options = Parser.Parse(args);

        if (options.DisplayHelp)
        {
            DisplayHelp();
            return;
        }

        string? path = options.Path;
        if (string.IsNullOrEmpty(path))
        {
            path = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(path))
        {
            Console.WriteLine("The specified path does not exist.");
            return;
        }

        Regex regex = new(options.Pattern!, RegexOptions.Compiled);
        if (options.SearchForFiles)
        {
            SearchForFiles(regex, path, options.Recursive);
        }

        if (options.SearchForDirectories)
        {
            SearchForDirectories(regex, path, options.Recursive);
        }
    }

    private static void SearchForFiles(Regex regex, string path, bool recursive)
    {
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            if (regex.IsMatch(file))
            {
                Console.WriteLine(file);
            }
        }

        if (recursive)
        {
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                SearchForFiles(regex, directory, recursive);
            }
        }
    }

    private static void SearchForDirectories(Regex regex, string path, bool recursive)
    {
        string[] directories = Directory.GetDirectories(path);
        foreach (string directory in directories)
        {
            if (regex.IsMatch(directory))
            {
                Console.WriteLine(directory);
            }

            if (recursive)
            {
                SearchForDirectories(regex, directory, recursive);
            }
        }
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("regexSearch [-r] [-f] [-d] <regex-pattern> [path]");
        Console.WriteLine("where options in [] are optional parameters.");
        Console.WriteLine("\"-r\" enables recursive search.");
        Console.WriteLine("\"-f\" specifies to only search for files.");
        Console.WriteLine("\"-d\" specifies to only search for directories.");
        Console.WriteLine("\"regex-pattern\" is the regex pattern that must match any file or folder name that is printed back on the screen.");
        Console.WriteLine("\"path\" allows to optionally specify a different path (other than the current working director) to search in. The path parameter may be absolute or relative.");
    }
}