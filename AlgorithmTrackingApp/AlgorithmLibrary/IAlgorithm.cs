namespace AlgorithmLibrary;

/// <summary>
/// CRRUD interface for the Algorithm Library.
/// </summary>

public interface IAlgorithmRepository
{
    Algorithm Create(Algorithm algorithm); // Create a new algorithm.
    List<Algorithm> ReadAll(); // Read all algorithms.
    Algorithm? Read(string id); // Read an algorithm.
    void Delete(string id); // Delete an algorithm.
    void Update(string oldId, Algorithm algorithm); // Update an algorithm.
    
    void ExecuteAlgorithm(string id); // Execute an algorithm.
}