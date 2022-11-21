using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class DragonHead : Item
	{
		[Constructable]
		public DragonHead() : base( 0x2DB4 )
		{
			LootType = LootType.Blessed;
			Weight = 50;
			Hue = 0;
		}

		public DragonHead( Serial serial ) : base( serial )
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

	public class FireDragonHead : DragonHead
	{
		[Constructable]
		public FireDragonHead()
		{
			Name = "a Fire Dragon Head";
			Hue = 1357;
		}
		
		public FireDragonHead( Serial serial ) : base( serial )
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

	public class VenomDragonHead : DragonHead
	{
		[Constructable]
		public VenomDragonHead()
		{
			Name = "a Venom Dragon Head";
			Hue = 1367;
		}
		
		public VenomDragonHead( Serial serial ) : base( serial )
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

	public class ShadowWyrmHead : DragonHead
	{
		[Constructable]
		public ShadowWyrmHead()
		{
			Name = "a Shadow Wyrm Head";
			Hue = 2051;
		}
		
		public ShadowWyrmHead( Serial serial ) : base( serial )
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

	public class OldDragonHead : DragonHead
	{
		[Constructable]
		public OldDragonHead()
		{
			Name = "an Old Dragon Head";
			Hue = 2982;
		}
		
		public OldDragonHead( Serial serial ) : base( serial )
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

	public class MythicalDragonHead : DragonHead
	{
		[Constructable]
		public MythicalDragonHead()
		{
			Name = "a Mythical Dragon Head";
			Hue = 2955;
		}
		
		public MythicalDragonHead( Serial serial ) : base( serial )
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

	public class AdolescentDragonHead : DragonHead
	{
		[Constructable]
		public AdolescentDragonHead()
		{
			Name = "an Adolescent Dragon Head";
			Hue = 2426;
		}
		
		public AdolescentDragonHead( Serial serial ) : base( serial )
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

	public class ChimeraHead : DragonHead
	{
		[Constructable]
		public ChimeraHead()
		{
			Name = "a Chimera Head";
			Hue = 67;
		}
		
		public ChimeraHead( Serial serial ) : base( serial )
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

	public class ElderDragonHead : DragonHead
	{
		[Constructable]
		public ElderDragonHead()
		{
			Name = "an Elder Dragon Head";
			Hue = 2212;
		}
		
		public ElderDragonHead( Serial serial ) : base( serial )
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

	public class ShadowDrakeHead : DragonHead
	{
		[Constructable]
		public  ShadowDrakeHead()
		{
			Name = "a Shadow Drake Head";
			Hue = 2051;
		}
		
		public ShadowDrakeHead( Serial serial ) : base( serial )
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

	public class JadeDragonHead : DragonHead
	{
		[Constructable]
		public  JadeDragonHead()
		{
			Name = "a Jade Dragon Head";
			Hue = 2963;
		}
		
		public JadeDragonHead( Serial serial ) : base( serial )
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