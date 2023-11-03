using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynEdu.DbMigration.Upgrade
{
    public class Upgrade1
    {
        private const string ConnectionString = "User Id=shop_user;Password=oracle;Data Source=//localhost:1521/xe;";


        public void ExecuteSql(string sql)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("SQL executed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error executing SQL: {ex.Message}");
                    }
                }
            }
        }

        public void Execute()
        {
            // Twój kod migracji, a następnie wywołanie ExecuteSql dla każdego polecenia SQL
            ExecuteSql("CREATE TABLE Products (productid INT PRIMARY KEY, name VARCHAR(255), value DECIMAL(10, 2), vat DECIMAL(5, 2), price DECIMAL(10, 2), discount DECIMAL(5, 2))");
            ExecuteSql("CREATE TABLE Code (productid INT PRIMARY KEY, EAN VARCHAR(13), FOREIGN KEY (productid) REFERENCES Products(productid))");
            ExecuteSql("CREATE TABLE AccessRights (userid INT PRIMARY KEY, guid VARCHAR(100), role VARCHAR(50))");
            ExecuteSql("CREATE TABLE Users (userid INT PRIMARY KEY, firstname VARCHAR(255), lastname VARCHAR(255), role VARCHAR(50), FOREIGN KEY (userid) REFERENCES AccessRights(userid))");
            ExecuteSql("CREATE TABLE Documents (documentid INT PRIMARY KEY, documentnumber VARCHAR(20), creationdate DATE, fiscalizationdate DATE, status VARCHAR(20))");
            ExecuteSql("CREATE TABLE Receipts (receiptid INT PRIMARY KEY, value DECIMAL(10, 2), status VARCHAR(20), additionalinfo VARCHAR(255))");
            ExecuteSql("CREATE TABLE ReceiptsPosition (positionid INT PRIMARY KEY, receiptid INT, name VARCHAR(255), value DECIMAL(10, 2), discount DECIMAL(5, 2), price DECIMAL(10, 2), positioninfo VARCHAR(255), status VARCHAR(20), FOREIGN KEY (receiptid) REFERENCES Receipts(receiptid))");
        }
    }
}
