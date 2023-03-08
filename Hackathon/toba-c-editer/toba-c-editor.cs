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
using System.Diagnostics;
using System.Threading;

namespace toba_c_editer
{
    public partial class Create_New : Form
    {
        //初期の変数
        int mozicoad = 10;
        string fileName= "New_repository";
        string save_mae="";
        int F11_count = 1;

        string output = "";
        int fontsize = 14;


        public Create_New()//初期設定
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 4;
            label2.Text = "New repository";
            
            SetTabWidth(richTextBox1, 13);
        }

        public void color(RichTextBox target,string word, Color color)//プログラムの色付け
        {
            //int found = -1;
            //do
            //{

            //    // 対象の RichTextBox から、キーワードが見つかる位置を探します。検索開始位置は、前回見つかった場所の次からとします。

            //    found = target.Find(word, found + 1, RichTextBoxFinds.MatchCase);



            //    if (found > -1)
            //    {

            //        target.SelectionStart = found;

            //        target.SelectionLength = word.Length;

            //        target.SelectionColor = color;
            //    }
            //    else
            //    {

            //        // キーワードが見つからなかった場合は、繰り返し処理を終了します。
            //        break;
            //    }

            //}
            //while (true);
        }

        public void hante()//セーブされているかの判定
        {
            if (richTextBox1.Text == save_mae)
            {
                label2.Text = Path.GetFileName(fileName);
            }
            else if (richTextBox1.Text != save_mae)
            {
                label2.Text = Path.GetFileName(fileName) + "*";
            }
        }

