using ElementalSouls.Enemies;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementalSouls
{
	public class ElementalSouls : Mod
	{
	}

	public class ElementalSoulsWorld : ModWorld
    {
        public static bool IceSoulDemonDowned;

        public override void Initialize()
        {
            IceSoulDemonDowned = false;
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"IceSoulDemonDowned", IceSoulDemonDowned },
            };
        }

        public override void Load(TagCompound tag)
        {
            IceSoulDemonDowned = tag.GetBool("IceSoulDemonDowned");
        }

        public static void MarkIceSoulDemonDowned()
        {
            IceSoulDemonDowned = true;
        }
    }
}