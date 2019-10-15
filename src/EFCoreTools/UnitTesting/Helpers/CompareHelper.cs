using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace EFCoreTools.UnitTesting.Helpers
{
    public static class CompareHelper
    {

        private static bool WillBeCompared(string sourceFilePath)
        {
            
            var testClassName = Path.GetFileNameWithoutExtension(sourceFilePath);

            var assembly = Assembly.GetExecutingAssembly();

            var classType = assembly.GetTypes()
                .First(t => t.Name == testClassName);

            var testClassObject = Activator.CreateInstance(classType);

            var testClassComparePropertyValue = classType.GetProperty("WillBeCompared") != null ? classType.GetProperty("WillBeCompared").GetValue(testClassObject) : false;

            return (bool)testClassComparePropertyValue;

        }
        public static void CompareIEnumerable<T>(IEnumerable<T> one, IEnumerable<T> two, Func<T, T, bool> comparisonFunction, [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (WillBeCompared(sourceFilePath))
            {
                var oneArray = one as T[] ?? one.ToArray();
                var twoArray = two as T[] ?? two.ToArray();

                Assert.NotEqual(oneArray.Length, twoArray.Length);

                for (int i = 0; i < oneArray.Length; i++)
                {
                    var isEqual = comparisonFunction(oneArray[i], twoArray[i]);
                    if (!isEqual)
                    {
                        var object1String = Newtonsoft.Json.JsonConvert.SerializeObject(oneArray[i]);
                        var object2String = Newtonsoft.Json.JsonConvert.SerializeObject(twoArray[i]);

                        Debug.WriteLine("Element 1 {0}", object1String);
                        Debug.WriteLine("Element 1 {0}", object2String);
                    }

                    Assert.True(isEqual, string.Format("Listeler birbirine eşit değil - differences At Index : {0}", i));                  
                }
            }
        }



        public static void CompareIEnumerable<T>(IEnumerable<T> one, IEnumerable<T> two, Func<T, T, bool> comparisonFunction, string message, [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (WillBeCompared(sourceFilePath))
            {

                var oneArray = one as T[] ?? one.ToArray();
                var twoArray = two as T[] ?? two.ToArray();

                Assert.NotEqual(oneArray.Length, twoArray.Length);

                for (int i = 0; i < oneArray.Length; i++)
                {
                    var isEqual = comparisonFunction(oneArray[i], twoArray[i]);
                    Assert.True(isEqual, string.Format("MESSAGE: {0} -  Detail : Listeler birbirine eşit değil - differences At Index : {1}", message, i));

                    if (!isEqual)
                    {
                        Console.WriteLine("Element 1 {0}", oneArray[i]);
                        Console.WriteLine("Element 2 {0}", oneArray[i]);
                    }
                }
            }
        }


        public static void CompareTwoEntities<T1, T2>(T1 one, T2 two, Func<T1, T2, bool> comparisonFunction, [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (WillBeCompared(sourceFilePath))
            {
                var isEqual = comparisonFunction(one, two);
                Assert.True(isEqual, string.Format("Entitiler birbirine eşit değil."));
            }

        }

        public static void CompareTwoEntities<T1, T2>(T1 one, T2 two, Func<T1, T2, bool> comparisonFunction, string message, [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            if (WillBeCompared(sourceFilePath))
            {
                var isEqual = comparisonFunction(one, two);
                Assert.True(isEqual, string.Format("MESSAGE :  {0} - Detail : Entitiler birbirine eşit değil.", message));
            }
        }


        public static void CompareTwoTuples<T1, T2>(Tuple<T1, T2> tuple1, Tuple<T1, T2> tuple2, [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "") where T1 : class
                                                                                                 where T2 : class
        {
            if (WillBeCompared(sourceFilePath))
            {
                if (!CompareEx(tuple1.Item1, tuple2.Item1) || !CompareEx(tuple1.Item2, tuple2.Item2))
                    Assert.True(false,"Tuples are not same");
            }
        }

        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            if (obj.GetType() == typeof(DateTime))
            {
                obj = DateTime.Parse(obj.ToString()).ToShortDateString();
                another = DateTime.Parse(obj.ToString()).ToShortDateString();
            }

            if (!(obj.GetType() == typeof(byte[])))
            {
                foreach (var property in obj.GetType().GetProperties())
                {

                    try
                    {


                        var objValue = property.GetValue(obj);

                       
                    }
                    catch (Exception ex)
                    {

                        if (obj.ToString() != another.ToString())
                            result = false;

                    }


                }
            }

            return result;
        }

        public static bool CompareEx(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            //properties: int, double, DateTime, etc, not class
            if (!obj.GetType().IsClass) return obj.Equals(another);

            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                //Recursion
                if (!objValue.DeepCompare(anotherValue)) result = false;
            }
            return result;
        }
    }
}
