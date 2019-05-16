using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        private int wordCount;
        private static string _filePath;
        private bool isSaved;

        public Form1()
        {
            InitializeComponent();
            wordCount = 0;
            timer1.Start();
            timer1.Tick += WordCouter;
            isSaved = false;
        }

        private void WordCouter(object sender, EventArgs e)
        {
            string[] words = richTextBox1.Text.Split(new char[] { ' ', ',', '.', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            wordCount = words.Length;
            WordsCountLabel.Text = wordCount.ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = richTextBox1.SelectionFont;
            if (oldFont.Bold)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
            richTextBox1.SelectionFont = newFont;
            richTextBox1.Focus();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = richTextBox1.SelectionFont;
            if (oldFont.Underline)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            richTextBox1.SelectionFont = newFont;
            richTextBox1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Font oldFont;
            Font newFont;
            oldFont = richTextBox1.SelectionFont;
            if (oldFont.Italic)
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            else
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
            richTextBox1.SelectionFont = newFont;
            richTextBox1.Focus();
        }

        private void toolStripButton4_Click(object sender, EventArgs e) //по лівому
        {
            if (this.richTextBox1.SelectionAlignment != HorizontalAlignment.Left)
                this.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            this.richTextBox1.Focus();
        }

        private void toolStripButton5_Click(object sender, EventArgs e) //центр
        {
            if (this.richTextBox1.SelectionAlignment != HorizontalAlignment.Center)
                this.richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            this.richTextBox1.Focus();
        }

        private void toolStripButton6_Click(object sender, EventArgs e) //по правому
        {
            if (this.richTextBox1.SelectionAlignment != HorizontalAlignment.Right)
                this.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            this.richTextBox1.Focus();
        }


        private void FontSizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            float newSize = (float)FontSizeNumeric.Value;
            FontFamily currentFontFamily;
            Font newFont;
            currentFontFamily = this.richTextBox1.SelectionFont.FontFamily;
            newFont = new Font(currentFontFamily, newSize);
            this.richTextBox1.SelectionFont = newFont;
            this.richTextBox1.Focus();
        }
        private void newMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Clear();
            isSaved = false;
        }

        private void openMenuItem_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Choose file for edit";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                    System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
                _filePath = openFileDialog1.FileName;
                isSaved = false;
            }


        }

        private void saveAsMenuItem_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Choose place, where save your document";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                isSaved = true;

            }
        }
        private void saveMenuItem_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                saveAsMenuItem_Click(sender, e);
                return;
            }

            System.IO.File.WriteAllText(_filePath, richTextBox1.Text);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program for editing text.\n\nAuthor: Igor Sas");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                DialogResult dr = MessageBox.Show("Save file?", "File is not saved!", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                switch (dr)
                {
                    case DialogResult.Yes: saveAsMenuItem_Click(sender, e); break;
                    case DialogResult.No: break;
                    case DialogResult.Cancel: e.Cancel = true; break;
                }
            }
        }
    }
}
