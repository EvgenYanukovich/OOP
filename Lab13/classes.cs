using System;


public interface IInformation
{
    bool Inform();
}


[Serializable]
public abstract class Information
{
    public abstract bool Inform();
}



[Serializable]
public abstract class Product : Information, IInformation
{
    public override string ToString()
    {
        return "Это товар.";
    }

    public override bool Inform()
    {
        Console.WriteLine("Вызван метод DoClone() из абстрактного класса BaseClone.");
        return true;
    }

    bool IInformation.Inform()
    {
        Console.WriteLine("Вызван метод DoClone() из интерфейса ICloneable.");
        return false;
    }
}


[Serializable]
public class Furniture : Product
{
    [NonSerialized]
    public int? value;

    public override string ToString()
    {
        return "Это мебель.";
    }
}


public sealed class Sofa : Furniture
{
    public override string ToString()
    {
        return "Это диван.";
    }
}


public class Bed : Furniture
{
    public override string ToString()
    {
        return "Это кровать.";
    }
}


public class Wardrobe : Furniture
{
    public override string ToString()
    {
        return "Это шкаф.";
    }
}


public class SlidingWardrobe : Wardrobe
{
    public override string ToString()
    {
        return "Это шкаф-купе.";
    }
}


public class Hanger : Furniture
{
    public override string ToString()
    {
        return "Это вешалка.";
    }
}


public class BedsideTable : Furniture
{
    public override string ToString()
    {
        return "Это тумба.";
    }
}


public class Chair : Furniture
{
    public override string ToString()
    {
        return "Это стул.";
    }
}


public class Printer
{
    public void IAmPrinting(Product someobj)
    {
        Console.WriteLine(someobj.ToString());
    }
}