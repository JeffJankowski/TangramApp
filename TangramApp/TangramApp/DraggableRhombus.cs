using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libSMARTMultiTouch.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace TangramApp
{
    class DraggableRhombus : DraggableBorder
    {
        public DraggableRhombus(int scaleFactor)
            : base()
        {
            //create a Rhombus
            Polygon poly1 = new Polygon();
            poly1.Stroke = System.Windows.Media.Brushes.Black;
            poly1.Fill = System.Windows.Media.Brushes.Tan;
            poly1.StrokeThickness = 2;
            // poly.HorizontalAlignment = HorizontalAlignment.Left;
            // poly.VerticalAlignment = VerticalAlignment.Center;
            System.Windows.Point Point1 = new System.Windows.Point(scaleFactor, 0);
            System.Windows.Point Point2 = new System.Windows.Point(scaleFactor * 3, 0);
            System.Windows.Point Point3 = new System.Windows.Point(scaleFactor * 2, scaleFactor);
            System.Windows.Point Point4 = new System.Windows.Point(0, scaleFactor);
            PointCollection myPoints = new PointCollection();
            myPoints.Add(Point1);
            myPoints.Add(Point2);
            myPoints.Add(Point3);
            myPoints.Add(Point4);
            poly1.Points = myPoints;
            Content = poly1;
        }
    }
}
