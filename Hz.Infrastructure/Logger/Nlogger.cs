using System;
using NLog;
using NLog.Config;

namespace Hz.Infrastructure.Logger
{
    public class Nlogger : ILogger
    {
        private readonly NLog.Logger logger = null;
        public Nlogger(string projectName)
        {
            var layout = "${longdate}|${logger}${newline}${message}${newline}";
            var config = new LoggingConfiguration();
            var errorFile = new NLog.Targets.FileTarget("errorfile")
            {
                FileName= "./log/error/${shortdate}.log",
                Layout = layout
            };
            var infoFile = new NLog.Targets.FileTarget("infofile")
            {
                FileName= "./log/info/${shortdate}.log",
                Layout = layout
            };
            config.AddRuleForOneLevel(LogLevel.Error, errorFile);
            config.AddRuleForOneLevel(LogLevel.Info, infoFile);

            NLog.LogManager.Configuration = config;

            logger = NLog.LogManager.GetLogger(projectName);
        }
        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
    }
}