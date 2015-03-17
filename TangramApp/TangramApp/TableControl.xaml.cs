using System;
using System.Collections.Generic;
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

namespace TangramApp
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : TableApplicationControl
    {
        private Canvas canvas = new Canvas();
        private DraggableBorder[] figures = new DraggableBorder[7];
        private int Scale = 30;

        private string m_InstructionText = "Tangram App";
        public TableControl()
        {
            //set background image
            canvas.Background = new SolidColorBrush(Colors.Navy);
            
            InitializeComponent();
        }

        private void TableApplicationControl_Loaded(object sender, RoutedEventArgs e)
        {
            TableLayoutRoot.Children.Add(canvas);

            //initialize figures, adding 5 triangles, 1 square, and 1 polygon
            initFigures();

            //sets the locations and orientations of the figures, so they are in a square 
            // whose top left corner is at the point (x,y)
            resetFigureLocation(Scale, Scale);

            //add all figures to the canvas
            foreach (DraggableBorder x in figures)
            {
                canvas.Children.Add(x);
            }
            
            //adds intro method "Tangram App"
            TableEffectControl.PlayInstruction(m_InstructionText);
        }

        private void resetFigureLocation(int xMin, int yMin)
        {
            //reorient and place figure1
            figures[0].RotateTransform.Angle = 0;
            figures[0].TranslateTransform.X = xMin;
            figures[0].TranslateTransform.Y = yMin;
            
            //reorient and place figure2
            figures[1].RotateTransform.Angle = 90;
            figures[1].TranslateTransform.X = xMin + Scale * 2;
            figures[1].TranslateTransform.Y = yMin - Scale * 2;

            //reorient and place figure3
            figures[2].RotateTransform.Angle = 0;
            figures[2].TranslateTransform.X = xMin;
            figures[2].TranslateTransform.Y = yMin + Scale * 6;

            //reorient and place figure 4
            figures[3].RotateTransform.Angle = 270;
            figures[3].TranslateTransform.X = xMin + Scale * 3;
            figures[3].TranslateTransform.Y = yMin + Scale * 3;

            //reorient and place figure 5
            figures[4].RotateTransform.Angle = 45;
            figures[4].TranslateTransform.X = xMin + Scale * 4.66;
            figures[4].TranslateTransform.Y = yMin + Scale * 2.66;
            
            //reorient and place figure 6
            figures[5].RotateTransform.Angle = 180;
            figures[5].TranslateTransform.X = xMin + Scale * 6;
            figures[5].TranslateTransform.Y = yMin;

            //reorient and place figure 7
            figures[6].RotateTransform.Angle = 45;
            figures[6].TranslateTransform.X = xMin + Scale * 5.5;
            figures[6].TranslateTransform.Y = yMin + Scale * 4;
        }

        private void initFigures()
        {
            //initiate tangram figures
            figures[0] = new DraggableTriangle(Scale * 4);
            figures[1] = new DraggableTriangle(Scale * 4);
            figures[2] = new DraggableRhombus(Scale * 2);
            figures[3] = new DraggableTriangle(Scale * 2);
            figures[4] = new DraggableRectangle(Scale * 2);
            figures[5] = new DraggableTriangle(Scale * 2);
            figures[6] = new DraggableTriangle(Scale * 3);

            //set scale enabled to false to prevent rescale
            foreach(DraggableBorder x in figures) {
                x.IsScaleEnabled = false;
            }


        }
    }
}
