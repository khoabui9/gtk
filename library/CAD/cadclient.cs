using System;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Media;
using EN;

namespace CAD
{
	public class CADClient : CAD
	{

		private SqliteConnection con;
		bool isOpen = false;

		public CADClient (string db)
		{
			string connstr = "Data Source=" + db;
			if (db != "")
			if (con == null)
				con = new SqliteConnection(connstr);
		}
		~CADClient()
		{
			closeConnection();
		}

		private void openConnection()
		{
			try
			{
				con.Open();
				isOpen = true;
			}
			catch (SqliteException e)
			{
				throw new Exception(e.ToString());
			}
		}

		private void closeConnection()
		{
			try
			{
				if (isOpen)
					con.Close();

			}
			catch (SqliteException ex)
			{
				Console.WriteLine("Closing connection failed.");
				Console.WriteLine("Error: {0}", ex.ToString());

			}
			finally
			{
				con.Dispose();
			}
		}
		public void create(EN.ENBase en)
		{
			Client c = (Client)en;
			int id = c.id;
			string name = c.name;
			string address = c.address;
			string city = c.city;
			try {
			openConnection();
			SqliteCommand cmd = new SqliteCommand ();
			cmd.Connection = con;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO clients(id, name, address, city) VALUES (@id, @name, @address, @city)";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@name", name);
			cmd.Parameters.AddWithValue("@address", address);
			cmd.Parameters.AddWithValue("@city", city);
			cmd.ExecuteNonQuery();
			closeConnection();
			}
			catch(Exception ex) {
				Console.WriteLine ("Client create failed.\nError:" + ex);
			}
		}
		public void update(EN.ENBase en) 
		{
			Client c = (Client)en;
			int id = c.id;
			string name = c.name;
			string address = c.address;
			string city = c.city;
			try {
				openConnection();
				SqliteCommand cmd = new SqliteCommand ();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "UPDATE clients SET name = @name, address = @address, city = @city WHERE id ='" + id  +"'";
				cmd.Parameters.AddWithValue("@name", name);
				cmd.Parameters.AddWithValue("@address", address);
				cmd.Parameters.AddWithValue("@city", city);
				cmd.ExecuteNonQuery();
				closeConnection();
			}
			catch(Exception ex) {
				Console.WriteLine ("Client update failed.\nError:" + ex);
			}
		}
		public void delete(int id) {
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "DELETE FROM clients WHERE id = '"+ id +"'";
				cmd.ExecuteNonQuery();
				closeConnection();
			}
			catch(Exception ex)
			{
				Console.WriteLine ("Client delete failed.\nError:" + ex);
			}
		}

		public ENBase read(int id)
		{
			Client c = null;
			try {

			openConnection();
			SqliteCommand cmd = new SqliteCommand();
			cmd.Connection = con;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM clients where id = id";
			SqliteDataReader reader = cmd.ExecuteReader();
			bool notEoF;

			notEoF = reader.Read();
			while (notEoF)
			{
				if (id == Int32.Parse(reader["id"].ToString()))
				{
					c = new Client(id,reader["name"].ToString(), reader["address"].ToString(), reader["city"].ToString());
					
				}
				notEoF = reader.Read();
			}
			reader.Close();
			closeConnection();
			
			}
			catch (Exception ex) {
				Console.WriteLine ("Client read failed.\nError:" + ex);
			}
			return (ENBase)c;
		}
		public int numberOfProducts(int id)
		{
			int count = 0;
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM products where clientid ='" + id + "'";
				SqliteDataReader reader = cmd.ExecuteReader();
				bool notEoF;

				notEoF = reader.Read();
				while (notEoF)
				{
					if (id == Int32.Parse(reader["clientid"].ToString()))
					{
						count++;
					}
					notEoF = reader.Read();
				}
				reader.Close();
				closeConnection();
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Client numberOfProducts failed.\nError:" + ex);
			}
			return count;
		}
		public bool checkIfExist(int id) {
			bool check = false;
			try {
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM clients where id = id";
				SqliteDataReader reader = cmd.ExecuteReader();
				bool notEoF;

				notEoF = reader.Read();
				while (notEoF)
				{
					if (id == Int32.Parse(reader["id"].ToString()))
					{
						//c = new Client(id,reader["name"].ToString(), reader["address"].ToString(), reader["city"].ToString());
						check = true;
					}
					notEoF = reader.Read();
				}
				reader.Close();
				closeConnection();
			}
			catch(Exception ex) {
				Console.WriteLine (ex);
			}
			return check;
		}

	}
}

