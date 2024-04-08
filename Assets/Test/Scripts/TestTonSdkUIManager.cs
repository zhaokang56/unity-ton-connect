// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using TonSdk.Connect;
// using TonSdk.Core;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class TestTonSdkUIManager : MonoBehaviour
// {
//     [Tooltip("Toggle if you want to use presaved wallet icons. (recommended)")]
//     public bool UseSavedWalletIcons = true;
//     [Tooltip("Wallet icons. Works only if UseSavedWalletIcons is enabled.")]
//     public List<Sprite> WalletIcons = new ();
//     private List<string> WalletsIconsList = new () {"tonkeeper", "tonhub", "openmask", "dewallet", "mytonwallet", "tonflow", "tonwallet", "xtonwallet"};
//
//     [Header("References")]
//     [SerializeField] private TonConnectHandler tonConnectHandler;
//     
//     [Header("UI")]
//     public TextMeshProUGUI addressText;
//     public Button ConnectWalletButton;
//     public GameObject testPanel;
//
//     public Button closeBtn;
//     public Button sendBtn;
//     // Start is called before the first frame update
//     void Awake()
//     {
//         TonConnectHandler.OnProviderStatusChanged += OnProviderStatusChange;
//         TonConnectHandler.OnProviderStatusChangedError += OnProviderStatusChangeError;
//         ConnectWalletButton.onClick.AddListener();
//         DisableSendTXModal();
//         DisableWalletInfoButton();
//         EnableConnectWalletButton();
//     }
//
//     private void OnProviderStatusChange(Wallet wallet)
//     {
//         if(tonConnectHandler.tonConnect.IsConnected)
//         {
//             Debug.Log("Wallet connected. Address: " + wallet.Account.Address + ". Platform: " + wallet.Device.Platform + "," + wallet.Device.AppName + "," + wallet.Device.AppVersion);
//             CloseConnectModal();
//             DisableConnectWalletButton();
//             EnableWalletInfoButton(ProcessWalletAddress(wallet.Account.Address.ToString(AddressType.Base64)));
//         }
//         else
//         {
//             EnableConnectWalletButton();
//             DisableWalletInfoButton();
//         }
//     }
//
//     private void OnProviderStatusChangeError(string message)
//     {
//
//     }
//     private void DisableSendTXModal()
//     {
//         testPanel.SetActive(false);
//     }
//     
//     private void ShowSendTXModal()
//     {
//         testPanel.SetActive(true);
//     }
//     
//     private void EnableConnectWalletButton()
//     {
//         // enable connect button
//         document.rootVisualElement.Q<VisualElement>("ConnectWalletButton").UnregisterCallback<ClickEvent>(ConnectWalletButtonClick);
//         document.rootVisualElement.Q<VisualElement>("ConnectWalletButton").RegisterCallback<ClickEvent>(ConnectWalletButtonClick);
//         document.rootVisualElement.Q<VisualElement>("ConnectWalletButton").style.display = DisplayStyle.Flex;
//     }
//     
//     private void DisableWalletInfoButton()
//     {
//         // disable wallet info and disconnect button
//         document.rootVisualElement.Q<VisualElement>("WalletInfoButton").style.display = DisplayStyle.None;
//         document.rootVisualElement.Q<VisualElement>("DisconnectWalletButton").style.display = DisplayStyle.None;
//     }
//     
//     private void ConnectWalletButtonClick()
//     {
//         ShowConnectModal();
//     }
//     
//     public void ShowConnectModal()
//     {
//         if (tonConnectHandler.tonConnect.IsConnected)
//         {
//             Debug.LogWarning("Wallet already connected. The connection window has not been opened. Before proceeding, please disconnect from your wallet.");
//             document.rootVisualElement.Q<VisualElement>("ConnectModal").style.display = DisplayStyle.None;
//             return;
//         }
//
//         document.rootVisualElement.Q<Label>("ConnectModal_Title").text = "Choose Wallet";
//
//         document.rootVisualElement.Q<VisualElement>("ModalContent").style.display = DisplayStyle.Flex;
//         document.rootVisualElement.Q<VisualElement>("ModalContentWeb").style.display = DisplayStyle.Flex;
//         document.rootVisualElement.Q<VisualElement>("ModalQRContent").style.display = DisplayStyle.None;
//         document.rootVisualElement.Q<VisualElement>("Button_Back").style.display = DisplayStyle.None;
//         document.rootVisualElement.Q<VisualElement>("ConnectModal").style.display = DisplayStyle.Flex;
//
//         document.rootVisualElement.Q<VisualElement>("Button_Back").UnregisterCallback<ClickEvent>(BackToMainContent);
//         document.rootVisualElement.Q<VisualElement>("Button_Back").RegisterCallback<ClickEvent>(BackToMainContent);
//         document.rootVisualElement.Q<VisualElement>("Button_Close").UnregisterCallback<ClickEvent>(CloseConnectModal);
//         document.rootVisualElement.Q<VisualElement>("Button_Close").RegisterCallback<ClickEvent>(CloseConnectModal);
//
//         Debug.Log("hello");
//         StartCoroutine(tonConnectHandler.LoadWallets("https://raw.githubusercontent.com/ton-blockchain/wallets-list/main/wallets.json", LoadWalletsCallback));
//     }
// }
