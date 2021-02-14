using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class MultiStepTraining
    {

        private List<Mat> descriptors;
        float[][] descriptors2;

        public MultiStepTraining()
        {
            this.descriptors = new List<Mat>();
        }

        public void ComputeDescriptor(Image<Bgr, byte> img, DenseSiftExtractor extractor)
        {
            VectorOfKeyPoint keyPoints = extractor.DenseSampling(img);
            Mat descriptors = (Mat) extractor.ComputeDescriptor(img, keyPoints);
            this.descriptors.Add(descriptors);
        }

        public void SaveDescriptorsToFile(string filename)
        {
            Mat toSave = new Mat();
            foreach (Mat mat in this.descriptors)
            {
                toSave.PushBack(mat);
            }
            toSave.Save(filename);
        }

        public void LoadDescriptorsFromFile(string filename)
        {
            Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
            this.descriptors.Add(mat);
        }

        public void ComputeCodeBook()
        {

        }

        public void SaveCodeBookToFile(string filename)
        {

        }

        public void LoadCodeBookFromFile(string filename)
        {

        }

        public void TrainModel(Mat descriptor, string className)
        {

        }

        public void SaveModelToFile(string filename)
        {

        }

        public void LoadModelFromFile(string filename)
        {

        }

    }
}
