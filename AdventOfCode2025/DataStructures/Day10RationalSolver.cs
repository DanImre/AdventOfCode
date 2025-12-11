using System.Numerics;

namespace DataStructures
{
    // Fully made by gemini
    // I couldn't be asked
    public class RationalSolver
    {
        public struct Rational
        {
            public BigInteger Num;
            public BigInteger Den;

            public Rational(BigInteger num, BigInteger den)
            {
                if (den == 0) throw new DivideByZeroException();
                var gcd = BigInteger.GreatestCommonDivisor(BigInteger.Abs(num), BigInteger.Abs(den));
                if (den < 0) { num = -num; den = -den; }
                Num = num / gcd;
                Den = den / gcd;
            }

            public static implicit operator Rational(long n) => new Rational(n, 1);
            public static Rational operator +(Rational a, Rational b) => new Rational(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);
            public static Rational operator -(Rational a, Rational b) => new Rational(a.Num * b.Den - b.Num * a.Den, a.Den * b.Den);
            public static Rational operator *(Rational a, Rational b) => new Rational(a.Num * b.Num, a.Den * b.Den);
            public static Rational operator /(Rational a, Rational b) => new Rational(a.Num * b.Den, a.Den * b.Num);

            public override string ToString() => Den == 1 ? Num.ToString() : $"{Num}/{Den}";
            public bool IsZero => Num == 0;
            public bool IsOne => Num == 1 && Den == 1;
        }

        public static Rational[,] GetRREF(long[,] input)
        {
            int rowCount = input.GetLength(0);
            int colCount = input.GetLength(1);

            Rational[,] matrix = new Rational[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    matrix[i, j] = input[i, j];

            int pivotRow = 0;
            for (int col = 0; col < colCount && pivotRow < rowCount; col++)
            {
                int sel = -1;
                for (int r = pivotRow; r < rowCount; r++)
                    if (!matrix[r, col].IsZero)
                    {
                        sel = r;
                        break;
                    }

                if (sel == -1)
                    continue;

                for (int k = 0; k < colCount; k++)
                {
                    var tmp = matrix[pivotRow, k];
                    matrix[pivotRow, k] = matrix[sel, k];
                    matrix[sel, k] = tmp;
                }

                var pivotVal = matrix[pivotRow, col];
                for (int k = 0; k < colCount; k++)
                    matrix[pivotRow, k] = matrix[pivotRow, k] / pivotVal;

                for (int r = 0; r < rowCount; r++)
                {
                    if (r == pivotRow || matrix[r, col].IsZero)
                        continue;

                    var factor = matrix[r, col];
                    for (int k = 0; k < colCount; k++)
                        matrix[r, k] = matrix[r, k] - (factor * matrix[pivotRow, k]);
                }
                pivotRow++;
            }
            return matrix;
        }
    }
}
