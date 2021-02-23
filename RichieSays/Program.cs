using Codex.Extension;
using Codex.Terminal.Helper;
using Codex.Terminal.Process;

ConsoleHelper.InitEncoding();
//################################################################

if (args != null && args.Length > 0)
{
    bool _reorganize = args.ArgAppear("REORGANIZE");

    if (_reorganize)
        Reorganize.Main(args);
}

//################################################################
ConsoleHelper.Ends();