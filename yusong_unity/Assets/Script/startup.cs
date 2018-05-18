//
// Startup.cs
// Copyright (c) 2013 Fireproof Studios, All Rights Reserved
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PaperPlaneTools;
using UnityEngine.UI;

public class startup : MonoBehaviour
{
    private float alpha = 0.0f;
    private float alphaSpeed = 2.0f;
    private bool check = false;
    private bool Ask = true;

    // Private Data
    private enum eState
    {
        WaitingForStoragePermission,
        DownloadingObb,
        Initialising,
        LoadingMainMenu,
        UnsupportedDeviceScreen,
    }

    private bool LinkFireproofWebsiteEnabled = false;
    private AsyncOperation LoadingOp = null;
    private eState State;

    const float FireproofLogoDuration = 2.0f;

    private float FireproofLogoTimer = 0.0f;

#if UNITY_ANDROID && GOOGLE
    private const string ReadStoragePermission = "android.permission.READ_EXTERNAL_STORAGE";

    private static Dictionary<string, string> StorageMessageTitleLocalised = new Dictionary<string, string>
    {
        {"English", "Storage Permission"},
        {"French", "Autorisation de stockage"},
        {"German", "Speicher-Erlaubnis"},
        {"Italian", "The Room: Old Sins nécessite votre autorisation pour lire les données du jeu"},
        {"Spanish", "Permiso de almacenamiento"},
        {"Russian", "Доступ к памяти"},
        {"Turkish", "Depolama İzni"},
        {"ChineseTraditional", "使用权限指南"},
        {"ChineseSimplified", "使用權限指南"},
        {"Japanese", "アクセス権限マニュアル"},
        {"Korean", "사용 권한 안내"}
    };

    private static Dictionary<string, string> StorageMessageLocalised = new Dictionary<string, string>
    {
        {"English", "The Room: Old Sins requires permission to read the game data file"},
        {"French", "The Room: Old Sins nécessite votre autorisation pour lire les données du jeu"},
        {"German", "The Room: Old Sins benötigt die Erlaubnis, Spieldaten zu lesen"},
        {"Italian", "The Room: Old Sins richiede i permessi per leggere il file dei dati del gioco"},
        {"Spanish", "The Room: Old Sins necesita permiso para leer el archivo de datos del juego"},
        {"Russian", "The Room: Old Sins требует разрешения на доступ для чтения файла игровых данных"},
        {"Turkish", "The Room: Old Sins'in oyun verisi dosyasını okuyabilmesi için izin gerekiyor"},
        {"Portuguese", "The Room: Old Sins requer permissão para fazer a leitura do arquivo de dados do jogo"},
        {"ChineseTraditional", "游戏需要访问存储空间，以保存游戏进度"},
        {"ChineseSimplified", "游戏需要访问存储空间，以保存游戏进度"},
        {"Japanese", "このゲームを利用するにはストレージへのアクセス権が必要となります"},
        {"Korean", " 저장공간확인 ，게임진도 저장"}
    };

    private static Dictionary<string, string> StorageMessageButtonLocalised = new Dictionary<string, string>
    {
        {"English", "OK"},
        {"French", "OK"},
        {"German", "OK"},
        {"Italian", "OK"},
        {"Spanish", "Vale"},
        {"Russian", "OK"},
        {"Turkish", "Tamam"},
        {"Portuguese", "OK"},
        {"ChineseTraditional", "確認"},
        {"ChineseSimplified", "确认"},
        {"Japanese", "同意する"},
        {"Korean", "확인"}
    };

    private static Dictionary<string, string> StorageMessageButtonquit = new Dictionary<string, string>
    {
        {"English", "Quit"},
        {"French", "Quit"},
        {"German", "Quit"},
        {"Italian", "Quit"},
        {"Spanish", "Quit"},
        {"Russian", "Quit"},
        {"Turkish", "Quit"},
        {"Portuguese", "Quit"},
        {"ChineseTraditional", "退出"},
        {"ChineseSimplified", "退出"},
        {"Japanese", "キャンセル"},
        {"Korean", "나가기"}
    };

    private static Dictionary<string, string> StorageMessageLocalisedSetting = new Dictionary<string, string>
    {
        {"English", "Please enable the storage access permission in settings."},
        {"French", "Please enable the storage access permission in settings."},
        {"German", "Please enable the storage access permission in settings."},
        {"Italian", "Please enable the storage access permission in settings."},
        {"Spanish", "Please enable the storage access permission in settings."},
        {"Russian", "Please enable the storage access permission in settings."},
        {"Turkish", "Please enable the storage access permission in settings."},
        {"Portuguese", "Please enable the storage access permission in settings."},
        {"ChineseTraditional", "請在設置窗口中啟用存儲權限."},
        {"ChineseSimplified", "请在设置窗口中启用存储权限."},
        {"Japanese", "設定画面でストレージの許可を有効にしてください"},
        {"Korean", " 설정메뉴에서 저장기능을 열어주세요"}
    };
#endif
    //----------------------------------------------------------------------------------------
    public void Awake()
    {
    }

    //----------------------------------------------------------------------------------------
    public void Start()
    {
        CheckPermissionAndObb();
    }

    public void Initialise()
    {
        State = eState.Initialising;
    }

