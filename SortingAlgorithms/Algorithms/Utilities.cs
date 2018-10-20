using System;

namespace Algorithms
{
    /// <summary>
    /// Helper class for input,output and calculating operations.
    /// </summary>
    public static class Utilities
     {
        /// <summary>
        /// Prints Algorithm results.
        /// </summary>
        /// <param name="al"> Used to print algorithm results. </param>
        public static void PrintResults(Algorithm al)
        {
            Console.WriteLine(al.algorithmNumber + "." + al +
                "\nrunning time = " + al.runningTime + " milliseconds\nmemory = " + al.memory + " bytes.");
        }

        /// <summary>
        /// Swaps two parameters values given by reference
        /// </summary>
        /// <param name="x"> Specifies x. </param>
        /// <param name="y"> Specifies y. </param>
        public static void Swap(ref int x,ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        /// <summary>
        /// Generates array of random ints.
        /// </summary>
        /// <param name="len"> Used to specify length of array. </param>
        /// <param name="min"> Used to specify lower boundary of array. </param>
        /// <param name="max"> Used to specify upper boundary of array. </param>
        /// <returns> Returns randomly generated array. </returns>
        public static int[] RandArrGen(int len, int min, int max)
        {
            Random randomNumber = new Random();
            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {
                //generating a random integer for every a[i] in range of(max-min)

              arr[i] = randomNumber.Next(min, max);
            }
            return arr;
        }

        /// <summary>
        /// Prints array of ints.
        /// </summary>
        /// <param name="arr"> Used to specify the printing array. </param>
        public static void PrintArray(int[] arr)
        {
            int arrLen = arr.Length;
            for (int i = 0; i < arrLen; i++)
            {
                Console.Write(arr[i] + "  ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Copies array.
        /// </summary>
        /// <param name="arr"> Array will be copied. </param>
        /// <returns> Returns copied array. </returns>
        public static int[] CopyArray(int[] arr)
        {
            int arrLen = arr.Length;
            int[] arrCopy = new int[arrLen];
            for (int i = 0; i < arrLen; i++)
            {
                arrCopy[i] = arr[i];
            }
            return arrCopy;
        }

        /// <summary>
        /// Starts input.
        /// </summary>
        /// <returns> Returns entered array. </returns>
        public static int[] StartInput()
        {
            int N, min, max;
            int[] arr = null;
            Console.WriteLine("Please enter the size of an array you want to sort");
            Console.Write("N = ");
            N = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the boundaries of array");

            //entering lower boundary of array
            Console.Write("MinValue = ");
            min = int.Parse(Console.ReadLine());

            //entering upper boundary of array
            Console.Write("MaxValue = ");
            max = int.Parse(Console.ReadLine());

            //generating an array of random ints
            arr = Utilities.RandArrGen(N, min, max);
            Console.WriteLine("This is your array consisted of random ints");

            //printing already generated array
            Utilities.PrintArray(arr);
            Console.WriteLine("Select which algorithm you want to perform");
            Console.WriteLine("1.Insertion sort.\n" +
                              "2.Bubble sort.\n" +
                              "3.Quick sort.\n" +
                              "4.Heap sort.\n" +
                              "5.Merge sort.\n" +
                              "6.All");
            return arr;
        }

        /// <summary>
        /// Checks whether string contains only numbers and '-',','.
        /// </summary>
        /// <param name="str"> Used to check input validity. </param>
        /// <returns> Returns boolean value describing the validity of str.</returns>
        public static bool Check(string str)
        {
            int len = str.Length, num;

            //if string is empty,return false
            if (str == "")
                return false;

            //if string contains only one number and it is
            //greater than 6 return false,else return true 
            if (!str.Contains("-") && !str.Contains(",") && int.TryParse(str, out num))
            {
                if (num > 6)
                    return false;
                else return true;
            }

            //if string starts or ends with '-' or ',' or contains both of them
            //or contains these symbols twice return false
            if (str.StartsWith("-") || str.StartsWith(",") ||
                 str.EndsWith("-") || str.EndsWith(",") || (str.Contains(",") && str.Contains("-"))
                 || str.Contains(",,") || str.Contains("--"))
            {
                return false;
            }

            //check if string symbols are numbers in 0-5 range or ',','-' symbols
            for (int i = 0; i < len; i++)
            {
                if (!(str[i] > '0' && str[i] <= '5') && str[i] != '-' && str[i] != ',')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks input ofalgorithm numbers.
        /// </summary>
        /// <param name="arr"> Used to store input.</param>
        /// <param name="str"> Used to split the string.</param>
        /// <returns> Returns boolean value descibing the validity of algorithm numbers input</returns>
        public static bool CheckInput(ref int[] arr, string str)
        {
            int len;
            string[] splittedStringArray;

            //if string is compatible for splitting and parsing
            //do operations and return true else return false
            if (Check(str))
            {
                //if entered string is in 2,3,..n format
                //then do operations and return true
                if (str.Contains(","))
                {
                    splittedStringArray = str.Split(',');
                    len = splittedStringArray.Length;
                    arr = new int[len];
                    for (int i = 0; i < len; i++)
                    {
                        //parsing numbers contained in str to arr
                        arr[i] = int.Parse(splittedStringArray[i]);

                        //if number is greater return false
                        //cause there is no algorithm with number 6
                        if (arr[i] > 6)
                            return false;
                    }
                    return true;
                }

                //if entered string is in i-j format
                //then do operations and return true
                else if (str.Contains("-"))
                {
                    splittedStringArray = str.Split('-');
                    int first = int.Parse(splittedStringArray[0]);
                    len = int.Parse(splittedStringArray[splittedStringArray.Length - 1]) - first + 1;
                   
                    //if range is bigger than 5 or less than 0 or first number is greater than 5
                    //return false  
                    if (len > 5 || len <= 0 || first > 5)
                        return false;

                    arr = new int[len];
                    //write numbers of string in arr
                    for (int i = 0; i < len; i++)
                    {
                        arr[i] = first++;
                    }

                    return true;
                }

                //if entered string contains only one number 
                //write it in arr[0]
                else if(int.Parse(str)==6)
                {
                    arr = new int[5];
                    for(int i=0;i<5;i++)
                    {
                        arr[i] = i + 1;
                    }
                    return true;
                }
                else
                {
                    arr = new int[1];
                    arr[0] = int.Parse(str);
                    return true;
                }
            }

            //in the end if string isn't compatible for
            //parsing and splitting return false
            else return false;
        }

        /// <summary>
        /// Inputs algorithm numbers. 
        /// </summary>
        /// <returns> Returns array of algorithm numbers. </returns>
        public static int[] Input()
        {
            int[] arr = null;
            string str;
            Console.WriteLine("Please enter the numbers of algorithm in this format.\n" +
                        "For example:2,3 or 2,3,4 or 2-4 or 5.Numbers must be smaller than 7.");
            while (true)
            {
                str = Console.ReadLine();

                //check if input isn't ok,i.e isn't compatible for splitting and parsing
                //or doesn't contain valid numbers
                //then repeat input process
                //else if everything is ok
                //then return the array
                //containing algorithm-specific numbers
                if (!CheckInput(ref arr, str))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\a\aInvalid input." +
                        "Please enter the numbers of algorithms in this format.\n" +
                        "For example:2,3 or 2,3,4 or 2-4 or 5.Numbers must be smaller than 7.");
                    Console.ResetColor();
                }
                else return arr;
            }
        }

        /// <summary>
        /// Prints final output.
        /// </summary>
        /// <param name="arr"> Used to specify algorithms. </param>
        public static void FinalOutput(Algorithm[] arr)
        {
            double runMin = RunMin(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].runningTime != 0)
                {
                    if (arr[i].runningTime == runMin)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Utilities.PrintResults(arr[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Utilities.PrintResults(arr[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Finds minimum running time.
        /// </summary>
        /// <param name="alArr"> Used to specify the array of algorithms. </param>
        /// <returns> Returns the minimal running time of algorithms. </returns>
        public static double RunMin(Algorithm[] alArr)
        {
            double runMin = alArr[0].runningTime;
            for (int i = 1; i < alArr.Length; i++)
            {
                if (alArr[i].runningTime == 0)
                    continue;
                if (runMin == 0 && alArr[i].runningTime != 0)
                    runMin = alArr[i].runningTime;
                if (runMin > alArr[i].runningTime)
                    runMin = alArr[i].runningTime;
            }
            return runMin;
        }
    }
}