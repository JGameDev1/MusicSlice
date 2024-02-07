using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    public Button HideBannerButton;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    void Awake()
    { InitializeAds(); }

    private void Start()
    {Advertisement.Banner.SetPosition(_bannerPosition);}

    public void InitializeAds()
    {_gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    { Debug.Log("Unity Ads initialization complete.");LoadBanner();}

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    { Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}"); }

    void LoadBanner(){Advertisement.Banner.Load("Banner_Android", new BannerLoadOptions{loadCallback=OnBannerLoad,errorCallback=OnBannerError});}

    void OnBannerLoad(){Advertisement.Banner.Show("Banner_Android");}
    void OnBannerError(string Message){Debug.Log("Banner Error");}
    void BannerHide(){Advertisement.Banner.Hide();}
}