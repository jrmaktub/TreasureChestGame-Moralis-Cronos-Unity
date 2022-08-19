﻿
using MoralisUnity.Sdk.Constants;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS
{
    /// <summary>
    /// Helper Values
    /// </summary>
    public static class TheGameConstants
    {
        // Fields -----------------------------------------
        public const string ProjectName = "Web3 Magic Treasure Chest";
        public const string PathCreateAssetMenu = MoralisConstants.PathMoralisSamplesCreateAssetMenu + "/" + ProjectName;
        
        ///////////////////////////////////////////
        // MenuItem Path
        ///////////////////////////////////////////
        public const string Moralis = "Moralis";
        public const string OpenReadMe = MoralisConstants.Open + " " + "ReadMe";
        private const string PathMoralisCreateAssetMenu = Moralis + "/" + MoralisConstants.Web3UnitySDK;
        private const string PathMoralisWindowMenu = "Window/" + Moralis + "/" + MoralisConstants.Web3UnitySDK;

        ///////////////////////////////////////////
        // MenuItem Priority
        ///////////////////////////////////////////

        // Skipping ">10" shows a horizontal divider line.
        public const int PriorityMoralisWindow_Primary = 10;
        public const int PriorityMoralisWindow_Secondary = 100;
        public const int PriorityMoralisWindow_Examples = 1000;
        public const int PriorityMoralisWindow_Samples = 10000;

        ///////////////////////////////////////////
        // Display Text
        ///////////////////////////////////////////
        public const string Registering = "Registering Player";
        public const string Unregistering = "Unregistering Player";
        public const string Opening = "Opening Treasure Chest";
        public const string Updating = "Updating Game";
        public const string Deleting = "Deleting Prize";
        public const string Selling = "Selling Prize";
        public const string Resetting = "Resetting Data";
        
    }
}