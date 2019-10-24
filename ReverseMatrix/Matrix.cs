using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseMatrix
{
    class Matrix
    {
        private float[,] A;

        //Конструкторы
        public Matrix()
        {
            A = new float[4, 4]
                {
                 {1, 1, 1, -1},
                 {0, 1, 1, -1},
                 {1, 1, 0, -3},
                 {-1, -1, -1, 0}
                };

        }
        public Matrix(ushort RowCol)
        {
            A = new float[RowCol, RowCol];
            for (int i = 0; i < RowCol; i++)
                for (int j = 0; j < RowCol; j++)
                {
                    Console.Write("M[{0},{1}] = ", i, j);
                    A[i, j] = Convert.ToSingle(Console.ReadLine());
                }
        }

        //Вывод в консоль
        public void Display()
        {
            int RowCol = (int)Math.Sqrt(A.Length);

            for (int i = 0; i < RowCol; i++)
            {
                for (int j = 0; j < RowCol; j++)
                {
                    Console.Write("{0, 10}", A[i, j]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void Reverse()
        {
            int RowCol = (int)Math.Sqrt(A.Length);

            float[][] B = new float[RowCol][];
            for (int i = 0; i < RowCol; i++)
                B[i] = new float[i + 1];

            float[][] T = new float[RowCol][];
            for (int i = 0; i < RowCol; i++)
            {
                T[i] = new float[RowCol];
                T[i][i] = 1;
            }

            BandT(ref B, ref T);
            T = ReverseT(T);
            B = ReverseB(B);
            A = Mul(T, B);
        }

        //Поиск эл. В и Т
        private void BandT(ref float[][] B, ref float[][] T)
        {
            for (int i = 0; i < T[0].Length; i++)
            {
                for (int j = 0; j < B[i].Length; j++)
                    B[i][j] = elB(i, j, B, T);

                for (int j = 0; j < T[i].Length; j++)
                    T[i][j] = elT(i, j, B, T);
            }
        }
        private float elB(int Row, int Col, float[][] B, float[][] T)
        {
            float Result = A[Row, Col];

            for (int i = 0; i < Col; i++)
            {
                Result -= B[Row][i] * T[i][Col];
            }

            return Result;
        }
        private float elT(int Row, int Col, float[][] B, float[][] T)
        {
            float Result = A[Row, Col];

            for (int i = 0; i < Row; i++)
            {
                Result -= B[Row][i] * T[i][Col];
            }

            return Result / B[Row][Row];
        }

        //Нахождение обратных матриц, матриц В и Т
        //Ошибка где-то тут
        private float[][] ReverseT(float[][] T)
        {
            float[][] ReverseT = new float[T.Length][];
            for (int i = 0; i < ReverseT.Length; i++)
            {
                ReverseT[i] = new float[ReverseT.Length];
                ReverseT[i][i] = 1;
            }

            for (int j = 0; j < T.Length; j++)
                for (int i = T.Length - 1; i >= 0; i--)
                    ReverseT[i][j] = (i != j) ? elRevT(i, j, T, ReverseT) : T[i][j];

            return ReverseT;
        }
        private float elRevT(int Row, int Col, float[][] T, float[][] Y)
        {
            float Res = -T[Row][Col];
            for (int i = Row + 1; i < Col; i++)
            {
                Res -= T[Row][i] * Y[i][Col];
            }
            return Res;
        }
        private float[][] ReverseB(float[][] B)
        {
            float[][] ReverseB = new float[B.Length][];
            for (int i = 0; i < ReverseB.Length; i++)
                ReverseB[i] = new float[i + 1];

            for (int i = 0; i < ReverseB.Length; i++)
                for (int j = ReverseB[i].Length - 1; j >= 0; j--)
                    ReverseB[i][j] = (i == j) ? 1 / B[i][i] : elRevB(i, j, B, ReverseB) / B[i][i];

            return ReverseB;
        }
        private float elRevB(int Row, int Col, float[][] B, float[][] X)
        {
            float Res = 0;
            for (int i = Row + 1; i <= Col; i++)
            {
                Res -= X[i][Col] * B[Row][i];
            }
            return Res;
        }

        //Произведение матриц В и Т
        private float[,] Mul(float[][] T, float[][] B)
        {
            float[,] ReverseA = new float[T.Length, T.Length];

            for (int Row = 0; Row < T.Length; Row++)
                for (int Col = 0; Col < T.Length; Col++)
                    for (int index = 0; index < T.Length; index++)
                        ReverseA[Row, Col] += (index >= B[Col].Length - 1) ? 
                            T[Row][index] * B[index][Col] : 
                            T[Row][index] * 0;

            return ReverseA;
        }
    }
}
