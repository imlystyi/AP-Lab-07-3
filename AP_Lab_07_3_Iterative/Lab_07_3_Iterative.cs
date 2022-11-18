/* Lab_07_3_Iterative.cs
 * Якубовський Владислав
 * Лабораторна робота № 7.3
 * Опрацювання динамічних багатовимірних масивів (ітераційний спосіб)
 * Варіант 24 */
namespace AP_Lab_07_3_Iterative
{
    public class Lab_07_3_Iterative
    {
        readonly static Random random = new();

        public static void MaxFromSum(int[,] matrix, out int maxSum)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1), sum = 0;
            maxSum = 0;

            // Цикл для обчислення сум елементів діагоналей, що знаходяться нижче за головну.
            for (int ii = 0; ii < rows; ii++)
            {
                for (int kk = ii, jj = 0; kk < rows && jj < cols; kk++, jj++)
                    sum += matrix[kk, jj];

                if (sum > maxSum)
                    maxSum = sum;

                sum = 0;
            }

            // Цикл для обчислення сум елементів діагоналей, що знаходяться вище за головну.
            for (int jj = 0; jj < cols; jj++)
            {
                for (int ii = 0, kk = jj; ii < rows && kk < cols; ii++, kk++)
                    sum += matrix[ii, kk];

                if (sum > maxSum)
                    maxSum = sum;

                sum = 0;
            }
        }

        static int MultiplyPositiveRows(int[,] matrix)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1), productToReturn = 1, localProduct = 1;

            for (int ii = 0; ii < rows; ii++)
            {
                for (int jj = 0; jj < cols; jj++)
                {
                    if (matrix[ii, jj] >= 0)
                        localProduct *= matrix[ii, jj];

                    else
                    {
                        localProduct = 1;
                        break;
                    }
                }

                productToReturn *= localProduct;

                localProduct = 1;
            }

            return productToReturn;
        }

        static int[,] GenerateMatrix(int rows, int cols, int minLimit, int maxLimit)
        {
            int[,] generatedMatrix = new int[rows, cols];

            for (int ii = 0; ii < rows; ii++)
                for (int jj = 0; jj < cols; jj++)
                    generatedMatrix[ii, jj] = random.Next(minLimit, maxLimit);

            return generatedMatrix;
        }

        static void DisplayMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1);

            for (int ii = 0; ii < rows; ii++)
                for (int jj = 0; jj < cols; jj++)
                    Console.Write((jj == 0 ? "||" : "") + $" {matrix[ii, jj]} ".PadRight(6) + (jj == cols - 1 ? "||\n" : ""));
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

            int[,] matrix = GenerateMatrix(k, n, minLimit, maxLimit);

            Console.WriteLine("Згенерована матриця: "); DisplayMatrix(matrix);
            
            int product = MultiplyPositiveRows(matrix);

            MaxFromSum(matrix, out int sum);

            Console.WriteLine($"Добуток елементів у тих рядках, які не містять від'ємних елементів: " + ((product == 1) ? "немає рядків без від'ємних елементів!\n" : $"{product}\n") +
                $"Максимум серед сум елементів діагоналей, паралельних до головної діагоналі матриці: {sum}");
        }
    }
}