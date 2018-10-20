using System;

namespace DiceLib
{
    /// <summary>
    /// class for Dice events arguments
    /// </summary>
    class DiceEventArgs : EventArgs
    {
        /// <summary>
        /// Message that specifies the event
        /// </summary>
        private readonly string message;

        /// <summary>
        /// Constructs new instance of DiceEventArgs with specific message.
        /// </summary>
        /// <param name="message"> Specific message for event args. </param>
        public DiceEventArgs(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Gets message for DiceEventArgs instance.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
