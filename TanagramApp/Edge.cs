using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

/**
 * Represents and edge between two Point objects. Edges are used to determine if
 * linear segments are insersecting between two pairs of Points.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace libSMARTMultiTouch.Controls
{
    public class Edge
    {
        public Point start, end;

        /**
         * Creates a new Edge using the starting and ending Points.
         * 
         * @param start The starting point
         * @param end The ending point
         */
        public Edge(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        /**
         * Determines if this Edge is intersecting with the given Edge.
         * s
         * Note: Algorithm adapted from Mario Cossi
         * http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/e5993847-c7a9-46ec-8edc-bfb86bd689e3
         * 
         * @param edge The Edge to check against
         * @return True if intersecting
         *         False if not
         */
        public bool IsIntersecting(Edge edge)
        {
            double deltaACy = this.start.Y - edge.start.Y;
            double deltaDCx = edge.end.X - edge.start.X;
            double deltaACx = this.start.X - edge.start.X;
            double deltaDCy = edge.end.Y - edge.start.Y;
            double deltthisAx = this.end.X - this.start.X;
            double deltthisAy = this.end.Y - this.start.Y;

            double denominator = deltthisAx * deltaDCy - deltthisAy * deltaDCx;
            double numerator = deltaACy * deltaDCx - deltaACx * deltaDCy;

            if (denominator == 0)
            {
                if (numerator == 0)
                {
                    // collinear. Potentially infinite intersection points.
                    // Check and return one of them.
                    if (this.start.X >= edge.start.X && this.start.X <= edge.end.X)
                        return true;
                    else if (edge.start.X >= this.start.X && edge.start.X <= this.end.X)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }

            double r = numerator / denominator;
            if (r < 0 || r > 1)
                return false;

            double s = (deltaACy * deltthisAx - deltaACx * deltthisAy) / denominator;
            if (s < 0 || s > 1)
                return false;

            return true;
        }

        /**
         * Overriden ToString method that displays the start and end Points.
         * 
         * @return Point representation of this Edge.
         */
        public override string ToString()
        {
            return "(" + start + ", " + end + ")";
        }
    }
}
