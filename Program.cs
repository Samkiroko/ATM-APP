using DotNetEnv;



class Program
{
  static void Main(string[] args)
  {
    Env.Load();
    string connectionString = $"server={Environment.GetEnvironmentVariable("DB_HOST")};user={Environment.GetEnvironmentVariable("DB_USER")};password={Environment.GetEnvironmentVariable("DB_PASSWORD")};database={Environment.GetEnvironmentVariable("DB_DATABASE")};";
    ATMSystem atm = new(connectionString);
    atm.Run();
  }


}