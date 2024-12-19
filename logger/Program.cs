using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{

    /**
    * Класс для логирования сообщений с различными уровнями серьезности.
    */
    public class Logger
    {
        public enum Severity
        {
            TRACE,
            DEBUG,
            INFO,
            WARNING,
            ERROR
        }

        /**
        *lazy<logger> - гарант создания одного экземпляра класса (singleton)
        *lockObject - объект для синхронизации потоков, чтобы не было конфликтов для одновременной записи
        *LogWriters - потокобезопастная коллекция для каждого файла логов
        * обявление начальныъ настроек логов(запись только в консоль, уровень - Debug, и шаблон для сообщений логов)
        */
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
        private readonly object lockObject = new object();
        private Severity logLevel = Severity.DEBUG;
        private bool logToConsole = true;
        private bool logToFile = false;
        private ConcurrentDictionary<string, StreamWriter> logWriters = new ConcurrentDictionary<string, StreamWriter>();
        private string logTemplate = "{t} | {L} -> {m}";

        private Logger()
        {
            // Конструктор
        }

        public static Logger GetInstance()
        {
            return instance.Value;
        }

        public void SetLogLevel(Severity level)
        {
            logLevel = level;
        }

        public void SetLogOutput(bool toConsole, bool toFile)
        {
            logToConsole = toConsole;
            logToFile = toFile;
        }

        public void SetLogTemplate(string template)
        {
            logTemplate = template;
        }

        /**
       *  асинхронное логирование сообщений с учетом уровня серьезности
       * severity - Уровень серьезности сообщения
       * messageParts - Части сообщения, которые будут объединены
       * sourceFile - Имя файла, откуда вызвано логирование
       * lineNumber - Номер строки, откуда вызвано логирование
       * task - Задача, представляющая асинхронную 
       * форматирование шаблона пользователя
       */
        public async Task WriteAsync(Severity severity, string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts)
        {
            if (severity < logLevel) return;

            string fullMessage = string.Join(" ", messageParts);
            string logMessage = logTemplate
                .Replace("{t}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                .Replace("{L}", severity.ToString())
                .Replace("{m}", $"{message} {fullMessage}")
                .Replace("{f}", sourceFile)
                .Replace("{n}", lineNumber.ToString());

            await Task.Run(() =>
            {
                lock (lockObject)
                {
                    if (logToConsole)
                    {
                        Console.WriteLine(logMessage);
                    }

                    if (logToFile)
                    {
                        string filePath = targetFile + $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log" ?? $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
                        var writer = logWriters.GetOrAdd(filePath, path => new StreamWriter(path, true, Encoding.UTF8));
                        writer.WriteLine(logMessage);
                        writer.Flush();
                    }
                }
            });
        }

        public void Close()
        {
            foreach (var writer in logWriters.Values)
            {
                writer.Close();
            }
            logWriters.Clear();
        }
    }
    /**
    * Статический класс для удобства логирования - макросы
    */
    public static class LoggerExtensions
    {
        public static Task LOGT(string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts) => Logger.GetInstance().WriteAsync(Logger.Severity.TRACE, message, sourceFile, lineNumber, targetFile, messageParts);
        public static Task LOGD(string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts) => Logger.GetInstance().WriteAsync(Logger.Severity.DEBUG, message, sourceFile, lineNumber, targetFile, messageParts);
        public static Task LOGI(string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts) => Logger.GetInstance().WriteAsync(Logger.Severity.INFO, message, sourceFile, lineNumber, targetFile, messageParts);
        public static Task LOGW(string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts) => Logger.GetInstance().WriteAsync(Logger.Severity.WARNING, message, sourceFile, lineNumber, targetFile, messageParts);
        public static Task LOGE(string message, string sourceFile, int lineNumber, string targetFile = null, params object[] messageParts) => Logger.GetInstance().WriteAsync(Logger.Severity.ERROR, message, sourceFile, lineNumber, targetFile, messageParts);
    }

    class Program
    {
        private static readonly Logger logger = Logger.GetInstance();

        static async Task Main(string[] args)
        {
            logger.SetLogOutput(true, true);
            logger.SetLogLevel(Logger.Severity.DEBUG);
            logger.SetLogTemplate("{t} | {L} | {f}:{n} -> {m} ");


            await LoggerExtensions.LOGT("Trace log", nameof(Program), 32, "trace.log");
            await LoggerExtensions.LOGI("Пользователь вошел в систему", nameof(Program), 60, "info.log", "User  123", "с успешным входом");
            await LoggerExtensions.LOGD("Отладочное сообщение с эмодзи ☻☺♣♦☺", nameof(Program), 62, "debug.log");
            await LoggerExtensions.LOGW("Warning log", nameof(Program), 32, "warning.log");
            await LoggerExtensions.LOGE("Ошибка в функции", nameof(Program), 61, "error.log", "functionName", "с кодом", 404);

            logger.Close();
        }
    }
}