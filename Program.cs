using System;
using System.Data.Odbc;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //This works
            OdbcConnection dbConnection = new OdbcConnection("DSN=athena");

            //This doesn't
            //OdbcConnection dbConnection = new OdbcConnection("Driver=Simba Athena ODBC Driver;AwsRegion=us-east-1;S3OutputLocation=s3://xxxxxxxxxxx;AuthenticationType=IAM Profile;AWSProfile=xxxxxxxxx;UseProxy=1;ProxyScheme=HTTPS;ProxyHost=xxxxxxxx;ProxyPort=xx;ProxyUID=xx;ProxyPWD=xx");

            dbConnection.Open();
            OdbcCommand dbCommand = dbConnection.CreateCommand();
            
            //This works
            dbCommand.CommandText = "SELECT * FROM my_table where col = 20060406 limit 10 ";
            
            //This two lines don't work
            //dbCommand.CommandText = "SELECT * FROM my_table where col = ? limit 10 ";
            //dbCommand.Parameters.AddWithValue("@col",20060406);

            OdbcDataReader dbReader = dbCommand.ExecuteReader();
            int fCount = dbReader.FieldCount;
            Console.Write(":");
            for (int i = 0; i < fCount; i++)
            {
                String fName = dbReader.GetName(i);
                Console.Write(fName + ":");
            }
            Console.WriteLine();
            while (dbReader.Read())
            {
                Console.Write(":");
                for (int i = 0; i < fCount; i++)
                {
                    String col = dbReader.GetString(i);

                    Console.Write(col + ":");
                }
                Console.WriteLine();
            }
        }
    }
}
