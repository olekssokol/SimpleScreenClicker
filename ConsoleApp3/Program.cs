using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using WindowsInput;
using System.Runtime.InteropServices;

namespace ConsoleApp3
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_RESTORE = 9;

        static void Main(string[] args)
        {
            AutomationElement rootElement = AutomationElement.RootElement;

            PropertyCondition condition = new PropertyCondition(AutomationElement.NameProperty, "Autodesk Revit 2024.1 - UNREGISTERED VERSION - [Project1 - Floor Plan: L1 - Architectural]");
            AutomationElement element = rootElement.FindFirst(TreeScope.Children, condition);

            condition = new PropertyCondition(AutomationElement.NameProperty, "Steel");

            var Architecture = element.FindAll(TreeScope.Children, condition);

            foreach (AutomationElement childElement in Architecture)
            {
                // Отримати інформацію про елемент, наприклад, ім'я
                var elementName = childElement.Current.Name;
                Console.WriteLine("Знайдено: " + elementName);
            }

            //InvokePattern invokePattern = Architecture.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

            //if (invokePattern != null)
            //{
            //    // Виклик методу Invoke для натискання на елемент
            //    invokePattern.Invoke();

            //    Console.WriteLine("Елемент натиснуто.");
            //}
            ////condition = new PropertyCondition(AutomationElement.NameProperty, "Model");

            //var Model = Architecture.FindFirst(TreeScope.Descendants, condition);

            //var elements = element.FindAll(TreeScope.Children, Condition.TrueCondition);

            //if (elements.Count > 0)
            //{
            //    Console.WriteLine("Список дочірніх елементів:");

            //    foreach (AutomationElement childElement in elements)
            //    {
            //        // Отримати інформацію про елемент, наприклад, ім'я
            //        var elementName = childElement.Current.Name;
            //        Console.WriteLine("Знайдено: " + elementName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("У активного елемента немає дочірніх елементів.");
            //}

            if (Architecture[1] != null)
            {
                // Отримуємо позицію цільового елементу
                Rect elementBounds = Architecture[1].Current.BoundingRectangle;

                // Переміщаємо мишу до цільового елементу
                SetCursorPos((int)(elementBounds.Left + elementBounds.Width / 2), (int)(elementBounds.Top + elementBounds.Height / 2));

                Task.Delay(1000).Wait();

                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            }
        }
    }
}
