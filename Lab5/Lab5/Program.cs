using System;

public enum FurnitureType
{
    Sofa,
    Bed,
    Wardrobe,
    // Добавьте другие типы мебели по мере необходимости
}

public struct ManufacturerInfo
{
    public string Name;
    public string Country;
    // Добавьте другие поля по мере необходимости
}

public class Warehouse
{
    public List<Product> products = new List<Product>();

    public Product this[int index]
    {
        get { return products[index]; }
        set { products[index] = value; }
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }

    public void PrintProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}

public class WarehouseController
{
    private Warehouse warehouse;

    public WarehouseController(Warehouse warehouse)
    {
        this.warehouse = warehouse;
    }

    public decimal GetTotalCostOfCabinets()
    {
        decimal totalCost = 0;
        foreach (var product in warehouse.products)
        {
            if (product is Furniture)
            {
                totalCost += product.Price;
            }
        }
        return totalCost;
    }

    public List<Product> GetFurnitureByManufacturer(string manufacturer)
    {
        List<Product> result = new List<Product>();
        foreach (var product in warehouse.products)
        {
            if (product.Manufacturer == manufacturer)
            {
                result.Add(product);
            }
        }
        return result;
    }

    public void SortProductsByPriceWeightRatio()
    {
        warehouse.products.Sort((x, y) =>
            (x.Price / x.Weight).CompareTo(y.Price / y.Weight));
    }
}

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
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }

    public override string ToString()
    {
        return $"Товар: Производитель = {Manufacturer}, Цена = {Price}, Вес = {Weight}";
    }

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

class Program
{
    static void Main(string[] args)
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
            IInformation cloneableSofa = sofa as IInformation;
            cloneableSofa.Inform();
        }

        if (sofa is Information)
        {
            Console.WriteLine("Объект sofa является экземпляром абстрактного класса Information.");
            Information baseSofa = sofa as Information;
            baseSofa.Inform();
        }

        Console.ReadKey();
    }
}