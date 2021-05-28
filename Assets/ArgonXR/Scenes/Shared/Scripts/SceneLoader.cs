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

    private AsyncOperationHandle m_test;
    private bool a = false;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {

    }

    protected void Start()
    {

    }

    protected void Update()
    {
        if (a)
        {

            Debug.Log(m_test.Status);
        }

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

        m_test = Addressables.LoadSceneAsync(address, LoadSceneMode.Single);
        a = true;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    #endregion // PRIVATE_METHODS
}