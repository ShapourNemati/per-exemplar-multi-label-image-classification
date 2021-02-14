using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class VocabularyCodebook
    {

        private int codewordsNumber = 256;
        private Mat vocabulary;
        public VocabularyCodebook()
        {

        }

        public Mat getVocabulary(Mat[] descriptors)
        {
            BOWKMeansTrainer bowTrainer = new BOWKMeansTrainer(codewordsNumber, new MCvTermCriteria(10, 0.01), 3, KMeansInitType.PPCenters);
            foreach (Mat descriptor in descriptors)
            {
                bowTrainer.Add(descriptor);
            }
            Mat vocabulary = new Mat();
            bowTrainer.Cluster(vocabulary);
            return vocabulary;
        }

        public void computeVocabulary(Mat[] descriptors, Feature2D featureExtractor)
        {
            this.vocabulary = getVocabulary(descriptors);
        }

        public float[] getHistogram(Mat descriptors)
        {
            double minVal = Double.MaxValue;
            int minIndex = 0;
            float[] histogram = new float[vocabulary.Height];
            for (int i = 0; i < descriptors.Rows; i++)
            {
                Mat d = descriptors.Row(i);
                for (int j = 0; j < this.vocabulary.Rows; j++)
                {
                    Mat v = this.vocabulary.Row(j);
                    double x = CvInvoke.Norm(d, v, NormType.L1);
                    if (x < minVal)
                    {
                        minVal = x;
                        minIndex = j;
                    }
                }
                histogram[minIndex]++;
            }
            return histogram;
        }

    }
}
