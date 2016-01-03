using System;
using Objects.Logger;

namespace ProductionLineMover.Logger
{
    internal class DefaultLogger : ILogger
    {
        public void Log(LoggingType type, string mesage)
        {
            var prefix = string.Empty;

            switch (type)
            {
                case LoggingType.Error:
                    prefix = "[ERROR]";
                    break;
                case LoggingType.Info:
                    prefix = "[Info]";
                    break;
                case LoggingType.Warning:
                    prefix = "[Warning]";
                    break;
            }

            Console.WriteLine(prefix + " " + mesage);
        }
    }
}
