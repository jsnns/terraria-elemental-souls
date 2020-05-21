using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ElementalSouls.Enemies
{
    class IceSoulDemon : ModNPC
    {

        private int lifeOfPlayer;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            npc.friendly = false;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.HitSound = SoundID.NPCHit2;
            npc.lifeMax = 100;
            npc.damage = 70;
            npc.aiStyle = 22; // the Floaty Gross aiStyle
            npc.width = 18;
            npc.height = 40;
            npc.defense = 6;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // we want a soul demon to spawn if
            // 1. there isn't another soul demon in the world
            // 2. the soul demon hasn't been killed for that biome

            bool correctBiome = spawnInfo.player.ZoneSnow;
            bool alreadySpawned = NPC.AnyNPCs(ModContent.NPCType<IceSoulDemon>());

            int playerHealth = spawnInfo.player.statLifeMax;

            if (correctBiome && !alreadySpawned)
            {
                lifeOfPlayer = spawnInfo.player.statLifeMax;
                return 0.1f * (playerHealth / 100);
            } else
            {
                return 0.0f;
            }
        }

        public override int SpawnNPC(int tileX, int tileY)
        {
            // setup the stats based on the last observed player
            npc.lifeMax = (int)(lifeOfPlayer * 0.2);
            npc.defense = (int)(lifeOfPlayer / 75);
            npc.damage = (int)(lifeOfPlayer / 7);

            return base.SpawnNPC(tileX, tileY);
        }

        public override void NPCLoot()
        {
            ElementalSoulsWorld.MarkIceSoulDemonDowned();
        }
    }
}
