using System;
using System.Collections.Generic;
using System.Threading;

namespace DiceLib
{
    /// <summary>
    /// Class for simulation of rolling a die
    /// </summary>
    class Dice
    {
        /// <summary>
        /// Toss values
        /// </summary>
        private readonly List<int> toss_values;

        /// <summary>
        /// Gets the value of count.
        /// </summary>
        public int Count
        {
            get; private set;
        }

        /// <summary>
        /// Random number for every toss
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Parameter-less constructor.Constructs new instance of Dice.
        /// </summary>
        public Dice()
        {
            this.Count = 0;
            this.toss_values = new List<int>();
            this.random = new Random();
        }

        /// <summary>
        /// Delegate type for handling dice events.
        /// </summary>
        /// <typeparam name="TEventArgs"> Generic parameter of delegate for event arguments.</typeparam>
        /// <param name="sender"> Sender.</param>
        /// <param name="e"> Argument of dice event.</param>
        public delegate void DiceEventHandler<TEventArgs>(object sender, TEventArgs e);

        /// <summary>
        /// Event for rolling 2 sixes in a row.
        /// </summary>
        public event DiceEventHandler<DiceEventArgs> two_six_row;

        /// <summary>
        /// Event for 5 conseqeunt tosses.Trigers if sum of numbers is greater than  or equal to 20.
        /// </summary>
        public event DiceEventHandler<DiceEventArgs> greater_than_or_equal_20;

        /// <summary>
        /// Raises two sixes in a row event.
        /// </summary>
        /// <param name="e"> Dice Event argument.</param>
        protected virtual void NewTwoSix(DiceEventArgs e)
        {
            if (this.two_six_row != null)
            {
                this.two_six_row(this, e);
            }
        }

        /// <summary>
        /// Raises greater than 20 in 5 consequent tosses event.
        /// </summary>
        /// <param name="e"> Dice Event argument.</param>
        protected virtual void NewGreaterThan20(DiceEventArgs e)
        {
            if (this.greater_than_or_equal_20 != null)
            {
                this.greater_than_or_equal_20(this, e);
            }
        }

        /// <summary>
        /// Simulates new two sixes in a row event.
        /// </summary>
        /// <param name="message"> Specific message for event argument.</param>
        public void SimulateNewTwoSix(string message)
        {
            DiceEventArgs e = new DiceEventArgs(message);
            this.NewTwoSix(e);
        }

        /// <summary>
        /// Simulates new greater than or equal to 20 event.
        /// </summary>
        /// <param name="message"> Specific message for event argument.</param>
        public void SimulateNewGreater(string message)
        {
            DiceEventArgs e = new DiceEventArgs(message);
            this.NewGreaterThan20(e);
        }

        /// <summary>
        /// Rolls dice n times.
        /// </summary>
        /// <param name="n"> Count of tosses.</param>
        public void RollN(int n)
        {
            int i = 0, sum = 0;

            //rolls die n times
            while (i != n)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Rolling a die...");
                Console.ResetColor();

                //rolls a die every 2 seconds
                Thread.Sleep(3000);

                //generate random integer number in range [1,7)
                //add it to toss values List
                this.toss_values.Add(random.Next(1, 7));

                Console.WriteLine((i + 1) + ".Dice toss value:" + this.toss_values[i]);

                //calculate the sum of tosses values
                sum += this.toss_values[i];

                //if 6 is shown two times in a row
                //raise TwoSixInaRow event 
                if (this.toss_values.Count > 1 && this.toss_values[i] == 6 && this.toss_values[i - 1] == 6)
                {
                    this.Count++;
                    this.SimulateNewTwoSix("\"Two sixes in a row\"");
                }


                //if there is more than 4 elements in toss values list
                if (this.toss_values.Count > 4)
                {
                    if (this.toss_values.Count != 5)
                    {
                        sum -= this.toss_values[i - 5];
                    }

                    //if in 5 consequent tosses the sum of number is gretaer than or equal to 20
                    //raise Greater than or equal to 20 event.
                    if (sum >= 20)
                    {
                        this.SimulateNewGreater("\"5 consequent tosses values is greater than or equal to 20\"");
                    }
                }
                ++i;
                Console.WriteLine("Number of two sixes in a row: " + this.Count + Environment.NewLine);
            }
        }
    }
}
