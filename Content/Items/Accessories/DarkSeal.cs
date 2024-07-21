using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using HieuLeague.Common.Players;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x02000080 RID: 128
    public class DarkSeal : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public static readonly int AdditiveDamageBonus = 5;
        public int DarkSc = 10;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 10;
            player.GetDamage(DamageClass.Generic) += (player.GetModPlayer<HieuModPlayer>().DarkSealStack + AdditiveDamageBonus)/100f;
            player.GetModPlayer<HieuModPlayer>().HasDarkSeal = true;
        }

    }
}
