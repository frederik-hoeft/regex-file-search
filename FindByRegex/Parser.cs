namespace FindByRegex;

internal static class Parser
{
    public static Options Parse(string[] args)
    {
        Options options = new();
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            if (arg == "-r")
            {
                options.Recursive = true;
            }
            else if (arg == "-f")
            {
                options.SearchForFiles = true;
            }
            else if (arg == "-d")
            {
                options.SearchForDirectories = true;
            }
            else if (arg == "-h")
            {
                options.DisplayHelp = true;
            }
            else if (arg.StartsWith("-"))
            {
                Console.WriteLine("Invalid option: " + arg);
                options.DisplayHelp = true;
                return options;
            }
            else if (options.Pattern == null)
            {
                options.Pattern = arg;
            }
            else if (options.Path == null)
            {
                options.Path = arg;
            }
            else
            {
                Console.WriteLine("Invalid argument: " + arg);
                options.DisplayHelp = true;
                return options;
            }
        }

        if (options.Pattern == null)
        {
            Console.WriteLine("Missing regex pattern.");
            options.DisplayHelp = true;
            return options;
        }

        if (!options.SearchForFiles && !options.SearchForDirectories)
        {
            options.SearchForFiles = true;
            options.SearchForDirectories = true;
        }

        return options;
    }
}