namespace Quiz
{
    class Program
    {
        static public string pathUser = @"C:\Quiz\user.txt";
        static public string pathTable = @"C:\Quiz\table.txt";

        interface IUser
        {
            string Login { get; }
            string Password { get; }
            string Birthday { get; }

            bool LogIn(string username, string password);
            bool Registration(string username, string password, string birthday);
            void TopTable(string tag);
            void PrintResults();
        }

        static void Main(string[] args)
        {
            //------Open account------\\
            User user = new User();
            //user.Registration("Eugene", "kara_h2ax", "07.12.2006");
            user.LogIn("Eugene", "kara_h2ax"); // User for testing
            bool regTime = true;
            while(regTime)
            {
                int choice;
                Console.WriteLine("1 - LogIn 2 - Registaration");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Login: ");
                        string login = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string password = Console.ReadLine();
                        if (user.LogIn(login, password))
                        {
                            regTime = false;
                        }
                        break;
                    case 2:
                        Console.Write("Enter Login: ");
                        login = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        password = Console.ReadLine();
                        Console.Write("Enter Birthday: ");
                        string birth = Console.ReadLine();
                        if (user.Registration(login, password, birth))
                        {
                            regTime = false;
                        }
                        break;
                    default:
                        Console.WriteLine("ERROR!");
                        break;
                }
            }
            //------Main Game------\\
            bool running = true;

            File.WriteAllText(pathTable, $"");

            File.AppendAllText(pathTable, $"Biology\n{user.Login}\n5\n");
            File.AppendAllText(pathTable, $"Biology\n{user.Login}\n8\n");
            File.AppendAllText(pathTable, $"Geography\n{user.Login}\n3\n");
            File.AppendAllText(pathTable, $"Biology\n{user.Login}\n1\n");
            File.AppendAllText(pathTable, $"Geography\n{user.Login}\n6\n");
            File.AppendAllText(pathTable, $"Geography\n{user.Login}\n0\n");

            //user.TopTable("Biology");

