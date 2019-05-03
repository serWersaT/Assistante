using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace Assistante
{
    class ConfigurationBase
    {
        public string Action { get; set; }

        public string Config()  /*функция принимает различные команды из других классов*/
        {
            string act = Action;

            if (File.Exists("configuration.bat"))   /*Если рабочий файл configuration.bat найден*/
            {
                string[] str = new string[100];     /*записываем все строки из файла в массив*/
                str = File.ReadAllLines("configuration.bat");

                switch (act)
                {
                    case "version":     /*возвращает версию программы*/
                        {
                            act = str[0];
                            act = act.Replace("version: ", "");
                            return act;
                        }

                    case "internet ping":   /*пингует интернет ресурс*/
                        {
                            act = str[1];
                            act = act.Replace("adress ping: ", "");
                            bool rezult = Ping(act);
                            if (rezult == true) return act;
                            else return "Нет доступа к сети интернет";
                        }

                    case "host ping":   /*пингует хост сервер для скачивания обновлений */
                        {
                            act = str[2];
                            act = act.Replace("adress host: ", "");
                            bool rezult = Ping(act);
                            if (rezult == true) return act;
                            else return "Нет доступа к серверу";
                        }
                    case "API yandex url":     /*возвращает адрес яндекс spreechAPI*/
                        {
                            act = str[3];
                            act = act.Replace("YANDEX API_URL: ", "");
                            return act;
                        }

                    case "API yandex key":     /*возвращает адрес яндекс spreechAPI*/
                        {
                            act = str[4];
                            act = act.Replace("YANDEX API_KEY: ", "");
                            if (act != "")
                                return act;
                            else return "Введите ключ пользователя yandex API";
                        }

                    case "API yandex format":     /*возвращает адрес яндекс spreechAPI*/
                        {
                            act = str[5];
                            act = act.Replace("YANDEX API_FORMAT: ", "");
                            if (act != "")
                                return act;
                            else return "Введите ключ пользователя yandex API";
                        }

                    case "API google url":     /*возвращает адрес яндекс spreechAPI*/
                        {
                            act = str[6];
                            act = act.Replace("GOOGLE API URL: ", "");
                                return act;
                        }
                    case "API google key":     /*возвращает адрес яндекс spreechAPI*/
                        {
                            act = str[7];
                            act = act.Replace("GOOGLE API KEY: ", "");
                            if (act != "")
                                return act;
                            else return "Введите ключ пользователя GOOGLE API";
                        }
                    case "List of Programm":
                        {
                            string spisok = list_of_programm();
                            return spisok;
                        }



                    default:
                        return "команда указана не верно"; /*данная фраза пользователю не выводится, необходима для отлова неверных команд*/
                }               
            }
            else /*Если файл не обнаружен по черт знает каким причинам, то восстанавливаем его первоначальную версию*/
            {
                File.AppendAllText("configuration.bat", "version: 1.0" + "\r\n" + "adress ping: google.com" + "\r\n" + "adress host: servernut.ru" + "\r\n" + "YANDEX API_URL: https://tts.voicetech.yandex.net/generate?"
                    + "\r\n" + "YANDEX API_KEY: 236319ab-f446-4705-93bd-a023ac620be4" + "\r\n" + "GOOGLE API URL: https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=" + "\r\n"
                    + "GOOGLE API KEY: AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
                return "Файл не найден или уделен. Все настройки сброшены до изначальных";
            }


        }


        bool Ping(string adress)    /*процедура проверки сервера (ping)*/
        {
            bool haveAnInternetConnection = false;
            Ping ping = new Ping();
            try
            {
                PingReply pingReply = ping.Send(adress);

                haveAnInternetConnection = (pingReply.Status == IPStatus.Success);
                if (haveAnInternetConnection)
                {
                    return true;
                }
                else return false;

            }
            catch
            {
                return false;   /*возвращаем false если пинг не удался*/
            }
        }


        string list_of_programm()   /*составляет список программ, установленных на компьютере*/
        {
            string displayName;
            string spisok = "";
            RegistryKey key;
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string +
                    subkey.GetValue("InstallLocation") as string;
                spisok += displayName + "\r\n";
            }
            return spisok;
        }
    }
}
