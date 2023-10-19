using System;
using System.Net.Http.Headers;

public class Password
{
    private string password;

    //Конструктор
    public Password(string password)
    {
        this.password = password;
    }

    //Индексатор
    public string this[string password] 
    {
        get {return password;}
        set {password = value;}
    }
    
    public static Password operator -(Password p, int item)
    {
        p.password = p.password.Remove(p.password.Length - 1);
        return p;
    }

    public static bool operator >(Password p1, Password p2)
    {
        return p1.password.Length > p2.password.Length;
    }

    public static bool operator <(Password p1, Password p2)
    {
        return p1.password.Length < p2.password.Length;
    }

    public static bool operator !=(Password p1, Password p2)
    {
        return p1.password != p2.password;
    }

    public static bool operator ==(Password p1, Password p2)
    {
        return p1.password == p2.password;
    }

    public static Password operator ++(Password p)
    {
        p.password = "default";
        return p;
    }

    public static bool operator true(Password p)
    {
        return p.password.Length >= 8;
    }

    public static bool operator false(Password p)
    {
        return p.password.Length < 8;
    }

    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
    }
    dynamic Product = new Production() { Id = 0 , OrganizationName = "Moloko"};

    public class Developer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
    }
    dynamic Dev = new Developer() { FullName = "Evgeniy", Id = 0, Department="BSTU" };
}

public static class StatisticOperation
{
    public static int Sum(Password p1, Password p2)
    {
        return p1.ToString().Length + p2.ToString().Length;
    }

    public static int Minus(Password p1, Password p2)
    {
        return p1.ToString().Length >= p2.ToString().Length ? p1.ToString().Length - p2.ToString().Length : p2.ToString().Length - p1.ToString().Length;
    }

    public static int Length(Password p1)
    {
        return p1.ToString().Length;
    }

    public static string MiddleSymbol(this string str)
    {
        return str.Length % 2 == 0 ? str.Substring(str.Length / 2 - 1, 2) : str.Substring(str.Length / 2, 1);
    }

    public static bool IsValidLength(this Password password)
    {
        var length = password.ToString().Length;
        return length >= 6 && length <= 12;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Password password1 = new Password("password1");
        Password password2 = new Password("password2");

        //-
        password1 = password1 - 1;
        Console.WriteLine($"Password after - operator?\n {password1}");

        //>
        bool isPassword1Longer = password1 > password2;
        Console.WriteLine($"Is password1 longer than password2?\n {isPassword1Longer}");

        //!=
        bool arePasswordsDifferent = password1 != password2;
        Console.WriteLine($"Are passwords different?\n {arePasswordsDifferent}");

        //++
        password1++;
        Console.WriteLine($"Password after ++ operator?\n {password1}");

        //true
        if (password1) 
        {
            Console.WriteLine($"Is password strong?\n true");
        }
        else
        {
            Console.WriteLine($"Is password strong?\n false");
        }


        // Проверка методов расширения
        string middleSymbol = "password".MiddleSymbol();
        Console.WriteLine($"Middle symbol of 'password'?\n {middleSymbol}");

        bool isValidLength = password1.IsValidLength();
        Console.WriteLine($"Is password of valid length?\n {isValidLength}");
    }
}
