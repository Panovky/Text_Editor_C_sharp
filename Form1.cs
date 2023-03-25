using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string old_text = "";
        string new_text = "";

        void save_as_file()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                old_text = textBox1.Text;
                File.WriteAllText(path, old_text);
                this.Text = $"Текстовый редактор - {path}";
            }
        }

        void save_file()
        {
            string path = this.Text.Substring(21);

            if (path == "Безымянный")
            {
                save_as_file();
            }
            else
            {
                old_text = textBox1.Text;
                File.WriteAllText(path, old_text);
            }
        }

        void return_to_default()
        {
            textBox1.Font = new Font("Arial", 12, FontStyle.Regular);
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.White;
        }

        bool check_changes()
        {
            if (this.Text == "Текстовый редактор")
            {
                return true;
            }

            new_text = textBox1.Text;

            if (new_text != old_text)
            {
                const string message = "Сохранить внесенные в текущий файл изменения?";
                const string caption = "Текстовый редактор";
                DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    save_file();
                    this.Text = "Текстовый редактор";
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    this.Text = "Текстовый редактор";
                    return true;
                }

                return false;
            }

            return true;
        }

        private void create_click(object sender, EventArgs e)
        {
            if (check_changes())
            {
                textBox1.Clear();
                return_to_default();
                textBox1.Visible = true;
                this.Text = "Текстовый редактор - Безымянный";
                old_text = "";
            }
        }

        private void open_click(object sender, EventArgs e)
        {
            if (check_changes())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    old_text = File.ReadAllText(path);
                    textBox1.Text = old_text;
                    return_to_default();
                    textBox1.Visible = true;
                    this.Text = $"Текстовый редактор - {path}";
                }
            }    
        }

        private void save_click(object sender, EventArgs e)
        {
            save_file();
        }

        private void save_as_click(object sender, EventArgs e)
        {
            save_as_file();
        }

        private void exit_click(object sender, EventArgs e) 
        {
            if (check_changes())
            {
                textBox1.Visible = false;
                this.Text = "Текстовый редактор";
            } 
        }

        private void font_click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.HideSelection = true;
            }   
        }

        private void forecolor_click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;
                textBox1.HideSelection = true;
            }            
        }

        private void backcolor_click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
            }           
        }

        private void about_programm_click(object sender, EventArgs e)
        {
            const string message = "Программа разработана для редактирования текстовых файлов с расширением .txt" +
                "\n\nРазработчик: Панова К.А." + "\n\n2023 г.";
            const string caption = "Текстовый редактор";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

    }
}