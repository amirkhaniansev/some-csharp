using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dictionary
{
    /// <summary>
    /// Test class for testing differeent implementations of IDictionary
    /// </summary>
    public static class DictionaryTest
    {
        public static void Test(IDictionary<int,int> dictionary,int entryAmount)
        {
            int[] randomArray = GenRandomArr(entryAmount);
            Stopwatch stopwatch = new Stopwatch();

            //running insertion test
            stopwatch.Start();
            for (var i = 0; i < randomArray.Length; i++)
            {
                dictionary.Add(randomArray[i], randomArray[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Insertion time for " + entryAmount + " elements to the " + dictionary.ToString() + " = "
                        + stopwatch.Elapsed.TotalMilliseconds + " ms. ");

            //running retrieve test

            int element;
            stopwatch.Restart();
            for (var i = 0; i < randomArray.Length; i++)
            {
                element = dictionary[randomArray[i]];
            }
            stopwatch.Stop();

            Console.WriteLine("Retrieve time for " + entryAmount + " elements from the " + dictionary.ToString() + " = "
                        + stopwatch.Elapsed.TotalMilliseconds + " ms. ");

            //running deletion test
            stopwatch.Restart();
            for (var i = 0; i < randomArray.Length; i++) 
            {
                dictionary.Remove(randomArray[i]);
            }
            stopwatch.Stop();
            Console.WriteLine("Deletion time for " + entryAmount + " elements from the " + dictionary.ToString() + " = "
                        + stopwatch.Elapsed.TotalMilliseconds + " ms. ");

            dictionary.Clear();

        }

        /// <summary>
        /// Generates random array of integers.
        /// </summary>
        /// <param name="length"></param>
        /// <returns> Returns new array consisted of random integers. </returns>
        private static int[] GenRandomArr(int length)
        {
            int[] array = new int[length];
            Random random = new Random();

            for (var i = 0; i < length; i++)
            {
                array[i] = random.Next();
            }

            return array;
        }
    }
}
