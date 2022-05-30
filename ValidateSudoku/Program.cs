using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        int[][] sudoku = {
            new int[] {7,8,4,  1,5,9,  3,2,6},
            new int[] {5,3,9,  6,7,2,  8,4,1},
            new int[] {6,1,2,  4,3,8,  7,5,9},
            new int[] {9,2,8,  7,1,5,  4,6,3},
            new int[] {3,5,7,  8,4,6,  1,9,2},
            new int[] {4,6,1,  9,2,3,  5,8,7},
            new int[] {8,7,6,  3,9,4,  2,1,5},
            new int[] {2,4,3,  5,6,1,  9,7,8},
            new int[] {1,9,5,  2,8,7,  6,3,4}
        };

        int[][] goodSudoku2 = {
            new int[] {1,4, 2,3},
            new int[] {3,2, 4,1},

            new int[] {4,1, 3,2},
            new int[] {2,3, 1,4}
        };

        int[][] badSudoku1 =  {
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},

            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},

            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9}
        };

        int[][] badSudoku2 = {
            new int[] {1,2,3,4,5},
            new int[] {1,2,3,4},
            new int[] {1,2,3,4},
            new int[] {1}
        };

        try
        {
            CheckSudoku(sudoku);
            CheckSudoku(goodSudoku2);
            CheckSudoku(badSudoku1);
            CheckSudoku(badSudoku2);
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
    }

    public static int GetLength(int[][] sudoku)
    {
        try
        {
            var length = sudoku.Count();
            if (Array.Exists(sudoku, row => row.Count() != length)) throw new Exception();

            return length;
        }
        catch
        {
            return -1;
        }
    }

    public static bool CheckLengthValid(int length)
    {
        try
        {
            if (length < 0) throw new Exception();
            var sqrt = Math.Sqrt(length);
            return sqrt == (int)sqrt;
        }
        catch
        {
            return false;
        }
    }

    public static int[][] GetColumns(int length, int[][] values)
    {
        List<int[]> columns = new List<int[]>();
        for (int i = 0; i < length; i++)
        {
            columns.Add(Enumerable.Range(1, length).ToArray());
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                columns[i][j] = values[j][i];
            }
        }

        return columns.ToArray();
    }

    public static int[][] GetLittleSquares(int length, int[][] values)
    {
        List<int[]> littleSquares = new List<int[]>();
        var sqrt = (int)Math.Sqrt(length);

        for (int i = 0; i < length; i = i + 1)
        {
            int littleSquareRow = (int)(i / sqrt) * sqrt;
            int littleSquareColumn = (int)(i % sqrt) * sqrt;

            List<int> littleSquare = new List<int>();
            for (int j = 0; j < sqrt; j++)
            {
                for (int k = 0; k < sqrt; k++)
                {
                    littleSquare.Add(values[littleSquareRow + j][littleSquareColumn + k]);
                }
            }

            littleSquares.Add(littleSquare.ToArray());
        }

        return littleSquares.ToArray();
    }

    public static bool CheckContainsAllNumbers(int length, int[] values)
    {
        try
        {
            int[] numbers = Enumerable.Range(1, length).ToArray();
            if (Array.Exists(numbers, number => !values.Contains(number))) throw new Exception();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool CheckSudoku(int[][] sudoku)
    {
        try
        {
            int length = GetLength(sudoku);
            bool isLengthValid = CheckLengthValid(length);
            if (!isLengthValid) throw new Exception("Invalid Length");

            var rows = sudoku;
            var isRowsInvalid = Array.Exists(rows, row => !CheckContainsAllNumbers(length, row));
            if (isRowsInvalid) throw new Exception("Theres invalid row(s)");

            var columns = GetColumns(length, sudoku);
            var isColumnsInvalid = Array.Exists(columns, column => !CheckContainsAllNumbers(length, column));
            if (isColumnsInvalid) throw new Exception("Theres invalid column(s)");

            var littleSquares = GetLittleSquares(length, sudoku);
            var isLittleSquaresInvalid = Array.Exists(littleSquares, littleSquare => !CheckContainsAllNumbers(length, littleSquare));
            if (isLittleSquaresInvalid) throw new Exception("Theres invalid little square(s)");

            Console.WriteLine(true);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(false + " " + ex.Message);
            return false;
        }
    }
}