using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ProjectManager : MonoBehaviour
{
    [System.Serializable]
    public class Project
    {
        public int id;
        public string name, client;
        public AssetReference launcherReference;
        public AssetLabelReference label;
        [HideInInspector]
        public AsyncOperationHandle handle;

        private long m_downloadSize;

        public Project(int id, string name, string client, AssetReference launcherReference, AssetLabelReference label)
        {
            this.id = id;
            this.name = name;
            this.client = client;
            this.launcherReference = launcherReference;
            this.label = label;
        }

        public void DownloadDependencies()
        {
            Debug.Log("Loading dependencies for project " + id + " : " + name);
            Addressables.GetDownloadSizeAsync(label).Completed += GetDownloadSizeCompleted;
        }

        protected void GetDownloadSizeCompleted(AsyncOperationHandle<long> obj)
        {
            if (obj.IsValid())
            {
                m_downloadSize = obj.Result;
                Debug.Log("Download size = " + m_downloadSize);
                handle = Addressables.DownloadDependenciesAsync(label);
                handle.Completed += DownloadCompleted;
            }
        }

        protected void DownloadCompleted(AsyncOperationHandle obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Dependencies successfully loaded");
            }
        }
    }

    #region PUBLIC_MEMBER_VARIABLES

    public List<Project> projectList;
    [HideInInspector]
    public List<Project> activeProjects = new List<Project>();

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PRIVATE_MEMBER_VARIABLES

    private ProjectPanel m_projectPanel;
    private AddProjects m_addProjects;
    private bool m_dirty;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS

    protected void Start()
    {
        //Caching.ClearCache();

        m_projectPanel = FindObjectOfType<ProjectPanel>();
        m_addProjects = FindObjectOfType<AddProjects>();

        Debug.Log("Number of projects : " + projectList.Count);

        int[] ids = PlayerPrefsX.GetIntArray("activeProjects");
        activeProjects = new List<Project>();
        foreach (int id in ids)
        {
            Project p = projectList.Find(e => e.id == id);
            if (p != null)
            {
                p.DownloadDependencies();
                activeProjects.Add(p);
            }
        }

        Debug.Log("Number of active projects : " + activeProjects.Count);

        m_dirty = true;

    }

    protected void Update()
    {
        if (m_dirty)
        {
            m_dirty = false;
            m_projectPanel.Sync();
            m_addProjects.Sync();
        }
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void SetActiveProjects(List<Project> projects)
    {
        Debug.Log("Setting active projects");

        List<int> ids = new List<int>();
        foreach (Project p in projects)
        {
            p.DownloadDependencies();
            ids.Add(p.id);
        }

        activeProjects = projects;

        Debug.Log("Number of active projects : " + activeProjects.Count);

        PlayerPrefsX.SetIntArray("activeProjects", ids.ToArray());

        m_dirty = true;
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    #endregion // PRIVATE_METHODS
}