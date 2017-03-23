using System;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Media;
using EN;


namespace CAD
{
	public class CADProduct : CAD
	{
		private SqliteConnection con;
		bool isOpen = false;
		public CADProduct (string db)
		{
			string connstr = "Data Source=" + db;
			if (db != "")
			if (con == null)
				con = new SqliteConnection(connstr);

		}
		~CADProduct()
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
			Product p = (Product)en;
			int id = p.id;
			string description = p.description;
			double price = p.price;
			int clientid = p.cid;
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "INSERT INTO products(id, clientid, description, price) VALUES (@id, @clientid, @price, @description)";
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@description", description);
				cmd.Parameters.AddWithValue("@price", price);
				cmd.Parameters.AddWithValue("@clientid", clientid);
				cmd.ExecuteNonQuery();
				closeConnection();
			}
			catch(Exception ex) {
				Console.WriteLine ("Product create failed.\nError:" + ex);
			}
		}

		public void delete(int id)
		{
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "DELETE FROM products WHERE clientid = '" + id +"'";
				cmd.ExecuteNonQuery();
				closeConnection();
			}
			catch(Exception ex) {
				Console.WriteLine ("Product delete failed.\nError:" + ex);
			}
		}

		public EN.ENBase read(int id)
		{
			Product p = null;
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT description, price, clientid FROM products where id = id";
				SqliteDataReader reader = cmd.ExecuteReader();
				bool notEoF;

				notEoF = reader.Read();
				while (notEoF)
				{
					if (id == Int32.Parse(reader["id"].ToString()))
					{
						p = new Product(id, Int32.Parse(reader["cid"].ToString()), reader["description"].ToString(), double.Parse(reader["price"].ToString()));

					}
					notEoF = reader.Read();
				}
				reader.Close();
				closeConnection();

			}
			catch
			{
				Console.WriteLine("");
			}
			return p;
		}

		public void update(EN.ENBase en)
		{
			Product p = (Product)en;
			int id = p.id;
			string description = p.description;
			double price = p.price;
			int clientid = p.cid;
			try
			{
				openConnection();
				SqliteCommand cmd = new SqliteCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "UPDATE products SET description = @description, price = @price, clientid = @clientid WHERE clientid = '" + clientid + "'";
				cmd.Parameters.AddWithValue("@description", description);
				cmd.Parameters.AddWithValue("@price", price);
				cmd.Parameters.AddWithValue("@clientid", clientid);
				cmd.ExecuteNonQuery();
				closeConnection();
			}
			catch
			{
				closeConnection();
			}
		}

	}
}

