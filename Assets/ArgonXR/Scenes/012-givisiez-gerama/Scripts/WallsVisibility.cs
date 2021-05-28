using UnityEngine;

public class WallsVisibility : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Transform[] walls;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private bool m_visible = false;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Start()
    {

    }

    protected void Update()
    {
        foreach (Transform wall in walls)
        {
            wall.gameObject.SetActive(m_visible);
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Visible(bool visible)
    {
        m_visible = visible;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS


    #endregion // PRIVATE_METHODS
}
