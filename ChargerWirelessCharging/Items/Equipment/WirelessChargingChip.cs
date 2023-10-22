using Nautilus.Assets;
using Nautilus.Utility;
using System.Reflection;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using System.IO;
using UnityEngine;
using static CraftData;
using System.Collections;

namespace ChargerWirelessCharging.Items.Equipment
{
    public static class WirelessChargingChip
    {
        public static PrefabInfo Info { get; } = PrefabInfo
            .WithTechType("WirelesschargingChip",
            "Wireless charging Chip",
            "This Chip managed Wireless charging for Loose Batteries in your Inventory. (Does not Stack for Loading Speed, build more Charger instead)")
            .WithIcon(GetItemSprite())
            .WithSizeInInventory(new Vector2int(1, 1));

        public static void Register()
        {
            var customPrefab = new CustomPrefab(Info);
            customPrefab.SetGameObject(GetGameObjectAsync);
            customPrefab.SetRecipe(GetBlueprintRecipe())
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment")
                .WithCraftingTime(3f);
            customPrefab.SetEquipment(EquipmentType.Chip);
            customPrefab.SetUnlock(TechType.PowerCellCharger);
            customPrefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            customPrefab.Register();
        }

        public static IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.MapRoomHUDChip);
            yield return task;
            GameObject prefab = task.GetResult();
            GameObject obj = GameObject.Instantiate(prefab);
            prefab.SetActive(false);

            gameObject.Set(obj);
        }

        public static string IconFileName => "WirelesschargingChip.png";

        public static Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets"), IconFileName));
        }

        public static RecipeData GetBlueprintRecipe()
        {
            return new RecipeData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.CopperWire,1),
                    new Ingredient(TechType.Titanium, 1),
                }
            };
        }
    }
}
