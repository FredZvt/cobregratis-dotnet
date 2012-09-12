
namespace BielSystems.Log
{
    public class LogHelper
    {
        protected ILogger Logger { get; set; }

        public LogHelper(ILogger logger)
        {
            this.Logger = logger;
        }

        public void Log(string message, params object[] args)
        {
            if (args != null)
            {
                message = string.Format(message, args);
            }

            Log(message);
        }

        public void Log(string message)
        {
            if (Logger != null)
            {
                Logger.Log(message);
            }
        }
    }
}
