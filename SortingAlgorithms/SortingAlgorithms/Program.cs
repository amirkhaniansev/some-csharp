using System;
using Algorithms;
using static Algorithms.Utilities;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        { 
            InsertionSort insertionSort = new InsertionSort();
            BubbleSort bubbleSort = new BubbleSort();
            QuickSort quickSort = new QuickSort();
            HeapSort heapSort = new HeapSort();
            MergeSort mergeSort = new MergeSort();
            int[] arr = StartInput();
            int[] sortedArray = null;
            int[] alNumbersArray = Utilities.Input();
            for (int i = 0; i < alNumbersArray.Length; i++)
            {
                switch (alNumbersArray[i])
                {
                    case 1: sortedArray = insertionSort.Sort(CopyArray(arr)); break;
                    case 2: sortedArray = bubbleSort.Sort(CopyArray(arr)); break;
                    case 3: sortedArray = quickSort.Sort(CopyArray(arr)); break;
                    case 4: sortedArray = heapSort.Sort(CopyArray(arr)); break;
                    case 5: sortedArray = mergeSort.Sort(CopyArray(arr)); break;
                    default: break;
                }
            }
            Algorithm[] alArr = { insertionSort, bubbleSort, quickSort, heapSort,  mergeSort };
            FinalOutput(alArr);
            Console.ReadLine();
        }
    }
}
