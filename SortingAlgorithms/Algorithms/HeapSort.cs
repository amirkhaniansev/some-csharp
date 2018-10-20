using System.Diagnostics;

namespace Algorithms
{
    /// <summary>
    /// Class for sorting sequences using heap sort algorithm
    /// </summary>
    public class HeapSort:Algorithm 
    {
        /// <summary>
        /// Parameter-less constructor to initialize algorithm-specific number
        /// </summary>
        public HeapSort():base(4) { }

        /// <summary>
        /// Makes array max heap.
        /// </summary>
        /// <param name="arr"> Used to specify array,that must be rearranged like heap.</param>
        /// <param name="size"> Used to specify the size of array.</param>
        /// <param name="index"> Used to specify index.</param>
        private void MakeHeap(int[] arr,int size,int index)
        {
            int Largest = index, Left = 2 * index + 1, Right = 2 * index + 2;
            if(Left < size && arr[Left] > arr[Largest])
            {
                Largest = Left;
            }
            if(Right < size && arr[Right] > arr[Largest])
            {
                Largest = Right;
            }
            if(Largest!=index)
            {
                Utilities.Swap(ref arr[index],ref arr[Largest]);
                this.MakeHeap(arr,size,Largest);
            }
        }

        /// <summary>
        /// Sorts the given array using heap sort algorithm
        /// </summary>
        /// <param name="arr"> Specifies sorting array. </param>
        /// <returns> Returns sorted array.</returns>
        public  int[] Sort(int[] arr)
        {
            this.memory = 4 * sizeof(int);
            Stopwatch counter = new Stopwatch();
            counter.Start();
            for(int i=arr.Length/2-1;i>=0;i--)
            {
                MakeHeap(arr,arr.Length, i);
            }
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Utilities.Swap(ref arr[0], ref arr[i]);
                this.MakeHeap(arr,i, 0);
            }
            counter.Stop();
            this.runningTime = counter.Elapsed.TotalMilliseconds;
            return arr;
        }
    }
}
