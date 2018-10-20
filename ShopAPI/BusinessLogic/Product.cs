namespace BusinessLogic
{
    /// <summary>
    /// Product structure
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets product ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets product Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets product Price.
        /// </summary>
        public double Price { get; set; }
    }
}
