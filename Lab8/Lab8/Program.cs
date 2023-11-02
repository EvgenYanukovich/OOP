using System;

class Programmer
{
    public delegate void RenameHandler(string oldName, string newName);
    public event RenameHandler Renamed;

    public delegate void NewPropertyHandler(string propertyName, string propertyValue);
    public event NewPropertyHandler NewPropertyAdded;

    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                Renamed?.Invoke(_name, value);
                _name = value;
            }
        }
    }

    private string _property;
    public string Property
    {
        get { return _property; }
        set
        {
            if (_property != value)
            {
                NewPropertyAdded?.Invoke("Property", value);
                _property = value;
            }
        }
    }
}

class Language
{
    public string Name { get; set; }
    public string Version { get; set; }

    public Language(string name, string version)
    {
        Name = name;
        Version = version;
    }

    public void OnRenamed(string oldName, string newName)
    {
        if (Name == oldName)
        {
            Name = newName;
            Console.WriteLine($"Язык {oldName} был переименован в {newName}");
        }
    }

    public void OnNewPropertyAdded(string propertyName, string propertyValue)
    {
        Console.WriteLine($"Добавлено новое свойство: {propertyName} со значением: {propertyValue}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Programmer programmer = new Programmer();
        
        Language csharp = new Language("C#", "9.0");
        Language python = new Language("Python", "3.9");

        programmer.Renamed += csharp.OnRenamed;
        programmer.Renamed += python.OnRenamed;

        programmer.NewPropertyAdded += csharp.OnNewPropertyAdded;
        
        programmer.Name = "John Doe";
        programmer.Property = "Senior Developer";
    }
}
