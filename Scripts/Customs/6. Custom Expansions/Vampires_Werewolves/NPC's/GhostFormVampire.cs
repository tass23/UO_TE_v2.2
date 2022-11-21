using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class GhostFormVampire : BaseVampire
	{
		[Constructable]
		public GhostFormVampire() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a wandering ghost";
			Body = 26;
			Hue = 0x4001;
			BaseSoundID = 0x482;
			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );
			SetHits( 46, 60 );
			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.EvalInt, 55.1, 70.0 );
			SetSkill( SkillName.Magery, 55.1, 70.0 );
			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 4000;
			Karma = -4000;
			VirtualArmor = 28;
			
			Container pack = new Backpack();
			pack.DropItem( new Gold( 0, 50 ) );
			pack.DropItem( new VampireShroud () );
			pack.Movable = false;
			AddItem( pack );
		}
		
		private DateTime LastCorpseCheck = DateTime.Now;
		private TimeSpan CheckDelay = TimeSpan.FromSeconds( 5.0 );
		public bool CanCheckCorpses()
		{
		     if( LastCorpseCheck.Add( CheckDelay ) < DateTime.Now ) return true;
		     return false;
		}
		
		public override void OnThink() 
		{ 
			if (CanCheckCorpses()) LookForCorpses();
			base.OnThink(); 
		}
		
		public virtual void LookForCorpses()
		{
			foreach ( Item item in GetItemsInRange( 5 ) ) 
			{ 
				if (item is Corpse)
				{
					DebugSay("found corpse");
					Corpse cor = (Corpse) item;
					if (cor.Amount == 400 || cor.Amount == 401)
					{
						DebugSay("found human corpse");
						Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( this, this.Map, 0x201 );
						this.Location = cor.Location;
						StealCorpse(cor);
					}
				}
			} 
		}
		
		public virtual void StealCorpse(Corpse cor)
		{
			{
				DebugSay("stealing corpse");
				Vampire vamp = new Vampire();
				vamp.Female = (cor.Amount == 401? true: false);
				vamp.Body = cor.Amount;
				vamp.Name = NameList.RandomName( (vamp.Female ? "female":"male"));
				vamp.HairHue = 1109;
				vamp.FacialHairHue = 1109;
				DropGear(vamp);
				EquipVamp(cor, vamp);
				FillBackPack(cor, vamp);
				if (Combatant != null) vamp.Combatant = Combatant;
				vamp.MoveToWorld(cor.Location, cor.Map);
				vamp.FixedParticles(0x3709, 1, 30, 9904, 1108, 6, EffectLayer.Head);
				//cor.Delete();				<----------------------------IF NOT COMMENTED: THIS WILL DELETE PLAYER CORPSES TOO INCLUDING GEAR NOT INSURED OR BLESSED
				this.Delete();
			}
		}
		
		public void DropGear(BaseVampire vamp)
		{
			ArrayList equipitems = new ArrayList(vamp.Items);
			foreach (Item item in equipitems)
			{
				if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack))
				{
					item.Delete();
				}
			}
			Container pack = vamp.Backpack;
			ArrayList packitems = new ArrayList( pack.Items );
			foreach (Item items in packitems)
			{
				items.Delete();
			}
		}
		
		public void EquipVamp( Corpse corpse, BaseVampire vamp )
		{
			DebugSay("stealing equipment");
			if( corpse != null && !corpse.Deleted )
			{
                List<Item> toEquip = corpse.EquipItems;
                for( int i = 0; i < toEquip.Count; ++i )
				{
                    vamp.AddItem( (Item)toEquip[ i ] );
				}
			}
		}

		public void FillBackPack( Corpse corpse, BaseVampire vamp )
		{
			DebugSay("stealing items");
			if( corpse != null && !corpse.Deleted )
			{
                List<Item> toTake = corpse.Items;
                for( int i = 0; i < toTake.Count; i++ )
				{
                    Backpack pack = vamp.Backpack as Backpack;
                    pack.DropItem( (Item)toTake[ i ] );
				}
			}
		}

		public GhostFormVampire( Serial serial ) : base( serial )
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
