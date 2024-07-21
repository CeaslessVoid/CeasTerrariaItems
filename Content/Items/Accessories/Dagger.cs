using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Dagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 34;
            base.Item.height = 30;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int BaseAttackSpeed = 7;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Generic) += BaseAttackSpeed / 100f;
        }
    }
}
