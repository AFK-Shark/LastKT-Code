using System;
using GrigoryanMaria_OptionalLib;

namespace GrigoryanMaria_OptionalService
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.Write("Ведите кол-во объектов для создания: ");
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Некорректный ввод. Введите кол-во объектов снова: ");
            }

            Optional<int>[] optionals = new Optional<int>[n];

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Введите значение для параметра {i + 1}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    optionals[i] = new Optional<int>(value);
                }
                else
                {
                    optionals[i] = new Optional<int>();
                }
            }

            ExtendedOptional<int> extendedOptional = new ExtendedOptional<int>();
            extendedOptional.OnOptionalFilled += OnOptionalFilledHandler;
            extendedOptional.OnOptionalEmptied += OnOptionalEmptiedHandler;

            Console.WriteLine("\nВаши объекты:");
            foreach (var opt in optionals)
            {
                Console.WriteLine(opt.ToString());
            }

            extendedOptional.SetValue(42);
            extendedOptional.SetValue(null);
        }

        static void OnOptionalFilledHandler(int value)
        {
            Console.WriteLine($"Заполненное значение: {value}");
        }

        static void OnOptionalEmptiedHandler()
        {
            Console.WriteLine("Пустое значение.");
        }
    }
}
