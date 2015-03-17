using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using libSMARTMultiTouch.Controls;
using libSMARTMultiTouch.Input;
using libSMARTMultiTouch.Behaviors;
using libSMARTMultiTouch;

/**
 * The main TableControl that drives the application.
 * Adapted from SMART's demo application.
 * 
 * @author Jeff Jankowski (hoodles)
 * @version 05.08.2010
 */
namespace TouchApplication
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : TableApplicationControl
    {
        private Square square;
        private Parallelogram parallel;
        private SmallTriangle sTriangle1, sTriangle2;
        private MediumTriangle mTriangle;
        private LargeTriangle lTriangle1, lTriangle2;
        private ArrayList tans;
        private const double SCALE_FACTOR = 250D;
        private bool debug;

        private Label hitTest;

        private Canvas canvas = new Canvas();

        // Associate a guaranteed ID with an IDProperties record properties (including a corrected ID) assoicated with the touch ID
        // Note: always use the IDProperties.ID rather than the touch ID for any array indexing, 
        // as Mouse IDs are not guaranteed to start from 0. The dictIDProperties.ID corrects this.
        private Dictionary<int, IDProperties> dictIDProperties = new Dictionary<int, IDProperties>();

        Random random = new Random(); // To randomly place DraggableBorders on the display

        /**
         * Initializes the application.
         */
        public TableControl()
        {
            InitializeComponent();
        }

        /**
         * Sets up the Tangram application once the SMART Table software has been loaded.
         */
        private void TableApplicationControl_Loaded(object sender, RoutedEventArgs e)
        {
            //turn point labels on or off
            debug = true;

            canvas.Background = new SolidColorBrush(Colors.Transparent); //Can see through to any graphics in the top level grid.

            // Add touch handlers to the canvas
            TouchInputManager.AddTouchContactDownHandler(canvas, new TouchContactEventHandler(Canvas_TouchDown));
            TouchInputManager.AddTouchContactMoveHandler(canvas, new TouchContactEventHandler(Canvas_TouchMove));
            TouchInputManager.AddTouchContactUpHandler(canvas, new TouchContactEventHandler(Canvas_TouchUp));

            //Add the canvas to the grid
            TableLayoutRoot.Children.Add(canvas);

            initTans();
            scatterTans();

            //debugging hit test label
            hitTest = new Label();
            hitTest.Width = 300;
            hitTest.VerticalContentAlignment = VerticalAlignment.Center;
            hitTest.HorizontalContentAlignment = HorizontalAlignment.Center;
            hitTest.FontWeight = FontWeights.Bold;
            hitTest.Foreground = Brushes.Blue;
            hitTest.SetValue(Canvas.ZIndexProperty, 9999);
            hitTest.Content = "";
            hitTest.SetValue(Canvas.LeftProperty, this.ActualWidth - 400);
            hitTest.SetValue(Canvas.TopProperty, this.ActualHeight - 50);
            canvas.Children.Add(hitTest);
        }

        /**
         * Creates the basic Tangram objects and adds them to the Canvas.
         */
        private void initTans()
        {
            //starting position for tans to be scattered from
            double x = 0D;
            double y = 0D;
            //list to keep track of all the tangrams
            tans = new ArrayList();

            parallel = new Parallelogram(x, y, SCALE_FACTOR);
            parallel.Name = "Parallelogram";
            square = new Square(x, y, SCALE_FACTOR);
            square.Name = "Square";
            sTriangle1 = new SmallTriangle(x, y, SCALE_FACTOR);
            sTriangle1.Name = "Small_Triangle_1";
            sTriangle2 = new SmallTriangle(x, y, SCALE_FACTOR);
            sTriangle2.Name = "Small_Triangle_2";
            mTriangle = new MediumTriangle(x, y, SCALE_FACTOR);
            mTriangle.Name = "Medium_Triangle";
            lTriangle1 = new LargeTriangle(x, y, SCALE_FACTOR);
            lTriangle1.Name = "Large_Triangle_1";
            lTriangle2 = new LargeTriangle(x, y, SCALE_FACTOR);
            lTriangle2.Name = "Large_Triangle_2";
   
            tans.Add(square);
            tans.Add(sTriangle1);
            tans.Add(lTriangle1);
            tans.Add(parallel);
            tans.Add(sTriangle2);
            tans.Add(mTriangle);
            tans.Add(lTriangle2);

            canvas.Children.Add(parallel);
            canvas.Children.Add(square);
            canvas.Children.Add(sTriangle1);
            canvas.Children.Add(sTriangle2);
            canvas.Children.Add(mTriangle);
            canvas.Children.Add(lTriangle1);
            canvas.Children.Add(lTriangle2);
            
            //add transformation listeners
            foreach (Tangram shape in tans)
            {
                //TouchInputManager.AddTouchContactDownHandler(shape, new TouchContactEventHandler(Tan_TouchContactDown));
                //shape.RestPositionReached += new EventHandler(Tan_RestPositionReached);
                shape.TranslateTransformUpdated += new EventHandler(Tan_Transformed);
                shape.RotateTransformUpdated += new EventHandler(Tan_Transformed);

                //add vertex labels if debugging is on
                foreach (Label l in shape.labels)
                {
                    if (debug)
                        canvas.Children.Add(l);
                }
            }
        }

        /**
         * Scatters the tangrams randomly, with as much spacing as possible to 
         * avoid starting collisions.
         */
        private void scatterTans()
        {
            double maxX = this.ActualWidth - SCALE_FACTOR;
            double maxY = this.ActualHeight - SCALE_FACTOR;

            int count = 0;
            int countY = 0;

            //this should scatter tans without them touching
            foreach (Tangram shape in tans)
            {
                /*shape.AnimateRotateTranslate(random.Nextdouble() * 360D, random.Nextdouble() * maxX, 
                    random.Nextdouble() * maxY, 0.5, 0.5, new TimeSpan(0, 0, 2));*/
                
                shape.AnimateRotateTranslate(random.NextDouble() * 360D, this.random.Next((int)((maxX / 7) * count), (int)((maxX / 7) * (count + 1))),
                    this.random.Next((int)((maxY / 2) * countY), (int)((maxY / 2) * (countY + 1))), 0.5, 0.5, new TimeSpan(0, 0, 2));

                count++;
                countY++;
                countY %= 2;
            }
        }

        private void Tan_TouchContactDown(object sender, TouchContactEventArgs e)
        {
            
            
        }

        private void Tan_RestPositionReached(Object sender, EventArgs e)
        {
            
        }

        /**
         * Updates tangram's points, edges, labels on each transformation.
         * 
         * @param sender The object that was transformed
         * @param e Event arguments
         */
        private void Tan_Transformed(Object sender, EventArgs e)
        {
            //we know its a Tangram object, so cast it
            Tangram senderTan = (Tangram)sender;

            senderTan.updatePointsAndLabels(canvas);
            senderTan.updateEdges();

            //update hit test to current tangram
            hitTest.Content = "Hit Test: " + senderTan.Name;

            //check for collisions
            // (algorithm could be more efficient)
            foreach (Edge edge in senderTan.edges)
            {
                foreach (Tangram tan in tans)
                {
                    if (tan != senderTan)
                    {
                        foreach (Edge checkEdge in tan.edges)
                        {
                            if (edge.IsIntersecting(checkEdge))
                            {
                                hitTest.Content = "Hit Test: " + senderTan.Name + " and " + tan.Name;
                            }
                        }
                    }
                }
            }

        }



        #region The Canvas Callbacks (used for drawing on the canvas)

        // These callbacks allow you to process touch events on the canvas.
        // You will likely be using:
        //      e.TouchContact.ID to get the ID, 
        //      e.TouchContact.Position to get the point coordinates
        private void Canvas_TouchDown(object sender, TouchContactEventArgs e)
        {
            IDProperties idProperties = this.GetID(e.TouchContact.ID); // idProperties associated with the mouse ID
        }

        private void Canvas_TouchMove(object sender, TouchContactEventArgs e)
        {
            IDProperties idProperties = this.GetID(e.TouchContact.ID); // idProperties associated with the mouse ID
        }

        private void Canvas_TouchUp(object sender, TouchContactEventArgs e)
        {
            IDProperties idProperties = this.GetID(e.TouchContact.ID); // idProperties associated with the mouse ID
        }
        #endregion

        #region Utilities

        // Given a mouse ID:
        // - create a dictionary entry storing IDProperties by mouse ID if we don't already have one
        // - return the IDProperties object that stores status information associated with that ID
        private IDProperties GetID(int mouseID)
        {
            // If we have the mouseID, return the corrected one
            if (dictIDProperties.ContainsKey(mouseID))
            {
                return dictIDProperties[mouseID];
            }
            else
            {
                // We don't have it. Add a new ID Property then return it
                IDProperties idProperties = new IDProperties(mouseID);
                dictIDProperties.Add(mouseID, idProperties);
                return idProperties;
            }
        }
        #endregion
    }

}
