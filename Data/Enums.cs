using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementalSouls.Data
{
    public enum Element
    {
        Ice,
        Earth,
        Fire,
        Wind,
        None
    }

    static class ElementData { 
        public static Element fromPlayerLocation(Player player)
        {
            if (player.ZoneJungle)
            {
                return Element.Earth;
            } else if (player.ZoneSnow)
            {
                return Element.Ice;
            } else if (player.ZoneUnderworldHeight)
            {
                return Element.Fire;
            } else if (player.ZoneSkyHeight)
            {
                return Element.Wind;
            }

            return Element.None;
        }

        public static Element elementFromString(string str)
        {
            return (Element) Enum.Parse(typeof(Element), str);
        }
    }
}
