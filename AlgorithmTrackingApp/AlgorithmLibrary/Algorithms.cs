namespace AlgorithmLibrary;

public class Algorithm
{
    private string _algoName = null!;
    //AlgoName is made private to prevent the user from changing the name of the algorithm
    //without using the updateAlgoName method.
    
    public string WorstCase { get; set; } = String.Empty;

    public string BestCase { get; set; } = String.Empty;

    public string AverageComplexity { get; set; } = String.Empty;

    public string AlgoName
    {
        get { return _algoName;} //returns the private name of the algorithm to the user.

        set
        {
            if (value == String.Empty) //should the user not enter a name for the algorithm, an exception is thrown
            {
                throw new InvalidNameException("Algorithm name cannot be blank!");
            }

            _algoName = value;
        }
    }

    public override string ToString()//ToString method overrides the default ToString method, returning the algorithm's description.
    {
        return $"Name: {AlgoName} | Worst Case: {WorstCase}" +
               $" | Best Case: {BestCase} | Average Complexity: {AverageComplexity}";
    }
    
    public string ToCsv()//ToCSV method return the algorithm's components to the file using | as a divider
    {
        return $"{AlgoName} | {WorstCase}" +
               $" | {BestCase} | {AverageComplexity}";
    }

}