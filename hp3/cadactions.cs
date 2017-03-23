using System;
using library;
using System.Collections.Generic;
using System.IO;
namespace hadap3
{
	public class Cadactions
	{
		//bool checkFirst = false;
		CAD.DataBase data;
		CAD.CADClient cadC = new CAD.CADClient("data");
		CAD.CADProduct cadP = new CAD.CADProduct("data");
		EN.Client c;
		EN.Product p;

//		string[] clientName = { "khoa0", "khoa1","khoa2","khoa3","khoa4","khoa5",
//			"khoa6","khoa7","khoa8","kho9","khoa10","khoa11","khoa12","khoa13","khoa14",
//			"khoa15","khoa16","khoa17","khoa18","khoa19"
//		};

		public Cadactions()
		{
		}
		public void createAndPopulateDB(string dbfn)
		{
			Console.Write (data);
			Random random = new Random ();
			int i = 1;
			int j = 1;
			int productCount = 0;
			if (File.Exists (dbfn)) {
			} else {
				data = new CAD.DataBase (dbfn, CAD.DataBase.Creation.YES);
				int number = random.Next (1, 20);
				while (i < number) {
					c = new EN.Client (i, "khoa" + i, "spain", "alcante");
					c.save (dbfn);
					int newNum = random.Next (1, 6);
					while (j < newNum) {
						p = new EN.Product (productCount++, i, "product" + j, j + 2.5);
						p.save (dbfn);
						j++;
					}
					j = 0;
					i++;
				}
			}
		}

		public string readClient(int id) {
			EN.Client cc;
			string a;
			cc = (EN.Client)cadC.read(id);
			if (cc == null)
				a = "cannot read client";
			else
				a = cc.name;
		
			return a;
		}

		public bool createClient(int id, string name, string address, string city) {		
			EN.Client cc = new EN.Client (id, name, address, city);
			bool check = cc.save ("data");
			if (check)
				return true;
			else 
				return false;
		}
		public void updateClient(int id, string name, string address, string city) {		
			EN.Client cc = new EN.Client (id, name, address, city);
			cadC.update (cc);
		}

		public bool deleteClient(int id) {	
			bool check1 = false;
			bool check = cadC.checkIfExist (id);
			if (check == true){
				cadC.delete (id);
				check1 = true;
			} 
			return check1;
		}
		public string nextClient(int id) {
			return readClient (id + 1);
		}
		public string prevClient(int id) {
			return readClient (id-1);
		}
		public int countNum(int id) {
			return cadC.numberOfProducts (id);
		}

		public void updateProducts(int id, int clientid, string description, double price) {
			EN.Product pp = new EN.Product (id, clientid, description, price);
			cadP.update (pp);
		}
		public void deleteProduct(int id) {
			cadP.delete (id);
		}
	}
}
