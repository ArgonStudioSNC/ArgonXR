using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Animator defaultPanel;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private Animator m_currentPanelAnimator = null;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    // Start is called before the first frame update
    protected void Start()
    {
        defaultPanel.Play("Middle");
        m_currentPanelAnimator = defaultPanel;
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void PanelUp(Animator panelAnimator)
    {
        m_currentPanelAnimator.SetTrigger("GoUp");
        panelAnimator.SetTrigger("GoUp");
        m_currentPanelAnimator = panelAnimator;

        Transform info = m_currentPanelAnimator.transform.Find("Show Projects/Scroll View/Viewport/Content");
        if (info) LayoutRebuilder.ForceRebuildLayoutImmediate(info.GetComponent<RectTransform>());
    }

    public void PanelDown(Animator panelAnimator)
    {
        m_currentPanelAnimator.SetTrigger("GoDown");
        panelAnimator.SetTrigger("GoDown");
        m_currentPanelAnimator = panelAnimator;
    }

    public void BackToDefaultPanel()
    {

        m_currentPanelAnimator.SetTrigger("GoDown");
        defaultPanel.SetTrigger("GoDown");
        m_currentPanelAnimator = defaultPanel;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS



    #endregion // PRIVATE_METHODS
}
