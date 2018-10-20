using System.Diagnostics;

namespace Algorithms
{
    /// <summary>
    /// Class for sorting sequences using insertion sort algorithm.
    /// </summary>
    public class InsertionSort:Algorithm
    {
        /// <summary>
        /// Parameter-less constructor to initialize the algorithm specific number.
        /// </summary>
        public InsertionSort():base(1) { }
       
        /// <summary>
        /// Sorts the given array.
        /// </summary>
        /// <param name="arr"> Used to specify the array,which will be sorted.</param>
        /// <returns> Returns sorted array. </returns>
        public  int[] Sort(int[] arr)
        {
            int length = arr.Length, j;
            this.memory = 4 * sizeof(int);

            //start calculating elapsed time
            Stopwatch counter = new Stopwatch();
            counter.Start();
            for(int i=1;i<length;i++)
            {
                j = i;
                while(j>0 && arr[j-1]>arr[j])
                {
                    Utilities.Swap(ref arr[j], ref arr[j - 1]);
                    j--;
                }
            }
            counter.Stop();
            //stop calculation
            this.runningTime = counter.Elapsed.TotalMilliseconds;
            return arr;
        }
    }
}
