using MySql.Data.MySqlClient;


namespace ATMApplication
{


  public class ATMSystem
  {
    private readonly MySqlConnection connection;

    public ATMSystem(string connectionString)
    {
      connection = new MySqlConnection(connectionString);
    }

    public void Run()
    {
      List<CardHolder> cardHolders = new();

      try
      {
        connection.Open();

        // Retrieve card holders from database
        string query = "SELECT * FROM cardholders";
        MySqlCommand command = new(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          CardHolder ch = new(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetDouble(5));
          cardHolders.Add(ch);
        }
        reader.Close();

        Console.WriteLine("Welcome to SSE-BANK ATM");
        Console.WriteLine("Please insert your debit card:");

        CardHolder currentUser = AuthenticateUser(cardHolders);

        Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");

        while (true)
        {
          PrintOptions();
          int option = GetOption();

          if (option == 1)
          {
            Deposit(currentUser);
          }
          else if (option == 2)
          {
            Withdraw(currentUser);
          }
          else if (option == 3)
          {
            CheckBalance(currentUser);
          }
          else if (option == 4)
          {
            Console.WriteLine("Thank you! Have a nice day.");
            break;
          }
          else
          {
            Console.WriteLine("Invalid option. Please try again.");
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      finally
      {
        connection.Close();
      }
    }

    private static CardHolder AuthenticateUser(List<CardHolder> cardHolders)
    {
      while (true)
      {
        string? debitcardNum = Console.ReadLine();

        CardHolder? currentUser = cardHolders.Find(a => a.CardNum == debitcardNum);

        if (currentUser != null)
        {
          Console.WriteLine("Please enter your PIN:");
          int v = Convert.ToInt32(Console.ReadLine());
          int userPin = v;

          if (currentUser.Pin == userPin)
          {
            return currentUser;
          }
          else
          {
            Console.WriteLine("Incorrect PIN. Please try again.");
          }
        }
        else
        {
          Console.WriteLine("Card not recognised. Please try again.");
        }
      }
    }

    private void Deposit(CardHolder currentUser)
    {
      Console.WriteLine("How much money would you like to deposit?");
      double deposit = Convert.ToInt32(Console.ReadLine());
      currentUser.Balance += deposit;
      UpdateBalance(currentUser);
      Console.WriteLine($"Thank you for depositing your money. Your current balance is: {currentUser.Balance}");
    }

    private void Withdraw(CardHolder currentUser)
    {
      Console.WriteLine("How much money would you like to withdraw?");
      double withdrawal = Convert.ToInt32(Console.ReadLine());

      if (currentUser.Balance < withdrawal)
      {
        Console.WriteLine("Insufficient balance :(");
      }
      else
      {
        currentUser.Balance -= withdrawal;
        UpdateBalance(currentUser);
        Console.WriteLine($"You're good to go! Thank you. New balance: {currentUser.Balance}");
      }
    }

    private static void CheckBalance(CardHolder currentUser)
    {
      Console.WriteLine($"Current balance: {currentUser.Balance}");
    }

    private void UpdateBalance(CardHolder currentUser)
    {
      string query = $"UPDATE cardholders SET balance = {currentUser.Balance} WHERE cardnum = '{currentUser.CardNum}'";
      MySqlCommand command = new(query, connection);
      command.ExecuteNonQuery();
    }

    private static void PrintOptions()
    {
      Console.WriteLine("Please choose one of the following options:");
      Console.WriteLine("1. Deposit");
      Console.WriteLine("2. Withdraw");
      Console.WriteLine("3. Check Balance");
      Console.WriteLine("4. Exit");
    }

    private static int GetOption()
    {
      int option;

      while (!int.TryParse(Console.ReadLine(), out option))
      {
        Console.WriteLine("Invalid input. Please enter a number.");
      }

      return option;
    }
  }


}
