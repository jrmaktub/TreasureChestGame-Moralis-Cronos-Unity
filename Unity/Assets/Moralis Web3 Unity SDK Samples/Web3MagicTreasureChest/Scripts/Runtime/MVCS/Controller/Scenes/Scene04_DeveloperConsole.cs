using System.Text;
using UnityEngine;
using MoralisUnity.Samples.Shared;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Exceptions;
using System.Collections.Generic;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Samples.Shared.Debugging;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.Scenes;

#pragma warning disable 1998, 4014
namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
    /// <summary>
    /// Core Scene Behavior - Using <see cref="Scene04_DeveloperConsoleUI"/>
    /// </summary>
    public class Scene04_DeveloperConsole : MonoBehaviour
    {
        //  Properties ------------------------------------
 
        //  Fields ----------------------------------------
        [SerializeField]
        private Scene04_DeveloperConsoleUI _ui;

        private StringBuilder _titleTextBuilder = new StringBuilder();
        private StringBuilder _outputTextStringBuilder = new StringBuilder();

        //  Unity Methods----------------------------------
        protected async void Start()
        {
            bool hasMoralis = await TheGameSingleton.Instance.HasMoralisUserAsync();
            if (!hasMoralis)
            {
                throw new RequiredMoralisUserException();
            }

            _ui.IsRegisteredButtonUI.Button.onClick.AddListener(IsRegisteredButtonUI_OnClicked);
            _ui.RegisterButtonUI.Button.onClick.AddListener(RegisterButtonUI_OnClicked);
            _ui.RewardPrizesButtonUI.Button.onClick.AddListener(RewardPrizesButtonUI_OnClicked);

            //
            _ui.UnregisterButtonUI.Button.onClick.AddListener(UnregisterButtonUI_OnClicked);
            _ui.SetGoldByPlusButtonUI.Button.onClick.AddListener(SetGoldByPlusButtonUI_OnClicked);
            _ui.SetGoldByMinusButtonUI.Button.onClick.AddListener(SetGoldByMinusButtonUI_OnClicked);
            _ui.AddTreasureButtonUI.Button.onClick.AddListener(AddTreasurePrizeButtonUI_OnClicked);
            _ui.SellTreasureButtonUI.Button.onClick.AddListener(SellTreasurePrizeButtonUI_OnClicked);
            _ui.DeleteAllTreasurePrizesButtonUI.Button.onClick.AddListener(DeleteAllTreasurePrizesButtonUI_OnClicked);
            _ui.BackButtonUI.Button.onClick.AddListener(BackButtonUI_OnClicked);

            _titleTextBuilder.Clear();
            RefreshUIAsync();
            
            // Mimic button press for user convenience
            IsRegisteredButtonUI_OnClicked();
        }


        //  General Methods -------------------------------
        private async UniTask RefreshUIAsync()
        {
            _ui.BackButtonUI.IsInteractable = true; 

            _ui.Text.text = _titleTextBuilder.ToString() + _outputTextStringBuilder.ToString();
        }

        private async UniTask<bool> EnsureIsRegisteredAsync()
        {
            // Use the cached here so its quick
            bool isRegisteredCached = TheGameSingleton.Instance.TheGameController.GetIsRegisteredCached();
            if (!isRegisteredCached)
            {
                _outputTextStringBuilder.Clear();
                _outputTextStringBuilder.AppendHeaderLine($"EnsureIsRegistered(). Failed.");
                await RefreshUIAsync();
            }

            return isRegisteredCached;
        }



        //  Event Handlers --------------------------------
        
        private async void IsRegisteredButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            await TheGameSingleton.Instance.TheGameController.ShowMessagePassiveAsync(
              async delegate ()
              {
    
                  bool isRegistered = await TheGameSingleton.Instance.TheGameController.GetIsRegisteredAsync();

                  _outputTextStringBuilder.Clear();
                  _outputTextStringBuilder.AppendHeaderLine($"isRegistered()");
                  _outputTextStringBuilder.AppendBullet($"result = {isRegistered}");

                  await RefreshUIAsync();
              });

        }

        
        private async void UnregisterButtonUI_OnClicked()
        {
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            bool isRegistered = await TheGameSingleton.Instance.TheGameController.GetIsRegisteredAsync();

            if (!isRegistered)
            {
                await TheGameSingleton.Instance.TheGameController.ShowMessageCustomAsync(
                    "Already Unregistered", 1000);
            }
            else
            {
                await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                    TheGameConstants.Unregistering,
                    async delegate ()
                    {
                        await TheGameSingleton.Instance.TheGameController.UnregisterAsync();

                        bool isRegisteredAfter = await TheGameSingleton.Instance.TheGameController.GetIsRegisteredAsync();

                        _outputTextStringBuilder.Clear();
                        _outputTextStringBuilder.AppendHeaderLine($"UnregisterAsync()");
                        _outputTextStringBuilder.AppendBullet($"result = {isRegisteredAfter}");
       
                        await RefreshUIAsync();
                    });
            }

        }

        
        private async void RegisterButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            bool isRegistered = await TheGameSingleton.Instance.TheGameController.GetIsRegisteredAsync();

            if (isRegistered)
            {
                await TheGameSingleton.Instance.TheGameController.ShowMessageCustomAsync(
                    "Already Registered.", 1000);
            }
            else
            {
                await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                    TheGameConstants.Registering,
                    async delegate()
                    {
                        await TheGameSingleton.Instance.TheGameController.RegisterAsync();

                        bool isRegisteredAfter = await TheGameSingleton.Instance.TheGameController.GetIsRegisteredAsync();

                        _outputTextStringBuilder.Clear();
                        _outputTextStringBuilder.AppendHeaderLine($"RegisterAsync()");
                        _outputTextStringBuilder.AppendBullet($"result = {isRegisteredAfter}");

                        await RefreshUIAsync();
                    });
            }
        }


        
        private async void SetGoldByPlusButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Updating,
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.SetGoldByAsync(2);
                    
                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"AddGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {gold}");

                    await RefreshUIAsync();
                });
        }


        private async void SetGoldByMinusButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Updating,
                async delegate ()
                {
                    int gold = await TheGameSingleton.Instance.TheGameController.SetGoldByAsync(-1);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"SpendGold()");
                    _outputTextStringBuilder.AppendBullet($"result = {gold}");

                    await RefreshUIAsync();
                });
        }

        
        private async void AddTreasurePrizeButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Updating,
                async delegate ()
                {
                    MoralisUser moralisUser = await TheGameSingleton.Instance.GetMoralisUserAsync();
                    TreasurePrizeMetadata treasurePrizeMetadata = new TreasurePrizeMetadata
                    {
                        Title = "test 123",
                        Price = 10
                    };
                    string metadata = TheGameHelper.ConvertMetadataObjectToString(treasurePrizeMetadata);
                    TreasurePrizeDto treasurePrizeDto = new TreasurePrizeDto(moralisUser.ethAddress, metadata);
                    List <TreasurePrizeDto> treasurePrizeDtos = 
                        await TheGameSingleton.Instance.TheGameController.AddTreasurePrizeAsync(treasurePrizeDto);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"AddTreasurePrize()");
                    _outputTextStringBuilder.AppendBullet($"result.Count = {treasurePrizeDtos.Count}");

                    await RefreshUIAsync();
                    
                });
        }

        
        private async void SellTreasurePrizeButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }
            
            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Selling,
                async delegate ()
                {
                    List<TreasurePrizeDto> treasurePrizeDtos = 
                        await TheGameSingleton.Instance.TheGameController.GetTreasurePrizesAsync();

                    if (treasurePrizeDtos.Count == 0)
                    {
                        Custom.Debug.LogWarning("Nothing to sell. That is ok.");
                        return;
                    }

                    // Sell the most recent
                    TreasurePrizeDto treasurePrizeDto = treasurePrizeDtos[treasurePrizeDtos.Count-1];
                    List<TreasurePrizeDto> treasurePrizeDtosAfter = 
                        await TheGameSingleton.Instance.TheGameController.SellTreasurePrizeAsync(treasurePrizeDto);

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"SellTreasurePrize()");
                    _outputTextStringBuilder.AppendBullet($"result.Count was = {treasurePrizeDtos.Count}");
                    _outputTextStringBuilder.AppendBullet($"result.Count is  = {treasurePrizeDtosAfter.Count}");

                    await RefreshUIAsync();
                });
        }
        
        private async void DeleteAllTreasurePrizesButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }
            
            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Deleting,
                async delegate ()
                {
                    List<TreasurePrizeDto> treasurePrizeDtos = 
                        await TheGameSingleton.Instance.TheGameController.DeleteAllTreasurePrizeAsync();

                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"DeleteAllTreasurePrizeAsync()");
                    _outputTextStringBuilder.AppendBullet($"result.Count = {treasurePrizeDtos.Count}");

                    await RefreshUIAsync();
                });
        }

        
        
            
        private async void RewardPrizesButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            if (!await EnsureIsRegisteredAsync())
            {
                return;
            }

            await TheGameSingleton.Instance.TheGameController.ShowMessageActiveAsync(
                TheGameConstants.Opening,
                async delegate ()
                {
                    int goldAmount = 22;
                    await TheGameSingleton.Instance.TheGameController.StartGameAndGiveRewardsAsync(goldAmount);

                    Reward reward = await TheGameSingleton.Instance.TheGameController.GetRewardsHistoryAsync();
                    
                    _outputTextStringBuilder.Clear();
                    _outputTextStringBuilder.AppendHeaderLine($"StartGameAndGiveRewards()");
                    _outputTextStringBuilder.AppendBullet($"Gold Spent = {goldAmount}");
                    _outputTextStringBuilder.AppendBullet($"reward.Title = {reward.Title}");
                    _outputTextStringBuilder.AppendBullet($"reward.Type = {reward.Type}");
                    _outputTextStringBuilder.AppendBullet($"reward.Price = {reward.Price}");
                    

                    await RefreshUIAsync();
                });
        }


        private void BackButtonUI_OnClicked()
        {
                          
            TheGameSingleton.Instance.TheGameController.PlayAudioClipClick();

            TheGameSingleton.Instance.TheGameController.LoadSettingsSceneAsync();
        }
    }
}