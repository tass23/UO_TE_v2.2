using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class DragonSkins : Item
	{
		public DragonSkins() : base( 0x1079 )
		{
			LootType = LootType.Blessed;
			Stackable = true;
			Weight = 5.0;
			Hue = 0;
		}

		public DragonSkins( Serial serial ) : base( serial )
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

	public class FireDragonSkins : DragonSkins
	{
		[Constructable]
		public FireDragonSkins()
		{
			Name = "Fire Dragon Skin";
			Hue = 1357;
		}

		public FireDragonSkins( Serial serial ) : base( serial )
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

	public class VenomDragonSkins : DragonSkins
	{
		[Constructable]
		public VenomDragonSkins()
		{
			Name = "Venom Dragon Skin";
			Hue = 1367;
		}
		
		public VenomDragonSkins( Serial serial ) : base( serial )
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

	public class ShadowWyrmSkins : DragonSkins
	{
		[Constructable]
		public ShadowWyrmSkins()
		{
			Name = "Shadow Wyrm Skin";
			Hue = 2051;
		}
		
		public ShadowWyrmSkins( Serial serial ) : base( serial )
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

	public class OldDragonSkins : DragonSkins
	{
		[Constructable]
		public OldDragonSkins()
		{
			Name = "Old Dragon Skin";
			Hue = 2982;
		}
		
		public OldDragonSkins( Serial serial ) : base( serial )
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

	public class MythicalDragonSkins : DragonSkins
	{
		[Constructable]
		public MythicalDragonSkins()
		{
			Name = "Mythical Dragon Skin";
			Hue = 2955;
		}
		
		public MythicalDragonSkins( Serial serial ) : base( serial )
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

	public class AdolescentDragonSkins : DragonSkins
	{
		[Constructable]
		public AdolescentDragonSkins()
		{
			Name = "Adolescent Dragon Skin";
			Hue = 2426;
		}
		
		public AdolescentDragonSkins( Serial serial ) : base( serial )
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

	public class ChimeraSkins : DragonSkins
	{
		[Constructable]
		public ChimeraSkins()
		{
			Name = "Chimera Skin";
			Hue = 67;
		}
		
		public ChimeraSkins( Serial serial ) : base( serial )
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

	public class ElderDragonSkins : DragonSkins
	{
		[Constructable]
		public ElderDragonSkins()
		{
			Name = "Elder Dragon Skin";
			Hue = 2212;
		}
		
		public ElderDragonSkins( Serial serial ) : base( serial )
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

	public class ShadowDrakeSkins : DragonSkins
	{
		[Constructable]
		public  ShadowDrakeSkins()
		{
			Name = "Shadow Drake Skin";
			Hue = 2051;
		}
		
		public ShadowDrakeSkins( Serial serial ) : base( serial )
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

	public class JadeDragonSkins : DragonSkins
	{
		[Constructable]
		public JadeDragonSkins()
		{
			Name = "Jade Dragon Skin";
			Hue = 2963;
		}
		
		public JadeDragonSkins( Serial serial ) : base( serial )
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