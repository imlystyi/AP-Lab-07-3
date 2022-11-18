/* Lab_07_3_Iterative_UT.cs
 * якубовський ¬ладислав
 * Unit-test до програми Lab_07_3_Iterative.cs */
namespace AP_Lab_07_3_Iterative_UT
{
    [TestClass]
    public class Lab_07_3_Iterative_UT
    {
        [TestMethod]
        public void TestMaxFromSum()
        {
            /* 1 2 3 4 5
             * 3 4 5 6 7
             * 5 6 7 8 9
             * 1 1 2 2 2 
             * 
             * max = 18 */

            int[,] matrix = { { 1, 2, 3, 4, 5 }, { 3, 4, 5, 6, 7 }, { 5, 6, 7, 8, 9 }, { 1, 1, 2, 2, 2 } };

            Lab_07_3_Iterative.MaxFromSum(matrix, out int sum);

            Assert.AreEqual(18, sum);
        }
    }
}