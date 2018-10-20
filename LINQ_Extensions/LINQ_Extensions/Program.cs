namespace LINQ_ExtensionsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLinqExtension.TestSelect();
            TestLinqExtension.TestWhere();
            TestLinqExtension.TestToDictionary();
            TestLinqExtension.TestToList();
            TestLinqExtension.TestGroupBy();
            TestLinqExtension.TestOrderBy();
        }
    }
}
