using System;


namespace EN
{
	public class Product : ENBase
	{
		public Product(int id, int cid = 0, string description = "", double price = 0.0)
		{
			this.id = id;
			this.cid = cid;
			this.description = description;
			this.price = price;
		}
		public string description { get; set; }
		public double price { get; set; }
		public int cid { get; set; }
		public int id { get; set; }
		public void save(string dbname)
		{
			CAD.CADProduct p = new CAD.CADProduct(dbname);
			p.create(this);
		}
	}
}

