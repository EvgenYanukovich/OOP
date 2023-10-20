using System;

public enum FurnitureType
{
    Sofa,
    Bed,
    Wardrobe,
}

public struct ManufacturerInfo
{
    public string Name;
    public string Country;
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

    public decimal GetTotalCostOfFurniture()
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

public partial class Product
{
    public override string ToString()
    {
        return $"Товар: Производитель = {Manufacturer}, Цена = {Price}, Вес = {Weight}";
    }
}

public class Lab5
{
    public void Test()
    {
        Sofa sofa = new Sofa();
        Bed bed = new Bed();

        FurnitureType furniture = FurnitureType.Sofa;

        if (furniture == FurnitureType.Sofa)
        {
            Console.WriteLine("Это диван из перечисления");
        }

        sofa.Price = 10;
        bed.Price = 15;

        Warehouse warehouse = new Warehouse();
        warehouse.AddProduct(sofa);
        warehouse.AddProduct(bed);

        WarehouseController warehouseController = new WarehouseController(warehouse);

        Console.WriteLine(warehouseController.GetTotalCostOfFurniture());
    }
    
}
