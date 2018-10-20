namespace MathServer
{
    /// <summary>
    /// Struct containing info about protocol text.
    /// </summary>
    public struct ProtocolInfo
    {
        /// <summary>
        /// Gets or sets operator sign.
        /// </summary>
        public char Operator { get; set; }

        /// <summary>
        /// Gets or sets first value.
        /// </summary>
        public double FirstValue { get; set; }

        /// <summary>
        /// Gets or sets second value.
        /// </summary>
        public double SecondValue { get; set; }
    }
}