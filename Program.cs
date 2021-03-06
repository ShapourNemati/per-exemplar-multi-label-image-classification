﻿using System;
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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MultiClassRankingForm());
            //TestHistogramFromCodebook();
            TestMultiStepTraining();
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
            Console.WriteLine("Computing 1st image descriptors");
            Console.WriteLine(DateTime.Now);
            VectorOfKeyPoint imgKeyPoints = extractor.DenseSampling(img);
            Mat x = (Mat)extractor.ComputeDescriptor(img, imgKeyPoints);
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Computing 2nd image descriptors");
            VectorOfKeyPoint imgKeyPoints2 = extractor.DenseSampling(img2);
            Mat y = (Mat)extractor.ComputeDescriptor(img2, imgKeyPoints2);
            Console.WriteLine(DateTime.Now);
            Mat[] descriptors = { x, y };
            var codeBook = new VocabularyCodebook();
            Console.WriteLine("Computing codebook vocabulary");
            Console.WriteLine(DateTime.Now);
            codeBook.computeVocabulary(descriptors, new SIFT());
            Console.WriteLine("Getting img histogram");
            Console.WriteLine(DateTime.Now);
            var z = codeBook.getHistogram(x);
            Console.WriteLine(DateTime.Now);
            Console.ReadLine();
        }

        static void TestMultiStepTraining()
        {
            MultiStepTraining t = new MultiStepTraining();
            Image<Bgr, byte> img = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0018_-_piatto_palmetta_40cm_2.jpg");
            Image<Bgr, byte> img2 = new Image<Bgr, byte>("E:\\UNI\\VISIONE\\Lab\\02 - CBIR system-20191016\\CeramicheFaenza\\Test\\0010_-_piatto_pavona_cm_20.jpg");
            var e = new DenseSiftExtractor();
            t.ComputeDescriptor(img, e);
            t.ComputeDescriptor(img2, e);
            t.SaveDescriptorsToFile("a.bmp");
            MultiStepTraining t2 = new MultiStepTraining();
            t2.LoadDescriptorsFromFile("a.bmp");
        }
    }
}
