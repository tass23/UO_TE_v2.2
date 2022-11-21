using System;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public abstract class BaseFocusingCrystal : BaseBashing	
	{
		public override int AosMinDamage{ get{ return 1; } }
		public override int AosMaxDamage{ get{ return 3; } }
		public override float MlSpeed{ get{ return 2.25f; } }

		[Constructable]
		public BaseFocusingCrystal() : base( 0x35DA )
		{
			Movable = true;
			LootType = LootType.Blessed;
			Light = LightType.Circle225;
			Weight = 5.0;
			Layer = Layer.TwoHanded;
		}
		
		public BaseFocusingCrystal( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

// 45 Crystals Total
// Blue Focusing Crystals_________________________________________________________________________________________	
	
	public class Blue1FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue1FocusingCrystal()	//Jedi
		{
			Name = "Ankarres Sapphire";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1095;
			WeaponAttributes.ResistColdBonus = 10;
			Attributes.WeaponSpeed = 15;
			Attributes.WeaponDamage = 3;
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 50;
			phys = 50;
			pois = fire = nrgy = chaos = direct = 0;
		}
		
		public Blue1FocusingCrystal( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}

	public class Blue2FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue2FocusingCrystal()	//Exile
		{
			Name = "Baas' Wisdom crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1100;
			Attributes.RegenMana = 4;
			WeaponAttributes.HitColdArea = 40;					
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 70;
			phys = 30;
			pois = fire = nrgy = chaos = direct = 0;
		}
		
		public Blue2FocusingCrystal( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}

	public class Blue3FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue3FocusingCrystal()	//Jedi
		{
			Name = "Kenobi's Legacy";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1185;
			Attributes.WeaponSpeed = 5;
			Attributes.WeaponDamage = 10;
			SkillBonuses.SetValues( 0, SkillName.Healing, 10.0 );					
		}

		public Blue3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}

	public class Blue4FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue4FocusingCrystal()	//Jedi
		{
			Name = "Krayt dragon pearl";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1366;
			WeaponAttributes.HitFireball = 25;
			Attributes.RegenStam = 4;					
		}
		
		public Blue4FocusingCrystal( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}

	public class Blue5FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue5FocusingCrystal()	//Exile
		{
			Name = "Permafrost crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1100;
			WeaponAttributes.HitColdArea = 75;					
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 90;
			phys = 10;
			pois = fire = nrgy = chaos = direct = 0;
		}
		
		public Blue5FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}

	public class Blue6FocusingCrystal : BaseFocusingCrystal 
	{
		private static int[] m_Hues = new int[] 
		{
			1100, 2909, 2471, 1556 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Blue6FocusingCrystal()	//Jedi
		{
			Name = "Upari crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1488;
			WeaponAttributes.HitLeechMana = 40;
			WeaponAttributes.HitLowerDefend = 10;
			Attributes.RegenStam = 2;
		}
		
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 30;
			phys = 70;
			pois = fire = nrgy = chaos = direct = 0;
		}
		
		public Blue6FocusingCrystal( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
	}	

// Green Focusing Crystals________________________________________________________________________________

	public class Green1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Green1FocusingCrystal()	//Jedi
		{
			Name = "Green Adegan crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1067;
			WeaponAttributes.HitLowerAttack = 40;
			WeaponAttributes.ResistColdBonus = 15;
		}

		public Green1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Green2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Green2FocusingCrystal()	//Exile
		{
			Name = "Allya's Redemption crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1075;
			WeaponAttributes.HitPoisonArea = 75;
			WeaponAttributes.ResistPoisonBonus = 80;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 80;
			phys = 20;
			cold = fire = nrgy = chaos = direct = 0;
		}
		
		public Green2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Green3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Green3FocusingCrystal()	//Jedi
		{
			Name = "Bondara's Folly crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1162;
			WeaponAttributes.HitLeechHits = 100;
			Attributes.RegenHits = 5;
		}

		public Green3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Green4FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Green4FocusingCrystal()	//Jedi
		{
			Name = "Dawn of Dagobah crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1062;
			WeaponAttributes.HitPoisonArea = 80;
			WeaponAttributes.HitLowerDefend = 25;
			Attributes.WeaponDamage = 75;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			pois = 90;
			phys = 10;
			cold = fire = nrgy = chaos = direct = 0;
		}

		public Green4FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Green5FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Green5FocusingCrystal()	//Jedi
		{
			Name = "Sunrider's Destiny crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1094;
			WeaponAttributes.HitLeechStam = 100;
			Attributes.WeaponSpeed = 30;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 20;
			phys = 30;
			direct = 50;
			cold = fire = pois = chaos = 0;
		}
		
		public Green5FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Yellow Focusing Crystals___________________________________________________________________________________

	public class Yellow1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1081, 1169, 1281, 2911 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Yellow1FocusingCrystal()	//Jedi
		{
			Name = "Yellow Dragite crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1159;
			WeaponAttributes.HitLowerAttack = 40;
			Attributes.WeaponDamage = 35;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 20;
			phys = 50;
			direct = 30;
			cold = fire = nrgy = pois = 0;
		}
		
		public Yellow1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Yellow2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1081, 1169, 1281, 2911 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Yellow2FocusingCrystal()	//Jedi
		{
			Name = "Heart of the Guardian";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1081;
			Attributes.RegenHits = 3;
			Attributes.RegenStam = 2;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 50;
			direct = 50;
			cold = fire = chaos = nrgy = pois = 0;
		}
		
		public Yellow2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Yellow3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1081, 1169, 1281, 2911 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Yellow3FocusingCrystal()	//Exile
		{
			Name = "Impact crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1161;
			Attributes.WeaponDamage = 75;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			direct = 100;
			cold = phys = fire = chaos = nrgy = pois = 0;
		}
		
		public Yellow3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// White Focusing Crystals________________________________________________________________________________

	public class White1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White1FocusingCrystal()	//Sith
		{
			Name = "Barab ore";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2040;
			WeaponAttributes.HitLightning = 5;
			WeaponAttributes.HitMagicArrow = 10;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 100;
			cold = phys = fire = chaos = direct = pois = 0;
		}
		
		public White1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White2FocusingCrystal()	//Jedi
		{
			Name = "Durindfire crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2962;
			WeaponAttributes.HitLowerAttack = 40;
			WeaponAttributes.HitHarm = 50;
		}

		public White2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White3FocusingCrystal()	//Sith
		{
			Name = "Eralam crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1153;
			Attributes.WeaponDamage = 60;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			direct = 100;
			cold = phys = fire = chaos = nrgy = pois = 0;
		}
		
		public White3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White4FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White4FocusingCrystal()	//Sith
		{
			Name = "Nextor crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1361;
			Attributes.WeaponDamage = 90;
			WeaponAttributes.HitLowerDefend = 45;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			direct = 50;
			phys = 50;
			cold = fire = chaos = nrgy = pois = 0;
		}

		public White4FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White5FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White5FocusingCrystal()	//Jedi
		{
			Name = "Jenruax crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 901;
			Attributes.WeaponSpeed = 75;
		}

		public White5FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White6FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White6FocusingCrystal()	//Exile
		{
			Name = "Rubat crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 781;
			Attributes.WeaponDamage = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			direct = 35;
			phys = 35;
			nrgy = 30;
			cold = fire = chaos = pois = 0;
		}
		
		public White6FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White7FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White7FocusingCrystal()	//Sith
		{
			Name = "Sapith crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2958;
			WeaponAttributes.HitLeechMana = 10;
			Attributes.WeaponDamage = 75;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 50;
			nrgy = 50;
			cold = fire = direct = chaos = pois = 0;
		}
		
		public White7FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class White8FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1001, 1150, 1153, 2955 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public White8FocusingCrystal()	//Jedi
		{
			Name = "Ultima-pearl";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2958;
			WeaponAttributes.HitManaDrain = 25;
			WeaponAttributes.HitPhysicalArea = 100;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 50;
			nrgy = 50;
			cold = fire = direct = chaos = pois = 0;
		}
		
		public White8FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Gray Focusing Crystals__________________________________________________________________________________

	public class Gray1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Gray1FocusingCrystal()	//Sith
		{
			Name = "Blackwing crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1175;
			WeaponAttributes.HitLeechMana = 25;
			Attributes.WeaponSpeed = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 50;
			pois = 50;
			cold = fire = direct = phys = nrgy = 0;
		}
		
		public Gray1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Gray2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Gray2FocusingCrystal()	//Sith
		{
			Name = "Lignan crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 840;
			WeaponAttributes.HitLeechStam = 50;
			Attributes.RegenHits = 5;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 15;
			pois = 15;
			cold = 15;
			fire = 15;
			direct = 15;
			phys = 10;
			nrgy = 15;
		}
		
		public Gray2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Gray3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1102, 1151, 1154, 1301 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Gray3FocusingCrystal()	//Sith
		{
			Name = "Stygium crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2406;
			SkillBonuses.SetValues( 0, SkillName.Stealth, 25.0 );
			Attributes.WeaponDamage = 15;
		}

		public Gray3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Red Focusing Crystals____________________________________________________________________

	public class Red1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red1FocusingCrystal()	//Jedi
		{
			Name = "Bondar crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2118;
			WeaponAttributes.HitFatigue = 10;
			WeaponAttributes.HitCurse = 10;
		}

		public Red1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red2FocusingCrystal()	//Exile
		{
			Name = "Allya's Exile crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1172;
			WeaponAttributes.HitFireball = 50;
			WeaponAttributes.ResistFireBonus = 100;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = 75;
			nrgy = 25;
			cold = chaos = direct = phys = pois = 0;
		}
		
		public Red2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red3FocusingCrystal()	//Sith
		{
			Name = "Cunning of Tyranus crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1481;
			WeaponAttributes.HitMagicArrow = 10;
			WeaponAttributes.HitHarm = 10;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 15;
			pois = 15;
			cold = 15;
			fire = 15;
			direct = 15;
			phys = 10;
			nrgy = 15;
		}
		
		public Red3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red4FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red4FocusingCrystal()	//Sith
		{
			Name = "Phond crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 233;
			WeaponAttributes.HitFireArea = 15;
			Attributes.WeaponDamage = 5;
		}

		public Red4FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red5FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red5FocusingCrystal()	//Exile
		{
			Name = "Qixoni crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2118;
			Attributes.WeaponSpeed = 45;
			Attributes.WeaponDamage = 65;
		}

		public Red5FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red6FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red6FocusingCrystal()	//Sith
		{
			Name = "Sigil crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1552;
			WeaponAttributes.HitLeechStam = 15;
			Attributes.RegenHits = 2;
			Attributes.WeaponSpeed = 30;
		}

		public Red6FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Red7FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			39, 233, 1172, 2910 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Red7FocusingCrystal()	//Sith
		{
			Name = "Synthetic crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1477;
			Attributes.WeaponSpeed = 55;
			Attributes.WeaponDamage = 100;
		}

		public Red7FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Pink Focusing Crystals_____________________________________________________________________________

	public class Pink1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1168, 1398, 1468, 2906 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Pink1FocusingCrystal()	//Sith
		{
			Name = "Damind crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1580;
			WeaponAttributes.HitLeechHits = 20;
			WeaponAttributes.HitLeechMana = 20;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 25;
			nrgy = 75;
			cold = chaos = direct = fire = pois = 0;
		}
		
		public Pink1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Pink2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1168, 1398, 1468, 2906 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Pink2FocusingCrystal()	//Jedi
		{
			Name = "Lorridian gemstone";
            Hue = Utility.RandomList( m_Hues );
			//Hue = 1168;
			Attributes.ReflectPhysical = Utility.RandomMinMax(8, 21);
            Attributes.RegenHits = Utility.RandomMinMax(3, 7);
		}

		public Pink2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Pink3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1168, 1398, 1468, 2906 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Pink3FocusingCrystal()	//Jedi
		{
			Name = "Ruusan crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 26;
			SkillBonuses.SetValues( 0, SkillName.Meditation, 25.0 );
			Attributes.WeaponSpeed = 50;
		}

		public Pink3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Purple Focusing Crystals_____________________________________________________________________

	public class Purple1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1163, 1170, 1460 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Purple1FocusingCrystal()	//Jedi
		{
			Name = "Hurrikaine crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 14;
			Attributes.ReflectPhysical = Utility.RandomMinMax(15, 25);
			Attributes.WeaponDamage = 60;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 25;
			nrgy = 75;
			cold = chaos = direct = fire = pois = 0;
		}
		
		public Purple1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Purple2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1163, 1170, 1460 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Purple2FocusingCrystal()	//Jedi
		{
			Name = "Windu's Guile crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1277;
			Attributes.ReflectPhysical = Utility.RandomMinMax(20, 31);
			WeaponAttributes.HitLeechHits = 100;
			Attributes.WeaponDamage = 75;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 15;
			nrgy = 85;
			cold = chaos = direct = fire = pois = 0;
		}

		public Purple2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Orange Focusing Crystals______________________________________________________________________________

	public class Orange1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Orange1FocusingCrystal()	//Jedi
		{
			Name = "Lambent crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2116;
			SkillBonuses.SetValues( 0, SkillName.Meditation, 10.0 );
			SkillBonuses.SetValues( 0, SkillName.Focus, 10.0 );
		}

		public Orange1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class Orange2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Orange2FocusingCrystal()	//Sith
		{
			Name = "Lava crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 39;
			WeaponAttributes.HitFireArea = 15;
			WeaponAttributes.HitFireball = 15;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = 100;
			cold = chaos = direct = nrgy = phys = pois = 0;
		}
		
		public Orange2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Orange3FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Orange3FocusingCrystal()	//Jedi
		{
			Name = "Solari crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 44;
			SkillBonuses.SetValues( 0, SkillName.DetectHidden, 20.0 );
			SkillBonuses.SetValues( 0, SkillName.Magery, 20.0 );
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 50;
			fire = 50;
			cold = chaos = direct = phys = pois = 0;
		}
		
		public Orange3FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Orange4FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1161, 1259, 1358, 2907 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Orange4FocusingCrystal()	
		{
			Name = "Velmorite crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 48;
			Attributes.ReflectPhysical = Utility.RandomMinMax(20, 31);
			Attributes.WeaponDamage = 45;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 50;
			fire = 50;
			cold = chaos = direct = phys = pois = 0;
		}
		
		public Orange4FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Cyan Focusing Crystals______________________________________________________________________________

	public class Cyan1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1173, 1366, 1391, 2908 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Cyan1FocusingCrystal()	//Jedi
		{			
			Name = "Mantle of the Force";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1366;			
			WeaponAttributes.HitLeechStam = Utility.RandomMinMax(75, 85);
			Attributes.WeaponDamage = Utility.RandomMinMax(25, 50);
		}

		public Cyan1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Cyan2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1173, 1366, 1391, 2908 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Cyan2FocusingCrystal()	//Jedi
		{	
			Name = "Meditation crystal";
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1328;			
			SkillBonuses.SetValues( 0, SkillName.Meditation, 15.0 );
			SkillBonuses.SetValues( 0, SkillName.Focus, 15.0 );
			WeaponAttributes.HitLeechMana = Utility.RandomMinMax(35, 41);
		}

		public Cyan2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
// Brown Focusing Crystals________________________________________________________________________

	public class Brown1FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1867, 2110 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Brown1FocusingCrystal()	//Exile
		{	
			Name = "Ulric's Redemption crystal";		
			Hue = Utility.RandomList( m_Hues );
			//Hue = 1867;			
			Attributes.RegenStam = 5;
			Attributes.WeaponDamage = 55;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 50;
			fire = 50;
			cold = nrgy = direct = phys = pois = 0;
		}
		
		public Brown1FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class Brown2FocusingCrystal : BaseFocusingCrystal
	{
		private static int[] m_Hues = new int[] 
		{
			1867, 2110 /*UO-The Expanse Custom Hues*/
		};

		[Constructable]
		public Brown2FocusingCrystal()	//Sith
		{	
			Name = "Vexxtal crystal";		
			Hue = Utility.RandomList( m_Hues );
			//Hue = 2110;
			WeaponAttributes.HitHarm = 50;
			WeaponAttributes.HitPoisonArea = 50;
		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			chaos = 50;
			pois = 50;
			cold = nrgy = direct = phys = fire = 0;
		}
		
		public Brown2FocusingCrystal( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}