using System;
using MatrixLib;

namespace MatrixProgram
{
    /// <summary>
    /// Static class for testing matrix operations
    /// </summary>
    public static class TestMatrix
    {
        /// <summary>
        /// Inputs matrix.
        /// </summary>
        public static Matrix Input()
        {
            int rowsValue, columnsValue;
            string rowsMessage = "Number of matrix raws must be non-negative integer.";
            string columnsMessage = "Number of matrix columns must be non-negative integer.";
            Console.WriteLine("Enter the matrix.");
            while (true)
            {
                Console.Write("Rows = ");

                //checking if input for rows is valid
                if (int.TryParse(Console.ReadLine(), out rowsValue))
                {
                    //if input for rows is negative repeat input process
                    if (rowsValue < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\a\a" + rowsMessage);
                        Console.ResetColor();
                    }
                    else break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\a\a" + rowsMessage);
                    Console.ResetColor();
                }
            }
            while (true)
            {
                Console.Write("Columns = ");

                //checking if input for columns is valid
                if (int.TryParse(Console.ReadLine(), out columnsValue))
                {
                    //if input for columns is negative repeat input process
                    if (columnsValue < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\a\a" + columnsMessage);
                        Console.ResetColor();
                    }
                    else break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\a\a" + columnsMessage);
                    Console.ResetColor();
                }
            }

            double[][] _2Darr = new double[rowsValue][];
            for (int i = 0; i < rowsValue; i++)
                _2Darr[i] = new double[columnsValue];

            for (int i = 0; i < rowsValue; i++)
            {
                for (int j = 0; j < columnsValue; j++)
                {
                    while (true)
                    {
                        Console.Write("Matrix Element[{0}][{1}] = ", i, j);
                        if (!double.TryParse(Console.ReadLine(), out _2Darr[i][j]))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\a\a" + "Matrix elements must be real numbers.");
                            Console.ResetColor();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return new Matrix(_2Darr, rowsValue, columnsValue);
        }

        /// <summary>
        /// Tests Matrix.Add()
        /// </summary>
        public static void RunAdd()
        {
            Console.WriteLine("Running Matrix.Add().");
            Matrix matrix1 = new Matrix(), matrix2 = new Matrix(), sum = new Matrix();
            matrix1 = Input();
            matrix2 = Input();
            Console.WriteLine("First matrix \n" + matrix1);
            Console.WriteLine("Second matrix \n" + matrix2);
            Console.WriteLine("Sum matrix \n" + (matrix1.Add(matrix1)));
            try
            {
                sum = matrix1.Add(matrix2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Tests Matrix.Mul()
        /// </summary>
        public static void RunMul()
        {
            Console.WriteLine("Running Matrix.Mul().");
            Matrix matrix1 = new Matrix(), matrix2 = new Matrix(), mul = new Matrix();
            matrix1 = Input();
            matrix2 = Input();
            Console.WriteLine("First matrix \n" + matrix1);
            Console.WriteLine("Second matrix \n" + matrix2);
            Console.WriteLine("Mul is \n " + matrix1.Mul(matrix2));
            try
            {
                mul = matrix1.Mul(matrix1);
                Console.WriteLine(mul);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Tests Matrix.ScalarMul()
        /// </summary>
        public static void RunScalarMul()
        {
            Console.WriteLine("Running Matrix.ScalarMul().");
            Matrix matrix = new Matrix();
            matrix = Input();
            Console.WriteLine("Scalar = ");
            Console.WriteLine(matrix.ScalarMul(double.Parse(Console.ReadLine())));
        }

        /// <summary>
        /// Tests Matrix.IsOrthogonal()
        /// </summary>
        public static void RunIsOrthogonal()
        {
            Console.WriteLine("Running IsOrthogonal().");
            Matrix matrix = new Matrix();
            matrix = Input();
            Console.WriteLine(matrix.IsOrthogonal() == true ? "is orthogonal" : "is not orthogonal.");
        }

        /// <summary>
        /// Tests Matrix.Transpose()
        /// </summary>
        public static void RunTranspose()
        {
            Console.WriteLine("Running Transpose().");
            Matrix matrix = new Matrix();
            matrix = Input();
            Console.WriteLine("Matrix is\n" + matrix);
            Console.WriteLine("Transpose matrix is\n" + matrix.Transpose());
        }

        /// <summary>
        /// Tests Matrix.MinEl()
        /// </summary>
        public static void RunMinEl()
        {
            Console.WriteLine("Running Min()");
            Matrix matrix = new Matrix();
            matrix = Input();
            Console.WriteLine("Matrix is\n" + matrix);
            Console.WriteLine("Min = " + matrix.MinEl());
        }

        /// <summary>
        /// Tests Matrix.MaxEl()
        /// </summary>
        public static void RunMaxEl()
        {
            Console.WriteLine("Running Max()");
            Matrix matrix = new Matrix();
            matrix = Input();
            Console.WriteLine("Matrix is\n" + matrix);
            Console.WriteLine("Max = " + matrix.MaxEl());
        }
        
        /// <summary>
        /// Tests Matrix.Translation()
        /// </summary>
        public static void RunTranslation()
        {
            Console.WriteLine("Running Translation()");
            double[,] vector = { { 4 }, { 5 }, { 7 }, { 1 } };
            Matrix vecMatrix = new Matrix(vector);
            Console.WriteLine("Translation matrix is\n" + Matrix.Translation(0, 4, 5));
            Console.WriteLine("Vector translated with (0,4,5) point\n" +
                Matrix.Translation(0,4,5).Mul(vecMatrix));
        }

        /// <summary>
        /// Tests Matrix.Inverse()
        /// </summary>
        public static void RunScalingMatrix()
        {
            Console.WriteLine("Running ScalingMatrix");
            double[,] vector = { { 4 }, { 5 }, { 7 }, { 1 } };
            Matrix vecMatrix = new Matrix(vector);
            Console.WriteLine("Scaling matrix is\n" + Matrix.ScalingMatrix(0, 4, 5));
            Console.WriteLine("Vector scaled with (0,4,5,1) scalars\n" +
                Matrix.ScalingMatrix(0,4,5,1).Mul(vecMatrix));
        }

        /// <summary>
        /// Tests Matrix.Inverse()
        /// </summary>
        public static void RunInverse()
        {
            Console.WriteLine("Running inverse matrix.");
            Matrix matrix = new Matrix(TestMatrix.Input());
            Console.WriteLine("Matrix is\n" + matrix);
            Console.WriteLine("Matrix inverse is\n" + matrix.Inverse());
            try
            {
                matrix = Input();
                //entering 
                //1 1
                //1 1
                Console.WriteLine(matrix.Inverse());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Tests Matrix.Rotation3D
        /// </summary>
        public static void RunRotation3D()
        {
            Console.WriteLine("Running Rotation3D");
            double[,] vector = { { 4 }, { 5 }, { 7 } };
            double angle;
            
            Matrix vecMatrix = new Matrix(vector);
            Console.Write("Angle = ");

            angle = double.Parse(Console.ReadLine());
            Console.WriteLine(angle);
            Console.WriteLine(Matrix.Rotation3D(angle, Matrix.Axis.X_axis));
            Console.WriteLine(Matrix.Rotation3D(angle, Matrix.Axis.X_axis).Mul(vecMatrix));
        }
    }
}
