using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using HieuLeague.Common.Players;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x0200007D RID: 125
    public class Cull : ModItem
    {
        // Token: 0x06000268 RID: 616 RVA: 0x00002058 File Offset: 0x00000258
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        // Token: 0x06000269 RID: 617 RVA: 0x00007FF0 File Offset: 0x000061F0
        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 1, 0, 0);
            base.Item.rare = 2;
        }

        public static readonly int AdditiveDamageBonus = 5;

        // Token: 0x0600026A RID: 618 RVA: 0x00008043 File Offset: 0x00006243
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += AdditiveDamageBonus / 100f;
            player.GetModPlayer<HieuModPlayer>().HasCullPassive = true;

        }
    }
}
