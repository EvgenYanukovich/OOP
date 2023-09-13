using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
namespace Lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
                // Вызов функций для каждой из подзадач

                // 1) Типы
                TestPrimitiveTypes();
                TestConversions();

                // 2) Строки
                TestStringOperations();

                // 3) Массивы
                TestArrays();

                // 4) Кортежи
                TestTuples();

                // 5) Локальная функция
                TestLocalFunction();

                // 6) Работа с checked/unchecked
                TestCheckedUnchecked();

                // Ожидание ввода перед закрытием окна
                Console.ReadLine();
            }

        // 1) Типы
        static void TestPrimitiveTypes()
        {
            bool boolVar = true;
            byte byteVar = 255;
            sbyte sbyteVar = -128;
            char charVar = 'A';
            decimal decimalVar = 123.45m;
            double doubleVar = 3.14159265359;
            float floatVar = 1.23f;
            int intVar = 42;
            uint uintVar = 123456;
            nint nintVar = 43;
            nuint nuintVar = 1234567;
            long longVar = 1234567890L;
            ulong ulongVar = 12345678901234567890UL;
            short shortVar = 32767;
            ushort ushortVar = 65535;

            Console.WriteLine($"Bool: {boolVar}");
            Console.WriteLine($"Byte: {byteVar}");
            Console.WriteLine($"SByte: {sbyteVar}");
            Console.WriteLine($"Char: {charVar}");
            Console.WriteLine($"Decimal: {decimalVar}");
            Console.WriteLine($"Double: {doubleVar}");
            Console.WriteLine($"Float: {floatVar}");
            Console.WriteLine($"Int: {intVar}");
            Console.WriteLine($"UInt: {uintVar}");
            Console.WriteLine($"NInt: {nintVar}");
            Console.WriteLine($"NUInt: {nuintVar}");
            Console.WriteLine($"Long: {longVar}");
            Console.WriteLine($"ULong: {ulongVar}");
            Console.WriteLine($"Short: {shortVar}");
            Console.WriteLine($"UShort: {ushortVar}");

            intVar = Convert.ToInt32(Console.ReadLine());
        }
        static void TestConversions()
        {
            decimal decimalValue = 3.14m;
            double doubleValue = (double)decimalValue;
            float floatValue = (float)doubleValue;
            int intValue = (int)floatValue; 
            char charValue = (char)intValue;
            long longValue = Convert.ToInt64(intValue);

            int intVar = 10;
            float floatVar = intVar;
            double doubleVar = floatVar;
            decimal decimalVar = intVar;
            long longVar = intVar;
            nint nintVar = intVar;

            Console.WriteLine($"Explicit: decimal->double - {doubleValue}, \ndouble->float - {floatValue}, \nfloat->int - {intValue}, \nint->char - {charValue}, \nint->long - {longValue}");
            Console.WriteLine($"Implicit: int->float - {floatVar}, \nfloat->double - {doubleVar}, \nint->decimal - {decimalVar}, \nint->long - {longVar}, \nint->nint - {nintVar}");

            int boxedInt = 42;
            object boxedObject = boxedInt;
            int unboxedInt = (int)boxedObject;

            var someValue = "Hello, C#";
            Console.WriteLine($"var: {someValue}");
            // someValue = 1 - Ошибка компиляции

            int? nullableInt = null;
            if (nullableInt.HasValue)
            {
                Console.WriteLine($"Nullable int: {nullableInt.Value}");
            }
            else
            {
                Console.WriteLine("Nullable int is null");
            }
        }

        // 2) Строки
        static void TestStringOperations()
        {
            Console.WriteLine("Строковые литералы: \nabcdefg\nабвгдеё\nВывод кавычек: \"\"\n\tТабуляция");

            string a = "qaz";
            string b = "wsx";

            a = string.Concat(a, "wsx");
            string d = $"{a} & {b}";
            string e = string.Format($"{0} & {1}", a, b);
            string aCopy = string.Copy(a);
            string bSubstring = b.Substring(0, 2);

            string words = "Text1 Text2 Text3";
            string[] splitWord = words.Split(' ');

            string originalS = "text text";
            string insertedS = " noText";
            string resultS = originalS.Insert(4, insertedS);

            string substringS = "text noText text";
            string deletedS = "noText";
            string withoutS = substringS.Replace(deletedS, "");

            string nValue = "";
            string nValue2 = null;
            if (string.IsNullOrEmpty(nValue))
            {
                Console.WriteLine("\nПустая строка nString");
            }
            if (string.IsNullOrEmpty(nValue2))
            {
                Console.WriteLine("Null строка nString2");
            }
            
            if (nValue == nValue2)
            {
                Console.WriteLine("Empty = Null");
            }
            else
            {
                Console.WriteLine("Empty != Null");
            }

            string nonNString = nValue2 ?? "";

            StringBuilder strBuilder = new StringBuilder("abc def 1234");
            strBuilder.Remove(0, 3);
            strBuilder.Insert(0, "xyz ");
            strBuilder.Append("5678");
            Console.WriteLine("\nНовая строка: " + strBuilder);
        }

        // 3) Массивы
        static void TestArrays()
        {
            int[,] matrix = new int[3, 3]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            string[] stringArray = { "apple", "banana", "cherry", "date" };
            Console.WriteLine("Array contents:");
            foreach (string item in stringArray)
            {
                Console.WriteLine(item);
            }

            int arrayLength = stringArray.Length;
            Console.WriteLine($"Array length: {arrayLength}");

            Console.Write("Enter the position to change (0-3): ");
            int position = int.Parse(Console.ReadLine());

            if (position >= 0 && position < stringArray.Length)
            {
                Console.Write("Enter the new value: ");
                string newValue = Console.ReadLine();
                stringArray[position] = newValue;
                Console.WriteLine("Array contents after change:");
                foreach (string item in stringArray)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Invalid position.");
            }

            double[][] jaggedArray = new double[3][];
            jaggedArray[0] = new double[2];
            jaggedArray[1] = new double[3];
            jaggedArray[2] = new double[4];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write($"Enter value for row {i}, column {j}: ");
                    jaggedArray[i][j] = double.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("Jagged Array contents:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + "\t");
                }
                Console.WriteLine();
            }

            var numbers = new[] { 1, 2, 3, 4, 5 };
            var text = "Hello, C#";
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            var upperText = text.ToUpper();

            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

        Console.WriteLine(upperText);
        }

        // 4) Кортежи
        static void TestTuples()
        {
            var myTuple = (42, "Hello", 'A', "World", 123456UL);

            Console.WriteLine($"Tuple: {myTuple}");

            Console.WriteLine($"Item 1: {myTuple.Item1}");
            Console.WriteLine($"Item 3: {myTuple.Item3}");

            var (num, greeting, letter, world, bigNum) = myTuple;

            Console.WriteLine($"Unpacked: num={num}, greeting={greeting}, letter={letter}, world={world}, bigNum={bigNum}");

            var (_, _, _, _, ulongValue) = myTuple;
            Console.WriteLine($"Unpacked ulongValue: {ulongValue}");

            var tuple1 = (1, "Hello");
            var tuple2 = (2, "World");

            bool areEqual = tuple1 == tuple2;
            Console.WriteLine($"Are tuples equal: {areEqual}");
        }

        // 5) Локальная функция
        static void TestLocalFunction()
        {
            (int, int, int, char) MyLocalFunction(int[] numbers, string text)
            {
                int max = numbers.Max();
                int min = numbers.Min();
                int sum = numbers.Sum();
                char firstChar = text[0];

                return (max, min, sum, firstChar);
            }

            int[] numbers = { 10, 20, 5, 30 };
            string inputText = "C# is fun";

            var result = MyLocalFunction(numbers, inputText);

            Console.WriteLine($"Max: {result.Item1}");
            Console.WriteLine($"Min: {result.Item2}");
            Console.WriteLine($"Sum: {result.Item3}");
            Console.WriteLine($"First char: {result.Item4}");
        }

        // 6) Работа с checked/unchecked
        static void TestCheckedUnchecked()
        {
            int CheckedFunction()
            {
                int maxValue = int.MaxValue;
                checked
                {
                    int result = maxValue + 1; // Генерирует OverflowException
                    return result;
                }
            }

            int UncheckedFunction()
            {
                int maxValue = int.MaxValue;
                unchecked
                {
                    int result = maxValue + 1; // Не генерирует OverflowException
                    return result;
                }
            }

            try
            {
                int checkedResult = CheckedFunction();
                Console.WriteLine($"Checked result: {checkedResult}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Checked function error: {ex.Message}");
            }

            int uncheckedResult = UncheckedFunction();
            Console.WriteLine($"Unchecked result: {uncheckedResult}");
        }
    }

}