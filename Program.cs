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

            void LogIn (string username, string password);
            void Registration (string username, string password, string birthday);
            void TopTable(string tag);
        }
        static void Main(string[] args)
        {
            
            User user = new User();
            user.Registration("Eugene","kara_h2ax","07.12.2006");
            Console.WriteLine(user.Login);
            //user.TopTable("Math");
            //File.Delete(pathTable);
            //File.Delete(pathUser);
        }
        public class User : IUser
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Birthday { set; get; }

            public void LogIn(string username, string password)
            {
                string[] lines = File.ReadAllLines(pathUser);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == username) 
                    {
                        Login = username;
                        Password = password;
                        break;
                    }
                }
                if (Login == null)
                {
                    Console.WriteLine("You need to register!\n" + new string('_',30));
                }
            }

            public void Registration(string username, string password, string birthday)
            {
                string[] lines = File.ReadAllLines(pathUser);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != username && i == lines.Length - 1)
                    {
                        Login = username;
                        Password = password;
                        Birthday = birthday;
                        File.AppendAllTextAsync(pathUser, $"{Login}\n{Password}\n{Birthday}\n");
                        break;
                    }
                }
            }

            public void TopTable(string tag)
            {
                string[] lines = File.ReadAllLines(pathTable);
                List<string> results = new List<string>();

                foreach (string line in lines)
                {
                    if (line.StartsWith(tag))
                    {
                        results.Add(line);
                    }
                }
                results.Sort((x, y) =>
                {
                    int scoreX = int.Parse(x.Split()[^2]); 
                    int scoreY = int.Parse(y.Split()[^2]);
                    return scoreY.CompareTo(scoreX); 
                });

                Console.WriteLine($"Top results for tag '{tag}':");
                for (int i = 0; i < Math.Min(results.Count, 20); i++) 
                {
                    Console.WriteLine(results[i]);
                }
            }
        }
    }
}
