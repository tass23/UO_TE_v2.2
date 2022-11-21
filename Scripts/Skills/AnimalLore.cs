using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Xanthos.Utilities;
using Xanthos.Interfaces;

namespace Server.SkillHandlers
{
	public class AnimalLore
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.AnimalLore].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTarget();

			m.SendLocalizedMessage( 500328 ); // What animal should I look at?

			return TimeSpan.FromSeconds( 1.0 );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !from.Alive )
				{
					from.SendLocalizedMessage( 500331 ); // The spirits of the dead are not the province of animal lore.
				}
				else if ( targeted is BaseCreature )
				{
					BaseCreature c = (BaseCreature)targeted;

					if ( !c.IsDeadPet )
					{
						#region UO-The Expanse
						if (( c.Body.IsAnimal || c.Body.IsMonster || c.Body.IsSea ) || ( from.AccessLevel >= AccessLevel.GameMaster ))	//( c.Body.IsAnimal || c.Body.IsMonster || c.Body.IsSea )
						#endregion
						{
							if ( (!c.Controlled || !c.Tamable) && from.Skills[SkillName.AnimalLore].Value < 100.0 )
							{
								from.SendLocalizedMessage( 1049674 ); // At your skill level, you can only lore tamed creatures.
							}
							else if ( !c.Tamable && from.Skills[SkillName.AnimalLore].Value < 110.0 )
							{
								from.SendLocalizedMessage( 1049675 ); // At your skill level, you can only lore tamed or tameable creatures.
							}
							else if ( !from.CheckTargetSkill( SkillName.AnimalLore, c, 0.0, 120.0 ) )
							{
								from.SendLocalizedMessage( 500334 ); // You can't think of anything you know offhand.
							}
							else
							{
								#region UO-The Expanse
								if ( from.AccessLevel == AccessLevel.Player )
								{
									from.CloseGump( typeof( AnimalLoreGump ) );
									from.SendGump( new AnimalLoreGump( c, from ) );
								}
								else
									from.SendGump( new AnimalLoreGump( c, from ) );
								#endregion
								//from.CloseGump( typeof( AnimalLoreGump ) );
								//from.SendGump( new AnimalLoreGump( c, from ) );
							}
						}
						else
						{
							from.SendLocalizedMessage( 500329 ); // That's not an animal!
						}
					}
					else
					{
						from.SendLocalizedMessage( 500331 ); // The spirits of the dead are not the province of animal lore.
					}
				}
				else
				{
					from.SendLocalizedMessage( 500329 ); // That's not an animal!
				}
			}
		}
	}

	public class AnimalLoreGump : Gump
	{
		private BaseCreature m_Bc;
		private Mobile m_From;
		
		private static string FormatExp( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}
		
		private static string FormatSkill( BaseCreature c, SkillName name )
		{
			Skill skill = c.Skills[name];

			if ( skill.Base < 10.0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", skill.Value );
		}

		private static string FormatAttributes( int cur, int max )
		{
			if ( max == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}/{1}</div>", cur, max );
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}

		private static string FormatDouble( double val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", val );
		}

		private static string FormatElement( int val )
		{
			if ( val <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}%</div>", val );
		}

		#region Mondain's Legacy
		private static string FormatDamage( int min, int max )
		{
			if ( min <= 0 || max <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}-{1}</div>", min, max );
		}
		#endregion

		private const int LabelColor = 0x24E5;
		
		public AnimalLoreGump( BaseCreature pet, Mobile from ) : base( 250, 50 )
		{
		
			m_Bc = pet;
			m_From = from;
		
			BaseCreature bc = (BaseCreature)pet;
			Mobile cm = bc.ControlMaster;
			
			int nextLevel = bc.NextLevel * bc.Level;
			
			AddPage( 0 );

			AddImage( 100, 100, 2080 );
			AddImage( 118, 137, 2081 );
			AddImage( 118, 207, 2081 );
			AddImage( 118, 277, 2081 );
			AddImage( 118, 347, 2081 );
			AddImage( 118, 417, 2081 );
			AddImage( 118, 487, 2081 );
			AddImage( 118, 557, 2083 );

			AddHtml( 147, 114, 208, 18, String.Format( "<center><i>{0}</i></center>", pet.Name ), false, false );

			AddButton( 240, 77, 2093, 2093, 4, GumpButtonType.Reply, 0 );

			AddImage( 140, 134, 2091 );
			AddImage( 140, 546, 2091 );

			int pages = ( Core.AOS ? 5 : 3 );
			int page = 0;


			#region Attributes
			AddPage( ++page );

			//AddImage( 128, 152, 2086 );
			//AddHtmlLocalized( 147, 150, 160, 18, 1049593, 200, false, false ); // Attributes

			AddHtmlLocalized( 130, 151, 55, 18, 1049578, LabelColor, false, false ); // Hits
			AddHtml( 180, 151, 75, 18, FormatAttributes( pet.Hits, pet.HitsMax ), false, false );

			AddHtmlLocalized( 130, 169, 55, 18, 1049579, LabelColor, false, false ); // Stamina
			AddHtml( 180, 169, 75, 18, FormatAttributes( pet.Stam, pet.StamMax ), false, false );

			AddHtmlLocalized( 130, 187, 55, 18, 1049580, LabelColor, false, false ); // Mana
			AddHtml( 180, 187, 75, 18, FormatAttributes( pet.Mana, pet.ManaMax ), false, false );

			AddHtmlLocalized( 270, 151, 70, 18, 1028335, LabelColor, false, false ); // Strength
			AddHtml( 330, 151, 40, 18, FormatStat( pet.Str ), false, false );

			AddHtmlLocalized( 270, 169, 70, 18, 3000113, LabelColor, false, false ); // Dexterity
			AddHtml( 330, 169, 40, 18, FormatStat( pet.Dex ), false, false );

			AddHtmlLocalized( 270, 187, 70, 18, 3000112, LabelColor, false, false ); // Intelligence
			AddHtml( 330, 187, 40, 18, FormatStat( pet.Int ), false, false );

			if ( Core.AOS )
			{
				int y = 205;

				if ( Core.SE )
				{
					double bd = Items.BaseInstrument.GetBaseDifficulty( pet );
					if ( pet.Uncalmable )
						bd = 0;

					AddHtmlLocalized( 270, 205, 70, 18, 1070793, LabelColor, false, false ); // Barding Difficulty
					AddHtml( 332, y, 40, 18, FormatDouble( bd ), false, false );

					y += 18;
				}

				AddImage( 128, 231, 2086 );
				AddHtmlLocalized( 147, 231, 160, 18, 1049594, 200, false, false ); // Loyalty Rating
				y += 18;

				AddHtmlLocalized( 153, 249, 160, 18, (!pet.Controlled || pet.Loyalty == 0) ? 1061643 : 1049595 + (pet.Loyalty / 10), LabelColor, false, false );
				
				AddImage( 128, 274, 2086 );
				AddHtmlLocalized( 147, 269, 160, 18, 3001016, 200, false, false ); // Miscellaneous

				AddHtmlLocalized( 153, 287, 160, 18, 1049581, LabelColor, false, false ); // Armor Rating
				AddHtml( 225, 287, 35, 18, FormatStat( pet.VirtualArmor ), false, false );
			}
			/*
			else
			{
				AddImage( 128, 278, 2086 );
				AddHtmlLocalized( 147, 273, 160, 18, 3001016, 200, false, false ); // Miscellaneous

				AddHtmlLocalized( 153, 291, 160, 18, 1049581, LabelColor, false, false ); // Armor Rating
				AddHtml( 320, 291, 35, 18, FormatStat( pet.VirtualArmor ), false, false );
			}
			*/

			//AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
			//AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, pages );
			#endregion

			#region Resistances
			if ( Core.AOS )
			{
				//AddPage( ++page );

				AddImage( 128, 310, 2086 );
				AddHtmlLocalized( 147, 310, 100, 18, 1061645, 200, false, false ); // Resistances

				AddImage( 153, 333, 10006, 2383 );
				AddHtmlLocalized( 170, 328, 90, 18, 1061646, LabelColor, false, false ); // Physical
				AddHtml( 220, 328, 35, 18, FormatElement( pet.PhysicalResistance ), false, false );
				
				AddImage( 153, 349, 10006, 37 );
				AddHtmlLocalized( 170, 346, 90, 18, 1061647, LabelColor, false, false ); // Fire
				AddHtml( 220, 348, 35, 18, FormatElement( pet.FireResistance ), false, false );

				AddImage( 153, 367, 10006, 2 );
				AddHtmlLocalized( 170, 364, 90, 18, 1061648, LabelColor, false, false ); // Cold
				AddHtml( 220, 364, 35, 18, FormatElement( pet.ColdResistance ), false, false );

				AddImage( 153, 385, 10006, 62 );
				AddHtmlLocalized( 170, 382, 90, 18, 1061649, LabelColor, false, false ); // Poison
				AddHtml( 220, 382, 35, 18, FormatElement( pet.PoisonResistance ), false, false );

				AddImage( 153, 403, 10006, 22 );
				AddHtmlLocalized( 170, 400, 90, 18, 1061650, LabelColor, false, false ); // Energy
				AddHtml( 220, 400, 35, 18, FormatElement( pet.EnergyResistance ), false, false );

				//AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
				//AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			}
			#endregion

			#region Damage
			if ( Core.AOS )
			{
				//AddPage( ++page );

				AddImage( 280, 311, 2086 );
				AddHtmlLocalized( 299, 310, 100, 18, 1017319, 200, false, false ); // Damage
				
				AddImage( 305, 331, 10006, 2383 );
				//AddHtmlLocalized( 153, 168, 160, 18, 1061646, LabelColor, false, false ); // Physical
				AddHtml( 322, 328, 35, 18, FormatElement( pet.PhysicalDamage ), false, false );
				
				AddImage( 305, 349, 10006, 37 );
				//AddHtmlLocalized( 153, 186, 160, 18, 1061647, LabelColor, false, false ); // Fire
				AddHtml( 322, 346, 35, 18, FormatElement( pet.FireDamage ), false, false );

				AddImage( 305, 367, 10006, 2 );
				//AddHtmlLocalized( 153, 204, 160, 18, 1061648, LabelColor, false, false ); // Cold
				AddHtml( 322, 364, 35, 18, FormatElement( pet.ColdDamage ), false, false );

				AddImage( 305, 385, 10006, 62 );
				//AddHtmlLocalized( 153, 222, 160, 18, 1061649, LabelColor, false, false ); // Poison
				AddHtml( 322, 382, 35, 18, FormatElement( pet.PoisonDamage ), false, false );

				AddImage( 305, 403, 10006, 22 );
				//AddHtmlLocalized( 153, 240, 160, 18, 1061650, LabelColor, false, false ); // Energy
				AddHtml( 322, 400, 35, 18, FormatElement( pet.EnergyDamage ), false, false );

				#region Mondain's Legacy
				if ( Core.ML )
				{
					AddHtmlLocalized( 130, 205, 70, 18, 1076750, LabelColor, false, false ); // Base Damage
					AddHtml( 180, 205, 75, 18, FormatDamage( pet.DamageMin, pet.DamageMax ), false, false );
				}
				#endregion

				//AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
				//AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			}
			#endregion

			#region Skills
			//AddPage( ++page );

			AddImage( 128, 423, 2086 );
			AddHtmlLocalized( 147, 423, 160, 18, 3001030, 200, false, false ); // Combat Ratings

			AddHtmlLocalized( 153, 441, 90, 18, 1044103, LabelColor, false, false ); // Wrestling
			AddHtml( 323, 441, 35, 18, FormatSkill( pet, SkillName.Wrestling ), false, false );

			AddHtmlLocalized( 153, 459, 90, 18, 1044087, LabelColor, false, false ); // Tactics
			AddHtml( 323, 459, 35, 18, FormatSkill( pet, SkillName.Tactics ), false, false );

			AddHtmlLocalized( 153, 477, 90, 18, 1044086, LabelColor, false, false ); // Magic Resistance
			AddHtml( 323, 477, 35, 18, FormatSkill( pet, SkillName.MagicResist ), false, false );

			AddHtmlLocalized( 153, 495, 90, 18, 1044061, LabelColor, false, false ); // Anatomy
			AddHtml( 323, 495, 35, 18, FormatSkill( pet, SkillName.Anatomy ), false, false );

			#region Mondain's Legacy
			if ( pet is CuSidhe )
			{
				AddHtmlLocalized( 153, 531, 90, 18, 1044077, LabelColor, false, false ); // Healing
				AddHtml( 323, 531, 35, 18, FormatSkill( pet, SkillName.Healing ), false, false );
			}
			else if ( pet is RainbowCuSidhe )
			{
				AddHtmlLocalized( 153, 531, 90, 18, 1044077, LabelColor, false, false ); // Healing
				AddHtml( 323, 531, 35, 18, FormatSkill( pet, SkillName.Healing ), false, false );
			}
			else
			{
				AddHtmlLocalized( 153, 513, 90, 18, 1044090, LabelColor, false, false ); // Poisoning
				AddHtml( 323, 513, 35, 18, FormatSkill( pet, SkillName.Poisoning ), false, false );
			}

			AddButton( 343, 565, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
			//AddButton( 320, 565, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			#endregion
			AddPage( ++page );
			
			AddImage( 128, 151, 2086 );
			AddHtmlLocalized( 147, 151, 160, 18, 3001032, 200, false, false ); // Lore & Knowledge

			AddHtmlLocalized( 153, 169, 90, 18, 1044085, LabelColor, false, false ); // Magery
			AddHtml( 323, 169, 35, 18, FormatSkill( pet, SkillName.Magery ), false, false );

			AddHtmlLocalized( 153, 187, 90, 18, 1044076, LabelColor, false, false ); // Evaluating Intelligence
			AddHtml( 323, 187, 35, 18,FormatSkill( pet, SkillName.EvalInt ), false, false );

			AddHtmlLocalized( 153, 205, 90, 18, 1044106, LabelColor, false, false ); // Meditation
			AddHtml( 323, 205, 35, 18, FormatSkill( pet, SkillName.Meditation ), false, false );

			#endregion

			#region Misc
			//AddPage( ++page );

			AddImage( 128, 223, 2086 );
			AddHtmlLocalized( 147, 223, 160, 18, 1049563, 200, false, false ); // Preferred Foods

			int foodPref = 3000340;

			if ( (pet.FavoriteFood & FoodType.FruitsAndVegies) != 0 )
				foodPref = 1049565; // Fruits and Vegetables
			else if ( (pet.FavoriteFood & FoodType.GrainsAndHay) != 0 )
				foodPref = 1049566; // Grains and Hay
			else if ( (pet.FavoriteFood & FoodType.Fish) != 0 )
				foodPref = 1049568; // Fish
			else if ( (pet.FavoriteFood & FoodType.Meat) != 0 )
				foodPref = 1049564; // Meat
			else if ( (pet.FavoriteFood & FoodType.Eggs) != 0 )
				foodPref = 1044477; // Eggs
            else if ((pet.FavoriteFood & FoodType.Metal) != 0)
                foodPref = 1049567; // Metal

			AddHtmlLocalized( 153, 241, 160, 18, foodPref, LabelColor, false, false );

			AddImage( 128, 259, 2086 );
			AddHtmlLocalized( 147, 259, 160, 18, 1049569, 200, false, false ); // Pack Instincts

			int packInstinct = 3000340;

			if ( (pet.PackInstinct & PackInstinct.Canine) != 0 )
				packInstinct = 1049570; // Canine
			else if ( (pet.PackInstinct & PackInstinct.Ostard) != 0 )
				packInstinct = 1049571; // Ostard
			else if ( (pet.PackInstinct & PackInstinct.Feline) != 0 )
				packInstinct = 1049572; // Feline
			else if ( (pet.PackInstinct & PackInstinct.Arachnid) != 0 )
				packInstinct = 1049573; // Arachnid
			else if ( (pet.PackInstinct & PackInstinct.Daemon) != 0 )
				packInstinct = 1049574; // Daemon
			else if ( (pet.PackInstinct & PackInstinct.Bear) != 0 )
				packInstinct = 1049575; // Bear
			else if ( (pet.PackInstinct & PackInstinct.Equine) != 0 )
				packInstinct = 1049576; // Equine
			else if ( (pet.PackInstinct & PackInstinct.Bull) != 0 )
				packInstinct = 1049577; // Bull

			AddHtmlLocalized( 153, 277, 160, 18, packInstinct, LabelColor, false, false );
			//Begin Pet Leveling Area
			if ( pet is BaseBioCreature || pet is BioCreature || pet is BioMount )
			{
			}
			else if ( m_From == cm == true && FSATS.EnablePetLeveling == true )
			{		
				bool nolevel = false;
				Type typ = pet.GetType();
				string nam = typ.Name;
				int petExp = bc.Exp;
				
				foreach  ( string check in FSATS.NoLevelCreatures )
				{
					if ( check == nam )
							nolevel = true;
				}

				if ( nolevel != true )
				{
					AddImage( 128, 295, 2086 );
					AddHtml( 147, 295, 60, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Experience</BASEFONT></div>" ), false, false );
					AddHtml( 153, 313, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Current</BASEFONT></div>" ), false, false );
					AddHtml( 153, 331, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Needed</BASEFONT></div>" ), false, false );
					
					AddImage( 128, 349, 2086 );
					AddHtml( 148, 349, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Level</BASEFONT></div>" ), false, false );
					AddHtml( 153, 367, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Current</BASEFONT></div>" ), false, false );
					AddHtml( 153, 385, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Max</BASEFONT></div>" ), false, false );
					
					AddImage( 128, 403, 2086 );
					AddHtml( 148, 403, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Breeding</BASEFONT></div>" ), false, false );
					AddHtml( 153, 421, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Gender</BASEFONT></div>" ), false, false );
					AddHtml( 153, 439, 55, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Breedable</BASEFONT></div>" ), false, false );
					//AddLabel(130, 323, 686, @"Exp:");
					//AddLabel(130, 343, 686, @"Current Level:");
					//AddLabel(130, 363, 686, @"Maximum Level:");
					//AddLabel(130, 383, 686, @"Exp Till Next Level:");
					//AddLabel(130, 403, 686, @"Gender:");
					//AddLabel(130, 423, 686, @"Can Mate:");
					if ( bc.Level >= 35 )
					{
						AddImage( 128, 439, 2086 );
						AddHtml(148, 457, 95, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Elemental Points</BASEFONT></div>" ), false, false );
						if ( bc.Level >= 35 && bc.Level <= 39 )
						{
							AddHtml(153, 475, 95, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Element: Fire</BASEFONT></div>" ), false, false );
						}
						if ( bc.Level >= 40 && bc.Level <= 44 )
						{
							AddHtml(153, 475, 95, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Element: Cold</BASEFONT></div>" ), false, false );
						}
						if ( bc.Level >= 45 && bc.Level <= 49 )
						{
							AddHtml(153, 475, 95, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Element: Poison</BASEFONT></div>" ), false, false );
						}
						if ( bc.Level >= 50 )
						{
							AddHtml(153, 475, 95, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Element: Energy</BASEFONT></div>" ), false, false );
						}
					}
					else
					{
						AddHtml( 153, 475, 85, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Ability Points</BASEFONT></div>" ), false, false );
						//AddLabel(130, 463, 686, @"Ability Points:");
					}
					AddHtml( 153, 493, 85, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#52442a>Generation</BASEFONT></div>" ), false, false );
					
					AddImage( 128, 511, 2086 );
					AddHtml( 148, 511, 75, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Owner</BASEFONT></div>" ), false, false );
					//AddLabel(130, 503, 686, @"Owner:");
					//AddLabel(130, 523, 686, @"Generation:");
					
					AddHtml( 303, 313, 55, 18, FormatExp ( pet.Exp ), false, false );
					AddHtml( 303, 331, 55, 18, FormatExp ( nextLevel ), false, false );
					AddHtml( 303, 367, 55, 18, FormatExp ( pet.Level ), false, false );
					AddHtml( 303, 385, 55, 18, FormatExp ( pet.MaxLevel ), false, false );
					//AddLabel(220, 323, 646, bc.Exp.ToString() );
					//AddLabel(220, 343, 646, bc.Level.ToString() );
					//AddLabel(220, 363, 646, bc.MaxLevel.ToString() );
					//AddLabel(252, 383, 646, nextLevel.ToString() );

			
					if ( m_From == cm && bc.AllowMating == true && FSATS.EnablePetBreeding == true )
					{
						AddButton(360, 458, 2224, 2117, 1, GumpButtonType.Reply, 0);
						AddHtml( 153, 457, 180, 18, String.Format ( @"<div align=left><BASEFONT COLOR=#002a43>Mate this pet with another?</BASEFONT></div>" ), false, false );
						//AddLabel(130, 461, 686, @"Mate this pet with another.");
					}
					if ( bc.AbilityPoints != 0 && m_From == cm && bc.Level < 35)
					{
						AddButton(360, 477, 2224, 2117, 3, GumpButtonType.Reply, 0);
					}
					else if ( bc.AbilityPoints != 0 && m_From == cm && bc.Level >= 35)
					{
						AddButton(360, 477, 2224, 2117, 4, GumpButtonType.Reply, 0);
					}
					
					if ( bc.Female != true )
					{
						AddHtml( 320, 421, 55, 18, String.Format ( @"<div align=right><BASEFONT COLOR=Black>Male</BASEFONT></div>" ), false, false );
						//AddLabel(177, 403, 646, @"Male");
					}
					else
					{
						AddHtml( 320, 421, 55, 18, String.Format ( @"<div align=right><BASEFONT COLOR=Black>Female</BASEFONT></div" ), false, false );
						//AddLabel(177, 403, 646, @"Female");
					}
					if ( bc.AllowMating != true )
					{
						AddHtml( 320, 439, 55, 18, String.Format ( @"<div align=right><BASEFONT COLOR=Black>No</BASEFONT></div>" ), false, false );
						//AddLabel(192, 423, 646, @"No");
					}
					else
					{
						AddHtml( 320, 439, 55, 18, String.Format ( @"<div align=right><BASEFONT COLOR=Black>Yes</BASEFONT></div>" ), false, false );
						//AddLabel(192, 423, 646, @"Yes");
					}
					if ( bc.Level >= 35 )
					{
						AddHtml( 303, 475, 55, 18, FormatExp ( pet.AbilityPoints ), false, false );
						//AddLabel(234, 443, 646, bc.AbilityPoints.ToString() );
					}
					else
					{
						AddHtml( 303, 475, 55, 18, FormatExp ( pet.AbilityPoints ), false, false );
						//AddLabel(219, 448, 646, bc.AbilityPoints.ToString() );
					}

					/*
					if ( bc.Evolves != true )
					{
						AddLabel(180, 483, 446, @"No");
					}
					else
					{
						AddLabel(180, 483, 446, @"Yes");
					}
					*/
					if ( cm != null )
					{
						AddHtml( 300, 511, 75, 18, String.Format( "<div align=right><BASEFONT COLOR=Black>{0}</BASEFONT></div>", cm.Name ), false, false );
						//AddLabel(303, 515, 646, cm.Name.ToString() );
					}
					else
					{
						AddHtml( 300, 511, 75, 18, String.Format ( "<div align=right><BASEFONT COLOR=Black>Unowned</BASEFONT></div>" ), false, false );
						//AddLabel(172, 503, 646, @"unowned" );
					}
					AddHtml( 303, 493, 55, 18, FormatExp ( pet.Generation ), false, false );
					//AddLabel(197, 523, 646, bc.Generation.ToString() );
				}
			}
			//End Pet Leveling Area
			/*
			if ( !Core.AOS )
			{
				AddImage( 128, 224, 2086 );
				AddHtmlLocalized( 147, 222, 160, 18, 1049594, 200, false, false ); // Loyalty Rating

				AddHtmlLocalized( 153, 240, 160, 18, (!c.Controlled || c.Loyalty == 0) ? 1061643 : 1049595 + (c.Loyalty / 10), LabelColor, false, false );
			}
			*/
			
			//AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, 1 );
			//AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );

			//AddButton( 343, 565, 5601, 5605, 0, GumpButtonType.Page, 1 );
			AddButton( 320, 565, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			#endregion
		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 1 )
			{
				Mobile breeder = new Mobile();

				foreach ( Mobile m in from.GetMobilesInRange( 5 ) )
				{
					if ( m is AnimalBreeder )
						breeder = m;
				}

				if ( breeder is AnimalBreeder )
				{
					from.SendMessage( "What creature would you like your pet to breed with?" );
           				from.Target = new BeginMatingTarget( m_Bc );
				}
				else
				{
					from.SendMessage( "You must be near an animal breeder in order to breed your pet." );
				}
			}

			if ( info.ButtonID == 2 )
			{
			}

			if ( info.ButtonID == 3 )
			{
				from.SendGump( new PetLevelGump( m_Bc ) );
			}
			if ( info.ButtonID == 4 )
			{
				from.SendGump( new ElementalLevelGump( m_Bc ) );
			}
		}
	}
  	public class BeginMatingTarget : Target 
      	{
		private BaseCreature m_Pet;

         	public BeginMatingTarget( BaseCreature pet ) : base ( 10, false, TargetFlags.None ) 
         	{ 
            		m_Pet = pet; 
         	} 
          
         	protected override void OnTarget( Mobile from, object target ) 
         	{
			if ( target is PlayerMobile )
			{
				from.SendMessage( "Huh? But the children would be so ugly" );
			}
			else if ( target is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)target;

				Type targettype = bc.GetType();
				Type pettype = m_Pet.GetType();
				Mobile breeder = new Mobile();
				Mobile owner = new Mobile();

				foreach ( Mobile m in bc.GetMobilesInRange( 5 ) )
				{
					if ( m is AnimalBreeder )
						breeder = m;

					if ( m == bc.ControlMaster )
						owner = m;
				}

				if ( bc.Controlled != true )
				{
					from.SendMessage( "That creature is not tamed." );
				}
				else if ( bc.ControlMaster == null )
				{
					from.SendMessage( "That creature has no master." );
				}
				else if ( bc.MatingDelay >= DateTime.Now )
				{
					from.SendMessage( "That creature has mated in that last six days, It cannot mate again so soon." );
				}
				else if ( bc.ControlMaster == from )
				{
					from.SendMessage( "You cannot breed two of your own pets together, You must find another player who has the same type of pet as your in order to breed." );
				}
				else if ( breeder == null )
				{
					from.SendMessage( "You must be near an animal breeder in order to breed your pet." );
				}
				else if ( owner == null )
				{
					from.SendMessage( "The owner of that pet is not near by to confirm mating between the two pets." );
				}
				else if ( targettype != pettype )
				{
					from.SendMessage( "You cannot crossbreed two different species together." );
				}
				else if ( bc.AllowMating != true )
				{
					from.SendMessage( "This creature is not at the correct level to breed yet." );
				}
				else if ( bc.Female == m_Pet.Female )
				{
					from.SendMessage( "You cannot breed two pets of the same gender together." );
				}
				else
				{
					from.SendGump( new AwaitingConfirmationGump( m_Pet, bc ) );
					owner.SendGump( new BreedingAcceptGump( m_Pet, bc ) );	
				}
			}
			else
			{
				from.SendMessage( "Your pet cannot breed with that." );
			}
		}
	}
}