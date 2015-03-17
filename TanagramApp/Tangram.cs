using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

/**
 * Abstract representation of a basic Tangram shape that extends SMART's
 * DraggableBorder class and sets the content to a System.Windows.Shapes.Polygon.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public abstract class Tangram : DraggableBorder
    {
        //polygon shape
        public Polygon poly;
        //fill color of the tangram
        public Color color;
        //vertices that are constantly updated
        public PointCollection points;
        //vertex labels
        public List<Label> labels;
        //pixel width of the label
        public int widthLabel;
        //list of shape Edges
        public List<Edge> edges;

        /**
         * Calls the superclass constructor and initializes the fields. 
         */
        public Tangram()
            :base()
        {
            IsScaleEnabled = false;
            IsFlickEnabled = false;
            color = Colors.Tan;

            points = new PointCollection();
            edges = new List<Edge>();
            poly = new Polygon();
            labels = new List<Label>();

            poly.Stroke = System.Windows.Media.Brushes.Black;
            poly.Fill = new SolidColorBrush(color);
            poly.StrokeThickness = 1;
        }

        /**
         * Updates the vertex points and vertex labels.
         * 
         * @param The Canvas object from the main TableControl
         */
        public void updatePointsAndLabels(Canvas canvas)
        {
            PointCollection pts = new PointCollection();
            int count = 0;

            //we take each original point from the polygon and apply 
            //the transformation to it
            foreach (Point pt in poly.Points)
            {
                Point p = TranslatePoint(pt, canvas);
                pts.Add(p);

                //update label
                Label l = labels[count];

                l.Content = "(" + (int)p.X + " , " + (int)p.Y + ")";
                //these set the position
                l.SetValue(Canvas.LeftProperty, p.X - (widthLabel / 2.0));
                l.SetValue(Canvas.TopProperty, p.Y - 25.0);

                count++;
            }

            points = pts;
        }

        /**
         * Updates the Tangram Edges based on the vertices.
         */
        public void updateEdges()
        {
            edges.Clear();

            for (int i = 0; i < points.Count; i++)
            {
                if ((i + 1) < points.Count)
                    edges.Add(new Edge(points[i], points[i + 1]));
                else
                    edges.Add(new Edge(points[i], points[0]));
            }
        }

        /**
         * Initializes the Tangram's starting vertex labels.
         */
        public void initLabels()
        {
            foreach (Point pt in points)
            {
                Label l = new Label();
                widthLabel = 160;
                l.Width = widthLabel;
                l.VerticalContentAlignment = VerticalAlignment.Center;
                l.HorizontalContentAlignment = HorizontalAlignment.Center;
                l.FontWeight = FontWeights.Bold;
                l.Foreground = Brushes.DarkRed;
                //make them always appear on top
                l.SetValue(Canvas.ZIndexProperty, 9999);

                l.Content = "(" + (int)pt.X + " , " + (int)pt.Y + ")";
                //these set the position
                l.SetValue(Canvas.LeftProperty, pt.X - (widthLabel / 2.0));
                l.SetValue(Canvas.TopProperty, pt.Y - 10);

                labels.Add(l);
            }
        }

        /**
         * All Tangram subclasses must implement this method.
         * This sets the starting vertices of each specific Tangram, which
         * defines their shape.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public abstract void initPoints(double x, double y, double scale);
    }
}
