using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;

namespace PerExemplarMultiLabelImageClassification.MultiClassRanking
{
    class DenseSiftExtractor
    {

        private int spacing = 4;
        private int[] radiuses = { 8, 12, 16, 24, 30 };

        public DenseSiftExtractor()
        {
        }

        public IOutputArray ComputeDescriptor(Image<Bgr, byte> img, VectorOfKeyPoint keyPoints)
        {
            SIFT sift = new SIFT();
            IOutputArray descriptors = new Mat();
            sift.Compute(img, keyPoints, descriptors);
            return descriptors;
        }

        public VectorOfKeyPoint DenseSampling(Image<Bgr, byte> img)
        {
            List<MKeyPoint> allKeyPoints = new List<MKeyPoint>();

            foreach (int r in radiuses)
            {
                for (int i = r; i < img.Width - r; i += spacing)
                {
                    for (int j = r; j < img.Height - r; j += spacing)
                    {
                        MKeyPoint p = new MKeyPoint();
                        p.Point = new System.Drawing.PointF(i, j);
                        p.Size = r;
                        allKeyPoints.Add(p);
                    }
                }
            }
            VectorOfKeyPoint keyPoints = new VectorOfKeyPoint(allKeyPoints.ToArray());
            return keyPoints;
        }
    }
}
