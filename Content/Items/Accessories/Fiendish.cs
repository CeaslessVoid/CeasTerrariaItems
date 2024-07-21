using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x02000088 RID: 136
    public class Fiendish : ModItem
    {
        // Token: 0x0600029D RID: 669 RVA: 0x00002058 File Offset: 0x00000258
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        // Token: 0x0600029E RID: 670 RVA: 0x000085E4 File Offset: 0x000067E4
        public override void SetDefaults()
        {
            base.Item.width = 30;
            base.Item.height = 42;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 5;
        }

        // Token: 0x0600029F RID: 671 RVA: 0x00008637 File Offset: 0x00006837
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AmpTome>(1);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.GoldCoin, 15);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 12;
        public static readonly int FlatDamageBonus = 4;

        // Token: 0x060002A0 RID: 672 RVA: 0x00008658 File Offset: 0x00006858
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Summon).Base += FlatDamageBonus;
            player.maxMinions += 1;
        }
    }
}
