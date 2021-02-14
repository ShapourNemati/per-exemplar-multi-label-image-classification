using PerExemplarMultiLabelImageClassification.MultiClassRanking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PerExemplarMultiLabelImageClassification
{
    public partial class MultiClassRankingForm : Form
    {

        private MultiClassClassifier classifier;
        public MultiClassRankingForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.SelectedPath;
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var imageList = Directory.GetFiles(textBox1.Text, "*.jpg");
            this.classifier = new MultiClassClassifier();
            this.classifier.Train(imageList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && Directory.Exists(textBox1.Text))
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}
