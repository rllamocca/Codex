using Codex.Terminal.Helper;
using Codex.Terminal.Process;

namespace RichieSays
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.InitEncoding();

            //################################################################

            if (args != null && args.Length > 0)
            {
                string _switch = args[0];
                _switch = _switch.ToUpper();

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
        }
    }
}
