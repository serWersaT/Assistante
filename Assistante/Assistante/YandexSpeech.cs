using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using NAudio.Wave;
using System.Windows.Forms;


namespace Assistante
{
    class YandexSpeech : ConfigurationBase
    {
        public string ActionToken { get; set; }
        WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
        public bool flag = false;
        void Speech(string text)
        {
            ConfigurationBase config = new ConfigurationBase { Action = "API yandex url" }; /*передаю команду в базовый класс*/
            string API_url = config.Config();
            config.Action = "API yandex key";
            string API_key = config.Config();
            config.Action = "API yandex format";
            string format = config.Config();


            string url = API_url + text + format + API_key;

            using (Stream ms = new MemoryStream())
            {
                if (flag == false)
                {
                    try
                    {
                        using (Stream stream = WebRequest.Create(url).GetResponse().GetResponseStream())
                        {
                            byte[] buffer = new byte[32768];
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                        }
                        ms.Position = 0;
                        using (WaveStream blockAlignedStream =
                            new BlockAlignReductionStream(
                                WaveFormatConversionStream.CreatePcmStream(
                                    new Mp3FileReader(ms))))
                        {

                            waveOut.Init(blockAlignedStream);
                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100);
                            }

                        }
                        
                    }

                    catch {/*выдать сообщение обошибке, записать в лог*/ }
                }

            }
        }



        CancellationTokenSource cts = new CancellationTokenSource();    /*останов потока, не работает*/
        //CancellationToken token = cts.Token();

        public void Speek(string[] str)
        {
            //var cts = new CancellationTokenSource();
            //var cancellationToken = cts.Token;

            //CancellationToken token = cts.Token();
            if (flag == false)
            {
                Task task = new Task(() =>
                {
                    foreach (var x in str)
                    {
                        Speech(x);
                        if (cts.Token.IsCancellationRequested)
                            return;                       
                    }


                }, cts.Token);
                task.Start();
            }
            //cts.Cancel();
            //if (flag == true) cts.Cancel();

        }

        public void Stop()
        {
            cts.Cancel();
            flag = true;
        }
    }
}
