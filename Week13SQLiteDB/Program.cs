using System.Data;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

FindCustomer(CreateConnection());
//DeleteCustomer(CreateConnection());
//DisplayProductWithCategory(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;
}



static void FindCustomer(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;
    string searchName;

    Console.WriteLine("Enter a first name to display customer data:");
    searchName = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"SELECT customer.rowid, customer.firstName, customer.lastName, status.statusType " +
        $"FROM customerStatus " +
        $"JOIN customer ON customer.rowid = customerStatus.customerId " +
        $"JOIN status ON status.rowid = customerStatus.statusId " +
        $"WHERE firstname LIKE '{searchName}'";

    reader = command.ExecuteReader();

    while(reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerStringName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);
        Console.WriteLine($"Search result: ID: {readerRowid}. {readerStringName} {readerStringLastName}. Status: {readerStringStatus}");
    }

    myConnection.Close();

}


static void DisplayProduct(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, ProductName, Price FROM product";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        int readerProductPrice = reader.GetInt32(2);

        Console.WriteLine($"{readerRowId}. {readerProductName}. Price: {readerProductPrice}");
    };

    myConnection.Close();
}


static void DisplayProductWithCategory(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT Product.rowid, product.ProductName, ProductCategory.CategoryName FROM product " +
        "JOIN ProductCategory ON ProductCategory.rowid = Product.CategoryId";
    reader = command.ExecuteReader();

    while (reader.Read())
    {

        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        string readerProductCategory = reader.GetString(2);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Category: {readerProductCategory}");
    }

    myConnection.Close();
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    //command.CommandText = "SELECT customer.firstName, customer.lastName, status.statusType " +
    //    "FROM customerStatus " +
    //    "JOIN customer ON customer.rowid = customerStatus.customerId " +
    //    "JOIN status ON status.rowid = customerStatus.statusId " +
    //    "ORDER BY status.statusType";


    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);

        Console.WriteLine($"{readerRowId}. {readerStringFirstName} {readerStringLastName}");
    };

    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{

    SQLiteCommand command;
    string fName, lName;

    Console.WriteLine("First name:");
    fName = Console.ReadLine();

    Console.WriteLine("Last name:");
    lName = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(FirstName, LastName) VALUES ('{fName}', '{lName}')";
    int rowsInserted = command.ExecuteNonQuery();

    Console.WriteLine($"{rowsInserted} new row has been inserted.");

    ReadData(myConnection);
}



static void DeleteCustomer(SQLiteConnection myConnection)
{

    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete:");

    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer " +
        $"WHERE rowid = {idToDelete}";

    int rowsDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsDeleted} has been deleted.");

    ReadData(myConnection);
}



//static void InsertCustomer(SQLiteConnection myConnection)
//{
//    SQLiteCommand command;
//    string fName, lName, dob;

//    Console.WriteLine($"Enter first name");
//    fName = Console.ReadLine();

//    Console.WriteLine($"Enter last name");
//    lName = Console.ReadLine();

//    Console.WriteLine($"Enter date of birth (mm-dd-yyyy)");
//    dob = Console.ReadLine();


//    command = myConnection.CreateCommand();
//    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
//        $"VALUES ('{fName}', '{lName}', '{dob}')";

//    int rowInserted = command.ExecuteNonQuery();
//    Console.WriteLine($"Row inserted: {rowInserted}");

//    ReadData(myConnection);
//}

//static void RemoveCustomer(SQLiteConnection myConnection)
//{
//    SQLiteCommand command;

//    string idToDelete;
//    Console.WriteLine("Enter and id to delete a customer:");
//    idToDelete = Console.ReadLine();

//    command = myConnection.CreateCommand();
//    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";

//    int rowRemoved = command.ExecuteNonQuery();
//    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

//    ReadData(myConnection);
//}

//static void FindCustomer ()
//{

//}