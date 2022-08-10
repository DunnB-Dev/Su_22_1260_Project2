namespace AlgorithmLibrary;

public class CsvAlgorithmRepository : IAlgorithmRepository
{
    private readonly string _filePath;

    public CsvAlgorithmRepository(string filePath)      //repository takes the file path as a parameter and passes it through
    {
        _filePath = filePath;
        //file paths are made equivalent 
        //so that _filePath may stay private
    }

    public Algorithm Create(Algorithm algorithm)    //Creates a new algorithm with an algorithm as a parameter
    {
        try
        {
            StreamWriter writer = new(_filePath, append: true);
            // A streamWriter is used to alter the file and add the new algorithm to it
            writer.WriteLine(algorithm.ToCsv());
            writer.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return algorithm;
    }

    public void Delete(string id)       //delete takes an ID as a parameter
    {
        try
        {
            string tempPath = _filePath + ".temp";
            //a temporary file is created to store the new data
            StreamWriter writer = new(tempPath, append: true);

            StreamReader reader = new(_filePath);
            var record = reader.ReadLine();
            while (record != null)
            {
                string[] fields = record.Split(" | ");
                // | is used as a separator between algorithm components
                if (fields[0] != id)
                {
                    writer.WriteLine(record);
                }

                record = reader.ReadLine();
            }

            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            File.Move(tempPath, _filePath);
            //the file is deleted and replaced with the desired algorithm removed
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public Algorithm? Read(string id)        //reads a single algorithm from the file
    {
        Algorithm? algorithm = null;
        try
        {
            StreamReader reader = new(_filePath);
            var record = reader.ReadLine();
            // Checking for EOF:
            while (record != null)
            {
                string[] fields = record.Split(" | ");
                if (fields[0] == id)
                {
                    algorithm = new()
                    {
                        AlgoName = fields[0],
                        WorstCase = fields[1],
                        BestCase = fields[2],
                        AverageComplexity = fields[3]
                        //all components of the algorithm are assigned to a field
                        //separated by | 
                    };
                    break;
                }

                record = reader.ReadLine();
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return algorithm;
    }

    public List<Algorithm> ReadAll()        //reads all algorithms in the file
    {
        List<Algorithm> algorithms = new();
        try
        {
            StreamReader reader = new(_filePath);
            var record = reader.ReadLine();
            while (record != null)
            {
                string[] fields = record.Split(',');
                //a comma is used instead of | for simplicity
                Algorithm algorithm = new()
                {
                    AlgoName = fields[0],
                    //only the name component is needed
                };
                algorithms.Add(algorithm);
                record = reader.ReadLine();
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return algorithms;
    }

    public void Update(string oldId, Algorithm algorithm)       //updates the file with al algorithm, taking an old ID and an
                                                                //algorithm as parameters for the update process
    {
        try
        {
            string tempPath = _filePath + ".temp";
            //a temporary file is made to store new algorithm data for updating
            StreamWriter writer = new(tempPath, append: true);

            StreamReader reader = new(_filePath);
            var record = reader.ReadLine();
            while (record != null)
            {
                string[] fields = record.Split(" | ");
                writer.WriteLine(fields[0] != oldId ? record : algorithm.ToCsv());

                record = reader.ReadLine();
            }

            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            //The old file is deleted
            File.Move(tempPath, _filePath);
            //And replaced with the new file
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private readonly int[] _integers = null!; 
    //a new array of integers is made for execution of the algorithm
    
    public void ExecuteAlgorithm(string id) //executes algorithm with the ID of the algorithm as a constructor
    {
        try
        {
            StreamReader reader = new(_filePath);
            var record = reader.ReadLine();
            while (record != null)
            {
                record.Split(" | ");
                for (var i = 1; i < _integers.Length; i++)
                {
                    var key = _integers[i];
                    var ri = i - 1;
                    while (ri >= 0 && _integers[ri] > key)
                    {
                        _integers[ri + 1] = _integers[ri];
                        ri--;
                    }
                    //insertion sort is used for all algorithms

                    _integers[ri + 1] = key;
                }

                record = reader.ReadLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
    