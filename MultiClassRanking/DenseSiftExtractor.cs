using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;
using System;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class DenseSiftExtractor
    {

        private int spacing = 4;

        public DenseSiftExtractor()
        {
        }

        public IOutputArray ComputeDescriptor(Image<Bgr, byte> img)
        {
            SIFT sift = new SIFT();

            
            int pointsSize = 
                (int) Math.Ceiling((double)img.Width / spacing) 
                * 
                (int) Math.Ceiling((double)img.Height / spacing);

            int index = 0;
            MKeyPoint[] points = new MKeyPoint[pointsSize];
            for (int i = 0; i < img.Width; i += spacing)
            {
                for (int j = 0; j < img.Height; j += spacing)
                {
                    MKeyPoint p = new MKeyPoint();
                    p.Point = new System.Drawing.PointF(i, j);
                    p.Size = 8;
                    points.SetValue(p, index++);
                }
            }

            Emgu.CV.Util.VectorOfKeyPoint keyPoints = new Emgu.CV.Util.VectorOfKeyPoint(points);
            IOutputArray descriptors = new Mat();
            sift.Compute(img, keyPoints, descriptors);
            return descriptors;
        }

    }
}
