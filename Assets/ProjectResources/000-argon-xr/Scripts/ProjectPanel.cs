using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ProjectManager;

public class ProjectPanel : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Transform welcome;
    public Transform projectGrid;
    public GameObject loaderPrefab;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private ProjectManager m_projectManager;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    // Start is called before the first frame update
    protected void Start()
    {
        m_projectManager = FindObjectOfType<ProjectManager>();
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Sync()
    {
        Debug.Log("Syncing ProjectPanel with ProjectManager");

        List<Project> projects = m_projectManager.activeProjects;
        Transform container = projectGrid.Find("Container");

        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        foreach (Project project in projects)
        {
            GameObject go = Instantiate(loaderPrefab);
            go.GetComponent<ProjectLoader>().SetProject(project);
            go.transform.SetParent(container, false);
        }

        int n = projects.Count;
        welcome.gameObject.SetActive(n == 0);
        projectGrid.gameObject.SetActive(n > 0);
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS



    #endregion // PRIVATE_METHODS
}