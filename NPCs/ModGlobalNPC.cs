using ElementalSouls;
using Microsoft.Xna.Framework;
using System.Runtime.ExceptionServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace yourmodnamehere.NPCs

{
    public class ElementalSouls : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            Player closestPlayer = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)];


            if ( Main.rand.Next(1) == 0) //Chance of dropping
            {
                string soulToDrop = null;
                if (closestPlayer.ZoneBeach && ElementalSoulsWorld.IceSoulDemonDowned) //if Ocean biome and enemy killed
                {
                    soulToDrop = "WaterSoul";

                }
                if (closestPlayer.ZoneJungle && ElementalSoulsWorld.IceSoulDemonDowned) //if Jungle Biome
                {
                    if (Main.rand.Next(1) == 0) // 1 in 7 chance of drop
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EarthSoul"));

                    }
                }
                if (closestPlayer.ZoneUnderworldHeight && ElementalSoulsWorld.IceSoulDemonDowned) // if underworld Biome
                {
                    if (Main.rand.Next(1) == 0) // 1 in 7 chance of drop
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireSoul"));

                    }
                }
                if (closestPlayer.ZoneSkyHeight && ElementalSoulsWorld.IceSoulDemonDowned) //if sky biome
                {
                    if (Main.rand.Next(1) == 0) // 1 in 7 chance of drop
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WindSould"));

                    }
                }
                if (closestPlayer.ZoneSnow && ElementalSoulsWorld.IceSoulDemonDowned)//if Snow biome 
                {
                    if (Main.rand.Next(1) == 0) // 1 in 7 chance of drop
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceSoul"));

                    }
                }

                if (soulToDrop != null)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(soulToDrop));
                }

            }
        }
    }
}






