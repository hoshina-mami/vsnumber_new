using UnityEngine;
using System.Collections;

public class AdBannerObserver : MonoBehaviour {
    public enum LayoutGravity {
		NO_GRAVITY = 0,
		CENTER_HORIZONTAL = 1,
		LEFT = 3,
		RIGHT = 5,
		FILL_HORIZONTAL = 7,
		CENTER_VERTICAL = 16,
		CENTER = 17,
		TOP = 48,
		BOTTOM = 80,
		FILL_VERTICAL = 112
    };

    private static AdBannerObserver sInstance;
    
    public static void Initialize() {
        Initialize(null, null, 0.0f, (int)LayoutGravity.BOTTOM);
    }
    
    public static void Initialize(string publisherId, string testDeviceId, float refresh, int layoutGravity) {
        if (sInstance == null) {
            // Make a game object for observing.
            GameObject go = new GameObject("_AdBannerObserver");
            go.hideFlags = HideFlags.HideAndDontSave;
            //DontDestroyOnLoad(go);
            // Add and initialize this component.
            sInstance = go.AddComponent<AdBannerObserver>();
            sInstance.mAdMobPublisherId = publisherId;
            sInstance.mAdMobTestDeviceId = testDeviceId;
            sInstance.mRefreshTime = refresh;
			sInstance.mLayoutGravity = layoutGravity;
        }
    }
    
	public static void Destruct() {
		Destroy(sInstance.gameObject);
	}

    public string mAdMobPublisherId;
    public string mAdMobTestDeviceId;
    public float mRefreshTime;
	public int mLayoutGravity;
    
#if UNITY_ANDROID && !UNITY_EDITOR
	AndroidJavaClass plugin;
	AndroidJavaClass unityPlayer;
	AndroidJavaObject activity;
#endif

    IEnumerator Start () {
#if UNITY_ANDROID && !UNITY_EDITOR
        plugin = new AndroidJavaClass("jp.radiumsoftware.unityplugin.admob.AdBannerController");
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        while (true) {
            plugin.CallStatic("tryCreateBanner", activity, mAdMobPublisherId, mAdMobTestDeviceId, mLayoutGravity);
            yield return new WaitForSeconds(Mathf.Max(30.0f, mRefreshTime));
        }
#else
        return null;
#endif
    }

	void OnDestroy() {
#if UNITY_ANDROID && !UNITY_EDITOR
		activity.Dispose();
		unityPlayer.Dispose();
		plugin.Dispose();
#endif
	}
}
