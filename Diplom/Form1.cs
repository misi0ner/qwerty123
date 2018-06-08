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

using speechv2;
using Microsoft.Win32;

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
                    listBox1.Items.Add(j.ToString() + ". " + str[i]);
                    ++j;
                }
            }
            else
            for (var i = 2; i < str.Length; ++i)
            {
                   s += str[i] + "\n";
                    listBox1.Items.Add(str[i]);
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
                    var headers = s2.Split('\t').ToList();
                    dataGridView1.TopLeftHeaderCell.Value = headers[0]; 
                    dataGridView1.ColumnCount = headers.Count -1;
                    for (int i = 1; i < headers.Count; ++i)
                    {
                        dataGridView1.Columns[i - 1].HeaderCell.Value = headers[i];
                        //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    }
                    
                    s += s2 + '\n';
                    for (int i = 2; i < str.Length; ++i)
                    {
                        var s3 = str[i].Replace(" ", "\t");//.Split(' ');
                        //for (int j = 0; j < s3.Length; ++j)
                        //    s += s3[j] + '\t';

                        var values = s3.Split('\t').ToList();
                        dataGridView1.Rows.Add(1);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = values[0];
                        for (int j = 1; j < values.Count; ++j)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[j - 1].Value = values[j];
                        }
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
                if(str[cur_state] == "вершина")
                {
                    cur_state++;
                    string name = str[cur_state++];
                    if (str[cur_state] == "снизу")
                    {
                        cur_state++;
                        var result = vertices.Find(v => v.Name == str[cur_state]);
                        cur_state++;
                        if (result != null)
                            vertices.Add(new Vertex(name, result.coordinate.X, result.coordinate.Y + 150));
                        else
                            return;
                    }
                    else if (str[cur_state] == "сверху")
                    {
                        cur_state++;
                        var result = vertices.Find(v => v.Name == str[cur_state]);
                        cur_state++;
                        if (result != null)
                            vertices.Add(new Vertex(name, result.coordinate.X, result.coordinate.Y - 150));
                        else
                            return;
                    }
                    else if (str[cur_state] == "слева")
                    {
                        cur_state++;
                        if (str[cur_state] == "снизу")
                        {
                            cur_state++;
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X - 150, result.coordinate.Y + 150));
                            else
                                return;
                        }
                        else if (str[cur_state] == "сверху")
                        {
                            cur_state++;
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X - 150, result.coordinate.Y - 150));
                            else
                                return;
                        }
                        else
                        {
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X - 150, result.coordinate.Y));
                            else
                                return;
                        }
                    }
                    else if (str[cur_state] == "справа")
                    {
                        cur_state++;
                        if (str[cur_state] == "снизу")
                        {
                            cur_state++;
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X + 150, result.coordinate.Y + 150));
                            else
                                return;
                        }
                        else if (str[cur_state] == "сверху")
                        {
                            cur_state++;
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X + 150, result.coordinate.Y - 150));
                            else
                                return;
                        }
                        else
                        {
                            var result = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                            if (result != null)
                                vertices.Add(new Vertex(name, result.coordinate.X + 150, result.coordinate.Y));
                            else
                                return;
                        }
                    }
                    else
                    {
                        vertices.Add(new Vertex(name, last_x + 150, last_y));
                        last_x += 150;
                    }
                }
                if(str[cur_state] == "ребро")
                {
                    cur_state++;
                    if (str[cur_state++] == "из") {
                        Vertex from = null, to = null;
                        from = vertices.Find(v => v.Name == str[cur_state]);
                        cur_state++;
                        if (str[cur_state++] == "в")
                        {
                            to = vertices.Find(v => v.Name == str[cur_state]);
                            cur_state++;
                        }
                        else
                            return; //Ошибка

                        if (from != null && to != null)
                        {

                            edges.Add(new Edge(/*str[cur_state++]*/ "", from, to));
                        }
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
               if(e.to.coordinate.Y < e.from.coordinate.Y - 8)
                    dw.DrawE(e.from.coordinate.X + 18, e.from.coordinate.Y - 8, e.to.coordinate.X +18, e.to.coordinate.Y + 43);
                else if (e.to.coordinate.Y > e.from.coordinate.Y + 43)
                    dw.DrawE(e.from.coordinate.X + 18, e.from.coordinate.Y + 43, e.to.coordinate.X + 18, e.to.coordinate.Y - 8);
                else if (e.to.coordinate.X < e.from.coordinate.X - 8)
                    dw.DrawE(e.from.coordinate.X - 8, e.from.coordinate.Y + 17, e.to.coordinate.X +50, e.to.coordinate.Y + 17);
                else if (e.to.coordinate.X > e.from.coordinate.X + 50)
                    dw.DrawE(e.from.coordinate.X + 50, e.from.coordinate.Y + 17, e.to.coordinate.X - 8, e.to.coordinate.Y + 17);
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
                {
                    ls = ToList(str);
                    dataGridView1.Visible = false;
                    webBrowser1.Visible = false;
                    listBox1.Visible = true;
                }
                if (str[0] == "таблица")
                {
                    ls = ToTable(str);
                    dataGridView1.Visible = true;
                    webBrowser1.Visible = false;
                    listBox1.Visible = false;
                }
                if (str[0] == "автомат")
                {
                    str = text.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    ToHTML(str);
                    
                    webBrowser1.Navigate(Application.StartupPath + @"\html_test.html");
                    System.Diagnostics.Process.Start("html_test.html");

                    dataGridView1.Visible = false;
                    webBrowser1.Visible = true;
                    listBox1.Visible = false;
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

                speechv2.Program.GoogleSpeechToText(outputFilename);
                MessageBox.Show(speechv2.Program.strres);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            if (key != null)
            {
                key.SetValue("Diplom.exe", 9000, RegistryValueKind.DWord);
            }

            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            if (key != null)
            {
                key.SetValue("Diplom.exe", 9000, RegistryValueKind.DWord);
            }
        }

        /* private void button1_Click(object sender, EventArgs e)
         {

         }*/
    }
}