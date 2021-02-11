using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
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
            extractor.DenseSampling(img);
            extractor.ComputeDescriptor(img, extractor.DenseSampling(img));
            Console.ReadLine();
        }

        static void TestBagOfWordsCreation()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            Image<Bgr, byte> img2 = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0010_-_piatto_pavona_cm_20.jpg");
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            Mat x = (Mat) extractor.ComputeDescriptor(img, extractor.DenseSampling(img));
            Mat y = (Mat) extractor.ComputeDescriptor(img2, extractor.DenseSampling(img2));
            Mat[] descriptors = { x, y };
            var vocabulary = new VocabularyCodebook().getVocabulary(descriptors);
            Console.ReadLine();
        }

        static void TestHistogramFromCodebook()
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            Image<Bgr, byte> img2 = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0010_-_piatto_pavona_cm_20.jpg");
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            VectorOfKeyPoint imgKeyPoints = extractor.DenseSampling(img);
            Mat x = (Mat)extractor.ComputeDescriptor(img, imgKeyPoints);
            VectorOfKeyPoint imgKeyPoints2 = extractor.DenseSampling(img2);
            Mat y = (Mat)extractor.ComputeDescriptor(img2, imgKeyPoints2);
            Mat[] descriptors = { x, y };
            var codeBook = new VocabularyCodebook();
            codeBook.computeVocabulary(descriptors, new SIFT());
            var z = codeBook.getHistogram(x, imgKeyPoints);
            Console.ReadLine();
        }
    }
}
