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
            TestBagOfWordsCreation();
        }

        static void TestDenseSiftExctracotr()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            extractor.ComputeDescriptor(img);
            Console.ReadLine();
        }

        static void TestBagOfWordsCreation()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            Image<Bgr, byte> img2 = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0010_-_piatto_pavona_cm_20.jpg");
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            var x = extractor.ComputeDescriptor(img);
            var y = extractor.ComputeDescriptor(img2);
            Mat[] descriptors = new Mat[x.Length + y.Length];
            int i = 0;
            foreach (Mat desc in x)
            {
                descriptors[i++] = desc;
            }
            foreach (Mat desc in y)
            {
                descriptors[i++] = desc;
            }
            var vocabulary = new VocabularyCodebook().getVocabulary(descriptors);
            Console.ReadLine();
        }
    }
}
