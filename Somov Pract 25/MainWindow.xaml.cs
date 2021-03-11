using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;


namespace Somov_Pract_25
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //рамка для canvas'a
        System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
        public MainWindow()
        {
            InitializeComponent();
            //Подписка на событие
            KeyPress.OnKeyPressed += KeyPress_OnKeyPress;  
            //описываем рамку
            rect.Height = canvas1.Height;
            rect.Width = canvas1.Width;
            rect.Stroke = System.Windows.Media.Brushes.Gray;
            //помещаем рамку в canvas
            canvas1.Children.Add(rect);
            //В выпадающем списке выбираем первый пункт
            ComBox.SelectedIndex = 0;
        }
        private int x = 0, y = 0, size = 1, moveSpeed = 1;
        Lattise gr; //Описываем объект как глобальный
        private void KeyPress_OnKeyPress(KeyPress.Key Key)
        {
            if (Key == KeyPress.Key.Up)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //Двигаем фигуру по y вверх
                    if (y - moveSpeed > 0 && y > 5)
                    {
                        y -= moveSpeed;
                    }
                    //на случай упирания в верхнюю границу
                    else
                    {
                        y = 5;
                    }
                    //Отражаем координату в textbox
                    cordYtxt.Text = y.ToString();
                });                             
            }
            if (Key == KeyPress.Key.Down)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //Двигаем фигуру по y вниз
                    if (y + 93 * size + moveSpeed < canvas1.Height)
                    {
                        y += moveSpeed;
                    }
                    //На случай упирания в нижнюю границу
                    else
                    {
                        y += (Int32)canvas1.Height - (y + 93 * size) - 2;
                    }
                    //Отражаем координату в textbox
                    cordYtxt.Text = y.ToString();
                });                
            }
            if (Key == KeyPress.Key.Left)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //Двигаем фигуру по x влево
                    if (x - moveSpeed > 0 && x > 5)
                    {
                        x -= moveSpeed;
                    }
                    //На случай упирания в левую границу
                    else
                    {
                        x = 5;
                    }
                    //Отражаем координату в textbox
                    cordXtxt.Text = x.ToString();
                });               
            }
            if (Key == KeyPress.Key.Right)
            {
                //Двигаем фигуру по x вправо
                this.Dispatcher.Invoke(() =>
                {
                    if (x + 93 * size + moveSpeed < canvas1.Width)
                    {
                        x += moveSpeed;
                    }
                    //На случай упирания в правую границу
                    else//if (x + 93 * size + moveSpeed >= canvas1.Width && x + 93 * size < canvas1.Width)
                    {
                        x += (Int32)canvas1.Width - (x + 93 * size) -2;
                    }
                    //Отражаем координату в textbox
                    cordXtxt.Text = x.ToString();
                });               
            }
            this.Dispatcher.Invoke(() =>
            {
                //Двигаем фигуру
                gr.Move(x, y);
            });            
        }       
        private void Start_Click(object sender, RoutedEventArgs e)
        {            
            canvas1.Children.Clear();//убираем старую фигуру
            canvas1.Children.Add(rect);//еще раз добавляем рамку
            //Создаем объект
            gr = new Lattise(1, 1, System.Windows.Media.Brushes.Red);
            //Добавляем линии объекта в canvas
            canvas1.Children.Add(gr.Figure1);
            canvas1.Children.Add(gr.Figure2);
            canvas1.Children.Add(gr.Figure3);
            canvas1.Children.Add(gr.Figure4);
            //Включаем кнопки взаимодествия с фигурой
            CB.IsEnabled = true;
            cordXtxt.IsEnabled = true;
            cordYtxt.IsEnabled = true;
            colorDlg.IsEnabled = true;
            sizetxt.IsEnabled = true;
            thicktxt.IsEnabled = true;
            speedtxt.IsEnabled = true;
            ComBox.IsEnabled = true;
            //Обнуляем счетчики и текстбоксы
            x = 0; y = 0;
            cordXtxt.Text = "0";
            cordYtxt.Text = "0";
            //Запускаем поток для отлавливания нажатий клавишь
            KeyPress.Start();
        }
        private void CB_Click(object sender, RoutedEventArgs e)
        {
            if (gr == null)
            {
                CB.IsChecked = false;
                return;
            }
            //если установлена галочка делаем фигуру видимой и наооборот
            if (CB.IsChecked.Value == true) gr.Show();
            else gr.Hide();
        }
        private void colorDlg_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dlg = new ColorDialog(); //объект класса ColorDialog
            //Если пользователь выбирает цвет красим фигуру
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                gr.Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(dlg.Color.A, dlg.Color.R, dlg.Color.G, dlg.Color.B));               
            }
        }
        private void thicktext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gr == null) return;
            if (Int32.TryParse(thicktxt.Text, out int value))
            {
                gr.Thickness = value;//Устанавливаем толщину фигуры
            }            
        }
        private void sizeContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gr == null) return;
            if (Int32.TryParse(sizetxt.Text, out size) && size < 5)
            {
                gr.Size = size;//Устанавливаем размер фигуры
            }
        }
        private void speedtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(speedtxt.Text, out moveSpeed))
            {

            }
        }
        private void cordtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Проверяем введенные данные
            if (cordXtxt == null || cordYtxt == null || gr == null) return;
            if (!Int32.TryParse(cordXtxt.Text, out x) || !Int32.TryParse(cordYtxt.Text, out y))
            {
                return;
            }
            //Двигаем фигуру по вручную установленным координатам
            gr.Move(x, y);
        }
        private void ProwerkaSize(object sender, TextCompositionEventArgs e)
        {
            //Проверяем введенные данные, важно: размер должен быть не больше 4
            if (!Int32.TryParse(e.Text, out int val) || val > 4)
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void ProwerkaText(object sender, TextCompositionEventArgs e)
        {
            //Проверяем введенные данные
            if (!Int32.TryParse(e.Text, out int val))
            {
                e.Handled = true; // отклоняем ввод
            }
        }
        private void ComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComBox.SelectedItem != null && gr != null)
            {
                switch (ComBox.SelectedIndex) //По индексу понимаем какое будет закругление
                {
                    case 0:
                        gr.LineCap = "Flat";
                        break;
                    case 1:
                        gr.LineCap = "Round";
                        break;
                    case 2:
                        gr.LineCap = "Triangle";
                        break;
                }
            }
        }
        #region Кнопки выхода и информации
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Разработчик - Сомов Михаил\n\nПрактическая работа №25. Вариант 14\n\nОписать объект решетка", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }       
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Останавливаем поток по закрытию формы
            KeyPress.Stop();
        }
        #endregion
    }
}
