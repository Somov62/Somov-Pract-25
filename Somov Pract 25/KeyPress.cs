using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Somov_Pract_25
{
    class KeyPress
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vkey);
        //Перечесление клавиш
        public enum Key { Up, Down, Left, Right };
        //Делегат события
        public delegate void keyPress(Key Key);
        //Событие
        public static event keyPress OnKeyPressed;
        //Отдельный поток для отлавливания клавиш
        
        static Thread th = new Thread(x =>
        {            
            while (true)
            {
                //Коды клавиш
                //Вверх - 0x26
                //Вниз - 40
                //Вправо - 0x27
                //Влево - 0x25
                if (OnKeyPressed != null)
                {
                    if (GetAsyncKeyState(0x26) != 0)
                        OnKeyPressed(Key.Up);
                    if (GetAsyncKeyState(40) != 0)
                        OnKeyPressed(Key.Down);
                    if (GetAsyncKeyState(0x27) != 0)
                        OnKeyPressed(Key.Right);
                    if (GetAsyncKeyState(0x25) != 0)
                        OnKeyPressed(Key.Left);
                }
                Thread.Sleep(100);
            }
        });
        //Список потоков
        static List<Thread> ThreadList = new List<Thread>();
        //Старт доп потока
        public static void Start()
        {            
            if (ThreadList.Count == 0)
            {
                ThreadList.Add(th);
                th.Start();
            }            
        }
        //Остановка доп потока
        public static void Stop()
        {
            th.Abort();
        }
    }
}

