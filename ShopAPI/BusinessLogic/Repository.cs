using System.Linq;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic
{
    /// <summary>
    /// Class for performing CRUD operations
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Database accessor
        /// </summary>
        private readonly DAL dataAccessor;

        /// <summary>
        /// Mapper for products
        /// </summary>
        private readonly Mapper.Mapper mapper;

        /// <summary>
        /// Creates new instance of repository.
        /// </summary>
        public Repository()
        {
            //creating DAL instance
            this.dataAccessor = new DAL();

            //creating mapper
            this.mapper = new Mapper.Mapper();
        }

        /// <summary>
        /// Creates new product and addes it to database.
        /// </summary>
        /// <param name="product"> Product </param>
        public void Create(Product product)
        {
            //converting product to data
            var productData = this.mapper.Map<Product, ProductData>(product);

            //adding product to database
            try
            {
                this.dataAccessor.AddProduct(productData);
            }

            catch (FailedDalOperationException fadbopex)
            {
                //if an error occured then throw an exception
                throw new FailedOperationException(fadbopex);
            }

        }

        /// <summary>
        /// Reads the products from database.
        /// </summary>
        /// <returns> Returns the enumberable of products. </returns>
        public IEnumerable<Product> Read()
        {
            //enumerable of product datas
            var productDatas = null as IEnumerable<ProductData>;

            //getting products from database
            try
            {
                productDatas = this.dataAccessor.GetProducts();
            }

            catch (FailedDalOperationException fadbopex)
            {
                //if an error occured then throw an exception
                throw new FailedOperationException(fadbopex);
            }

            //converting product datas to products

            var products = productDatas.Select(productData =>
                this.mapper.Map<ProductData,Product>(productData));

            //returning products
            return products;
        }

        /// <summary>
        /// Deletes the product with the given id.
        /// </summary>
        /// <param name="id"> ID. </param>
        public void Delete(int id)
        {
            //deleting the product with the given id
            try
            {
                this.dataAccessor.DeleteProduct(id);
            }

            catch (FailedDalOperationException fadbopex)
            {
                //if an error occured then throw an exception
                throw new FailedOperationException(fadbopex);
            }
           
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="newProduct"> New product</param>
        public void Update(Product newProduct)
        {
            //updating product
            try
            {
                this.dataAccessor.UpdateProduct(
                    this.mapper.Map<Product,ProductData>(newProduct));
            }

            catch (FailedDalOperationException fadbopex)
            {
                //if an error occured then throw an exception
                throw new FailedOperationException(fadbopex);
            }
        }
    }
}
