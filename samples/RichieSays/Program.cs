using Codex.Utility;
using Codex.Utility.Terminal;
using Codex.Utility.Terminal.Process;

ConsoleHelper.Init();
//################################################################

if (args != null && args.Length > 0)
{
    bool _reorganize = args.ArgAppear("REORGANIZE");

    if (_reorganize)
        Reorganize.Main(args);
}

//################################################################
ConsoleHelper.End();