        public int save()//セーブする関数
        {
            if(fileName == "New_repository")
            {
                SaveFileDialog ofd = new SaveFileDialog();
                ofd.FileName = "New_repository.c";
                ofd.Title = "保存場所を選択してください";
                ofd.Filter = "cファイル|*.c;|すべてのファイル(*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    StreamWriter c = File.CreateText(fileName);
                    c.WriteLine(richTextBox1.Text);
                    c.Close();
                    textBox3.AppendText($"\r\n{fileName}として保存しました");
                    save_mae = richTextBox1.Text;
                    hante();
                    return 0;
                }
                else if(ofd.ShowDialog() == (DialogResult.Cancel)){
                    return 1;
                }
                return 0;
            }
            else
            {
                StreamWriter c = new StreamWriter(fileName, false);
                c.WriteLine(richTextBox1.Text);
                c.Close();
                textBox3.AppendText($"\r\n{fileName}として上書き保存しました");
                save_mae = richTextBox1.Text;
                hante();
                return 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//ドロップダウンリストの取得
        {
            mozicoad= comboBox1.SelectedIndex;
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)//文字エンコードの選択
        {
            if(mozicoad == 10)
            {
                MessageBox.Show("文字コードを選択してください");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "cファイル(*.c)|*.c|すべてのファイル(*.*)|*.*";
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = $"{ofd.FileName}";
                var text = "";
                switch (mozicoad)
                {
                    case 0:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("shift_jis")))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                    case 1:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("euc - jp")))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                    case 2:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("iso - 2022 - jp")))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                    case 3:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                    case 4:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                    case 5:
                        using (StreamReader sr = new StreamReader(fileName, Encoding.UTF32))
                        {
                            text = sr.ReadToEnd();
                            textBox3.AppendText($"\r\nopen {fileName}");
                        }break;
                }
                richTextBox1.Text=(text);
                save_mae = richTextBox1.Text;
            }
        }

        private void 名前を付けて保存ToolStripMenuItem_Click(object sender, EventArgs e)//ボタンでの新規保存
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.FileName = "New_repository.c";
            ofd.Title = "保存場所を選択してください";
            ofd.Filter = "cファイル|*.c;|すべてのファイル(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileName = ofd.FileName;
                StreamWriter c = File.CreateText(fileName);
                c.WriteLine(richTextBox1.Text);
                c.Close();
                textBox3.AppendText($"\r\n{fileName}として保存しました");
                hante();
            }
        }

        private void 上書き保存ToolStripMenuItem_Click(object sender, EventArgs e)//ボタンでの上書き保存
        {   
            if (fileName == "New_repository")
            {
                MessageBox.Show("ファイルが保存されていません");
            }
            else
            {
                StreamWriter c = new StreamWriter(fileName, false);
                c.WriteLine(richTextBox1.Text);
                c.Close();
                textBox3.AppendText($"\r\n{fileName}として上書き保存しました");
                label2.Text = Path.GetFileName(fileName);
                hante();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)//閉じるボタン
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//新規開始
        {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            pInfo.FileName = "toba-c-editor.exe";
            Process.Start(pInfo);
        }

        private void フルスクリーンモードToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F11_count++;
            if (F11_count % 2 == 0)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.textBox3.Size = new Size(1924, 260);
            }
            else if (F11_count % 2 == 1)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
                this.textBox3.Size = new Size(1924, 185);
            }
        }

        private void 拡大ToolStripMenuItem_Click(object sender, EventArgs e)//文字を大きくする
        {
            fontsize = fontsize + 2;
            richTextBox1.Font = new Font("ＭＳ Ｐ明朝",fontsize);
        }

        private void 縮小ToolStripMenuItem_Click(object sender, EventArgs e)//文字を小さくする
        {
            fontsize = fontsize - 2;
            richTextBox1.Font = new Font("ＭＳ Ｐ明朝", fontsize);
        }

        private void 新しいターミナルToolStripMenuItem_Click(object sender, EventArgs e)//ターミナルをリセット
        {
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)//コンパイル
        {
            comp();
        }

        private void comp() //コンパイル
        {
            int error_flag = 0;
            error_flag = save();
            if (error_flag == 1) { return; }

            string dic = Path.GetDirectoryName(fileName);
            string name = Path.GetFileNameWithoutExtension(fileName);


            string txt = "";
            using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
            {
                txt = sr.ReadToEnd();
            }

            int len = txt.Length;
            int main_void;
            int main;
            if (txt.LastIndexOf(" main(void)") == -1)
            {
                main_void = len + 100;
            }
            else
            {
                main_void = txt.LastIndexOf(" main(void)");
            }

            if (txt.LastIndexOf(" main()") == -1)
            {
                main = len + 100;
            }
            else
            {
                main = txt.LastIndexOf(" main()");
            }

            if (txt.IndexOf("return", 0) != -1)
            {
                if ((main_void < txt.LastIndexOf("return")) || (main < txt.LastIndexOf("return")))
                {
                    txt = txt.Insert(txt.LastIndexOf("return"), "while(1){}");
                }
                else
                {
                    txt = txt.Insert(txt.LastIndexOf("}"), "while(1){}");
                }
            }

            if (txt.IndexOf("return", 0) == -1)
            {
                txt = txt.Insert(txt.LastIndexOf("}") - 1, "while(1){}");
            }

            StreamWriter c = File.CreateText(dic + "\\" + name + "_temp.c");
            c.WriteLine(txt);
            c.Close();

            var compile = new ProcessStartInfo()
            {
                FileName = "gcc.exe",
                WorkingDirectory = dic,
                CreateNoWindow = true,
                Arguments = $"{name + "_temp.c"} -finput-charset=utf-8 -fexec-charset=cp932 -o {name + ".exe"}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = Process.Start(compile);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            byte[] out_bytesUTF8 = System.Text.Encoding.Default.GetBytes(output);
            string jis_output = System.Text.Encoding.UTF8.GetString(out_bytesUTF8);
            textBox3.AppendText($"\r\n{jis_output}");

            string error = process.StandardError.ReadToEnd();
            byte[] error_bytesUTF8 = System.Text.Encoding.Default.GetBytes(error);
            string jis_error = System.Text.Encoding.UTF8.GetString(error_bytesUTF8);
            textBox3.AppendText($"\r\n{jis_error}");
            if (jis_error == "")
            {
                textBox3.AppendText("\r\nエラー無く正常にコンパイルされました");
            }
            File.Delete(dic + "\\" + name + "_temp.c");
        }

        private void button2_Click(object sender, EventArgs e)//実行
        {
            int error_flag = 0;
            if (fileName == "New repository")
            {
                error_flag = save();
            }
            error_flag = save();
            if(error_flag == 1) { return; }
            comp();//MessageBox.Show("seikou");
            Thread t = new Thread(new ThreadStart(exe));
            t.Start();
            textBox3.AppendText(output);
        }
        
        private void exe()//実行
        {
            string dic = Path.GetDirectoryName(fileName);
            string name = Path.GetFileNameWithoutExtension(fileName);

            

            Process execution = new Process();

            execution.StartInfo.FileName = $"{dic}\\{name}.exe";

            execution.StartInfo.CreateNoWindow = false;
            execution.StartInfo.UseShellExecute = false;

            execution.Start();
            execution.WaitForExit();
        }

        private void Create_New_KeyDown(object sender, KeyEventArgs e)//ショートカットキーの判定
        {
            int error_flag = 0;
            if (e.KeyData == (Keys.S | Keys.Control))
            {
                error_flag = save();
            }
            if (error_flag == 1) { return; }
            if(e.KeyData == Keys.F11)
            {
                F11_count++;
                if (F11_count % 2 == 0)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.textBox3.Size = new Size(1924,260);
                    フルスクリーンモードToolStripMenuItem.Checked = true;
                }
                else if(F11_count % 2 == 1)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState= FormWindowState.Maximized;
                    this.textBox3.Size = new Size(1924, 185);
                    フルスクリーンモードToolStripMenuItem.Checked = false;
                }
                
            }
            if(e.KeyData == (Keys.Control | Keys.Oemplus))
            {
                fontsize = fontsize + 2;
                richTextBox1.Font = new Font("Courier New", fontsize);
                richTextBox1.SelectionFont = new Font("Courier New", fontsize);
            }
            if(e.KeyData == (Keys.Control | Keys.OemMinus))
            {
                if (2 >= fontsize)
                {
                    return;
                }
                fontsize = fontsize - 2;
                if (2 >= fontsize)
                {
                    return;
                }
                richTextBox1.Font = new Font("Courier New", fontsize);
                richTextBox1.SelectionFont = new Font("Courier New", fontsize);
            }
        }

        private void Create_New_FormClosing(object sender, FormClosingEventArgs e)//保存してないときに閉じようとするとでるやつ
        {
            if(richTextBox1.Text == save_mae)
            {
                return;
            }
            else if(richTextBox1.Text != save_mae)
            {
                DialogResult result = MessageBox.Show("内容が保存されていません。終了しますか？","注意",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    return;
                }
                else if(result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)//セーブ済みかの判定
        {
            hante();
            color(richTextBox1,"#include",Color.Red);
        }


        // User32.dllをインポートしてWin32APIのSendMessageメソッドを定義
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int[] lParam);

        // タブストップを表す定数 (203)
        private const int EM_SETTABSTOPS = 0x00CB;

        // タブ幅を設定する
        private void SetTabWidth(System.Windows.Forms.RichTextBox textBox, int tabWidth)
        {
            // Win32APIのSendMessageメソッドでタブストップ（タブ幅）を設定
            SendMessage(textBox.Handle, EM_SETTABSTOPS, 1, new int[] { tabWidth });
        }

    }
}