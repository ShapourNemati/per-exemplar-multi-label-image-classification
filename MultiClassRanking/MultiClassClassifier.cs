using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class MultiClassClassifier
    {

        private List<float[]> histograms;
        private string[] dbImages;

        public MultiClassClassifier()
        {

        }

        public void Train(string[] dbImageFilePaths)
        {
            this.dbImages = dbImageFilePaths;
            DenseSiftExtractor extractor = new DenseSiftExtractor();
            Image<Bgr, byte>[] dbImages = new Image<Bgr, byte>[dbImageFilePaths.Length];
            VectorOfKeyPoint[] keyPoints = new VectorOfKeyPoint[dbImageFilePaths.Length];
            Mat[] descriptors = new Mat[dbImageFilePaths.Length];
            for (int i = 0; i < dbImages.Length; i++)
            {
                dbImages[i] = new Image<Bgr, byte>(dbImageFilePaths[i]);
                keyPoints[i] = extractor.DenseSampling(dbImages[i]);
                descriptors[i] = (Mat) extractor.ComputeDescriptor(dbImages[i], keyPoints[i]);
            }
            
            var codeBook = new VocabularyCodebook();
            codeBook.computeVocabulary(descriptors, new SIFT());
            this.histograms = new List<float[]>();
            for (int i = 0; i < dbImages.Length; i++)
            {
                histograms.Add(codeBook.getHistogram(descriptors[i]));
            }
        }
    }

}
