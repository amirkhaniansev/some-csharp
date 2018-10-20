using System;

namespace MatrixLib
{
    /// <summary>
    /// Matrix class for matrix operations like multiplication,transponse,etc
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// base 2D array for matrix
        /// </summary>
        private  double[][] _2DArray;

        /// <summary>
        /// Gets 32-bit integer representation of the number of rows in matrix 
        /// </summary>
        public int Rows
        {
            get;private set;
        }

        /// <summary>
        /// Gets 32-bit integer representation of the number of columns in matrix.
        /// </summary>
        public  int Columns
        {
            get;private set;
        }


        /// <summary>
        /// Gets the matrix element.
        /// </summary>
        /// <param name="i"> Specifies row number. </param>
        /// <param name="j"> Specifies column number.</param>
        /// <returns> Returns matrix element with indexes [row][column]. </returns>
        public double this[int row, int column]
        {
            get
            {
                if(row <0 || row>=this.Rows || column<0 || column>=this.Columns)
                {
                    throw new IndexOutOfRangeException("Out of range!");
                }
                return this._2DArray[row][column];
            }
            private set
            {
                if (row < 0 || row >= this.Rows || column < 0 || column >= this.Columns)
                {
                    throw new IndexOutOfRangeException("Out of range!");
                }
                this._2DArray[row][column] = value;
            }
        }

        /// <summary>
        /// Paramter-less constructor,constructs empty matrix.
        /// </summary>
        public Matrix()
        {
            this.Rows = this.Columns = 1;
            this._2DArray = new double[Rows][];
            this._2DArray[0] = new double[Columns];
        }

        /// <summary>
        /// Parameterized constructor,constructs matrix by the given values of rows and columns.
        /// </summary>
        /// <param name="rows_value"> Number of rows in matrix. </param>
        /// <param name="columns_value"> Number of colums in matrix </param>
        public Matrix(int rowsValue,int columnsValue)
        {
            //checking if arguments are valid for constructing matrix
            if (rowsValue < 0 || columnsValue < 0 )
            {
                throw new Exception("Number of raws and columns must be positive integers.");
            }

            //assigning fields values
            this.Rows = rowsValue;
            this.Columns = columnsValue;

            //constructing matrix
            this._2DArray = new double[Rows][];
            for(int i=0; i < Rows; i++)
            {
                this._2DArray[i] = new double[Columns];
            }
        }

        /// <summary>
        /// Creates the copy of matrix passed as argument.
        /// </summary>
        /// <param name="arg"> This matrix is copied. </param>
        public Matrix(Matrix arg):this(arg.Rows,arg.Columns)
        {
            for(int i=0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                    this[i,j] = arg[i,j];
            }

        }

        /// <summary>
        /// Constructs class Matrix instance with given 2D array
        /// </summary>
        /// <param name="_2Darr"> Specifies 2D array. </param>
        /// <param name="n"> Number of rows. </param>
        /// <param name="m"> Number of columns. </param>
        public Matrix(double[][] _2Darr,int n ,int m):this(n,m)
        {
            for(int i=0;i<this.Rows;i++)
            {
                for(int j=0;j<this.Columns;j++)
                {
                    this[i,j] = _2Darr[i][j];
                }
            }
        }

