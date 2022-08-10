using AlgorithmLibrary;

/*
 *        Section:              CSCI 1260 - 090
 *        Name:                 Brycen Dunn (E00613421)
 *        Date:                 08/02/22
 *        Date last revised:    08/09/22
 *        Project name:         AlgorithmApp
 *        for:                  Project 2
 */

namespace AlgorithmApp;

public static class Program
{
    public static void Main(string[] args)
    {
        
        IAlgorithmRepository repo = new CsvAlgorithmRepository("Algorithm.csv");    //A new interface repository is generated

        string answer;
        do
        {
            var algorithms = repo.ReadAll(); //A new List with the data type algorithm is generated
            ShowAllAlgorithms(algorithms);
            ShowMenu();
            answer = ReadString("Enter your choice: ");
            switch (answer.ToUpper())
            {
                case "C":
                    repo.Create(ReadAlgo());    //Creates a new algorithm and adds it to the repository
                    break;
                case "V":
                    ViewAlgo(repo);                     //Views the details of a singular algorithm from the repository
                    break;
                case "D":
                    DeleteAlgo(repo);                   //Deletes an algorithm from the repository
                    break;
                case "U":
                    UpdateAlgo(repo);                   //Updates an algorithm in the repository
                    break;
                case "E":
                    ExecuteAlgorithm(repo);             //Executes an algorithm from the repository
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        } while (answer.ToUpper() != "X");
        
        static Algorithm ReadAlgo()    
        {
            Algorithm algo = new()
            {
                AlgoName = ReadString("Enter your sorting algorithm's name: ")
            };
            ReadWorstCase(algo);            //input the worst case
            ReadBestCase(algo);             //input best case
            ReadAverageComplexity(algo);    //input average complexity
            return algo;
        }
        
        static void ViewAlgo(IAlgorithmRepository repo)
        {
            var algoName = ReadString("Enter the name of the algorithm you would like to view: ");
            var algo = repo.Read(algoName);     //Reads the algorithm name from the console and views it from the 
            //repository
            if(algo != null)
            {
                Console.WriteLine(algo);
            }
            else
            {
                Console.WriteLine($"{algoName} was not found!");
            }
        }
        
        static void ShowAllAlgorithms(List<Algorithm> algorithm)    //takes an algorithm as a parameter
        {
            Console.WriteLine();
            Console.WriteLine(new String('-', 54));
            string s1 = "Name";
            string s2 = "Worst Case";
            string s3 = "Best Case";
            string s4 = "Average Complexity";
            Console.WriteLine($"| {s1,-5}| {s2,-10} | {s3,-10}| {s4,-10} | ");  //Spaces the algorithm componenets out
            Console.WriteLine(new String('-', 54)); //String content header
            foreach (Algorithm algo in algorithm)
            {
                Console.WriteLine($"{algo.AlgoName,-30} {algo.WorstCase,4} {algo.BestCase, 4} {algo.AverageComplexity, 4}");
                //reads record from repository
            }
        }
        
        static void ReadWorstCase(Algorithm algorithm)  //takes an algorithm as a paremeter
        {
            do
            {
                string worstCase = ReadString("Please enter the sorting algorithm's worst case: ");
                try
                {
                    algorithm.WorstCase = worstCase;
                    break;
                }
                catch(InvalidWorstCaseException ex) //prevents erroneous user input
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        static void ExecuteAlgorithm(IAlgorithmRepository repo) //takes the repository as a parameter
        {
            do
            {
                string algoName = ReadString("Please enter the sorting algorithm you would like to use: ");
                repo.Read(algoName);
                
                    Console.WriteLine("Please enter the array you would like to sort: ");
                    string[] array;
                    
                    try
                    {
                        do
                        {
                            array = ReadString("").Split(',');
                        } while (array.Length == 0);
                        //the array entered by the user is split by commas for
                        //parsing in the next step
                        
                        int[] intArray = new int[array.Length];
                        for (int i = 0; i < array.Length; i++)
                        {
                            intArray[i] = int.Parse(array[i]);
                        }
                        //for all items in the array, integers are parsed 
                        //for sorting in the next step

                        for (var i = 1; i < intArray.Length; i++)
                        {
                            var key = intArray[i];
                            //uses intArray from previous parsing
                            var ri = i - 1;
                            while (ri >= 0 && intArray[ri] > key)
                            {
                                intArray[ri + 1] = intArray[ri];
                                ri--;
                            }
                            intArray[ri + 1] = key;
                        }   
                        //Insertion sorting is used to sort all arrays inputted by the user

                        Console.WriteLine("Sorted array: ");    
                        foreach (int i in intArray)
                        {
                            Console.Write($"{i} ");
                        }
                        Console.WriteLine();
                        break;
                    }
                    catch(InvalidExecuteException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            } while (true);
        }
        
        static void ReadBestCase(Algorithm algorithm)   //Takes an algorithm as a parameter
        {
            do
            {
                string bestCase = ReadString("Please enter the sorting algorithm's best case: ");
                try
                {
                    algorithm.BestCase = bestCase;
                    //Algorithm best case is read from user input and stored in a file
                    break;
                }
                catch(InvalidBestCaseException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }
        
        static void ReadAverageComplexity(Algorithm algorithm)  //Takes an algorithm as a parameter
        {
            do
            {
                string averageComplexity = ReadString("Please enter the sorting algorithm's average complexity: ");
                try
                {
                    algorithm.AverageComplexity = averageComplexity;
                    //Algorithm average complexity is read from user input and added the repository
                    break;
                }
                catch(InvalidAverageComplexityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }
        
        static void UpdateAlgo(IAlgorithmRepository repo)   //Takes a repository as a parameter
        {
            var algoName = ReadString("Enter the name of the algorithm you would like to update: ");
            var algo = repo.Read(algoName);
            if (algo != null)
            {
                var newAlgo = ReadAlgo();
                repo.Update(algoName, newAlgo);
                //updates the algorithm by deleting and replacing the file it was in with the new information
            }
            else
            {
                Console.WriteLine($"{algoName} was not found!");
            }
        }
        
        static void DeleteAlgo(IAlgorithmRepository repo)   //takes the repository as a parameter
        {
            var algoName = ReadString("Enter the name of the algorithm you would like to delete: ");
            var algo = repo.Read(algoName);
            if (algo != null)
            {
                Console.WriteLine("Are you sure you want to delete this algorithm? (y/n)");
                var answer = ReadString("");
                if (answer is "y"or "Y")
                {
                    repo.Delete(algoName);
                    Console.WriteLine($"{algoName} was removed!");
                }
                else if (answer is "n" or "N")
                {
                    Console.WriteLine("Algorithm was not deleted!");
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
            else
            {
                Console.WriteLine($"{algoName} was not found!");
            }
        }
        
        static void ShowMenu()
        {
            Console.WriteLine("C = Create Algorithm\nV = View Algorithm\nD = Delete Algorithm\n" +
                              "U = Update Algorithm\nE = Execute Algorithm\nX = Exit");
        }
        
        static string ReadString(string prompt) //takes a prompt as a parameter
        {
            string value = "|";
            Console.Write(prompt);
            //writes the prompt parameter to the console
            string? valueStr = Console.ReadLine();
            if(valueStr != null)
            {
                value = valueStr.Trim();
            }
            return value;
            //reads and trims a string
        }

        ShowMenu();
    }

}