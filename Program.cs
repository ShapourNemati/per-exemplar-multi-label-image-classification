using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using PerExemplarMultiLabelImageClassification.MultiClassRanking;

namespace PerExemplarMultiLabelImageClassification
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MultiClassRankingForm());
            TestDenseSiftExctracotr();
        }

        static void TestDenseSiftExctracotr()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            Mat descriptor = (Mat)extractor.ComputeDescriptor(img);
            Console.WriteLine(descriptor.GetValueRange().Max + " " + descriptor.GetValueRange().Min);
            Console.ReadLine();
        }
    }
}
