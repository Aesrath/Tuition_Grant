using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01.Models
{
	public class PokedexMgr
	{
		private static List<Pokedex> PokeList =
		   new List<Pokedex>
		   {
			new Pokedex(1,"Bulbasaur","Grass","Poison",126,126,90),
			new Pokedex(2,"Ivysaur","Grass","Poison",156,158,120),
			new Pokedex(3,"Venusaur","Grass","Poison",198,200,160),
			new Pokedex(4,"Charmander","Fire","",128,108,78),
			new Pokedex(5,"Charmeleon","Fire","",160,140,116),
			new Pokedex(6,"Charizard","Fire","Fly",212,182,156),
			new Pokedex(7,"Squirtle","Water","",112,142,88),
			new Pokedex(8,"Wartortle","Water","",144,176,118),
			new Pokedex(9,"Blastoise","Water","",186,222,158),
			new Pokedex(10,"Caterpie","Bug","",62,66,90),
			new Pokedex(11,"Metapod","Bug","",56,86,100),
			new Pokedex(12,"Butterfree","Bug","Fly",144,144,120),
			new Pokedex(13,"Weedle","Bug","Poison",68,64,80),
			new Pokedex(14,"Kakuna","Bug","Poison",62,82,90),
			new Pokedex(15,"Beedrill","Bug","Poison",144,130,130),
			new Pokedex(16,"Pidgey","Normal","Fly",94,90,80),
			new Pokedex(17,"Pidgeotto","Normal","Fly",126,122,126),
			new Pokedex(18,"Pidgeot","Normal","Fly",170,166,166),
			new Pokedex(19,"Rattata","Normal","",92,86,60),
			new Pokedex(20,"Raticate","Normal","",146,150,110),
			new Pokedex(21,"Spearow","Normal","Fly",102,78,80),
			new Pokedex(22,"Fearow","Normal","Fly",168,146,130),
			new Pokedex(23,"Ekans","Poison","",112,112,70),
			new Pokedex(24,"Arbok","Poison","",166,166,120),
			new Pokedex(25,"Pikachu","Electric","",124,108,70),
			new Pokedex(26,"Raichu","Electric","",200,154,120),
			new Pokedex(27,"Sandshrew","Ground","",90,114,100),
			new Pokedex(28,"Sandslash","Ground","",150,172,150),
			new Pokedex(29,"Nidoran F","Poison","",100,104,110),
			new Pokedex(30,"Nidorina","Poison","",132,136,140),
			new Pokedex(31,"Nidoqueen","Poison","Ground",184,190,180),
			new Pokedex(32,"Nidoran M","Poison","",110,94,92),
			new Pokedex(33,"Nidorino","Poison","",142,128,122),
			new Pokedex(34,"Nidoking","Poison","Ground",204,170,162),
			new Pokedex(35,"Clefairy","Fairy","",116,124,140),
			new Pokedex(36,"Clefable","Fairy","",178,178,190),
			new Pokedex(37,"Vulpix","Fire","",106,118,76),
			new Pokedex(38,"Ninetales","Fire","",176,194,146),
			new Pokedex(39,"Jigglypuff","Normal","Fairy",98,54,230),
			new Pokedex(40,"Wigglytuff","Normal","Fairy",168,108,280),
			new Pokedex(41,"Zubat","Poison","Fly",88,90,80),
			new Pokedex(42,"Golbat","Poison","Fly",164,164,150),
			new Pokedex(43,"Oddish","Grass","Poison",134,130,90),
			new Pokedex(44,"Gloom","Grass","Poison",162,158,120),
			new Pokedex(45,"Vileplume","Grass","Poison",202,190,150),
			new Pokedex(46,"Paras","Bug","Grass",122,120,70),
			new Pokedex(47,"Parasect","Bug","Grass",162,170,120),
			new Pokedex(48,"Venonat","Bug","Poison",108,118,120),
			new Pokedex(49,"Venomoth","Bug","Poison",172,154,140),
			new Pokedex(50,"Diglett","Ground","",108,86,20),
			new Pokedex(51,"Dugtrio","Ground","",148,140,70),
			new Pokedex(52,"Meowth","Normal","",104,94,80),
			new Pokedex(53,"Persian","Normal","",156,146,130),
			new Pokedex(54,"Psyduck","Water","",132,112,100),
			new Pokedex(55,"Golduck","Water","",194,176,160),
			new Pokedex(56,"Mankey","Fighting","",122,96,80),
			new Pokedex(57,"Primeape","Fighting","",178,150,130),
			new Pokedex(58,"Growlithe","Fire","",156,110,110),
			new Pokedex(59,"Arcanine","Fire","",230,180,180),
			new Pokedex(60,"Poliwag","Water","",108,98,80),
			new Pokedex(61,"Poliwhirl","Water","",132,132,130),
			new Pokedex(62,"Poliwrath","Water","Fighting",180,202,180),
			new Pokedex(63,"Abra","Pyschic","",110,76,50),
			new Pokedex(64,"Kadabra","Pyschic","",150,112,80),
			new Pokedex(65,"Alakazam","Pyschic","",186,152,110),
			new Pokedex(66,"Machop","Fighting","",118,96,140),
			new Pokedex(67,"Machoke","Fighting","",154,144,160),
			new Pokedex(68,"Machamp","Fighting","",198,180,180),
			new Pokedex(69,"Bellsprout","Grass","Poison",158,78,100),
			new Pokedex(70,"Weepinbell","Grass","Poison",190,110,130),
			new Pokedex(71,"Victreebel","Grass","Poison",222,152,160),
			new Pokedex(72,"Tentacool","Water","Poison",106,136,80),
			new Pokedex(73,"Tentacruel","Water","Poison",170,196,160),
			new Pokedex(74,"Geodude","Rock","Ground",106,118,80),
			new Pokedex(75,"Graveler","Rock","Ground",142,156,110),
			new Pokedex(76,"Golem","Rock","Ground",176,198,160),
			new Pokedex(77,"Ponyta","Fire","",168,138,100),
			new Pokedex(78,"Rapidash","Fire","",200,170,130),
			new Pokedex(79,"Slowpoke","Water","Pyschic",110,110,180),
			new Pokedex(80,"Slowbro","Water","Pyschic",184,198,190),
			new Pokedex(81,"Magnemite","Electric","Steel",128,138,50),
			new Pokedex(82,"Magneton","Electric","Steel",186,180,100),
			new Pokedex(83,"Farfetch'd","Normal","Fly",138,132,104),
			new Pokedex(84,"Doduo","Normal","Fly",126,96,70),
			new Pokedex(85,"Dodrio","Normal","Fly",182,150,120),
			new Pokedex(86,"Seel","Water","",104,138,130),
			new Pokedex(87,"Dewgong","Water","Ice",156,192,180),
			new Pokedex(88,"Grimer","Poison","",124,110,160),
			new Pokedex(89,"Muk","Poison","",180,188,210),
			new Pokedex(90,"Shellder","Water","",120,112,60),
			new Pokedex(91,"Cloyster","Water","Ice",196,196,100),
			new Pokedex(92,"Gastly","Ghost","Poison",136,82,60),
			new Pokedex(93,"Haunter","Ghost","Poison",172,118,90),
			new Pokedex(94,"Gengar","Ghost","Poison",204,156,120),
			new Pokedex(95,"Onix","Rock","Ground",90,186,70),
			new Pokedex(96,"Drowzee","Pyschic","",104,140,120),
			new Pokedex(97,"Hypno","Pyschic","",162,196,170),
			new Pokedex(98,"Krabby","Water","",116,110,60),
			new Pokedex(99,"Kingler","Water","",178,168,110),
			new Pokedex(100,"Voltorb","Electric","",102,124,80),
			new Pokedex(101,"Electrode","Electric","",150,174,120),
			new Pokedex(102,"Exeggcute","Grass","Pyschic",110,132,120),
			new Pokedex(103,"Exeggutor","Grass","Pyschic",232,164,190),
			new Pokedex(104,"Cubone","Ground","",102,150,100),
			new Pokedex(105,"Marowak","Ground","",140,202,120),
			new Pokedex(106,"Hitmonlee","Fighting","",148,172,100),
			new Pokedex(107,"Hitmonchan","Fighting","",138,204,100),
			new Pokedex(108,"Lickitung","Normal","",126,160,180),
			new Pokedex(109,"Koffing","Poison","",136,142,80),
			new Pokedex(110,"Weezing","Poison","",190,198,130),
			new Pokedex(111,"Rhyhorn","Ground","Rock",110,116,160),
			new Pokedex(112,"Rhydon","Ground","Rock",166,160,210),
			new Pokedex(113,"Chansey","Normal","",40,60,500),
			new Pokedex(114,"Tangela","Grass","",164,152,130),
			new Pokedex(115,"Kangaskhan","Normal","",142,178,210),
			new Pokedex(116,"Horsea","Water","",122,100,60),
			new Pokedex(117,"Seadra","Water","",176,150,110),
			new Pokedex(118,"Goldeen","Water","",112,126,90),
			new Pokedex(119,"Seaking","Water","",172,160,160),
			new Pokedex(120,"Staryu","Water","",130,128,60),
			new Pokedex(121,"Starmie","Water","Pyschic",194,192,120),
			new Pokedex(122,"Mr. Mime","Pyschic","Fairy",154,196,80),
			new Pokedex(123,"Scyther","Bug","Fly",176,180,140),
			new Pokedex(124,"Jynx","Ice","Pyschic",172,134,130),
			new Pokedex(125,"Electabuzz","Electric","",198,160,130),
			new Pokedex(126,"Magmar","Fire","",214,158,130),
			new Pokedex(127,"Pinsir","Bug","",184,186,130),
			new Pokedex(128,"Tauros","Normal","",148,184,150),
			new Pokedex(129,"Magikarp","Water","",42,84,40),
			new Pokedex(130,"Gyarados","Water","Fly",192,196,190),
			new Pokedex(131,"Lapras","Water","Ice",186,190,260),
			new Pokedex(132,"Ditto","Normal","",110,110,96),
			new Pokedex(133,"Eevee","Normal","",114,128,110),
			new Pokedex(134,"Vaporeon","Water","",186,168,260),
			new Pokedex(135,"Jolteon","Electric","",192,174,130),
			new Pokedex(136,"Flareon","Fire","",238,178,130),
			new Pokedex(137,"Porygon","Normal","",156,158,130),
			new Pokedex(138,"Omanyte","Rock","Water",132,160,70),
			new Pokedex(139,"Omastar","Rock","Water",180,202,140),
			new Pokedex(140,"Kabuto","Rock","Water",148,142,60),
			new Pokedex(141,"Kabutops","Rock","Water",190,190,120),
			new Pokedex(142,"Aerodactyl","Rock","Fly",182,162,160),
			new Pokedex(143,"Snorlax","Normal","",180,180,320),
			new Pokedex(144,"Articuno","Ice","Fly",198,242,180),
			new Pokedex(145,"Zapdos","Electric","Fly",232,194,180),
			new Pokedex(146,"Moltres","Fire","Fly",242,194,180),
			new Pokedex(147,"Dratini","Dragon","",128,110,82),
			new Pokedex(148,"Dragonair","Dragon","",170,152,122),
			new Pokedex(149,"Dragonite","Dragon","Fly",250,212,182)
		   };

		public static Pokedex GetPokemon(int id)
		{
			if (id < 1 || id > 149)
			{
				return null;
			}

			else
			{
				return PokeList[id-1];
			}
		}

		public static Pokedex BestAtt()
		{
			Pokedex best = PokeList[0];
			foreach (Pokedex p in PokeList)
			{
				if(p.Attack > PokeList[p].Attack)
				{
					best = PokeList[p];
				}
				return best;

			}
		}




	}
}
