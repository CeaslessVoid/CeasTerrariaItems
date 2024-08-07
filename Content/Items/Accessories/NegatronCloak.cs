using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class NegatronCloak : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 28;
            base.Item.height = 32;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Mantle>(1);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.08f;
        }
    }
}
