using System.Diagnostics;

namespace Algorithms
{
    /// <summary>
    /// Class for sorting array using merge sort algorithm
    /// </summary>
    public class MergeSort :Algorithm
    {
        /// <summary>
        /// Parameter-less constructor to initialize the algorithm-specific number
        /// </summary>
        public MergeSort():base(5) { }

        /// <summary>
        /// Merges the given array.
        /// </summary>
        /// <param name="arr"> Specifies array that must be merged. </param>
        /// <param name="left"> Left index. </param>
        /// <param name="middle"> Middle index. </param>
        /// <param name="right"> Right index.</param>
        private void Merge(int[] arr,int left,int middle,int right)
        {
            int  i,j,k, firstN = middle - left + 1, secondN = right - middle;
            int[] Left = new int[firstN];
            int[] Right = new int[secondN];

            Stopwatch counter = new Stopwatch();
            counter.Start();
            for (i=0; i<firstN; i++)
            {
                Left[i] = arr[left + i];
            }
            for(j=0;j<secondN;j++)
            {
                Right[j] = arr[middle + j + 1];
            }
            i = j = 0;k = left;

            while (i<firstN && j<secondN)
            {
                if(Left[i]<=Right[j])
                {
                    arr[k++] = Left[i++];
                }
                else
                {
                    arr[k++] = Right[j++];
                }
            }
            while(i<firstN)
            {
                arr[k++] = Left[i++];
            }
            while(j<secondN)
            {
                arr[k++] = Right[j++];
            }
            counter.Stop();
            this.runningTime += counter.Elapsed.TotalMilliseconds;
        }

        private void mergeSort(int[] arr,int left,int right)
        {
            if(left<right)
            {
                int middle = left + (right - left) / 2;
                mergeSort(arr, left, middle);
                mergeSort(arr, middle + 1, right);
                Merge(arr, left, middle, right);
            }
        }
        /// <summary>
        /// Sorts the given array with merge sort algorithm.
        /// </summary>
        /// <param name="arr"> Specifies array that must be sorted.</param>
        /// <returns> Returns sorted array.</returns>
        public int[] Sort(int[] arr)
        {
            this.memory = arr.Length * sizeof(int);
           
            mergeSort(arr, 0, arr.Length - 1);
           
            return arr;
        }
    }
}
