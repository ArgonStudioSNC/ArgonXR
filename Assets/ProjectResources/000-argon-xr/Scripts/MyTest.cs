using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MyTest : MonoBehaviour
{
    public string NextScene = "012-givisiez-gerama_ar";
    public AssetReference Level;

    long downloadSize;
    AsyncOperationHandle asyncHandle;

    void Awake()
    {
        //if (Level.RuntimeKeyIsValid()) Addressables.GetDownloadSizeAsync(Level.RuntimeKey).Completed += GetDownloadSize_Completed;
    }

    void GetDownloadSize_Completed(AsyncOperationHandle<long> obj)
    {
        if (obj.IsValid())
        {
            downloadSize = obj.Result;
            Debug.Log(downloadSize);
            asyncHandle = Addressables.DownloadDependenciesAsync(Level.RuntimeKey);
            asyncHandle.Completed += Preloader_Completed;
        }
    }

    void Preloader_Completed(AsyncOperationHandle obj)
    {
        if (obj.IsValid())
        {
            if (Level.RuntimeKeyIsValid()) Addressables.GetDownloadSizeAsync(Level.RuntimeKey).Completed += GetDownloadSize_Completed2;
        }
    }

    void GetDownloadSize_Completed2(AsyncOperationHandle<long> obj)
    {
        if (obj.IsValid())
        {
            downloadSize = obj.Result;
            Debug.Log(downloadSize);
        }
    }
}
