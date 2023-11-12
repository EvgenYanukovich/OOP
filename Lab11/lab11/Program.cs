namespace lab11 {
    internal class Program {
        static void Main(string[] args) {
            ReflectorTestingFor("lab02.DoubleStack, lab10");
            ReflectorTestingFor("lab11.Reflector");

            ReflectorTestingFor("System.Object");
            ReflectorTestingFor("System.Int32");
            ReflectorTestingFor("System.String");
            ReflectorTestingFor("System.Action");


            lab02.DoubleStack doubleStack = new lab02.DoubleStack(new List<double>() { 1.7, 6.3, 3.2, 8.0 });
            Console.WriteLine($"Before Invoke: {doubleStack}");
            Reflector.Invoke(doubleStack, "Push", new object[] { 10.5 });
            Console.WriteLine($"After Invoke: {doubleStack}");


            lab02.DoubleStack doubleStack1 = Reflector.Create<lab02.DoubleStack>("lab02.DoubleStack, lab10", new object[] { "TestStack" });
            Console.WriteLine(doubleStack1.GetInfo());
        }

        static void ReflectorTestingFor(string typeName) {
            Console.WriteLine($"\n\n\t\tReflector testing for '{typeName}'");
            Console.WriteLine($"<GetType>: {Reflector.GetType(typeName)}");
            Console.WriteLine($"<HasPublicConstructors>: {Reflector.HasPublicConstructors(typeName)}");
            Console.WriteLine($"<GetAssemblyInfo>: {Reflector.GetAssemblyInfo(typeName)}");
            Console.WriteLine($"<GetPublicMethods>:\n\t{string.Join("\n\t", Reflector.GetPublicMethods(typeName))}");
            Console.WriteLine($"<GetPublicFields>:\n\t{string.Join("\n\t", Reflector.GetPublicFields(typeName))}");
            Console.WriteLine($"<GetPublicProperties>:\n\t{string.Join("\n\t", Reflector.GetPublicProperties(typeName))}");
            Console.WriteLine($"<GetImplementedInterfaces>:\n\t{string.Join("\n\t", Reflector.GetImplementedInterfaces(typeName))}");
        }
    }
}