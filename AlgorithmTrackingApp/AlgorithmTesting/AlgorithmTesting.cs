namespace AlgorithmTesting;
using AlgorithmLibrary;

[TestClass]
public class AlgorithmTesting
{
    [TestMethod]
    public void AlgorithmCanHaveAName()
    {
        var algorithm = new Algorithm();
        algorithm.AlgoName = "Bubble Sort";
        Assert.AreEqual("Bubble Sort", algorithm.AlgoName);
    }

    [TestMethod]
    public void AlgorithmCanHaveABestCase()
    {
        var algorithm = new Algorithm();
        algorithm.BestCase = "Best Case: O(n)";
        Assert.AreEqual("Best Case: O(n)", algorithm.BestCase);
    }
    [TestMethod]
    public void AlgorithmCanHaveAWorstCase()
    {
        var algorithm = new Algorithm();
        algorithm.WorstCase = "Worst Case: O(n^2)";
        Assert.AreEqual("Worst Case: O(n^2)", algorithm.WorstCase);
    }
    [TestMethod]
    public void AlgorithmCanHaveAComplexity()
    {
        var algorithm = new Algorithm();
        algorithm.AverageComplexity = "Complexity: O(n^2)";
        Assert.AreEqual("Complexity: O(n^2)", algorithm.AverageComplexity);
    }
}

[TestClass]
public class CsvTesting
{
    [TestMethod]
    public void AnAlgorithmCanBeCreated()
    {
        //We want to test that an algorithm can be created and that it has the correct properties once it has been added to the repository
        IAlgorithmRepository repo = new CsvAlgorithmRepository("TestFile1.csv");
        Algorithm algo = new();
        algo.AlgoName = "Bubble Sort";
        algo.WorstCase = "Worst Case";
        algo.BestCase = "Best Case";
        algo.AverageComplexity = "Average Complexity";
        repo.Create(algo);
        Assert.AreEqual("Bubble Sort | Worst Case | Best Case | Average Complexity", repo.ReadAll()[2].AlgoName);
    }

    [TestMethod]
    public void AnAlgorithmCanBeDeleted()
    {
        //We want to test that an algorithm can be deleted from the repository
        IAlgorithmRepository repo = new CsvAlgorithmRepository("TestFileDelete.csv");
        Algorithm algo = new();
        algo.AlgoName = "Bubble Sort";
        algo.WorstCase = "Worst Case";
        algo.BestCase = "Best Case";
        algo.AverageComplexity = "Average Complexity";
        repo.Create(algo);
        File.Delete("TestFileDelete.csv");
        Assert.AreEqual(0, repo.ReadAll().Count);
    }

    [TestMethod]
    public void AnArrayCanBeSorted()
    {
        //We want to test that an array of integers can be sorted using an insertion sort algorithm
        int[] numbers = { 3,2,5,1,3,8,20 };
        for (var i = 1; i < numbers.Length; i++)
        {
            var key = numbers[i];
            var ri = i - 1;
            while (ri >= 0 && numbers[ri] > key)
            {
                numbers[ri + 1] = numbers[ri];
                ri--;
            }
            numbers[ri + 1] = key;
        }
        Assert.AreEqual(1, numbers[0]);
    }
}

[TestClass]
public class InvalidExceptionTesting
{
    [TestMethod]
    public void AnExceptionIsThrownWhenTheFileIsNotFound()
    {
        //We want to test that an exception is thrown when the file is not found
        try
        {
            IAlgorithmRepository repo = new CsvAlgorithmRepository("TestFileNameException.csv");
            Algorithm algo = new();
            algo.AlgoName = "";
            repo.Create(algo);
            Assert.Fail();
        }
        catch (InvalidNameException ex)
        {
            Assert.AreEqual("Algorithm name cannot be blank!", ex.Message);
        }
    }
}
