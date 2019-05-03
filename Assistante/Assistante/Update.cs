using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Assistante
{
    class Update: ConfigurationBase
    {
        public void UpdateProgramm()
        {
            ConfigurationBase config = new ConfigurationBase { Action = "internet ping" }; /*передаю команду в базовый класс*/
            string action = config.Config();
            //MessageBox.Show(action);
            if (action != "Нет доступа к серверу")
            {
                if (File.Exists("instruction.bat"))
                {
                    //выполнить работу по обновлению
                }
                else
                {
                    //скачать файл instruction.bat
                    config.Action = "host ping";
                    string url = config.Config();
                    string file = "instruction.bat";
                    MessageBox.Show(url);
                    if (url != "Нет доступа к серверу")
                    {
                        WebClient wc = new WebClient();
                        wc.DownloadFile(url,file);
                    }
                    
                }
            }
        }

        
        private void instructionFile()
        {
            string[] str = new string[100];     /*записываем все строки из файла в массив*/
            str = File.ReadAllLines("instruction.bat");
            string Line = "";

            try
            {
                Line = File.ReadLines("configuration.bat").Skip(0).First();
            }
            catch   /*Если файл configuration.bat не обнаружен по черт знает каким причинам, то восстанавливаем его первоначальную версию*/
            {
                MessageBox.Show("Файл не найден или уделен. Все настройки сброшены до изначальных");
                string configurate = Config();
            }

            if (str[0] == Line)
            {
                MessageBox.Show("обновление не требуется");
            }
            else
            {
                int i = 1;
                try
                {
                    while (str[i] != null)
                    {
                        WebClient wc = new WebClient();
                        wc.DownloadFile(str[i], str[i + 1]);
                        i = i + 2;
                    }
                }
                catch
                { /*добавить лог файл*/}
            }

        }
    }
}
