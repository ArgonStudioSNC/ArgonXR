using UnityEngine;
using UnityEngine.UI;
using static ProjectManager;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ProjectLoader : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Image progression;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private Project m_project;
    private bool m_dead = false;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Update()
    {   
        if (!m_dead && m_project.handle.IsValid())
        {
            progression.fillAmount = m_project.handle.PercentComplete;

            if (m_project.handle.Status == AsyncOperationStatus.Succeeded)
            {
                m_dead = true;
                LoadingCompleted();
            }
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void SetProject(Project p)
    {
        if (p != null) m_project = p;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private void LoadingCompleted()
    {
        Debug.Log("Project " + m_project.id + " " + m_project.name + " loaded successfully!" );

        Addressables.InstantiateAsync(m_project.launcherReference.RuntimeKey, transform.parent).Completed += Completed;

        void Completed(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.IsValid())
            {
                Destroy(gameObject);
                LayoutRebuilder.ForceRebuildLayoutImmediate(obj.Result.transform.GetComponent<RectTransform>());
            }
        }
    }

    #endregion // PRIVATE_METHODS
}
