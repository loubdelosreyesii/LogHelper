using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogHelper
{
    public static class LogWriter
    {
        public static void LogWrite(this RichTextBox txtLog,string logMessage,bool IsSuccess)
        {
            DateTime dateNow = DateTime.Now;
            string m_exePath,strTransactionLogPath;
            string userName = Environment.UserName;

            string strFileName = $"\\TransactionLog_{userName}_{dateNow.ToString("MM-dd-yyyy")}.txt";
           
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location );
            strTransactionLogPath = m_exePath + "\\Logs";

            if (!Directory.Exists(strTransactionLogPath) ){
                Directory.CreateDirectory(strTransactionLogPath);
            }
            if (!File.Exists(strTransactionLogPath + strFileName))
                File.Create(strTransactionLogPath + strFileName).Dispose();

            try
            {
                using (StreamWriter w = File.AppendText(strTransactionLogPath + strFileName))
                    AppendLog(txtLog,logMessage, w,userName,IsSuccess);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void AppendLog(this RichTextBox txtLog,string logMessage, TextWriter txtWriter,string userName,  bool IsSuccess )
        {
            try
            {
                string strMessage = $"{DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss")} [{userName}]: {logMessage}";
                txtWriter.WriteLine(strMessage);

                if (!IsSuccess)
                    txtLog.SelectionColor = System.Drawing.Color.Red;
                
                txtLog.AppendText(strMessage + Environment.NewLine);

                txtLog.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
