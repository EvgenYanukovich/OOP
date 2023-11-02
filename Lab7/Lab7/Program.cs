using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// обобщенный интерфейс с операциями добавить, удалить, просмотреть.
public interface IOperations<T>
{
    void Add(T item);
    void Remove(T item);
    T View(Predicate<T> predicate);
}

// обобщенный тип (класс) CollectionType<T>, в котором лежит обобщённая коллекция.
public class CollectionType<T> : IOperations<T>
{
    private List<T> collection = new List<T>();

    public void Add(T item)
    {
        try
        {
            collection.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция добавления завершена.");
        }
    }

    public void Remove(T item)
    {
        try
        {
            collection.Remove(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция удаления завершена.");
        }
    }

    public T View(Predicate<T> predicate)
    {
        try
        {
            return collection.Find(predicate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return default(T);
        }
        finally
        {
            Console.WriteLine("Операция просмотра завершена.");
        }
    }

    //методы сохранения объектов типа CollectionType<T> в файл и чтения из него.
    public void SaveToFile(string fileName)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(collection);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine("Коллекция успешно сохранена в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
        }
    }

    public void LoadFromFile(string fileName)
    {
        try
        {
            var jsonString = File.ReadAllText(fileName);
            collection = JsonSerializer.Deserialize<List<T>>(jsonString);
            Console.WriteLine("Коллекция успешно загружена из файла.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке из файла: {ex.Message}");
        }
    }
}

// пользовательский класс
public class UserClass
{
    public string? Name { get; set; }
}

// использование обобщения для стандартных типов данных
class Program
{
    static void Main(string[] args)
    {
        var intCollection = new CollectionType<int>();
        intCollection.Add(1);

        var userClassCollection = new CollectionType<UserClass>();
        userClassCollection.Add(new UserClass { Name = "Test" });

        userClassCollection.SaveToFile("test.json");
        userClassCollection.LoadFromFile("test.json");
    }
}
