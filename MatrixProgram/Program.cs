using System;

namespace MatrixProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMatrix.RunAdd();
            TestMatrix.RunMul();
            TestMatrix.RunScalarMul();
            TestMatrix.RunInverse();
            TestMatrix.RunTranspose();
            TestMatrix.RunIsOrthogonal();
            TestMatrix.RunTranslation();
            TestMatrix.RunScalingMatrix();
            TestMatrix.RunRotation3D();
            TestMatrix.RunMinEl();
            TestMatrix.RunMaxEl();
            Console.ReadLine();     
        }
    }
}
