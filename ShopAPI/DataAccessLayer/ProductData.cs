namespace DataAccessLayer
{
    /// <summary>
    /// Data about Product
    /// </summary>
    public class ProductData
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
