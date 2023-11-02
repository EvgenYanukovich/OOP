// Базовый пользовательский класс исключений
using System.Collections;
using System.Diagnostics;

public class MyBaseException : Exception
{
    public string? Value { get; set; }
    public MyBaseException(string message)
        : base(message)
    {
    }
}

// Первый производный класс исключений
public class MyDerivedException1 : MyBaseException
{
    public MyDerivedException1(string message)
        : base(message)
    {
    }
}

// Второй производный класс исключений
public class MyDerivedException2 : MyBaseException
{
    public MyDerivedException2(string message)
        : base(message)
    {
    }
}

// Третий производный класс исключений
public class MyDerivedException3 : MyBaseException
{
    public MyDerivedException3(string message)
        : base(message)
    {
    }
}

public static class Logger
{
    public static void ConsoleLogger(MyBaseException exc)
    {
        Console.WriteLine("\n------Error");
        Console.WriteLine($"Message: {exc.Message}");
        Console.WriteLine($"Wrong name: {exc.Value}");
        Console.WriteLine("Path: {0}", exc.TargetSite);
        foreach (DictionaryEntry d in exc.Data)
            Console.WriteLine("-> {0}, {1}", d.Key, d.Value);
    }

    public static void ConsoleLogger(SystemException exc)
    {
        Console.WriteLine("\n------Error");
        Console.WriteLine($"Message: {exc.Message}");
        Console.WriteLine("Path: {0}", exc.TargetSite);
        foreach (DictionaryEntry d in exc.Data)
            Console.WriteLine("-> {0}, {1}", d.Key, d.Value);
    }
}

public class Lab6
{
    public static void Test(int x, int y, int[] arr, string path)
    {
        try
        {
            // Проверяем, не передан ли нулевой указатель в качестве массива
            if (arr == null)
            {
                throw new ArgumentNullException("arr");
            }

            // Проверяем, не передан ли нулевой указатель в качестве пути к файлу
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            // Проверяем, не является ли путь к файлу пустой строкой
            if (path == "")
            {
                throw new ArgumentException("path cannot be empty");
            }

            // Проверяем, не является ли делитель нулем
            if (y == 0)
            {
                throw new DivideByZeroException();
            }

            // Выполняем деление
            int result = x / y;

            // Проверяем, не выходит ли индекс за границы массива
            if (x < 0 || x >= arr.Length)
            {
                throw new IndexOutOfRangeException();
            }

            int element = arr[x];

            using (var reader = new StreamReader(path))
            {
                string? line = reader.ReadLine();
                Console.WriteLine(line);


                Debug.Assert(line == "", "Строка line пуста");
            }
        }
        catch (MyBaseException e) 
        {
            Console.WriteLine($"MyBaseException: {e.Message}");
            Logger.ConsoleLogger(e);
        }
        catch (SystemException e) // Обрабатываем стандартные исключения
        {
            Console.WriteLine($"SystemException: {e.Message}");
            Logger.ConsoleLogger(e);
        }
        finally // Выполняем завершающие действия в любом случае
        {
            Console.WriteLine("DoSomething finished");
        }
    }
}
