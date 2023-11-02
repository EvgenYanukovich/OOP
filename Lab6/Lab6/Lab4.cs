using System;

public interface IInformation
{
    bool Inform();
}

public abstract class Information
{
    public abstract bool Inform();
}

// Определение класса Товар
public abstract partial class Product : Information, IInformation
{
    public string? Manufacturer { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }

    // из абстрактного класса
    public override bool Inform()
    {
        Console.WriteLine("Вызван метод Inform() из абстрактного класса Information.");
        return true;
    }

    // из интерфейса
    bool IInformation.Inform()
    {
        Console.WriteLine("Вызван метод Inform() из интерфейса IInformation.");
        return false;
    }
}
// Определение класса Мебель
public class Furniture : Product
{
    public override string ToString()
    {
        return "Это мебель.";
    }
}

// Определение класса Диван
public sealed class Sofa : Furniture
{
    public override string ToString()
    {
        return "Это диван.";
    }
}

// Определение класса Кровать
public class Bed : Furniture
{
    public override string ToString()
    {
        return "Это кровать.";
    }
}

// Определение класса Шкаф
public class Wardrobe : Furniture
{
    public override string ToString()
    {
        return "Это шкаф.";
    }
}

// Определение класса Шкаф-купе
public class SlidingWardrobe : Wardrobe
{
    public override string ToString()
    {
        return "Это шкаф-купе.";
    }
}

// Определение класса Вешалка
public class Hanger : Furniture
{
    public override string ToString()
    {
        return "Это вешалка.";
    }
}

// Определение класса Тумба
public class BedsideTable : Furniture
{
    public override string ToString()
    {
        return "Это тумба.";
    }
}

// Определение класса Стул
public class Chair : Furniture
{
    public override string ToString()
    {
        return "Это стул.";
    }
}

// Определение класса Printer 
public class Printer
{
    public void IAmPrinting(Product someobj)
    {
        Console.WriteLine(someobj.ToString());
    }
}

class Lab4
{
    void Test()
    {
        Sofa sofa = new Sofa();
        Bed bed = new Bed();
        Wardrobe wardrobe = new Wardrobe();

        Product[] products = new Product[] { sofa, bed, wardrobe };

        Printer printer = new Printer();

        printer.IAmPrinting(sofa);
        printer.IAmPrinting(bed);
        printer.IAmPrinting(wardrobe);

        Product product = new Sofa();

        if (sofa is IInformation)
        {
            Console.WriteLine("Объект sofa поддерживает интерфейс IInformation.");
            IInformation cloneableSofa = sofa;
            cloneableSofa.Inform();
        }

        if (sofa is Information)
        {
            Console.WriteLine("Объект sofa является экземпляром абстрактного класса Information.");
            Information baseSofa = sofa;
            baseSofa.Inform();
        }
    } 
    static void Main(string[] args)
    {
        new Lab4().Test();
        new Lab5().Test();
        Lab6.Test(3, 1, new int[3] { 1, 2, 3 }, "");

    }
}