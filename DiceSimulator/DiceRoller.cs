using System;

namespace DiceLib
{
    /// <summary>
    /// Class for testing Dice
    /// </summary>
    public class DiceRoller
    {
        /// <summary>
        /// Event handler for Two sixes in a row event.
        /// </summary>
        /// <param name="sender"> Sender.</param>
        /// <param name="e"> Dice Event argument.</param>
        private void TwoSix(object sender, DiceEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Triggered " + e.Message + " event");
            Console.ResetColor();
        }

        /// <summary>
        /// Event handler for greater than or equal to 20.
        /// </summary>
        /// <param name="sender"> Sender.</param>
        /// <param name="e"> Dice Event argument.</param>
        private void GreaterOrEq20(object sender, DiceEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Triggered " + e.Message + "event");
            Console.ResetColor();
        }


        /// <summary>
        /// Tests Dice class
        /// </summary>
        /// <param name="n"> Number of tosses.</param>
        public void Run(int n)
        {
            Dice dice = new Dice();

            //register TwoSix event handler to two_six_row event
            dice.two_six_row += this.TwoSix;

            //register GreaterOrEq20 event handler to greater than or equal to 20 event
            dice.greater_than_or_equal_20 += this.GreaterOrEq20;
            dice.RollN(n);
            Console.Read();
        }
    }
}
