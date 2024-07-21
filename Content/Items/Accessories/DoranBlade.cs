using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace HieuLeague.Content.Items.Accessories;

public class DoranBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[((ModItem)this).Type] = 1;
    }

    public override void SetDefaults()
    {
        ((Entity)((ModItem)this).Item).width = 28;
        ((Entity)((ModItem)this).Item).height = 32;
        ((ModItem)this).Item.accessory = true;
        ((ModItem)this).Item.value = Item.sellPrice(0, 2, 0, 0);
        ((ModItem)this).Item.rare = 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 10);
        recipe.AddIngredient(ItemID.StoneBlock, 25);
        recipe.AddIngredient(ItemID.WoodenSword, 1);
        recipe.AddIngredient(ItemID.GoldCoin, 5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public static readonly int AdditiveDamageBonus = 5;

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Melee) += AdditiveDamageBonus / 100f;
        player.GetDamage(DamageClass.Ranged) += AdditiveDamageBonus / 100f;
        player.statLifeMax2 += 20;
    }
}
