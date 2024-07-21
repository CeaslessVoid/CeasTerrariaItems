using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Crystalline : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 40;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RubyCR>(1);
            recipe.AddIngredient<Reju>(1);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();

        }

        public static readonly int BaseHealthRegen = 15;
        public static readonly int AdditiveHealthBonus = 85;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += BaseHealthRegen / 10;
            player.statLifeMax2 += AdditiveHealthBonus;
        }
    }
}
