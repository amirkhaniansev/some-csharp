using System.Diagnostics;

namespace Algorithms
{
    /// <summary>
    /// Class for sorting sequences using bubble sort algorithm.
    /// </summary>
    public class BubbleSort : Algorithm
    {     
        /// <summary>
        /// Parameter-less constructor of BubbleSort
        /// </summary>
        public BubbleSort():base(2) { }

        /// <summary>
        /// Sorts the given array.
        /// </summary>
        /// <param name="arr"> Used to specify the array,that will be sorted. </param>
        /// <returns> Returns already sorted array. </returns>
        public int[] Sort(int[] arr)
        {
            int arrLen = arr.Length;
            this.memory = 4 * sizeof(int);

            //start calculation of elapsed time
            Stopwatch counter = new Stopwatch();
            counter.Start();
            for(int i=0;i<arrLen;i++)
            {
                for(int j=0;j<arrLen-i-1;j++)
                {
                    if(arr[j]>arr[j+1])
                    {
                        Utilities.Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
            //stop calculating time
            counter.Stop();          
            this.runningTime = counter.Elapsed.TotalMilliseconds;
            return arr;
        }
    }
}