using System;

namespace CAD
{
	public interface CAD
	{
		void create(EN.ENBase en);
		EN.ENBase read(int id);
		void update(EN.ENBase en);
		void delete(int id);
	}
}

