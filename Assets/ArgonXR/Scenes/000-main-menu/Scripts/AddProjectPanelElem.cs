using UnityEngine;
using UnityEngine.UI;
using static ProjectManager;

public class AddProjectPanelElem : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Text nameText;
    public Text clientText;
    public Button button;
    public Sprite add;
    public Sprite remove;

    #endregion // PUBLIC_MEMBER_VARIABLES


    #region PUBLIC_METHODS

    public void Setup(Project projet, bool isSelected)
    {
        nameText.text = projet.name;
        clientText.text = projet.client;
        button.image.sprite = isSelected ? remove : add;

        button.onClick.AddListener(ButtonClicked);
        
        void ButtonClicked()
        {
            if (isSelected) GetComponentInParent<AddProjects>().AddUnselectedProjectElem(projet);
            else GetComponentInParent<AddProjects>().AddSelectedProjectElem(projet);
            Destroy(gameObject);
        }
    }

    #endregion // PUBLIC_METHODS

}
