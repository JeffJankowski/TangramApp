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
 * Represents a Medium Triangle Tangram shape.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public class MediumTriangle : Tangram
    {
        /**
         * Creates a new Medium Triangle based on the given attributes.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public MediumTriangle(double x, double y, double scale)
            : base()
        {
            initPoints(x, y, scale);
            initLabels();
            updateEdges();
        }

        /**
         * This sets the starting vertices of the Medium Triangle.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public override void initPoints(double x, double y, double scale)
        {
            double side = 1D / 2D * scale;
            points.Add(new System.Windows.Point(x, y));
            points.Add(new System.Windows.Point(x, y + side));
            points.Add(new System.Windows.Point(x + side, y + side));
            poly.Points = points;
            Content = poly;
        }

    }
}
