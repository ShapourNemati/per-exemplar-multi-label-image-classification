using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;
using System;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class DenseSiftExtractor
    {

        private int spacing = 4;
        private int[] radiuses = { 8, 12, 16, 24, 30 };

        public DenseSiftExtractor()
        {
        }

        public IOutputArray[] ComputeDescriptor(Image<Bgr, byte> img)
        {
            SIFT sift = new SIFT();
            IOutputArray[] descriptorsList = { new Mat(), new Mat(), new Mat(), new Mat(), new Mat() };
            

            int descriptorsIndex = 0;
            foreach (int r in radiuses)
            {
                int pointsSize = 
                    (int) Math.Ceiling((double)(img.Width - 2 * r) / spacing) 
                    * 
                    (int) Math.Ceiling((double)(img.Height - 2 * r) / spacing);
                int keypoint_index = 0;
                MKeyPoint[] points = new MKeyPoint[pointsSize];

                for (int i = r; i < img.Width - r; i += spacing)
                {
                    for (int j = r; j < img.Height - r; j += spacing)
                    {
                        MKeyPoint p = new MKeyPoint();
                        p.Point = new System.Drawing.PointF(i, j);
                        p.Size = r;
                        points.SetValue(p, keypoint_index++);
                    }
                }
                Emgu.CV.Util.VectorOfKeyPoint keyPoints = new Emgu.CV.Util.VectorOfKeyPoint(points);
                IOutputArray descriptors = new Mat();
                sift.Compute(img, keyPoints, descriptors);
                descriptorsList.SetValue(descriptors, descriptorsIndex);
                descriptorsIndex++;
            }

            return descriptorsList;
        }

    }
}
