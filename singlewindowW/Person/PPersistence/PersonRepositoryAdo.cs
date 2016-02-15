using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PDomain;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace PPersistence
{
    public class PersonRepositoryAdo : IRepository<Person>
    {
        static string connectionString = ConnectionStrings.Location;
        private Database db;

        public PersonRepositoryAdo()
        {
            db = new SqlDatabase(connectionString);
        }

        public void Create(Person item)
        {
            string queryString = "INSERT INTO [Person] ([FirstName],[LastName],[Age]) VALUES (@FirstName,@LastName,@Age)";
            try
            {
                DbCommand command = db.GetSqlStringCommand(queryString);
                db.AddInParameter(command, "FirstName", DbType.String, item.FirstName);
                db.AddInParameter(command, "LastName", DbType.String, item.LastName);
                db.AddInParameter(command, "Age", DbType.Int32, item.Age);
                db.AddInParameter(command, "ID", DbType.Int32, item.ID);
                db.ExecuteReader(command);
                string qs2= "SELECT MAX(ID) FROM Person";
                command = db.GetSqlStringCommand(qs2);
                IDataReader reader = db.ExecuteReader(command);
                reader.Read();
                int id =  reader.GetInt32(0);
                foreach (var pn in item.phonenumbers)
                {
                    qs2 = "INSERT INTO [PhoneNumbers] ([PersonID],[PhoneNumber],[PhoneNumberType]) VALUES (@ID,@PhoneNumber,@PhoneNumberType)";
                    command = db.GetSqlStringCommand(qs2);
                    db.AddInParameter(command, "PhoneNumber", DbType.String, pn.PhoneNumber);
                    db.AddInParameter(command, "PhoneNumberType", DbType.String, pn.PhoneNumberType);
                    db.AddInParameter(command, "ID", DbType.String, id);
                    db.ExecuteReader(command);
                }
            }
            catch { }
        }

        public void Delete(Person item)
        {
            string queryString = "DELETE FROM [Person] WHERE ID LIKE @ID";
            try
            {
                DbCommand command = db.GetSqlStringCommand(queryString);
                db.AddInParameter(command, "ID", DbType.Int32, item.ID);
                db.ExecuteReader(command);
            }
            catch { }
        }
        
        public IEnumerable<Person> GetAll()
        {
            //string queryString = "SELECT Person.ID, Person.FirstName, Person.LastName, Person.Age," +
            //    "PhoneNumbers.ID, PhoneNumbers.PhoneNumber, PhoneNumbers.PhoneNumberType " +
            //    "FROM [Person] INNER JOIN [PhoneNumbers] ON Person.ID=PhoneNumbers.PersonID";
            string queryString = "SELECT * FROM [Person]";
            List<Person> result = new List<Person>();
            try
            {
                DbCommand command = db.GetSqlStringCommand(queryString);
                IDataReader reader = db.ExecuteReader(command);
                while (reader.Read())///Mapper seperatly
                {
                    result.Add(new Person()
                    {
                        ID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Age = reader.GetInt32(3),
                        phonenumbers = new List<PhoneNumbers>()
                    });
                }

                for(int i=0; i<result.Count; i++)
                {
                    queryString = "SELECT PhoneNumber, ID, PhoneNumberType " +
                    "FROM [PhoneNumbers] WHERE PersonId=@PID";////
                    command = db.GetSqlStringCommand(queryString);
                    db.AddInParameter(command, "PID", DbType.String, result[i].ID);
                    reader = db.ExecuteReader(command);
                    while (reader.Read())///Mapper seperatly
                    {
                        result[i].phonenumbers.Add(new PhoneNumbers()
                        {
                            PhoneNumber = reader.GetString(0),
                            ID = reader.GetInt32(1),
                            PhoneNumberType = reader.GetString(2),
                            PersonID = result[i].ID
                        });
                    }
                }
            }
            catch { }
            return result;
        }

        public Person GetByID(int id)
        {
            string queryString = "SELECT * FROM [Person] WHERE ID LIKE @ID";
            Person result = new Person();
            try
            {
                DbCommand command = db.GetSqlStringCommand(queryString);
                db.AddInParameter(command, "ID", DbType.Int32, id);
                IDataReader reader = db.ExecuteReader(command);
                reader.Read();///////SIngle Mepper
                result.ID = (int)reader["ID"];
                result.FirstName = (string)reader["FirstName"];
                result.LastName = (string)reader["LastName"];
                result.Age = (int)reader["Age"];
                result.phonenumbers = new List<PhoneNumbers>();

                queryString = "SELECT PhoneNumbers.PhoneNumber, PhoneNumbers.ID, PhoneNumbers.PhoneNumberType " +
                   "FROM [Person] INNER JOIN [PhoneNumbers] ON Person.ID=PhoneNumbers.PersonID WHERE Person.Id=@PID";
                command = db.GetSqlStringCommand(queryString);
                db.AddInParameter(command, "PID", DbType.String, result.ID);
                reader = db.ExecuteReader(command);
                while (reader.Read())///Mapper seperatly
                {
                    result.phonenumbers.Add(new PhoneNumbers()
                    {
                        PhoneNumber = reader.GetString(0),
                        ID = reader.GetInt32(1),
                        PhoneNumberType = reader.GetString(2),
                        PersonID = result.ID
                    });
                }
            }
            catch { }
            return result;
        }

        public void Update(Person item)
        {
            string queryString = "UPDATE [PERSON] SET FirstName=@FirstName,LastName=@LastName,Age=@Age WHERE ID LIKE @ID";
            try
            {
                DbCommand command = db.GetSqlStringCommand(queryString);
                db.AddInParameter(command, "FirstName", DbType.String, item.FirstName);
                db.AddInParameter(command, "LastName", DbType.String, item.LastName);
                db.AddInParameter(command, "Age", DbType.Int32, item.Age);
                db.AddInParameter(command, "ID", DbType.Int32, item.ID);
                db.ExecuteReader(command);
                string qs2;
                foreach (var pn in item.phonenumbers)
                {
                    qs2 = "UPDATE [PhoneNumbers] SET PhoneNumber=@PhoneNumber,PhoneNumberType=@PhoneNumberType WHERE ID LIKE @ID";
                    command = db.GetSqlStringCommand(qs2);
                    db.AddInParameter(command, "PhoneNumber", DbType.String, pn.PhoneNumber);
                    db.AddInParameter(command, "PhoneNumberType", DbType.String, pn.PhoneNumberType);
                    db.AddInParameter(command, "ID", DbType.Int32, pn.ID);
                    db.ExecuteReader(command);
                }
            }
            catch { }
        }
    }
}