    //----------------------------------------------------------------------------------------
    public void Update()
    {
#if UNITY_ANDROID && GOOGLE
        if (Application.platform == RuntimePlatform.Android)
        {
            if (AndroidPermissionsManager.IsPermissionGranted(ReadStoragePermission) && check)
            {
                Debug.Log("");
                CheckPermissionAndObb();
                check = false;
            }
            else if (!AndroidPermissionsManager.IsPermissionGranted(ReadStoragePermission) && check)
            {
                Debug.Log("");
                check = false;
                CheckPermissionAndObb();
            }
        }
#endif

        switch (State)
        {
            case eState.WaitingForStoragePermission:
                break;
            case eState.DownloadingObb:
                //Debug.Log( "Downloading Obb" );
                break;

            case eState.Initialising:
                break;

            case eState.LoadingMainMenu:
                if (LoadingOp != null)
                {
                    if (LoadingOp.isDone)
                    {
                        LoadingOp = null;
                    }
                }
                break;

            case eState.UnsupportedDeviceScreen:
                if (LinkFireproofWebsiteEnabled)
                {
                    if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
                    {
                        Application.OpenURL("http://fireproofgames.com/the-room-two-refund");
                    }
                }
                break;

            default:
                break;
        }
    }

#if UNITY_ANDROID && GOOGLE

    private void CheckPermissionAndObb()
    {
#if UNITY_EDITOR
        if (!CanLoadFromResources())
#else
            if ( !CanLoadFromResources() || !AndroidPermissionsManager.IsPermissionGranted(ReadStoragePermission))
#endif
        {
            Debug.Log("【ldd】Failed to load the resources test file");

            if (!AndroidPermissionsManager.IsPermissionGranted(ReadStoragePermission))
            {
                Debug.Log("【ldd】We don't have permission to read storage, request it now");
                ShowStoragePermisisonMessage();
            }
            else
            {
                Debug.Log("【ldd】Assume we don't have the OBB and start download process");
               // DownloadObb();
            }
        }
        else
        {
            Debug.Log("Loaded the resources test file");
            Initialise();
        }
    }

    private bool CanLoadFromResources()
    {
        Texture test = Resources.Load<Texture>("WaterPourHelmet/WaterPourHelmet_000");
        return test != null;
    }

    private void ShowStoragePermisisonMessage()
    {
        State = eState.WaitingForStoragePermission;


        string languageKey = "English";


        string storageMessageTitle = StorageMessageTitleLocalised["English"];
        string storageMessage = StorageMessageLocalised["English"];
        string storageMessageButton = StorageMessageButtonLocalised["English"];
        string storageMessageButtonQuit = StorageMessageButtonquit["English"];
        string storageMessageSetting = StorageMessageLocalisedSetting["English"];

        if (StorageMessageTitleLocalised.ContainsKey(languageKey))
        {
            storageMessageTitle = StorageMessageTitleLocalised[languageKey];
        }
       
        if (StorageMessageLocalised.ContainsKey(languageKey))
        {
            if (!AndroidPermissionsManager.RequestPermissionRationale(ReadStoragePermission) && Ask == false)
            {
                Debug.Log("[ldd]此时准备调往设置界面");
                storageMessage = StorageMessageLocalisedSetting[languageKey];
            }
            else
            {
                storageMessage = StorageMessageLocalised[languageKey];
            }
        }

        if (StorageMessageButtonLocalised.ContainsKey(languageKey))
        {
            storageMessageButton = StorageMessageButtonLocalised[languageKey];
        }

        if (StorageMessageButtonquit.ContainsKey(languageKey))
        {
            storageMessageButtonQuit = StorageMessageButtonquit[languageKey];
        }


        // Simple native alert with button handler
        Alert aler = new Alert(storageMessageTitle, storageMessage);
        aler.SetPositiveButton(storageMessageButton, () =>
        {
            RequestStoragePermission();
        });

        aler.SetNegativeButton(storageMessageButtonQuit, () =>
        {
            appExit();
        })
        .Show();

    }

    private void appExit()
    {
        Application.Quit();
    }

    private void RequestStoragePermission()
    {

        Debug.Log("222222222RequestStoragePermission");
        AndroidPermissionsManager.RequestPermission(new[] { ReadStoragePermission }, new AndroidPermissionCallback(
                grantedPermission =>
                {
                    ReadPermissionGranted();
                    Debug.Log("ReadPermissionGranted1111111");
                },
                deniedPermission =>
                {
                    if (!AndroidPermissionsManager.RequestPermissionRationale(ReadStoragePermission) && Ask == false)
                    {
                        Debug.Log("AndroidPermissionsManager.RequestPermissionRationale(ReadStoragePermission");
                        AndroidPermissionsManager.GoSetting();
                        check = true;
                    }
                    else
                    {
                        ReadPermissionDenied();
                        Debug.Log("ReadPermissionDenied222222222");
                    }
                }));

    }

    public void ReadPermissionGranted()
    {
        Debug.Log("ReadPermissionGranted");
        check = true;
        //CheckPermissionAndObb();
    }

    public void ReadPermissionDenied()
    {

        if (!AndroidPermissionsManager.RequestPermissionRationale(ReadStoragePermission))
        {
            Debug.Log("AndroidPermissionsManager.RequestPermissionRationale(ReadStoragePermission");
            Ask = false;
        }
        CheckPermissionAndObb();

        Debug.Log("ReadPermissionDenied");
    }
#endif
    //----------------------------------------------------------------------------------------
    private void StartLoadingMainMenu()
    {
        State = eState.LoadingMainMenu;
    }

    //----------------------------------------------------------------------------------------
    private void GoToMainMenu()
    {
        // Authenticate with GameCenter

        GameObject.DestroyImmediate(gameObject);
        Resources.UnloadUnusedAssets();
    }

    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    private void EnableLink()
    {
        LinkFireproofWebsiteEnabled = true;
    }



}
