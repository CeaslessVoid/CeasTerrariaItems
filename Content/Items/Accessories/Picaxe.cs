using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class Picaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 34;
            base.Item.height = 34;
            base.Item.accessory = true;
            base.Item.value = Item.sellPrice(0, 2, 0, 0);
            base.Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldPickaxe, 1);
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumPickaxe, 1);
            recipe.AddIngredient(ItemID.GoldCoin, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public static readonly int AdditiveDamageBonus = 10;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;
        }
    }
}
