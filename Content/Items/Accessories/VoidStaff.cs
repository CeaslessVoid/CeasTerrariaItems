using HieuLeague.Common.Players;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories
{
    // Token: 0x02000021 RID: 33
    public class VoidStaff : ModItem
    {
        // Token: 0x06000098 RID: 152 RVA: 0x00002058 File Offset: 0x00000258
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[base.Type] = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 40;
            base.Item.height = 32;
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
            recipe.AddIngredient(templateMod2.Find<ModItem>("DeathshroudJewelUlt"), 1);
            recipe.AddIngredient<Blighting>(1);
            recipe.AddIngredient<Needless>(1);
            recipe.AddIngredient(ItemID.SpectreBar, 15);
            recipe.AddIngredient(ItemID.AvengerEmblem, 1);
            recipe.AddIngredient(ItemID.PlatinumCoin, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }

        public static readonly int AdditiveDamageBonus = 20;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += AdditiveDamageBonus / 100f;
            player.GetDamage(DamageClass.Magic) += AdditiveDamageBonus / 100f;

            player.GetModPlayer<HieuModPlayer>().hasBlighting = true;
        }
    }
}
