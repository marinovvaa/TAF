namespace AutoFramework.Helpers
{
    public class LogHelper
    {
        //Global declaration of log file name
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);

        private static StreamWriter _streamWriter = null;

        private static string GetLogDirPath()
        {            
            string outputDir = Environment.CurrentDirectory.ToString();
            string relPath = "\\Logs";
            var result = outputDir + relPath;
            return result;
        }

        //Create a file which can store the log information
        public static void CreateLogFile()
        {
            //This will be changed!!
            string dir = GetLogDirPath();

            if(Directory.Exists(dir))
            {
                _streamWriter = File.AppendText(dir + _logFileName + ".log");
            }
            else
            {
                Directory.CreateDirectory(dir);
                _streamWriter = File.AppendText(dir + _logFileName + ".log");
            }
        }

        //Create a method which can write the log message in the log file
        public static void Write(string logMessage)
        {
            _streamWriter.Write("{0} {1} ", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamWriter.WriteLine($"{logMessage}");
            _streamWriter.Flush();
        }
        


    }
}
