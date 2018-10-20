using DiceLib;

namespace DiceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            DiceRoller dr = new DiceRoller();
            dr.Run(50);
        }
    }
}