            while (running)
            {
                Console.WriteLine(new string('_', 30));

                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Start new Quiz");
                Console.WriteLine("2. Results");
                Console.WriteLine("3. Top list");
                Console.WriteLine("4. Account settings");
                Console.WriteLine("5. Exit");

                Console.WriteLine(new string('_', 30));

                Quiz quiz = new Quiz();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter category(1 - Biology 2 - Geography): ");
                        int ch = int.Parse(Console.ReadLine());
                        string category = "";
                        switch (ch)
                        {
                            case 1:
                                category = "Biology";
                                break;
                            case 2:
                                category = "Geography";
                                break;
                            default:
                                Console.WriteLine("ERROR!!!");
                                break;
                        }
                        File.AppendAllText(pathTable, $"{category}\n{user.Login}\n{quiz.Start(category)}\n");
                        break;
                    case "2":
                        Console.Clear();
                        user.PrintResults();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter category(1 - Biology 2 - Geography): ");
                        ch = int.Parse(Console.ReadLine());
                        category = "";
                        switch (ch)
                        {
                            case 1:
                                category = "Biology";
                                break;
                            case 2:
                                category = "Geography";
                                break;
                            default:
                                Console.WriteLine("ERROR!!!");
                                break;
                        }
                        user.TopTable(category);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("This option isn't valid now, sorry!");
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("There is no such choice!");
                        break;
                }
            }
        }

        public struct Quiz
        {
            private Dictionary<string, List<string>> questions;

            private Dictionary<string, List<string>> answers;
            public Quiz()
            {
                questions = new Dictionary<string, List<string>>()
                {
                { "Geography", new List<string>
                    {
                    "The capital of France?",// Paris
                    "The highest mountain in the world?",// Everest
                    "The longest river in the world?",// Nile
                    "How many countries?",// 195
                    "The richest country in the world?",// USA
                    "The poorest country in the world?", // Burundi
                    "The biggest country in the world?", // Russia
                    "The smallest country in the world?",// Vatican City
                    "Number of continents?",// 7
                    "What is the official currency of India?"// Rupee
                    }
                },
                { "Biology", new List<string>
                    {
                    "Which animal is a carnivorous scavenger?",// Hyena
                    "The name of which life-form is derived from the Latin word for “mushroom” but also applies to yeast and mold?",// Fungus
                    "What is the name of the dormant state in which an animal’s body temperature decreases and heartbeat and breathing rate slow down?",// Hibernation
                    "Which American biologist popularized sociobiology, the study of the genetic basis of the social behaviour of all animals, including humans?",// Edward O. Wilson
                    "What are the building blocks of proteins?",// Amino acids
                    "How many legs does a spider have?",// 8
                    "Chlorella algae has been studied as a food source because it is rich in which nutrients?",// Proteins
                    "Which of these crabs is largest?",// Tasmanian crab
                    "What is another name for the so-called sea wasp, which can be extremely dangerous to humans?",// Box jellyfish
                    "What substance acts as a catalyst for biological reactions in living organisms?" // Enzyme
                    }
                },
                { "Mix", new List<string>
                    {
                    "Which animal is a carnivorous scavenger?",// Hyena
                    "The highest mountain in the world?",// Everest
                    "The longest river in the world?",// Nile
                    "Which American biologist popularized sociobiology, the study of the genetic basis of the social behaviour of all animals, including humans?",// Edward O. Wilson
                    "The richest country in the world?",// USA
                    "How many legs does a spider have?",// 8
                    "Chlorella algae has been studied as a food source because it is rich in which nutrients?",// Proteins
                    "The smallest country in the world?",// Vatican City
                    "What is another name for the so-called sea wasp, which can be extremely dangerous to humans?",// Box jellyfish
                    "What is the official currency of India?" // Rupee
                    }
                }
                };

                answers = new Dictionary<string, List<string>>()
                {
                { "Geography", new List<string>
                    {
                    "Paris",
                    "Everest",
                    "Nile",
                    "195",
                    "USA",
                    "Burundi", 
                    "Russia",
                    "Vatican City",
                    "7",
                    "Rupee"
                    }
                },
                { "Biology", new List<string>
                    {
                    "Hyena",
                    "Fungus",
                    "Hibernation",
                    "Edward O. Wilson",
                    "Amino acids",
                    "8",
                    "Proteins",
                    "Tasmanian crab",
                    "Box jellyfish",
                    "Enzyme"
                    }
                    
                },
                { "Mix", new List<string>
                    {
                    "Hyena",// B
                    "Everest",// G
                    "Nile",// G
                    "Edward O. Wilson",// B
                    "USA",// G
                    "8",// B
                    "Proteins",// B
                    "Vatican City",// G
                    "Box jellyfish",// B
                    "Rupee" // G
                    }
                }
                };
            }
            public string Start(string category)
            {
                int count = 0;
                int c = 0;
                Random rnd = new Random();
                Console.WriteLine($"Category: {category}\n");

                List<string> categoryAnswers = answers[category];
                List<string> categoryQuestions = questions[category];
                foreach (var question in categoryQuestions)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(categoryAnswers[rnd.Next(i, 10)]);
                    }
                    Console.WriteLine(categoryAnswers[c]);
                    c++;
                    Console.WriteLine(question);
                    string answer = Console.ReadLine();

                    if (CheckAnswer(answer, categoryAnswers))
                    {
                        count += 1;
                    }
                }
                Console.WriteLine($"Your result: {count}");
                return Convert.ToString(count);
            }

            private bool CheckAnswer(string answer,List<string> categoryAnswers)
            {
                foreach (var answerEx in categoryAnswers)
                {
                    //Console.WriteLine(answerEx);
                    if (answerEx == answer)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public struct User : IUser
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Birthday { set; get; }

            public bool LogIn(string username, string password)
            {
                string[] lines = File.ReadAllLines(pathUser);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == username)
                    {
                        Login = username;
                        Password = password;
                        Console.WriteLine($"Welcome {Login}!");
                        return true;
                    }
                }
                if (lines.Length == 0)
                {
                    Console.WriteLine("You need to register!\n" + new string('_', 30));
                    return false;
                }
                return false;
            }

            public void PrintResults()
            {
                string[] lines = File.ReadAllLines(pathTable);
                foreach (string line in lines) {  Console.WriteLine(line); }
            }

            public bool Registration(string username, string password, string birthday)
            {
                string[] lines = File.ReadAllLines(pathUser);
                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != username && i == lines.Length - 3)
                        {
                            Login = username;
                            Password = password;
                            Birthday = birthday;
                            File.AppendAllText(pathUser, $"{Login}\n{Password}\n{Birthday}\n");
                            return true;
                        }
                        else if (lines[i] == username) { Console.WriteLine("This account exists!"); }
                    }
                }
                else if (lines.Length == 0)
                {
                    Login = username;
                    Password = password;
                    Birthday = birthday;
                    File.AppendAllText(pathUser, $"{Login}\n{Password}\n{Birthday}\n");
                    return true;
                }
                else
                {
                    Console.WriteLine("ERROR!!!");
                    return false;
                }
                return false;
            }
            public void TopTable(string tag)
            {
                string[] lines = File.ReadAllLines(pathTable);
                List<string> results = new List<string>();

                int iEx = 0;
                foreach (string line in lines)
                {
                    if (line.StartsWith(tag))
                    {
                        results.Add(lines[iEx + 2]);
                    }
                    iEx++;
                }

                //foreach (string line in results) { Console.WriteLine(line); }

                iEx = 0;
                Console.WriteLine($"Top results for tag '{tag}':");
                for (int i = 0; i < Math.Min(results.Count, 20); i++)
                {
                    Console.WriteLine(results[i] + " point(s)");
                }
            }


        }
    }
}