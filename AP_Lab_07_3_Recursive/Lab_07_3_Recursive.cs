/* Lab_07_3_Recursive.cs
 * Якубовський Владислав
 * Лабораторна робота № 7.3
 * Опрацювання динамічних багатовимірних масивів (рекурсивний спосіб)
 * Варіант 24 */
namespace AP_Lab_07_3_Recursive
{
    public class Lab_07_3_Recursive
    {
        readonly static Random random = new();

        public static void MaxFromSum(int[,] matrix, int rows, int cols, ref int maxSum, 
            int sum = 0, bool higherDiagonals = false, int ii = 0, int jj = 0, int kk = 0)
        {

            // Частина для обчислення сум елементів діагоналей, що знаходяться нижче за головну.
            if (!higherDiagonals && ii < rows)
            {
                if (kk < rows && jj < cols)
                {
                    sum += matrix[kk, jj];

                    kk++; jj++;

                    MaxFromSum(matrix, rows, cols, ref maxSum, sum, higherDiagonals, ii, jj, kk);
                }

                if (sum > maxSum)
                    maxSum = sum;

                sum = 0;

                ii++; kk = ii; jj = 0;

                MaxFromSum(matrix, rows, cols, ref maxSum, sum, higherDiagonals, ii, jj, kk);
            }

            else if (!higherDiagonals)
            {
                higherDiagonals = true;

                ii = 0; jj = 0; kk = 0;

                MaxFromSum(matrix, rows, cols, ref maxSum, sum, higherDiagonals, ii, jj, kk);
            }

            // Частина для обчислення сум елементів діагоналей, що знаходяться вище за головну.
            else if (jj < cols)
            {
                if (ii < rows && kk < cols)
                {
                    sum += matrix[ii, kk];

                    ii++; kk++;

                    MaxFromSum(matrix, rows, cols, ref maxSum, sum, higherDiagonals, ii, jj, kk);
                }

                if (sum > maxSum)
                    maxSum = sum;

                sum = 0;

                jj++; ii = 0; kk = jj;

                MaxFromSum(matrix, rows, cols, ref maxSum, sum, higherDiagonals, ii, jj, kk);
            }
        }

        static int MultiplyPositiveRows(int[,] matrix, int rows, int cols, int productToReturn = 1, int localProduct = 1, int ii = 0, int jj = 0)
        {
            if (ii < rows)
            {
                if (jj < cols)
                {
                    if (matrix[ii, jj] >= 0)
                    {
                        localProduct *= matrix[ii, jj];

                        return MultiplyPositiveRows(matrix, rows, cols, productToReturn, localProduct, ii, ++jj);
                    }

                    else return MultiplyPositiveRows(matrix, rows, cols, productToReturn, 1, ++ii, 0);
                }

                else
                {
                    productToReturn *= localProduct;
                    return MultiplyPositiveRows(matrix, rows, cols, productToReturn, 1, ++ii, 0);
                }
            }

            return productToReturn;
        }

        static void GenerateMatrix(ref int[,] matrix, int rows, int cols, int minLimit, int maxLimit, int ii = 0, int jj = 0)
        {
            if (ii < rows)
            {
                if (jj < cols)
                {
                    matrix[ii, jj] = random.Next(minLimit, maxLimit);
                    GenerateMatrix(ref matrix, rows, cols, minLimit, maxLimit, ii, ++jj);
                }

                else GenerateMatrix(ref matrix, rows, cols, minLimit, maxLimit, ++ii, 0);
            }
        }

        static void DisplayMatrix(int[,] matrix, int rows, int cols, int ii = 0, int jj = 0)
        {
            if (ii < rows)
            {
                if (jj < cols)
                {
                    Console.Write((jj == 0 ? "||" : "") + $" {matrix[ii, jj]} ".PadRight(6) + (jj == cols - 1 ? "||\n" : ""));

                    DisplayMatrix(matrix, rows, cols, ii, ++jj);
                }

                else DisplayMatrix(matrix, rows, cols, ++ii, 0);
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Default;

            Console.Write("Введіть розмір матриці (через кому \"k\", \"n\"): ");

            string[] varArray = Console.ReadLine()!.Split(';');

            int k = int.Parse(varArray[0]), n = int.Parse(varArray[1]);

            Console.Write("Введіть найменші та найбільші можливі значення масиву через крапку з комою: ");

            varArray = Console.ReadLine()!.Split(';');

            int minLimit = int.Parse(varArray[0]), maxLimit = int.Parse(varArray[1]);

            int[,] matrix = new int[k, n];

            GenerateMatrix(ref matrix, k, n, minLimit, maxLimit);

            Console.WriteLine("Згенерована матриця: "); DisplayMatrix(matrix, k, n);

            int product = MultiplyPositiveRows(matrix, k, n), sum = 0;

            MaxFromSum(matrix, k, n, ref sum);

            Console.WriteLine($"Добуток елементів у тих рядках, які не містять від'ємних елементів: " + ((product == 1) ? "немає рядків без від'ємних елементів!\n" : $"{product}\n") +
                $"Максимум серед сум елементів діагоналей, паралельних до головної діагоналі матриці: {sum}");
        }
    }
}
