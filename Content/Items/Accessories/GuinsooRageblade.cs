using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    public class GuinsooRageblade : ModItem
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
            base.Item.value = Item.sellPrice(0, 4, 0, 0);
            base.Item.rare = ItemRarityID.Master;
        }

        public override void AddRecipes()
        {
            Mod templateMod2 = null;
            ModLoader.TryGetMod("WeaponAugs", out templateMod2);


            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(templateMod2.Find<ModItem>("PowerShardUlt"), 15);
            recipe.AddIngredient(templateMod2.Find<ModItem>("DeterminationJewelUlt"), 1);
            recipe.AddIngredient<Recurve>(1);
            recipe.AddIngredient<Picaxe>(1);
            recipe.AddIngredient<AmpTome>(1);
            recipe.AddIngredient(ItemID.ShroomiteBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 10;
            player.GetAttackSpeed(DamageClass.Generic) += (20 + (player.GetModPlayer<HieuModPlayer2>().GuinsooRagebladestacks)) / 100f;

            player.GetDamage(DamageClass.Melee).Flat += 20;
            player.GetDamage(DamageClass.Ranged).Flat += 20;

            player.GetModPlayer<HieuModPlayer2>().HasGuinsooRageblade = true;
        }
    }
}
