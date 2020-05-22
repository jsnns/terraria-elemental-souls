using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ElementalSouls.Data;
using Extensions;
using System.CodeDom;
using System.Security.Policy;

namespace ElementalSouls.Enemies
{
    class BaseSoulDemon : ModNPC
    {
        private int lifeOfPlayer;
        public Element elementalType;
        public override string Texture => "ElementalSouls/Enemies/IceSoulDemon";

        public virtual bool IsSpawned()
        {
            return NPC.AnyNPCs(ModContent.NPCType<BaseSoulDemon>());
        }

        public override void NPCLoot()
        {
            ElementalSoulsWorld.MarkSoulDemonDowned(elementalType);
        }

        public BaseSoulDemon()
        {
            elementalType = Element.None;
        }

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

            Element currentElement = ElementData.fromPlayerLocation(spawnInfo.player);
            
            bool alreadySpawned = IsSpawned();
            int playerHealth = spawnInfo.player.statLifeMax;

            if (elementalType.Equals(Element.None))
            {
                // don't spawn if it's a generic demon soul. 
                return 0.0f;
            }

            if (elementalType != currentElement || alreadySpawned || ElementalSoulsWorld.IsSoulDemonDowned(elementalType))
            {
                return 0.0f;
            } else {
                lifeOfPlayer = spawnInfo.player.statLifeMax;
                return 0.1f * (playerHealth / 50);
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
    }

    class IceSoulDemon : BaseSoulDemon
    {
        public IceSoulDemon()
        {
            elementalType = Element.Ice;
        }

        public override bool IsSpawned() => NPC.AnyNPCs(ModContent.NPCType<IceSoulDemon>());
    }
}
