using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	hadap3.Cadactions aa;
	public MainWindow(string dbfn = "") : base (Gtk.WindowType.Toplevel)
	{
		createAndPopulateDB("data");
		Build();
	}

	public void createAndPopulateDB(string dbfn) 
	{
		 aa = new hadap3.Cadactions();
		aa.createAndPopulateDB(dbfn);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}



	protected void deleteClient (object sender, EventArgs e)
	{
		bool check = false;
		if (entry3.Text.Trim() == "") {

		} else {
			int id;
			string str = entry3.Text.Trim();
			bool isNum = int.TryParse (str, out id);
			if (isNum && id > 0) {
				check = aa.deleteClient (id);
				if (check) {
					aa.deleteProduct (id);
					label11.Text = "delete successful";
					label12.Text = "";
					entry3.Text = "0";
					Empty2 ();
				}
				else 
					label11.Text = "Error erasing client";
			}
		}

	}



	protected void readClient (object sender, EventArgs e)
	{
		if (entry3.Text.Trim() == "") {

		} else {
			int id;
			string str = entry3.Text.Trim();
			bool isNum = int.TryParse (str, out id);
			if (isNum && id > 0) {
				string read = aa.readClient (id);
				if (read != "cannot read client") {
					label12.Text = read;
					label11.Text = "read successful";
					updateNumberOfProducts (id);
				} else {
					label11.Text = "read failed, enter again id";
					Empty2 ();
				}
			} else {
				label11.Text = "please enter again id";
				Empty2 ();
			}
		}
	}

	protected void updateClient (object sender, EventArgs e)
	{
		if (entry3.Text.Trim () != "") {
			int id;
			string str = entry3.Text.Trim ();
			int.TryParse (str, out id);
			string name = entry4.Text.Trim ();
			string address = entry5.Text.Trim ();
			string city = entry6.Text.Trim ();
			if (id > 0 && name != "" && address != "" && city != "") {
				aa.updateClient (id, name, address, city);
				Empty ();
			}
		}
	}
		

	protected void createClient (object sender, EventArgs e)
	{
		
		int id;
		string str = entry3.Text.Trim();
		int.TryParse (str, out id);
		string name = entry4.Text.Trim ();
		string address = entry5.Text.Trim ();
		string city = entry6.Text.Trim ();
		if (name != "" && address != "" && city != "") {
			bool check = aa.createClient (id, name, address, city);
			if (check)
				label11.Text = "create successful";
			else
				label11.Text = "create failed, user existed";
			Empty ();
			Empty2 ();
		}
	}
	protected void updateNumberOfProducts(int id) {
		int numOfproduct = aa.countNum (id);
		label8.Text = numOfproduct.ToString ();
	}

	protected void Empty() {
		entry3.Text = "";
		entry4.Text = "";
		entry5.Text = "";
		entry6.Text = "";
	}
	protected void Empty2() {
		label12.Text = "";
		label8.Text = "";
	}

	protected void nextClient (object sender, EventArgs e)
	{
		int id;
		string str = entry3.Text.Trim();
		int.TryParse (str, out id);
		label12.Text = aa.nextClient (id);
		entry3.Text = (id + 1).ToString();
		updateNumberOfProducts (id + 1);
	}

	protected void prevClient (object sender, EventArgs e)
	{
		int id;
		string str = entry3.Text.Trim();
		int.TryParse (str, out id);
		label12.Text = 	aa.prevClient (id);
		entry3.Text = (id - 1).ToString();
		updateNumberOfProducts (id -1);
	}
}
