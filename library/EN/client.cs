using System;
using System.Collections.Generic;
using System.Data;

namespace EN
{
	public class Client : ENBase
	{

		public Client(int id, string name = "", string address = "", string city = "") {
			this.id = id;
			this.name = name;
			this.address = address;
			this.city = city;

		}

		public string name { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public int id { get; set; }
		public List<Product> products 
		{
			get; set;
			//get {{ ... }}
		}

		public void addProduct(Product p) {
			this.products.Add(p);
		}
		public void removeProduct(Product p) {
			this.products.Remove(p);
		}
		public bool save(string dbname) {
			
			bool check2 = false;
			CAD.CADClient a = new CAD.CADClient(dbname);
			bool check = a.checkIfExist (this.id);
			if (check != true) {
				a.create (this);
				check2 = true;
			}
			return check2;
		}
	}
}

