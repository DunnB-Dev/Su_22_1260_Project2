namespace AlgorithmLibrary;

public class InvalidNameException : Exception
{
    public InvalidNameException() : base("Invalid name!")
    {
    }

    public InvalidNameException(string message) : base(message)
    {
    }

    /// <summary>
    /// A Two Argument Constructor for the InvalidNameException class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="inner"></param>
    
    public InvalidNameException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class InvalidWorstCaseException : Exception  //Invalid Exception from worst case
{
    public InvalidWorstCaseException() : base("Invalid worst case!")
    {
    }
    
    public InvalidWorstCaseException(string message) : base(message)
    {
    }
    
    public InvalidWorstCaseException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class InvalidBestCaseException : Exception   //Invalid exception for best case
{
    public InvalidBestCaseException() : base("Invalid best case!")
    {
    }
    
    public InvalidBestCaseException(string message) : base(message)
    {
    }
    
    public InvalidBestCaseException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class InvalidAverageComplexityException : Exception  //Invalid exception for average complexity
{
    public InvalidAverageComplexityException() : base("Invalid average complexity!")
    {
    }
    
    public InvalidAverageComplexityException(string message) : base(message)
    {
    }
    
    public InvalidAverageComplexityException(string message, Exception inner) : base(message, inner)
    {
    }
}

public class InvalidExecuteException : Exception  //Invalid exception for algorithm execution
{
    public InvalidExecuteException() : base("Invalid execute!")
    {
    }
    
    public InvalidExecuteException(string message) : base(message)
    {
    }
    
    public InvalidExecuteException(string message, Exception inner) : base(message, inner)
    {
    }
}