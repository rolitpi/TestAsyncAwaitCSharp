using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        private static bool IsCompleted = false;

        static async Task Factorial()
        {
            Console.WriteLine($"Factorial начался в потоке {Thread.CurrentThread.ManagedThreadId}");
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"Run Factorial начался в потоке {Thread.CurrentThread.ManagedThreadId}");
                    int result = 1;
                    for (int i = 1; i <= 6; i++)
                    {
                        Thread.Sleep(50);
                        Console.WriteLine($"Мы сейчас что-то считаем в потоке {Thread.CurrentThread.ManagedThreadId}");
                        result *= i;
                    }
                    Console.WriteLine($"Факториал равен {result} в потоке {Thread.CurrentThread.ManagedThreadId}");
                });
                Console.WriteLine($"Factorial закончился в потоке {Thread.CurrentThread.ManagedThreadId}");
            }
            finally
            {
                Console.WriteLine($"Finally в потоке {Thread.CurrentThread.ManagedThreadId}");
            }
        }
        // определение асинхронного метода
        static async void FactorialAsync()
        {
            Console.WriteLine($"Начало метода FactorialAsync в потоке {Thread.CurrentThread.ManagedThreadId}"); // выполняется синхронно
            await Factorial();                            // выполняется асинхронно
            await Factorial();
            IsCompleted = true;
            Console.WriteLine($"Конец метода FactorialAsync в потоке {Thread.CurrentThread.ManagedThreadId}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Начало Main в потоке {Thread.CurrentThread.ManagedThreadId}");
            FactorialAsync();   // вызов асинхронного метода

            while ( !IsCompleted )
            {
                Console.WriteLine($"Мы сейчас что-то рисуем в потоке {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(5);
            }

            Console.WriteLine($"Конец Main в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();
        }
    }

    public static class AsyncExtensions
    {
        public static void RemoveAwaitWarning(this Task task)
        {
            
        }
    }
}