        /// <summary>
        /// Cosntructs matrix with given multi-dimensional array.
        /// </summary>
        /// <param name="multiDimArr"> multi-dimensional array.</param>
        public Matrix(double[,] multiDimArr):this(multiDimArr.GetLength(0),multiDimArr.GetLength(1))
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    this[i, j] = multiDimArr[i,j];
                }
            }
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="arg"> Specifies the matrix ,that will be added to first matrix. </param>
        /// <returns> Returns sum of two matrices. </returns>
        public Matrix Add(Matrix arg) 
        {
            // Matrices must have the same number of raws and columns to be added
            // So we need to check it
            if(this.Rows != arg.Rows || this.Columns != arg.Columns)
            {
                throw new Exception("Matrices must have the same number of raws and columns to be added");
            }

            //constructing result matrix for sum
            Matrix result = new Matrix(this);
            
            //adding two matrices
            for(int i=0;i < this.Rows; i++)
            {
                for(int j=0;j < this.Columns; j++)
                {
                    result[i,j] += arg[i,j];

                }
            }
            return result;
        }

        /// <summary>
        /// Multiplies initial matrix by the matrix passed as argument.
        /// </summary>
        /// <param name="matrix"> Specifies matrix which will be multiplied by initial matrix.</param>
        /// <returns> Returns multiplied matrix. </returns>
        public Matrix Mul(Matrix matrix)
        {
            //checking whether matrices are multiplicable
            //if not throw exception
            if(this.Columns != matrix.Rows)
            {
                throw new Exception("Matrices are not multiplicable.");
            }

            //initial matrix is this.rows x this.columns
            //argument matrix is matrix.rows x matrix.columns
            //result matrix will be this.rows x matrix.columns 
            Matrix result = new Matrix(this.Rows,matrix.Columns);

            //multiplying matrices
            for(int i = 0; i < result.Rows; i++)
            {
                for(int j=0; j < result.Columns; j++)
                {
                    for(int k=0; k < this.Columns; k++)
                    {
                        result[i,j] += this[i,k] * matrix[k,j]; 
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Multiplies matrix by scalar.
        /// </summary>
        /// <param name="num"> Specifies the scalar which will be multiplied by matrix elements. </param>
        /// <returns> Returns the matrix multiplied by scalar. </returns>
        public Matrix ScalarMul(double num)
        {
            //constructing new matrix by copying initial matrix
            Matrix result = new Matrix(this);

            //multiplying matrix by scalar
            for(int i=0; i < result.Rows; i++)
            {
                for(int j=0; j < result.Columns; j++)
                {
                    result[i,j] *= num;
                }
            }

            return result;
        }

        /// <summary>
        /// Transposes matrix.
        /// </summary>
        /// <returns> Returns transposed matrix. </returns>
        public Matrix Transpose()
        {
            Matrix result = new Matrix(this.Columns, this.Rows);
            for(int i=0;i < result.Rows; i++)
            {
                for(int j=0;j < result.Columns; j++)
                {
                    result[i,j] = this[j,i]; 
                }
            }
            return result;
        }

        /// <summary>
        /// Constructs n x n identity matrix.
        /// </summary>
        /// <param name="n"> The size of square matrix. </param>
        /// <returns> Returns n x n square matrix.</returns>
        public static Matrix IdentityMatrix(int n)
        {
            Matrix identityMatrix = new Matrix(n, n);
            for (int i = 0; i < identityMatrix.Rows; i++)
            {
                identityMatrix[i, i] = 1;
            }
            return identityMatrix;
        }
        
        /// <summary>
        /// Constructs translation matrix for a vector in R x R
        /// </summary>
        /// <param name="x"> X coordinate. </param>
        /// <param name="y"> Y coordinate.</param>
        /// <returns> Returns translation matrix. </returns>
        public static Matrix Translation(double x,double y)
        {
            Matrix translatioMatrix = new Matrix(Matrix.IdentityMatrix(3));
            translatioMatrix[0, 2] = x;
            translatioMatrix[1, 2] = y;
            return translatioMatrix;
        }

        /// <summary>
        /// Constructs translation matrix for a vector in R x R x R
        /// </summary>
        /// <param name="x"> X coordinate. </param>
        /// <param name="y"> Y coordinate. </param>
        /// <param name="z"> Z coordinate.</param>
        /// <returns> Returns translation matrix. </returns>
        public static Matrix Translation(double x,double y,double z)
        {
            Matrix translationMatrix = new Matrix(Matrix.IdentityMatrix(4));
            translationMatrix[0, 3] = x;
            translationMatrix[1, 3] = y;
            translationMatrix[2, 3] = z;
            return translationMatrix;
        }

        /// <summary>
        /// Constructs translation matrix for a vector in R x R x ... x R
        /// </summary>
        /// <param name="VecCoord"> Coordinates for translation. </param>
        /// <returns> Returns translation matrix. </returns>
        public static Matrix Translation(params double[] VecCoord)
        {
            Matrix translationMatrix = Matrix.IdentityMatrix(VecCoord.Length+1);
            for(int i=0;i < VecCoord.Length;i++)
            {
                translationMatrix[i, VecCoord.Length] = VecCoord[i];
            }
            return translationMatrix;
        }

        /// <summary>
        /// Constructs scaling matrix for a vector in R x R
        /// </summary>
        /// <param name="x"> X coordinate.</param>
        /// <param name="y"> Y coordinate.</param>
        /// <returns> Returns scaling matrix.</returns>
        public static Matrix ScalingMatrix(double x, double y)
        {
            Matrix scalingMatrix = new Matrix(2, 2);
            scalingMatrix[0, 0] = x;
            scalingMatrix[1, 1] = y;
            return scalingMatrix;
        }

        /// <summary>
        /// Constructs scaling matrix for a Vector in R x R x R
        /// </summary>
        /// <param name="x"> X coordinate.</param>
        /// <param name="y"> Y coordinate.</param>
        /// <param name="z"> Z coordinate.</param>
        /// <returns> Returns scaling matrix.</returns>
        public static Matrix ScalingMatrix(double x,double y,double z)
        {
            Matrix scalingMatrix = new Matrix(3, 3);
            scalingMatrix[0, 0] = x;
            scalingMatrix[1, 1] = y;
            scalingMatrix[2, 2] = z;
            return scalingMatrix;
        }

        /// <summary>
        /// Constructs scaling matrix for a vector in R x R x ... x R
        /// </summary>
        /// <param name="ScalarCoord"> Scalars for scaling. </param>
        /// <returns> Returns scaling matrix. </returns>
        public static Matrix ScalingMatrix(params double[] ScalarCoord)
        {
            Matrix scalingMatrix = new Matrix(ScalarCoord.Length, ScalarCoord.Length);
            for(int i=0;i<scalingMatrix.Rows;i++)
            {
                scalingMatrix[i, i] = ScalarCoord[i];
            }
            return scalingMatrix;
        }

        /// <summary>
        /// Augments two matrices.
        /// </summary>
        /// <param name="matrix"> Matrix that will be augmented to the initial matrix. </param>
        /// <returns> Returns augmented matrix. </returns>
        public Matrix Augment(Matrix matrix)
        {
            if(this.Rows!=matrix.Rows)
            {
                throw new Exception("Matrices cannot be augmented.");
            }
            Matrix result = new Matrix(this.Rows, this.Columns + matrix.Columns);

            //assigning initial matrix to the left side of result
            for(int i=0;i<this.Rows;i++)
            {
                for (int j = 0; j < this.Columns; j++)
                    result[i, j] = this[i, j];
            }

            //assigning parameter matrix to the right side of result
            for(int i=0;i<this.Rows;i++)
            {
                for (int j = this.Columns; j < result.Columns; j++)
                    result[i, j] = matrix[i,j-this.Columns];
            }

            return result;
        }

        /// <summary>
        /// Constructs inverse matrix for instance if the latter is invertible.
        /// </summary>
        /// <returns> Returns inverse matrix. </returns>
        public Matrix Inverse()
        {
            if(!this.IsSquare())
            {
                throw new Exception("Matrix must be square to have inverse matrix.");
            }
            Matrix res = new Matrix(this);

            //this is identity matrix
            //which will be transformed to inverse matrix
            //if the given matrix is invertible
            Matrix idM = new Matrix(Matrix.IdentityMatrix(res.Rows));
            double temp;
            int i, j;

            //performing matrix inverse using Gauss-Jordan method
            for (j = 0; j < this.Rows; j++)
            {
                for (i = j; i < this.Rows; i++)
                {
                    if (res[i, j] != 0)
                    {
                        for (int k = 0; k < this.Rows; k++)
                        {
                            temp = res[j, k]; res[j, k] = res[i, k]; res[i, k] = temp;
                            temp = idM[j, k]; idM[j, k] = idM[i, k]; idM[i, k] = temp;
                        }
                        temp = 1 / res[j, j];
                        for (int k = 0; k < this.Rows; k++)
                        {
                            res[j, k] *= temp;
                            idM[j, k] *= temp;
                        }
                        for (int l = 0; l < this.Rows; l++)
                        {
                            if (l != j)
                            {
                                temp = (-res[l, j]);
                                for (int k = 0; k < this.Rows; k++)
                                {
                                    res[l, k] = res[l, k] + temp * res[j, k];
                                    idM[l, k] = idM[l, k] + temp * idM[j, k];
                                }
                            }
                        }
                        break;
                    }

                    //if detected a row with 0s throw exception 
                    //because in that case matrix is non-invertible
                    if (res[i, j] == 0)
                    {
                        throw new Exception("Non-invertible matrix.");
                    }
                }
            }
            return idM;
        }

        /// <summary>
        /// enum class for axes
        /// </summary>
        public enum Axis
        {
            X_axis,Y_axis,Z_axis
        }
        
        /// <summary>
        /// Constructs rotation matrix for a vector in R x R x R.
        /// </summary>
        /// <param name="angle"> Angle in degrees.Positive angle is clockwise rotation,negative angle:anti-clockwise.</param>
        /// <param name="axis"> Specifies axis; X,Y or Z.</param>
        /// <returns> Returns rotation matrix.</returns>
        public static Matrix Rotation3D(double angle,Axis axis)
        {
            Matrix rotationMatrix = new Matrix(3,3);
            double sinThetta = Math.Sin(angle*(Math.PI/180));
            double cosThetta = Math.Cos(angle*(Math.PI/180));
             
            //if axis is X-axis
            //then construct rotation matrix about x-axis
            if(axis == Axis.X_axis)
            {
                rotationMatrix[0, 0] = 1;
                rotationMatrix[1, 1] = rotationMatrix[2,2] = cosThetta;
                rotationMatrix[1, 2] = (-sinThetta);
                rotationMatrix[2, 1] = sinThetta;
            }

            //if axis is Y-axis
            //then construct rotation matrix about y-axis
            if (axis==Axis.Y_axis)
            {
                rotationMatrix[0, 0] = rotationMatrix[2, 2] = cosThetta;
                rotationMatrix[0, 2] = sinThetta;
                rotationMatrix[2, 0] = (-sinThetta);
                rotationMatrix[1, 1] = 1;
            }

            //if axis is Z-axis
            //then construct rotation matrix about Z-axis
            if (axis==Axis.Z_axis)
            {
                rotationMatrix[0, 0] = rotationMatrix[1, 1] = cosThetta;
                rotationMatrix[0, 1] = (-sinThetta);
                rotationMatrix[1, 0] = sinThetta;
                rotationMatrix[2, 2] = 1;
            }
            return rotationMatrix;
        }

        /// <summary>
        /// Finds the smallest element of matrix.
        /// </summary>
        /// <returns> Returns the smallest element of matrix.</returns>
        public double MinEl()
        {
            double min = this[0, 0];

            //iterating all over the matrix to find the largest element 
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if (this[i, j] < min)
                        min = this[i, j];
                }
            }
            return min;
        }

        /// <summary>
        /// Finds largest element of matrix.
        /// </summary>
        /// <returns> Returns largest element of matrix.</returns>
        public double MaxEl()
        {
            double max = this[0, 0];

            //iterating all over the matrix to find the largest element 
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    if (this[i, j] > max)
                        max = this[i, j];
                }
            }
            return max;
        }

        /// <summary>
        /// Checks if matrix is orthogonal.
        /// </summary>
        /// <returns> Returns true if matrix is orthogonal;otherwis:false. </returns>
        public bool IsOrthogonal()
        {
            if(this.IsSquare())
            {
                Matrix res = new Matrix(this);

                //Matrix is orthogonal if and only if it is square matrix
                //and the multiplication of matrix and its transpose matrix is identity matrix
                //calculating the multiplication

                res = res.Mul(res.Transpose());
                return res.Equals(Matrix.IdentityMatrix(res.Rows));
            }
            else
            {
                //if matrix is not square matrix return false
                return false;
            }
        }

        /// <summary>
        /// Checks if matrix is square matrix.
        /// </summary>
        /// <returns> Returns true if matrix is square matrix,otherwise false. </returns>
        public bool IsSquare()
        {
            return this.Rows == this.Columns;
        }

        /// <summary>
        /// Checks equality of two instances.
        /// </summary>
        /// <param name="obj"> Object instance. </param>
        /// <returns> Returns true,if matrices are equal and false;otherwise. </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            else
            {
                Matrix matrix = (Matrix)obj;
                if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
                    return false;
                else
                {
                    for(int i=0;i<this.Rows;i++)
                    {
                        for (int j = 0; j < this.Columns; j++)
                            if (this[i, j] != matrix[i, j])
                                return false;
                    }
                    return true;
                }
            }
        }

        /// <summary>
        /// Gets hash code for instance.
        /// </summary>
        /// <returns> Returns hash code.</returns>
        public override int GetHashCode()
        {
            int hash = 1;
            for(int i=0;i<this.Rows;i++)
            {
                for (int j = 0; j < this.Columns; j++)
                    hash ^= (int)this[i, j];
            }
            return hash;
        }

        /// <summary>
        ///  Converts the numerical value of matrix to its equivalent string representation.  
        /// </summary>
        /// <returns> Returns the string representation of matrix. </returns>
        public override string ToString()
        {
            string matrixStringRepres = "";

            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    matrixStringRepres += this[i, j].ToString() + " ";
                }
                matrixStringRepres += Environment.NewLine;
            }
            return matrixStringRepres;
        }
    }
}
