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

            BandT(ref B, ref T, RowCol);


        }

        //Поиск эл. В и Т
        private void BandT(ref float[][] B, ref float[][] T, int RowCol)
        {
            for (int i = 0; i < RowCol; i++)
            {
                for (int j = 0; j < B[i].Length; j++)
                    B[i][j] = elB(i, j, B, T);
                for (int j = 0; j < RowCol; j++)
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
        //Произведение матриц В и Т
    }
}
