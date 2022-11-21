using System;
using Server.Network;
using Server.Items;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public abstract class CrystalFormation : Item	
	{
		[Constructable]
		public CrystalFormation() : base( 12263 )
		{
			Movable = false;
			Name = "Mysterious Crystal Formation";
			Light = LightType.Circle150;
		}
		
		public CrystalFormation( Serial serial ) : base( serial )
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

	public class AnkarresFormation : CrystalFormation 
	{
		[Constructable]
		public AnkarresFormation()
		{
			// make the attachment
			CustomData c = new CustomData();
			// assign the properties
			c.Data = "AnkarresFormation";
			// attach it to the object
			XmlAttach.AttachTo(this, c);
		}
		
		public AnkarresFormation( Serial serial ) : base( serial ) 
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

	public class BaasWisdomFormation : CrystalFormation 
	{
		[Constructable]
		public BaasWisdomFormation()
		{
			CustomData c = new CustomData();
			c.Data = "BaasWisdomFormation";
			XmlAttach.AttachTo(this, c);		
		}
		
		public BaasWisdomFormation( Serial serial ) : base( serial ) 
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

	public class KenobisLegacyFormation : CrystalFormation 
	{
		[Constructable]
		public KenobisLegacyFormation()
		{
			CustomData c = new CustomData();
			c.Data = "KenobisLegacyFormation";
			XmlAttach.AttachTo(this, c);
		}
		
		public KenobisLegacyFormation( Serial serial ) : base( serial ) 
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

	public class KraytFormation : CrystalFormation 
	{
		[Constructable]
		public KraytFormation()
		{
			CustomData c = new CustomData();
			c.Data = "KraytFormation";
			XmlAttach.AttachTo(this, c);
		}
		
		public KraytFormation( Serial serial ) : base( serial ) 
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

	public class PermafrostFormation : CrystalFormation 
	{
		[Constructable]
		public PermafrostFormation()
		{
			CustomData c = new CustomData();
			c.Data = "PermafrostFormation";
			XmlAttach.AttachTo(this, c);	
		}
		
		public PermafrostFormation( Serial serial ) : base( serial ) 
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
}