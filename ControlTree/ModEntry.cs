﻿using GenericModConfigMenu;
using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ControlTree
{
    // ReSharper disable once UnusedType.Global
    internal class ModEntry : Mod
    {
        // ReSharper disable once InconsistentNaming
        private ModConfig Config = null!;

        public override void Entry(IModHelper helper)
        {
            // Monitor.Log("Hello World", LogLevel.Debug);
            Config = Helper.ReadConfig<ModConfig>();

            helper.Events.Input.ButtonsChanged += OnButtonsChanged!;
            helper.Events.GameLoop.GameLaunched += OnGameLaunched!;

            if (Config.ChangeOak) TreePatch.ChangeTreeType(TreeTypeEnum.Oak.Id);
            if (Config.ChangeMaple) TreePatch.ChangeTreeType(TreeTypeEnum.Maple.Id);
            if (Config.ChangePine) TreePatch.ChangeTreeType(TreeTypeEnum.Pine.Id);
            if (Config.ChangeMahogany) TreePatch.ChangeTreeType(TreeTypeEnum.Mahogany.Id);
            if (Config.ChangeGreenRainType1) TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType1.Id);
            if (Config.ChangeGreenRainType2) TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType2.Id);
            if (Config.ChangeGreenRainType3) TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType3.Id);
            if (Config.ChangeMystic) TreePatch.ChangeTreeType(TreeTypeEnum.Mystic.Id);

            foreach (var textureName in helper.ModContent.Load<List<string>>("assets/textures.json"))
            {
                Monitor.Log($"Load texture: {textureName}");
                TreePatch.TextureMapping[textureName] = helper.ModContent.Load<Texture2D>($"assets/{textureName}");
            }

            TreePatch.InitConfig(Config, Monitor);
            SpriteBatchPatch.InitConfig(Config);

            Harmony harmony = new(ModManifest.UniqueID);
            harmony.PatchAll();
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: ModManifest,
                reset: () => Config = new ModConfig(),
                save: () => Helper.WriteConfig(Config)
            );

            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.mod_enable.name"),
                tooltip: () => Helper.Translation.Get("config.mod_enable.tooltip"),
                getValue: () => Config.ModEnable,
                setValue: value => Config.ModEnable = value
            );
            configMenu.AddKeybindList(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.mod_enable_toggle_key.name"),
                tooltip: () => Helper.Translation.Get("config.mod_enable_toggle_key.tooltip"),
                getValue: () => Config.ModEnableToggleKey,
                setValue: value => Config.ModEnableToggleKey = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.texture_change.name"),
                tooltip: () => Helper.Translation.Get("config.texture_change.tooltip"),
                getValue: () => Config.TextureChange,
                setValue: value => Config.TextureChange = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.minish_tree.name"),
                tooltip: () => Helper.Translation.Get("config.minish_tree.tooltip"),
                getValue: () => Config.MinishTree,
                setValue: value => Config.MinishTree = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.highlight_tree_seed.name"),
                tooltip: () => Helper.Translation.Get("config.highlight_tree_seed.tooltip"),
                getValue: () => Config.HighlightTreeSeed,
                setValue: value => Config.HighlightTreeSeed = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.render_tree_trunk.name"),
                tooltip: () => Helper.Translation.Get("config.render_tree_trunk.tooltip"),
                getValue: () => Config.RenderTreeTrunk,
                setValue: value => Config.RenderTreeTrunk = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.render_leafy_shadow.name"),
                tooltip: () => Helper.Translation.Get("config.render_leafy_shadow.tooltip"),
                getValue: () => Config.RenderLeafyShadow,
                setValue: value => Config.RenderLeafyShadow = value
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_oak.name"),
                tooltip: () => Helper.Translation.Get("config.change_oak.tooltip"),
                getValue: () => Config.ChangeOak,
                setValue: value =>
                {
                    Config.ChangeOak = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.Oak.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_maple.name"),
                tooltip: () => Helper.Translation.Get("config.change_maple.tooltip"),
                getValue: () => Config.ChangeMaple,
                setValue: value =>
                {
                    Config.ChangeMaple = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.Maple.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_pine.name"),
                tooltip: () => Helper.Translation.Get("config.change_pine.tooltip"),
                getValue: () => Config.ChangePine,
                setValue: value =>
                {
                    Config.ChangePine = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.Pine.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_mahogany.name"),
                tooltip: () => Helper.Translation.Get("config.change_mahogany.tooltip"),
                getValue: () => Config.ChangeMahogany,
                setValue: value =>
                {
                    Config.ChangeMahogany = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.Mahogany.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_green_rain_type1.name"),
                tooltip: () => Helper.Translation.Get("config.change_green_rain_type1.tooltip"),
                getValue: () => Config.ChangeGreenRainType1,
                setValue: value =>
                {
                    Config.ChangeGreenRainType1 = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType1.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_green_rain_type2.name"),
                tooltip: () => Helper.Translation.Get("config.change_green_rain_type2.tooltip"),
                getValue: () => Config.ChangeGreenRainType2,
                setValue: value =>
                {
                    Config.ChangeGreenRainType2 = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType2.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_green_rain_type3.name"),
                tooltip: () => Helper.Translation.Get("config.change_green_rain_type3.tooltip"),
                getValue: () => Config.ChangeGreenRainType3,
                setValue: value =>
                {
                    Config.ChangeGreenRainType3 = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.GreenRainType3.Id, value);
                }
            );
            configMenu.AddBoolOption(
                mod: ModManifest,
                name: () => Helper.Translation.Get("config.change_mystic.name"),
                tooltip: () => Helper.Translation.Get("config.change_mystic.tooltip"),
                getValue: () => Config.ChangeMystic,
                setValue: value =>
                {
                    Config.ChangeMystic = value;
                    TreePatch.ChangeTreeType(TreeTypeEnum.Mystic.Id, value);
                }
            );
        }

        private void OnButtonsChanged(object sender, ButtonsChangedEventArgs e)
        {
            if (!ShouldEnable(forInput: true)) return;
            if (!Config.ModEnableToggleKey.JustPressed()) return;
            Config.ModEnable = !Config.ModEnable;
            Helper.WriteConfig(Config);
        }

        private static bool ShouldEnable(bool forInput = false)
        {
            if (!Context.IsWorldReady || !Context.IsMainPlayer) return false;

            if (!forInput) return true;

            if (!Context.IsPlayerFree && !Game1.eventUp) return false;

            return Game1.keyboardDispatcher.Subscriber == null;
        }
    }
}