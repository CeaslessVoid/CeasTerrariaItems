using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x0200001F RID: 31
    public class Cloak : ModItem
    {
        // Token: 0x0600008E RID: 142 RVA: 0x00002058 File Offset: 0x00000258
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        // Token: 0x0600008F RID: 143 RVA: 0x000032F8 File Offset: 0x000014F8
        public override void SetDefaults()
        {
            base.Item.width = 36;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x0000334B File Offset: 0x0000154B
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

        // Token: 0x06000091 RID: 145 RVA: 0x0000336D File Offset: 0x0000156D
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 7f;
        }
    }
}
