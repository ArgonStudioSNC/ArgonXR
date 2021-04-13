using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class GeramaLauncher : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public AssetReference infoPanelRef;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private PanelManager m_panelManager;
    private GameObject m_infoPanel;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Start()
    {
        m_panelManager = FindObjectOfType<PanelManager>();
        if (m_panelManager)
        {
            if(infoPanelRef.RuntimeKeyIsValid())
            {
                Addressables.InstantiateAsync(infoPanelRef.RuntimeKey).Completed += Callback;
            }
        }

        void Callback(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.IsValid())
            {
                m_infoPanel = obj.Result;

                m_infoPanel.transform.Find("Bottom/CloseButton").GetComponent<Button>().onClick.AddListener(OnClick);

                void OnClick()
                {
                    m_panelManager.BackToDefaultPanel();
                }

                m_infoPanel.transform.SetParent(m_panelManager.transform, false);
            }
        }
    }

    protected void OnDestroy()
    {
        Destroy(m_infoPanel);
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void OpenInfoPanel()
    {
        m_panelManager.PanelUp(m_infoPanel.GetComponent<Animator>());
    }

    #endregion // PUBLIC_METHODS
}
