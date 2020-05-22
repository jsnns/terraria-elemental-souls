using ElementalSouls.Data;
using ElementalSouls.Enemies;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementalSouls
{
	public class ElementalSouls : Mod
	{
	}

	public class ElementalSoulsWorld : ModWorld
    {
        internal static Dictionary<Element, bool> soulDemonsDowned;

        public override void Initialize()
        {
            soulDemonsDowned = new Dictionary<Element, bool>();
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"demonNames", soulDemonsDowned.Keys.ToList().ConvertAll(x => x.ToString()) },
                {"demonValues", soulDemonsDowned.Values.ToList() }
            };
        }

        public override void Load(TagCompound tag)
        {
            // we store the list as a list<string> since we can't seralize the enum 
            // but to use effectively at runtime we need a list of list<Element>
            List<Element> names = tag.Get<List<string>>("demonNames").ConvertAll(x => ElementData.elementFromString(x));
            
            List<bool> values = tag.Get<List<bool>>("demonValues");
            
            // combine list of Element and bool to form a dictionary<Element, bool>S
            soulDemonsDowned = names.Zip(values, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);

            mod.Logger.Info("Loaded demon values = " + ToPrettyString(soulDemonsDowned));
        }

        public static void MarkSoulDemonDowned(Element elementalType)
        {
            soulDemonsDowned[elementalType] = true;
        }

        public static bool IsSoulDemonDowned(Element elementalType)
        {
            if (soulDemonsDowned.ContainsKey(elementalType))
            {
                return soulDemonsDowned[elementalType];
            }

            return false;
        }

        public static string ToPrettyString(Dictionary<Element, bool> dict)
        {
            var str = new StringBuilder();
            str.Append("{");
            foreach (var pair in dict)
            {
                str.Append(String.Format(" {0}={1} ", pair.Key, pair.Value));
            }
            str.Append("}");
            return str.ToString();
        }

    }
}