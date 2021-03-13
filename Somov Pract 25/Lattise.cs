using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Somov_Pract_25
{
    class Lattise
    {
        private int _x = 0;
        private int _y = 0;
        private int _size;
        private int _thickness = 1;
        string _linecap = "Flat";
        System.Windows.Media.Brush _color1;
        Line gorizont1, gorizont2, vertikal1, vertikal2;

        public System.Windows.Media.Brush Color //Свойство цвета
        {
            get { return _color1; }
            set 
            { 
            //Берем цвет и красим все линии
            _color1 = value;
            gorizont1.Stroke = _color1;
            gorizont2.Stroke = _color1;
            vertikal1.Stroke = _color1;
            vertikal2.Stroke = _color1;
            }
        }
        public int Thickness //Свойство толщины линий
        {
            get { return _thickness; }
            set 
            {
                _thickness = value;
                gorizont1.StrokeThickness = _thickness;
                gorizont2.StrokeThickness = _thickness;
                vertikal1.StrokeThickness = _thickness;
                vertikal2.StrokeThickness = _thickness;
            }
        }
        public int Size //Свойство размера фигуры
        {
            get { return _size; }
            set 
            { 
                //size - множитель
                _size = value;
                gorizont1.X1 = 0; gorizont1.Y1 = 31 * _size;
                gorizont1.X2 = 92 * _size; gorizont1.Y2 = 31 * _size;
                gorizont2.X1 = 0; gorizont2.Y1 = 62 * _size;
                gorizont2.X2 = 92 * _size; gorizont2.Y2 = 62 * _size;
                vertikal1.X1 = 31 * _size; vertikal1.Y1 = 0;
                vertikal1.X2 = 31 * _size; vertikal1.Y2 = 92 * _size;
                vertikal2.X1 = 62 * _size; vertikal2.Y1 = 0;
                vertikal2.X2 = 62 * _size; vertikal2.Y2 = 92 * _size;               
            }
        }
        public string LineCap //Свойства закругления линий
        {
            get { return _linecap; }
            set
            {
                if (value == "Flat")
                {
                    gorizont1.StrokeStartLineCap = PenLineCap.Flat;
                    gorizont2.StrokeStartLineCap = PenLineCap.Flat;
                    vertikal1.StrokeStartLineCap = PenLineCap.Flat;
                    vertikal2.StrokeStartLineCap = PenLineCap.Flat;
                    gorizont1.StrokeEndLineCap = PenLineCap.Flat;
                    gorizont2.StrokeEndLineCap = PenLineCap.Flat;
                    vertikal1.StrokeEndLineCap = PenLineCap.Flat;
                    vertikal2.StrokeEndLineCap = PenLineCap.Flat;
                }
                if (value == "Round")
                {
                    gorizont1.StrokeStartLineCap = PenLineCap.Round;
                    gorizont2.StrokeStartLineCap = PenLineCap.Round;
                    vertikal1.StrokeStartLineCap = PenLineCap.Round;
                    vertikal2.StrokeStartLineCap = PenLineCap.Round;
                    gorizont1.StrokeEndLineCap = PenLineCap.Round;
                    gorizont2.StrokeEndLineCap = PenLineCap.Round;
                    vertikal1.StrokeEndLineCap = PenLineCap.Round;
                    vertikal2.StrokeEndLineCap = PenLineCap.Round;
                }
                if (value == "Triangle")
                {
                    gorizont1.StrokeStartLineCap = PenLineCap.Triangle;
                    gorizont2.StrokeStartLineCap = PenLineCap.Triangle;
                    vertikal1.StrokeStartLineCap = PenLineCap.Triangle;
                    vertikal2.StrokeStartLineCap = PenLineCap.Triangle;
                    gorizont1.StrokeEndLineCap = PenLineCap.Triangle;
                    gorizont2.StrokeEndLineCap = PenLineCap.Triangle;
                    vertikal1.StrokeEndLineCap = PenLineCap.Triangle;
                    vertikal2.StrokeEndLineCap = PenLineCap.Triangle;
                }
            }
        }
        //Свойства возвращающие линии чтобы поместить их в canvas
        public Line Figure1
        {
            get { return gorizont1; }
        }
        public Line Figure2
        {
            get { return gorizont2; }
        }
        public Line Figure3
        {
            get { return vertikal1; }
        }
        public Line Figure4
        {
            get { return vertikal2; }
        }
        public void Show()
        {
            gorizont1.Visibility = Visibility.Visible;
            gorizont2.Visibility = Visibility.Visible;
            vertikal1.Visibility = Visibility.Visible;
            vertikal2.Visibility = Visibility.Visible;
        }
        public void Hide()
        {
            gorizont1.Visibility = Visibility.Hidden;
            gorizont2.Visibility = Visibility.Hidden;
            vertikal1.Visibility = Visibility.Hidden;
            vertikal2.Visibility = Visibility.Hidden;
        }
        public void Move(int newx, int newy)
        {
            _x = newx;
            _y = newy;
            //Устанавливаем отступы по координатам
            gorizont1.Margin = new Thickness(_x, _y, 0, 0);
            gorizont2.Margin = new Thickness(_x, _y, 0, 0);
            vertikal1.Margin = new Thickness(_x, _y, 0, 0);
            vertikal2.Margin = new Thickness(_x, _y, 0, 0);
        }
        public Lattise(int size, int thickness, System.Windows.Media.Brush color)
        {
            //Создаем и описываем линии
            gorizont1 = new Line();
            gorizont1.Stroke = color;
            gorizont1.StrokeThickness = thickness;

            gorizont2 = new Line();
            gorizont2.Stroke = color;
            gorizont2.StrokeThickness = thickness;

            vertikal1 = new Line();
            vertikal1.Stroke = color;
            vertikal1.StrokeThickness = thickness;

            vertikal2 = new Line();
            vertikal2.Stroke = color;
            vertikal2.StrokeThickness = thickness;

            gorizont1.X1 = 0; gorizont1.Y1 = 31 * size;
            gorizont1.X2 = 92 * size; gorizont1.Y2 = 31 * size;
            gorizont2.X1 = 0; gorizont2.Y1 = 62 * size;
            gorizont2.X2 = 92 * size; gorizont2.Y2 = 62 * size;
            vertikal1.X1 = 31 * size; vertikal1.Y1 = 0;
            vertikal1.X2 = 31 * size; vertikal1.Y2 = 92 * size;
            vertikal2.X1 = 62 * size; vertikal2.Y1 = 0;
            vertikal2.X2 = 62 * size; vertikal2.Y2 = 92 * size;

            gorizont1.Margin = new Thickness(_x, _y, 0, 0);
            gorizont2.Margin = new Thickness(_x, _y, 0, 0);
            vertikal1.Margin = new Thickness(_x, _y, 0, 0);
            vertikal2.Margin = new Thickness(_x, _y, 0, 0);
        }

    }
}
