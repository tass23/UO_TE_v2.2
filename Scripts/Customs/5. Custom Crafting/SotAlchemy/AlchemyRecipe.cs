using System;
using System.Collections.Generic;
using Server;

namespace Server.Items
{
    public enum AlRecipe
    {
        None,

        PhysResist,
        FireResist,
        ColdResist,
        PoisonResist,
        EnergyResist,

        MagicResist
    }

    public class AlchemyRecipe
    {
        private List<AlchemyInfo> m_AlchemyInfo;

        private AlRecipe m_RecipeType;
        public AlRecipe RecipeType
        {
            get { return m_RecipeType; }
            set { m_RecipeType = value; }
        }

        public AlchemyRecipe(List<AlchemyInfo> alchemyInfo)
        {
            m_AlchemyInfo = alchemyInfo;
            CheckRecipes();
        }

        private void CheckRecipes()
        {
            if (CheckPhysResist())
                RecipeType = AlRecipe.PhysResist;
            else if (CheckFireResist())
                RecipeType = AlRecipe.FireResist;
            else if (CheckColdResist())
                RecipeType = AlRecipe.ColdResist;
            else if (CheckPoisonResist())
                RecipeType = AlRecipe.PoisonResist;
            else if (CheckEnergyResist())
                RecipeType = AlRecipe.EnergyResist;
            else if (CheckMagicResist())
                RecipeType = AlRecipe.MagicResist;
            else
                RecipeType = AlRecipe.None;
        }

        private bool CheckPhysResist()
        {
            // Begin of recipe
            Item FirstIngr = new Garlic(); // The first ingredient
            Item SecondIngr = new MandrakeRoot(); // The second ingredient
            Item AdditItem = new Longsword(); // The additional item to add to and then remove from the pot

            int MinFirstIngrTemp = 100; // Minimum temperature the first ingredient should be added to the pot
            int MinSecondIngrTemp = 100; // Minimum temperature the second ingredient should be added to the pot
            int MinAdditItemTemp = 100; // Minimum temperature the additional item should be added to the pot

            int MaxFirstIngrTemp = 10000; // Maximum temperature the first ingredient should be added to the pot
            int MaxSecondIngrTemp = 10000; // Maximum temperature the second ingredient should be added to the pot
            int MaxAdditItemTemp = 10000; // Maximum temperature the additional item should be added to the pot

            int MinSecAfterFirstIngrAdded = 5; // Minimum seconds should past before second ingredient can be added
            int MinSecAfterSecondIngrAdded = 5; // Minimum seconds should past before additional item can be added            

            int MaxSecAfterFirstIngrAdded = 10; // Maximum seconds to add the second ingredient (beyond this time the potion will be ruined)
            int MaxSecAfterSecondIngrAdded = 10; // Maximum seconds to add the additional item
            int MaxSecAfterAdditItemAdded = 10; // Maximum seconds the additional item can stay in the pot
            // End of recipe
            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;            

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }

        private bool CheckFireResist()
        {
            Item FirstIngr = new SulfurousAsh();
            Item SecondIngr = new Ginseng();
            Item AdditItem = new Kindling();

            int MinFirstIngrTemp = 150;
            int MinSecondIngrTemp = 200;
            int MinAdditItemTemp = 5;

            int MaxFirstIngrTemp = 180;
            int MaxSecondIngrTemp = 10000;
            int MaxAdditItemTemp = 95;

            int MinSecAfterFirstIngrAdded = 10;
            int MinSecAfterSecondIngrAdded = 15;

            int MaxSecAfterFirstIngrAdded = 15;
            int MaxSecAfterSecondIngrAdded = 25;
            int MaxSecAfterAdditItemAdded = 10;

            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }

        private bool CheckColdResist()
        {
            Item FirstIngr = new BlackPearl();
            Item SecondIngr = new GraveDust();
            Item AdditItem = new IronOre();

            int MinFirstIngrTemp = 0;
            int MinSecondIngrTemp = 0;
            int MinAdditItemTemp = 0;

            int MaxFirstIngrTemp = 95;
            int MaxSecondIngrTemp = 95;
            int MaxAdditItemTemp = 95;

            int MinSecAfterFirstIngrAdded = 5;
            int MinSecAfterSecondIngrAdded = 5;

            int MaxSecAfterFirstIngrAdded = 10;
            int MaxSecAfterSecondIngrAdded = 10;
            int MaxSecAfterAdditItemAdded = 10;

            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }

