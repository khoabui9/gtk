namespace CAD
{
	using System;
	using Mono.Data.Sqlite;
	using System.IO;

	public class DataBase
	{
		// Data: ----------------------------------
		/// <summary>
		/// Creation enum.
		/// </summary>
		public enum Creation
		{
			NO,
			YES
		}

		/// <summary>
		/// The connection string.
		/// </summary>
		private string cs;

		/// <summary>
		/// The connection
		/// </summary>
		private SqliteConnection con;
		/// <summary>
		/// Gets the connection.
		/// </summary>
		/// <value>The connection.</value>
		public SqliteConnection connection { get { return con; } }

		private void createConnection ()
		{
			if (con == null)
				con = new SqliteConnection (cs);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CAD.DataBase"/> class.
		/// </summary>
		/// <param name="fname">Fname.</param>
		/// <param name="create">Create.</param>
		public DataBase (string fname, Creation create = Creation.NO)
		{
			if (fname != "") {

				cs = "Data Source=" + fname;
				//con = new SqliteConnection(cs);
				createConnection ();

				if (create == Creation.YES)
					createDB (fname);
			} else
				con = null;
		}

		~DataBase ()
		{
			closeConnection ();
		}

		/// <summary>
		/// Creates the Database file
		/// </summary>
		/// <param name="fn">Fn.</param>
		private void createDB (string fn)
		{ 
			SqliteConnection.CreateFile (fn);
			createClientTable ();
			createProductTable ();
		}

		/// <summary>
		/// Creates the person table.
		/// </summary>
		private void createClientTable ()
		{
			openConnection ();
			using (SqliteCommand cmd = new SqliteCommand (con)) {
				// DROP
				cmd.CommandText = "DROP TABLE IF EXISTS clients";
				cmd.ExecuteNonQuery ();

				// CREATE
				cmd.CommandText = @"CREATE TABLE clients (id INTEGER PRIMARY KEY, name TEXT, 
				                    address TEXT, city TEXT)";
				cmd.ExecuteNonQuery ();
			}
			closeConnection ();
		}

		/// <summary>
		/// Creates the product table.
		/// </summary>
		private void createProductTable ()
		{
			openConnection ();
			using (SqliteCommand cmd = new SqliteCommand (con)) {
				cmd.CommandText = "DROP TABLE IF EXISTS products";
				cmd.ExecuteNonQuery ();
				cmd.CommandText = @"CREATE TABLE products (id INTEGER PRIMARY KEY, 
                                                    description TEXT,  price REAL,
                                                    clientid  INTEGER, 
                                                    FOREIGN KEY (clientid) REFERENCES clients (id))";
				cmd.ExecuteNonQuery ();
			}
			closeConnection ();
		}

		/// <summary>
		/// Opens the connection.
		/// </summary>
		public void openConnection ()
		{
			try {
				con.Open ();

			} catch (SqliteException ex) { 
				Console.WriteLine ("Opening connection failed.");
				Console.WriteLine ("Error: {0}", ex.ToString ());
			} 
		}

		/// <summary>
		/// Closes the connection.
		/// </summary>
		public void closeConnection ()
		{
			try {
				if (isOpen)
					con.Close ();

			} catch (SqliteException ex) { 
				Console.WriteLine ("Closing connection failed.");
				Console.WriteLine ("Error: {0}", ex.ToString ());

			} finally {
				con.Dispose ();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="CAD.DataBase"/> is open.
		/// </summary>
		/// <value><c>true</c> if is open; otherwise, <c>false</c>.</value>
		public bool isOpen { get { return con?.State == System.Data.ConnectionState.Open; } }
	}
}

