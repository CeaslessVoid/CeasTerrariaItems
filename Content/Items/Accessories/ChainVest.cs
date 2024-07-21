using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x0200000B RID: 11
    public class ChainVest : ModItem
    {
        // Token: 0x0600002A RID: 42 RVA: 0x00002058 File Offset: 0x00000258
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        // Token: 0x0600002B RID: 43 RVA: 0x000025E0 File Offset: 0x000007E0
        public override void SetDefaults()
        {
            base.Item.width = 28;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 5;
        }

        // Token: 0x0600002C RID: 44 RVA: 0x00002633 File Offset: 0x00000833
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Cloth>(1);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        // Token: 0x0600002D RID: 45 RVA: 0x00002654 File Offset: 0x00000854
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 9;
        }
    }
}
