using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab11 {
    public static class Reflector {
        public static Type GetType(Object obj) {
            return obj.GetType();
        }

        public static Type GetType(string typeName) { 
            Type? typeObject = Type.GetType(typeName);

            if (typeObject == null) {
                throw new ReflectorException($"Can't find type '{typeName}'");
            }

            return typeObject;
        }

        public static bool HasPublicConstructors(string typeName) {
            return (
                GetType(typeName)
                    .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                    .Length > 0
            );
        }

        public static string GetAssemblyInfo(string typeName) {
            return GetType(typeName).Assembly.GetName().FullName; ;
        }

        public static IEnumerable<string> GetPublicMethods(string typeName) {
            return (
                GetType(typeName)
                    .GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public)
                    .Select(methodInfo => methodInfo.Name)
            );
        }

        public static IEnumerable<string> GetPublicFields(string typeName) {
            return (
                GetType(typeName)
                    .GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public)
                    .Select(fieldInfo => fieldInfo.Name)
            );
        }

        public static IEnumerable<string> GetPublicProperties(string typeName) {
            return (
                GetType(typeName)
                    .GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public)
                    .Select(propertyInfo => propertyInfo.Name)
            );
        }

        public static IEnumerable<string> GetImplementedInterfaces(string typeName) {
            return (
                GetType(typeName)
                    .GetInterfaces()
                    .Select(interfaceInfo => interfaceInfo.Name)
            );
        }

        public static IEnumerable<string> GetMethodsWithParameter(string typeName, string parameterType) {
            return (
                GetType(typeName)
                    .GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(
                        methodInfo 
                            => methodInfo
                                .GetParameters()
                                .Where(parameterInfo => parameterInfo.ParameterType == GetType(parameterType))
                                .Count() > 0
                    )
                    .Select(methodInfo => methodInfo.Name)
            );
        }

        public static Object? Invoke(Object obj, string methodName, Object?[]? parameters = null) {
            return (
                GetType(obj)
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(methodInfo => methodInfo.Name == methodName)
                    .First()
                    .Invoke(obj, parameters)
            );
        }

        public static T Create<T>(string typeName, Object?[]? parameters = null) {
            if (!HasPublicConstructors(typeName)) {
                throw new ReflectorException($"The type '{typeName}' has no public constructors");
            }

            var constructor = (
                GetType(typeName)
                    .GetConstructor(
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        CallingConventions.HasThis,
                        parameters?.Select(p => p.GetType()).ToArray() ?? new Type[] { },
                        null
                     )
            );

            if ( constructor == null ) {
                throw new ReflectorException($"The type '{typeName}' has no public constructors with given parameters types");
            }

            return (T)constructor.Invoke(parameters);
        }
    }
}
