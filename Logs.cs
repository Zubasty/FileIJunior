interface ILogger
{
    public void WriteError(string message);
}

interface IRuleLog
{
    public bool CanWriteLog();
}

class RuleLogAllTime : IRuleLog
{
    public bool CanWriteLog() => true;
}

class RuleLogOnlyFriday : IRuleLog
{
    public bool CanWriteLog() => DateTime.Now.DayOfWeek == DayOfWeek.Friday;
}

abstract class LogWriter : ILogger
{
    private readonly IRuleLog _ruleLog;

    public LogWriter(IRuleLog ruleLog)
    {
        _ruleLog = ruleLog;
    }

    public virtual void WriteError(string message)
    {
        if (_ruleLog.CanWriteLog())
            PrintLog(message);
    }

    protected abstract void PrintLog(string message);
}

class ConsoleLogWritter : LogWriter
{
    public ConsoleLogWritter(IRuleLog ruleLog) : base(ruleLog) { }

    protected override void PrintLog(string message)
    {
        Console.WriteLine(message);
    }
}

class FileLogWritter : LogWriter
{
    public FileLogWritter(IRuleLog ruleLog) : base(ruleLog) { }

    protected override void PrintLog(string message)
    {
        File.WriteAllText("log.txt", message);
    }
}

class CompositeLogWritter : ILogger
{
    private IEnumerable<LogWriter> _logsWriter;

    public CompositeLogWritter(IEnumerable<LogWriter> logsWriter)
    {
        _logsWriter = logsWriter;
    }

    public void WriteError(string message)
    {
        foreach (var logWriter in _logsWriter)
        {
            logWriter.WriteError(message);
        }
    }
}

class PathFinder
{
    private ILogger _logger;

    public PathFinder(ILogger logger)
    {
        _logger = logger;
    }

    public void Find()
    {
        _logger.WriteError("что-то пишет в лог. „то не принципиально.");
    }
}

class Programm
{
    public static void Main()
    {
        PathFinder fileAllTime = new PathFinder(new FileLogWritter(new RuleLogAllTime()));
        PathFinder consoleAllTime = new PathFinder(new ConsoleLogWritter(new RuleLogAllTime()));
        PathFinder fileFriday = new PathFinder(new FileLogWritter(new RuleLogOnlyFriday()));
        PathFinder consoleFriday = new PathFinder(new ConsoleLogWritter(new RuleLogOnlyFriday()));
        PathFinder consoleAllTimeAndFileFriday = new PathFinder(new CompositeLogWritter(new List<LogWriter>()
            {
                new ConsoleLogWritter(new RuleLogAllTime()),
                new FileLogWritter(new RuleLogOnlyFriday())
            }));
    }
}