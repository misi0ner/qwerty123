using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

using NAudio;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;

namespace Diplom
{
    public partial class Form1 : Form
    {
        string fileName;
        string text;
        public Form1()
        {
            InitializeComponent();
        }

        private string ToList(string[] str)
        {
            string s = "";
            if (str[1] == "нумерованный")
            {
                int j = 1;
                for (var i = 2; i < str.Length; ++i)
                {
                    s += j.ToString() + ". " + str[i] + "\n";
                    ++j;
                }
            }
            else
            for (var i = 2; i < str.Length; ++i)
            {
                   s += str[i] + "\n";
            }
            return s;
        }

        private string ToTable(string[] str)
        {
            string s = "";
            if (str[1].Contains("название таблицы"))
            {
                var s1 = str[1].Replace("название таблицы ", "");
                if (s1.Contains("заголовки"))
                {
                    var s2 = s1.Replace(" заголовки ", "\t").Replace(" следующий заголовок ", "\t");//.Split('/');
                    //for (int i = 0; i < s2.Length; ++i)
                    //{
                    //    s += s2[i] + '\t';
                    //}
                    s += s2 + '\n';
                    for (int i = 2; i < str.Length; ++i)
                    {
                        var s3 = str[i].Replace(" ", "\t");//.Split(' ');
                        //for (int j = 0; j < s3.Length; ++j)
                        //    s += s3[j] + '\t';
                        s += s3 + '\n';
                    }
                }
            }
            var ts = s.Split('\t', '\n');
            int len = 0;
            foreach (var x in ts)
                if (x.Length > len)
                    len = x.Length;
            s = "";
            int cnt = 0;
            foreach (var x in ts)
            {
                s += x.PadRight(len) + '\t';
                ++cnt;
                if (cnt == 3)
                {
                    cnt = 0;
                    s += '\n';
                }
            }
            return s;
        }

        class Vertex
        {
            public string Name = "";
            public Point coordinate;

            public Vertex(string name, int x, int y)
            {
                Name = name;
                coordinate = new Point(x, y);
            }
        }

        class Edge
        {
            public string Name = "";
            public Vertex from, to;
            public Edge(string name, Vertex f, Vertex t)
            {
                Name = name;
                from = f;
                to = t;
            }
        }

        private void ToHTML(string[] str)
        {
            int cur_state = 1;
            int last_x = 50, last_y = 100;
            List<Vertex> vertices = new List<Vertex>();
            List<Edge> edges = new List<Edge>();
            ConsoleApplication2.DrawHTML dw = new ConsoleApplication2.DrawHTML();
            while (cur_state < str.Length && (str[cur_state].ToLower() == "вершина" || str[cur_state].ToLower() == "ребро"))
            {
                if(str[cur_state].ToLower() == "вершина")
                {
                    cur_state++;
                    vertices.Add(new Vertex(str[cur_state++], last_x + 150, last_y));
                    last_x += 150;
                }
                if(str[cur_state].ToLower() == "ребро")
                {
                    cur_state++;
                    if (str[cur_state++].ToLower() == "из") {
                        Vertex from = null, to = null;
                        foreach(var v in vertices)
                            if(v.Name == str[cur_state])
                                from = v;
                        cur_state++;
                        if (str[cur_state++].ToLower() == "в")
                        {
                            foreach (var v in vertices)
                                if (v.Name == str[cur_state])
                                    to = v;
                            cur_state++;
                        }
                        else
                            return; //Ошибка

                        if (from != null && to != null)
                            edges.Add(new Edge(/*str[cur_state++]*/ "", from, to));
                    }
                    else
                        return; //Ошибка
                }
            }

            foreach(var v in vertices)
            {
                dw.DrawV(v.Name, v.coordinate.X, v.coordinate.Y);
            }
            foreach(var e in edges)
            {
                dw.DrawE(e.from.coordinate.X + 40, e.from.coordinate.Y + 40, e.to.coordinate.X, e.to.coordinate.Y);
            }

            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(dw.GetHTML());
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

            System.IO.File.WriteAllText("html_test.html", dw.GetHTML());
        }

        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        private void Text_Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                text = File.ReadAllText(fileName).ToLower().Replace(" конец строки ", "/");
                var str = text.Split('/');
                //var ls = str.ToList<string>();
                //var fname = fileName.Replace(".txt", "2.txt");
                //File.WriteAllLines(fname, str);
                string ls = "";
                if (str[0] == "список")
                    ls = ToList(str);
                if (str[0] == "таблица")
                    ls = ToTable(str);
                if (str[0] == "автомат")
                {
                    str = text.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ToHTML(str);
                    return;
                }

                pictureBox1.Image = DrawText(ls, SystemFonts.MenuFont,Color.Black, pictureBox1.BackColor);
            }
        }




        //---------------Запись голоса с микрофона

        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;

        string outputFilename = "comand1.wav";
        //Получение данных из входного буфера 
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //Записываем данные из буфера в файл
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            }
        }
        //Завершаем запись
        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            waveIn.StopRecording();
        }
        //Окончание записи
        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                waveIn.Dispose();
                waveIn = null;
                writer.Close();
                writer = null;
            }
        }
        //Начинаем запись - обработчик нажатия кнопки
        private void Voice_button_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Start Recording");
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                //встроенный микрофон ноутбука имеет номер 0
                waveIn.DeviceNumber = 0;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(8000, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }


        //Прерываем запись - обработчик нажатия второй кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            if (waveIn != null)
            {
                StopRecording();
            }
        }





        private void Tools_Click(object sender, EventArgs e)
        {

        }

        private void Help_Click(object sender, EventArgs e)
        {
            
        }

       /* private void button1_Click(object sender, EventArgs e)
        {

        }*/
    }
}