using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P03.Models
{
	public class PokedexMgr
	{
		public static List<Pokedex> GetAllPokedex()
		{
			string sql = @"SELECT * FROM Pokedex";
			List<Pokedex> PokeList = new List<Pokedex>();
			// TODO: P03 Task 2a - Get all Pokedex From Database
			// PokeList = DBUtl ... ...
			PokeList = DBUtl.GetList<Pokedex>(sql);
			return PokeList;
		}

		public static Pokedex GetPokedex(int id)
		{
			string sql = @"SELECT * FROM Pokedex
                         WHERE Id = {0}";
			List<Pokedex> PokeList = new List<Pokedex>();
			// TODO: P03 Task 2b - Get the specified Pokedex given the id
			// PokeList = DBUtl ... ...
			PokeList = DBUtl.GetList<Pokedex>(sql,id);


			if (PokeList.Count == 1)
				return PokeList[0];
			else
				return null;
		}

		public static List<Pokedex> FindPokedex(string type)
		{
			string sql = @"SELECT * FROM Pokedex
                         WHERE type1 = '{0}'
                            OR type2 = '{0}'";
			List<Pokedex> PokeList = new List<Pokedex>();
			// TODO: P03 Task 2c - Get all Pokedex based on a given type
			// PokeList = DBUtl ... ...
			PokeList = DBUtl.GetList<Pokedex>(sql,type);

			return PokeList;
		}

		public static List<Pokedex> FindPokedex(string t1, string t2)
		{
			string sql = @"SELECT * FROM Pokedex
                         WHERE type1 = '{0}'
                           AND type2 = '{1}'";
			List<Pokedex> PokeList = new List<Pokedex>();
			// TODO: P03 Task 2d - Get all Pokedex which belongs to t1 and t2
			// PokeList = DBUtl ... ...
			PokeList = DBUtl.GetList<Pokedex>(sql,t1,t2);

			return PokeList;
		}
	}
}
