using System;

namespace MathServer
{
    /// <summary>
    /// Class for doing mathematical operations.
    /// </summary>
    public class MathService:IMathService
    {
        /// <summary>
        /// Adds 2 values.
        /// </summary>
        /// <param name="firstValue">1st value.</param>
        /// <param name="secondValue">2nd value.</param>
        /// <returns> Returns the sum of 2 values.</returns>
        public double Add(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        /// <summary>
        /// Divides 1st value by 2nd value.
        /// </summary>
        /// <param name="firstValue"> 1st value. </param>
        /// <param name="secondValue"> 2nd value. </param>
        /// <returns> Returns the division. </returns>
        public double Div(double firstValue, double secondValue)
        {
            if (secondValue == 0)
            {
                throw new DivideByZeroException("Zero division");
            }

            return firstValue / secondValue;
        }

        /// <summary>
        /// Multiplies 2 values.
        /// </summary>
        /// <param name="firstValue"> 1st value. </param>
        /// <param name="secondValue"> 2nd value. </param>
        /// <returns> Returns multiplication of 2 values. </returns>
        public double Mult(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        /// <summary>
        /// Subtracts 2nd value from 1st value.
        /// </summary>
        /// <param name="firstValue"> 1st value. </param>
        /// <param name="secondValue"> 2nd value. </param>
        /// <returns> Retunrs subtraction. </returns>
        public double Sub(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }
    }
}
