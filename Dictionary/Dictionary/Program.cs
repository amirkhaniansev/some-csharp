using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryLib;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> dictionaryFCL = new Dictionary<int, int>();
            DictionaryRB<int, int> dictionaryRB = new DictionaryRB<int, int>();
            DictionaryAVL<int, int> dictionaryAVL = new DictionaryAVL<int, int>();

            //320 entries
            DictionaryTest.Test(dictionaryFCL, 320);
            DictionaryTest.Test(dictionaryRB, 320);
            DictionaryTest.Test(dictionaryAVL, 320);

            //640 entries
            DictionaryTest.Test(dictionaryFCL, 640);
            DictionaryTest.Test(dictionaryRB, 640);
            DictionaryTest.Test(dictionaryAVL, 640);

            //1280 entries
            DictionaryTest.Test(dictionaryFCL, 1280);
            DictionaryTest.Test(dictionaryRB, 1280);
            DictionaryTest.Test(dictionaryAVL, 1280);

        }
    }
}
