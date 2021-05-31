﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public enum LoadingState
    {
        Inactive,
        Loading,
        Success,
        Error
    }

    #region PUBLIC_MEMBER_VARIABLES

    public LoadingState ActiveLoadingState { get; set; }
    public AsyncOperationHandle<SceneInstance> handle { get; set; }

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField]
    private CanvasGroup m_canvasGroup = null;
    [SerializeField]
    private Image m_loadingLogo = null;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {

    }

    protected void Update()
    {
        //Debug.Log(ActiveLoadingState);
        switch (ActiveLoadingState)
        {
            case LoadingState.Inactive:
                break;
            case LoadingState.Loading:
                if (!handle.IsDone)
                {
                    ActiveLoadingState = LoadingState.Success;
                }
                break;
            case LoadingState.Success:
                ActiveLoadingState = LoadingState.Inactive;
                break;
            case LoadingState.Error:
            default:
                break;
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public static SceneLoader Instance { get; set; }

    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Loading scene by name : " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByAddress(string address)
    {
        if(Instance.ActiveLoadingState == LoadingState.Inactive)
        {
            Instance.ActiveLoadingState = LoadingState.Loading;
            Debug.Log("Loading scene by address : " + address);
            Instance.StartCoroutine(Instance.StartLoad(address));
        }
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private IEnumerator StartLoad(string address)
    {
        yield return StartCoroutine(FadeLoadingScreen(1, 0.3f));

        yield return StartCoroutine(Rotate(0.5f));
        yield return StartCoroutine(Rotate(0.5f));
        yield return StartCoroutine(Rotate(0.5f));

        handle = Addressables.LoadSceneAsync(address, LoadSceneMode.Single, true);
        while (ActiveLoadingState == LoadingState.Loading)
        {
            yield return StartCoroutine(Rotate(0.5f));
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 0.3f));
    }

    private IEnumerator FadeLoadingScreen(float targetValue, float duration)
    { 
        float startValue = m_canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            m_canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        m_canvasGroup.alpha = targetValue;
    }

    private IEnumerator Rotate(float duration)
    {
        float startValue = m_loadingLogo.fillAmount;
        float targetValue = 1.0f - startValue;
        float time = 0;

        m_loadingLogo.fillClockwise = targetValue == 1.0f ? true : false;
        while (time < duration)
        {
            m_loadingLogo.fillAmount = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        m_loadingLogo.fillAmount = targetValue;
    }

    #endregion // PRIVATE_METHODS
}