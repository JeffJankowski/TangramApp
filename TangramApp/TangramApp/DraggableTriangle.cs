using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libSMARTMultiTouch.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TangramApp
{
    class DraggableTriangle: DraggableBorder
    {
        public DraggableTriangle(int scaleFactor)
            : base()
        {
            //create a Triangle
            Polygon poly1 = new Polygon();
            poly1.Stroke = System.Windows.Media.Brushes.Black;
            poly1.Fill = System.Windows.Media.Brushes.Tan;
            poly1.StrokeThickness = 2;
            // poly.HorizontalAlignment = HorizontalAlignment.Left;
            // poly.VerticalAlignment = VerticalAlignment.Center;
            System.Windows.Point Point1 = new System.Windows.Point(0, 0);
            System.Windows.Point Point2 = new System.Windows.Point(0, scaleFactor * 2);
            System.Windows.Point Point3 = new System.Windows.Point(scaleFactor, scaleFactor);
            PointCollection myPoints = new PointCollection();
            myPoints.Add(Point1);
            myPoints.Add(Point2);
            myPoints.Add(Point3);
            poly1.Points = myPoints;
            Content = poly1;
        }
    }
}
