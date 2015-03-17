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
 * Represents a Large Triangle Tangram shape.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public class LargeTriangle : Tangram
    {
        /**
         * Creates a new Large Triangle based on the given attributes.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public LargeTriangle(double x, double y, double scale)
            : base()
        {
            initPoints(x, y, scale);
            initLabels();
            updateEdges();
        }

        /**
         * This sets the starting vertices of the Large Triangle.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public override void initPoints(double x, double y, double scale)
        {
            Double side = 1D / Math.Sqrt(2D) * scale;
            points.Add(new Point(x, y));
            points.Add(new Point(x, y + side));
            points.Add(new Point(x + side, y + side));
            poly.Points = points;
            Content = poly;
        }

    }
}
