using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class SceneLoader : MonoBehaviour
{

    #region PUBLIC_MEMBER_VARIABLES

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {

    }

    protected void Start()
    {

    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene by name : " + sceneName);

        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByAddress(string address)
    {
        Debug.Log("Loading scene by address : " + address);

        Addressables.LoadSceneAsync(address, LoadSceneMode.Single);
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    #endregion // PRIVATE_METHODS
}