using System;
using System.Collections.Generic;
using System.Linq;
using LINQ_ExtensionsLib;

namespace LINQ_ExtensionsTest
{
    /// <summary>
    /// Tester class for LinqExtension methods.
    /// </summary>
    public static  class TestLinqExtension
    {
        /// <summary>
        /// Tets Select method.
        /// </summary>
        public static void TestSelect()
        {
            int[] list = {5, 4, 5, 2, 8, 6, 9, 6, 32, 4, 8, 5, 6};
            IEnumerable<int> divs = list.ExtensionSelect(x => x / 2);

            Console.WriteLine("Testing LinqExtension Select.");
            for (var counter = divs.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.Write(counter.Current + "  ");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Tests Where method.
        /// </summary>
        public static void TestWhere()
        {
            List<KeyValuePair<int,string>> list = new List<KeyValuePair<int, string>>();
            list.Add(new KeyValuePair<int, string>(4,"Marco"));
            list.Add(new KeyValuePair<int, string>(11,"Mickey"));
            list.Add(new KeyValuePair<int, string>(28,"Chris"));
            list.Add(new KeyValuePair<int, string>(12,"Vazgen"));
            list.Add(new KeyValuePair<int, string>(29,"Morris"));

            Console.WriteLine("Testing LinqExtensions Where method.");

            IEnumerable<KeyValuePair<int, string>> newList = list.ExtensionWhere(str => str.Value.StartsWith("M"));

            for (var counter = newList.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.Write(counter.Current);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Tests ToDictionary method.
        /// </summary>
        public static void TestToDictionary()
        {
            KeyValuePair<int, string>[] dictArray =
            {
                new KeyValuePair<int, string>(4, "Alber"),
                new KeyValuePair<int, string>(11, "Eric"),
                new KeyValuePair<int, string>(44, "Alex"),
                new KeyValuePair<int, string>(78, "Brian"),
            };

            Console.WriteLine("Testing LinqExtensions ToDictioanary method.");
            Dictionary<int, KeyValuePair<int, string>> dictionary = dictArray.ExtensionToDictionary(kv => kv.Key);

            for (var counter = dictionary.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.Write(counter.Current + "  ");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Tets ToList method
        /// </summary>
        public static void TestToList()
        {
            int[] listArray = {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233};

            Console.WriteLine("Testing LinqExtension ToList method");

            List<int> list = listArray.ExtensionToList();

            for (var counter = list.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.Write(counter.Current + "  ");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Tets GroupBy method.
        /// </summary>
        public static void TestGroupBy()
        {
            Dictionary<string,int> dictionary = new Dictionary<string,int>();
            
            dictionary.Add("Alber",17);
            dictionary.Add("Robert",19);
            dictionary.Add("Davit",17);
            dictionary.Add("Helen",19);
            dictionary.Add("George",15);
            dictionary.Add("Craig",19);

            Console.WriteLine("Testing LinqExtensiosn GroupBy method");
            IEnumerable<IGrouping<int,KeyValuePair<string, int>>> groupedEnumerable =
                dictionary.ExtensionGroupBy(kv => kv.Value);

            for (var counter = groupedEnumerable.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.WriteLine();
                for (var keyCounter = counter.Current.GetEnumerator(); keyCounter.MoveNext() != false;)
                {
                    Console.Write(keyCounter.Current.Key + "  ");
                }

                Console.WriteLine(counter.Current.Key);

            }

            Console.ReadLine();
        }

        /// <summary>
        /// Tests OrderBy method.
        /// </summary>
        public static void TestOrderBy()
        {
            List<KeyValuePair<int,string>> list = new List<KeyValuePair<int, string>>();
            list.Add(new KeyValuePair<int, string>(15,"Peter"));
            list.Add(new KeyValuePair<int, string>(42,"Alen"));
            list.Add(new KeyValuePair<int, string>(11,"Chris"));
            list.Add(new KeyValuePair<int, string>(18,"Mary"));
            list.Add(new KeyValuePair<int, string>(19,"Lupita"));

            IOrderedEnumerable<KeyValuePair<int, string>> orderedEnumerable =
                list.ExtensionOrderBy(kv => kv.Value, false);
            Console.WriteLine("Testing LinqExtensions OrderBy method.");

            for (var counter = orderedEnumerable.GetEnumerator(); counter.MoveNext() != false;)
            {
                Console.WriteLine(counter.Current.Value + "  ");
            }

            Console.ReadLine();
        }
    }
}