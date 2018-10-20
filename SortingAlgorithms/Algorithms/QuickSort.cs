using System.Diagnostics;

namespace Algorithms
{
    /// <summary>
    /// Class for sorting sequences using quick sort algorithm
    /// </summary>
    public class QuickSort:Algorithm
    {      
        /// <summary>
        /// Parameter-less constructor to initialize algorithm-specific number.
        /// </summary>
        public QuickSort():base(3) { }

        /// <summary>
        /// Partitions the array.
        /// </summary>
        /// <param name="arr"> Partitioning array. </param>
        /// <param name="low"> Lower bounadary. </param>
        /// <param name="high"> Higher boundary. </param> 
        /// <returns> Returns partitioning index. </returns>
        private int Partition(int[] arr,int low,int high)
        {
            int pivot = arr[high], i = low - 1;
            Stopwatch counter = new Stopwatch();
            counter.Start();
            for (int j=low;j <= high-1;j++)
            {
                if(arr[j]<=pivot)
                {
                    i++;
                    //swapping arr[i] and arr[j]
                    Utilities.Swap(ref arr[i], ref arr[j]);
                }
            }

            //swapping arr[i+1] and arr[high]
            Utilities.Swap(ref arr[i + 1], ref arr[high]);
            counter.Stop();
            this.runningTime += counter.Elapsed.TotalMilliseconds;
            return (i + 1);
        }

        /// <summary>
        /// Sorts array using quick sort algorithm. 
        /// </summary>
        /// <param name="arr"> Specifies array,which will be sorted. </param>
        /// <returns> Returns already sorted array.</returns>
        public int[] Sort(int[] arr)
        {
            int[] stack = new int[arr.Length + 1];
            int top = 1, high, low, partition_index;
            stack[0] = 0;
            stack[1] = arr.Length-1;
            this.memory = (arr.Length + 4) * sizeof(int);
            Stopwatch counter = new Stopwatch();

            //starts counting time
            counter.Start();
            while(top >= 0)
            {
                high = stack[top--];low = stack[top--];
                counter.Stop();
                partition_index = Partition(arr, low, high);
                counter.Start();
                if (partition_index - 1 > low)
                {
                    stack[++top] = low;
                    stack[++top] = partition_index - 1;
                }
                if(partition_index+1 < high)
                {
                    stack[++top] = partition_index + 1;
                    stack[++top] = high;
                }
            }
            //stops counting time
            counter.Stop();
            this.runningTime +=  counter.Elapsed.TotalMilliseconds;
            return arr;
        }

    }
}
