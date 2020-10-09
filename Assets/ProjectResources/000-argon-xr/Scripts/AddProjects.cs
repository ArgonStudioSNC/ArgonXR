using System.Collections.Generic;
using UnityEngine;
using static ProjectManager;

public class AddProjects : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Transform SelectedProjectContainer;
    public Transform UnselectedProjectContainer;

    public GameObject addProjectPanelElem;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private Transform m_noSelectedProject;
    private Transform m_allSelectedProject;
    private ProjectManager m_projectManager;
    private List<Project> m_selectedProjectList = new List<Project>();
    private List<Project> m_unselectedProjectList = new List<Project>();

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Start()
    {
        m_projectManager = FindObjectOfType<ProjectManager>();
        m_noSelectedProject = SelectedProjectContainer.GetChild(0);
        m_allSelectedProject = UnselectedProjectContainer.GetChild(0);
    }

    protected void Update()
    {
        m_noSelectedProject.gameObject.SetActive(m_selectedProjectList.Count == 0);
        m_allSelectedProject.gameObject.SetActive(m_unselectedProjectList.Count == 0);
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Sync()
    {
        Debug.Log("Syncing AddProjectPanel with ProjectManager");

        m_selectedProjectList = new List<Project>();
        m_unselectedProjectList = new List<Project>();

        foreach (Transform child in SelectedProjectContainer)
        {
            if (child != m_noSelectedProject) Destroy(child.gameObject);
        }

        foreach (Transform child in UnselectedProjectContainer)
        {
            if (child != m_allSelectedProject) Destroy(child.gameObject);
        }

        foreach (Project p in m_projectManager.activeProjects)
        {
            AddSelectedProjectElem(p);
        }

        foreach (Project p in m_projectManager.projectList)
        {
            if (!m_projectManager.activeProjects.Contains(p))
            {
                AddUnselectedProjectElem(p);
            }
        }
    }

    public void ValidateChanges()
    {
        m_projectManager.SetActiveProjects(m_selectedProjectList);
    }

    public void ResetChanges()
    {
        m_projectManager.SetActiveProjects(m_projectManager.activeProjects);
    }

    public void AddSelectedProjectElem(Project project)
    {
        Project p = m_unselectedProjectList.Find(e => e.id == project.id);
        if (p != null) m_unselectedProjectList.Remove(p);

        m_selectedProjectList.Add(project);

        GameObject elem = Instantiate(addProjectPanelElem);
        elem.GetComponent<AddProjectPanelElem>().Setup(project, true);
        elem.transform.SetParent(SelectedProjectContainer, false);
    }

    public void AddUnselectedProjectElem(Project project)
    {
        Project p = m_selectedProjectList.Find(e => e.id == project.id);
        if (p != null) m_selectedProjectList.Remove(p);

        m_unselectedProjectList.Add(project);

        GameObject elem = Instantiate(addProjectPanelElem);
        elem.GetComponent<AddProjectPanelElem>().Setup(project, false);
        elem.transform.SetParent(UnselectedProjectContainer, false);
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS



    #endregion // PRIVATE_METHODS
}