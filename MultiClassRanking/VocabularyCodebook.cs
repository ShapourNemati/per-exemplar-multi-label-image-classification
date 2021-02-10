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

        public VocabularyCodebook()
        {

        }

        public Mat getVocabulary(Mat[] descriptors)
        {
            BOWKMeansTrainer bowTrainer = new BOWKMeansTrainer(codewordsNumber, new MCvTermCriteria(100, 0.01), 3, KMeansInitType.PPCenters);
            foreach (Mat descriptor in descriptors)
            {
                bowTrainer.Add(descriptor);
            }
            Mat vocabulary = new Mat();
            bowTrainer.Cluster(vocabulary);
            return vocabulary;
        }

    }
}
