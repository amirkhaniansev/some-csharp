namespace Algorithms
{
    /// <summary>
    /// Base class for algorithms.
    /// </summary>
    public class Algorithm
    {
        /// <summary>
        /// Specific algorithm number.
        /// </summary>
        public readonly int algorithmNumber;

        /// <summary>
        /// Gets running time of algorithm.
        /// </summary>
        public double runningTime
        {
            get;protected set;
        }

        /// <summary>
        /// Gets amount of allocated memory of algorithm.
        /// </summary>
        public int memory
        {
            get;protected set;
        }

        /// <summary>
        /// Parameterized constructor of class Algorithm
        /// </summary>
        /// <param name="num"> Used to specify the algorithm-specific number. </param>
        public Algorithm(int num)
        {
            algorithmNumber = num;
        }
    }
}
