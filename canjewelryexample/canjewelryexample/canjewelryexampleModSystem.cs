using System.Collections.Generic;
using canjewelry.src.api;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;

namespace canjewelryexample
{
    /// <summary>
    /// The hoe is done by JSON instead, see assets/canjewelryexample/patches/hoe.json;
    /// the saw below repeats that patch from code.
    /// Where the gems are drawn on the model lives in assets/canjewelryexample/config/gemvisuals.
    /// </summary>
    public class canjewelryexampleModSystem : ModSystem
    {
        // Before the core, so the entries also land in the config defaults on the first start.
        public override double ExecuteOrder() => -1.0;

        public override void StartPre(ICoreAPI api)
        {
            base.StartPre(api);

            // Two sockets, each taking a socket item of tier 2 or lower.
            CANJewelryRegistry.RegisterDefaultSockets("game:cleaver-*", 2, 2);
            // Gems of the "melee" group now fit the cleaver.
            CANJewelryRegistry.RegisterItemGroupMembers("melee", "cleaver-");

            // Layout by the "metal" code variant; metals not listed get no sockets.
            CANJewelryRegistry.RegisterDefaultVariantSockets("game:shears-*", "metal", new Dictionary<string, int[]>
            {
                { "copper", new[] { 1 } },
                { "iron",   new[] { 2, 1 } },
                { "steel",  new[] { 3, 3 } },
            });
            CANJewelryRegistry.RegisterItemGroupMembers("mining", "shears-");

            // The saw takes the same attributes as the hoe patch, only written from code (see AssetsFinalize).
            CANJewelryRegistry.RegisterDefaultSockets("game:saw-*", 1, 2);
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            base.AssetsFinalize(api);

            // Same as "canGemGroups" / "canAllowedGems" in hoe.json: accepts the gems of the
            // "melee" group plus two picked by name, without adding the saw to any group.
            foreach (Item item in api.World.SearchItems(new AssetLocation("game:saw-*")))
            {
                if (item.Attributes == null) item.Attributes = new JsonObject(new JObject());
                item.Attributes.Token[CANJewelryAttributes.GemGroups] = new JArray("melee");
                item.Attributes.Token[CANJewelryAttributes.AllowedGems] = new JArray("amethyst", "quartz");
            }
        }
    }
}
