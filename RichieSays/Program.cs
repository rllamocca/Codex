using Codex.Terminal.Helper;
using Codex.Terminal.Process;

ConsoleHelper.InitEncoding();
//################################################################

if (args != null && args.Length > 0)
{
    string _switch = args[0].ToUpper();

    switch (_switch)
    {
        case "REORGANIZE":
            Reorganize.Main(args);
            break;
        default:
            break;
    }
}

//################################################################
ConsoleHelper.Ends();