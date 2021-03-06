﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

/**
 * Represents a Square Tangram shape.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public class Square : Tangram
    {
        /**
         * Creates a new Square based on the given attributes.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public Square(double x, double y, double scale)
            : base()
        {
            initPoints(x, y, scale);
            initLabels();
            updateEdges();
        }

        /**
         * This sets the starting vertices of the Square.
         * 
         * @param x The starting x position
         * @param y The starting y position
         * @param scale The scale constant used in this application
         *              (the longest side of the largest tangram)
         */
        public override void initPoints(double x, double y, double scale)
        {
            double side = Math.Sqrt(2D) / 4D * scale;
            points.Add(new System.Windows.Point(x, y));
            points.Add(new System.Windows.Point(x, y + side));
            points.Add(new System.Windows.Point(x + side, y + side));
            points.Add(new System.Windows.Point(x + side, y));
            poly.Points = points;
            Content = poly;
        }

    }
}
