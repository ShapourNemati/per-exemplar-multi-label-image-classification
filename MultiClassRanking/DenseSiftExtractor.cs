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
            IOutputArray[] descriptors = { new Mat(), new Mat(), new Mat(), new Mat(), new Mat() };
            
            int pointsSize = 
                (int) Math.Ceiling((double)img.Width / spacing) 
                * 
                (int) Math.Ceiling((double)img.Height / spacing);

            int maxRadius = radiuses[radiuses.Length - 1];
            int descriptorsIndex = 0;
            foreach (int r in radiuses)
            {
                int keypoint_index = 0;
                MKeyPoint[] points = new MKeyPoint[pointsSize];
                for (int i = maxRadius; i < img.Width - maxRadius; i += spacing)
                {
                    for (int j = maxRadius; j < img.Height - maxRadius; j += spacing)
                    {
                        MKeyPoint p = new MKeyPoint();
                        p.Point = new System.Drawing.PointF(i, j);
                        p.Size = r;
                        points.SetValue(p, keypoint_index++);
                    }
                }
                Emgu.CV.Util.VectorOfKeyPoint keyPoints = new Emgu.CV.Util.VectorOfKeyPoint(points);
                IOutputArray descriptor = new Mat();
                sift.Compute(img, keyPoints, descriptor);
                descriptors.SetValue(descriptor, descriptorsIndex);
                descriptorsIndex++;
            }

            return descriptors;
        }

    }
}
