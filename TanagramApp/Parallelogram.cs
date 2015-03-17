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
 * Represents a Parallelogram Tangram shape.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public class Parallelogram : Tangram
    {
        /**
         * Creates a new Parallelogram based on the given attributes.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public Parallelogram(double x, double y, double scale)
            : base()
        {
            initPoints(x, y, scale);
            initLabels();
            updateEdges();
        }

        /**
         * This sets the starting vertices of the Parallelogram.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public override void initPoints(double x, double y, double scale)
        {
            double smallSide = 1D / 4D * scale;
            double largeSide = 1D / 2D * scale;
            points.Add(new System.Windows.Point(x, y));
            points.Add(new System.Windows.Point(x, y + largeSide));
            points.Add(new System.Windows.Point(x + smallSide, y + largeSide + smallSide));
            points.Add(new System.Windows.Point(x + smallSide, y + smallSide));
            poly.Points = points;
            Content = poly;
        }

    }
}