        private bool CheckPoisonResist()
        {
            Item FirstIngr = new NoxCrystal();
            Item SecondIngr = new Nightshade();
            Item AdditItem = new PoisonFieldScroll();

            int MinFirstIngrTemp = 80;
            int MinSecondIngrTemp = 150;
            int MinAdditItemTemp = 500;

            int MaxFirstIngrTemp = 100;
            int MaxSecondIngrTemp = 170;
            int MaxAdditItemTemp = 10000;

            int MinSecAfterFirstIngrAdded = 5;
            int MinSecAfterSecondIngrAdded = 35;

            int MaxSecAfterFirstIngrAdded = 10;
            int MaxSecAfterSecondIngrAdded = 45;
            int MaxSecAfterAdditItemAdded = 5;

            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }

        private bool CheckEnergyResist()
        {
            Item FirstIngr = new DaemonBlood();
            Item SecondIngr = new PigIron();
            Item AdditItem = new ShadowIronOre();

            int MinFirstIngrTemp = 100;
            int MinSecondIngrTemp = 50;
            int MinAdditItemTemp = 25;

            int MaxFirstIngrTemp = 150;
            int MaxSecondIngrTemp = 100;
            int MaxAdditItemTemp = 60;

            int MinSecAfterFirstIngrAdded = 20;
            int MinSecAfterSecondIngrAdded = 20;

            int MaxSecAfterFirstIngrAdded = 30;
            int MaxSecAfterSecondIngrAdded = 30;
            int MaxSecAfterAdditItemAdded = 5;

            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }

        private bool CheckMagicResist()
        {
            Item FirstIngr = new SpidersSilk();
            Item SecondIngr = new BatWing();
            Item AdditItem = new Spellbook();

            int MinFirstIngrTemp = 80;
            int MinSecondIngrTemp = 200;
            int MinAdditItemTemp = 5;

            int MaxFirstIngrTemp = 100;
            int MaxSecondIngrTemp = 10000;
            int MaxAdditItemTemp = 80;

            int MinSecAfterFirstIngrAdded = 10;
            int MinSecAfterSecondIngrAdded = 30;

            int MaxSecAfterFirstIngrAdded = 15;
            int MaxSecAfterSecondIngrAdded = 40;
            int MaxSecAfterAdditItemAdded = 5;

            bool FirstIngrAdded = false;
            bool SecondIngrAdded = false;
            bool AdditItemAdded = false;

            int FirstIngrAddedSecond = 0;
            int SecondIngrAddedSecond = 0;
            int AdditItemAddedSecond = 0;

            for (int i = 0; i < m_AlchemyInfo.Count; i++)
            {
                AlchemyInfo ai = m_AlchemyInfo[i];
                List<Item> ItemsInPot = ai.ItemsInPot;
                int second = ai.Second;
                int temperature = ai.Temperature;

                if (ItemsInPot.Count > 1)
                    return false;
                else if (ItemsInPot.Count > 0)
                {
                    foreach (Item item in ItemsInPot)
                    {
                        if (item.GetType() == FirstIngr.GetType() && !FirstIngrAdded && temperature >= MinFirstIngrTemp && temperature <= MaxFirstIngrTemp)
                        {
                            FirstIngrAdded = true;
                            FirstIngrAddedSecond = second;
                        }
                        else if (item.GetType() == SecondIngr.GetType() && FirstIngrAdded && !SecondIngrAdded && second <= FirstIngrAddedSecond + MaxSecAfterFirstIngrAdded && second >= FirstIngrAddedSecond + MinSecAfterFirstIngrAdded && temperature >= MinSecondIngrTemp && temperature <= MaxSecondIngrTemp)
                        {
                            SecondIngrAdded = true;
                            SecondIngrAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && FirstIngrAdded && SecondIngrAdded && !AdditItemAdded && second <= SecondIngrAddedSecond + MaxSecAfterSecondIngrAdded && second >= SecondIngrAddedSecond + MinSecAfterSecondIngrAdded && temperature >= MinAdditItemTemp && temperature <= MaxAdditItemTemp)
                        {
                            AdditItemAdded = true;
                            AdditItemAddedSecond = second;
                        }
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second >= AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            return false;
                        else if (item.GetType() == AdditItem.GetType() && AdditItemAdded && second < AdditItemAddedSecond + MaxSecAfterAdditItemAdded)
                            continue;
                        else
                            return false;
                    }
                }
                else continue;
            }
            if (AdditItemAdded)
                return true;
            else
                return false;
        }
    }
}