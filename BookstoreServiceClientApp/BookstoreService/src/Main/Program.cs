namespace BookstoreService
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Bookstore();
            
            service.Run();
        }
    }
}
