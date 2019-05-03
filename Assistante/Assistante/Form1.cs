using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Assistante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fghToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update up = new Update();
            up.UpdateProgramm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] strings = new string[5];          
            strings[0] = "Новости на канале Пятница совершенно не похожи на традиционные информационные выпуски.";
            strings[1] = "События освещаются исключительно в позитивном ключе, без политики и происшествий";
            strings[2] = "За просмотром передачи Пятница News становится ясно, что наш мир удивителен и скрывает много интересного";
            strings[3] = "Ведущие делают все, чтобы избавить выпуски новостей от штампов и заезженности";
            strings[4] = "В эфире представлены передачи, посвященные моде и стилю жизни";

            YandexSpeech YA = new YandexSpeech();
            YA.Speek(strings);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YandexSpeech ya = new YandexSpeech();
            ya.Stop();
        }
    }
}
