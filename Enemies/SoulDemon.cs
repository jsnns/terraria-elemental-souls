using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementalSouls.Enemies
{
    class IceSoulDemon : ModNPC
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            npc.friendly = false;
            npc.lifeMax = 100;
            npc.damage = 70;
            npc.aiStyle = 3; // the zombie aiStyle for now.
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we want a soul demon to spawn if
            // 1. there isn't another soul demon in the world
            // 2. the soul demon hasn't been killed for that biome

            bool correctBiome = spawnInfo.player.ZoneSnow;
            bool alreadySpawned = NPC.AnyNPCs(ModContent.NPCType<IceSoulDemon>());

            int playerHealth = spawnInfo.player.statLifeMax;

            if (correctBiome && !alreadySpawned && !ElementalSoulsWorld.IceSoulDemonDowned)
            {
                return 0.1f * (playerHealth / 100);
            } else
            {
                return 0.0f;
            }
        }

        public override void NPCLoot()
        {
            ElementalSoulsWorld.MarkIceSoulDemonDowned();
        }
    }
}
