using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class ZekeCovergence : ModItem
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
            base.Item.rare = ItemRarityID.Master;
        }

        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("UnleashJewelUlt"), 1);
            recipe.AddIngredient<Mantle>(1);
            recipe.AddIngredient<Cloth>(1);
            recipe.AddIngredient<RubyCR>(1);
            recipe.AddIngredient<Forbidden>(1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaRegen += (int)((float)player.manaRegen * 1.0f);
            player.statLifeMax2 += 100;
            player.statDefense += 5;
            player.maxMinions += 3;
            player.endurance += 0.05f;

            player.GetModPlayer<HieuModPlayer2>().hasZeke = true;
        }
    }
}
