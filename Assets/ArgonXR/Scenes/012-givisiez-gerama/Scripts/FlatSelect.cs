using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatSelect : MonoBehaviour
{
    public Text title;
    public Text subTitle;
    public Transform[] entities;

    protected void Start()
    {
        for (int i = 0; i<entities.Length; i++)
        {
            int j = i;
            Button button = entities[j].GetComponentInChildren<Button>();
            button.onClick.AddListener(delegate{ SelectFlat(j); } );
        }
        SelectFlat(PlayerPrefs.GetInt("012_givisiez_flatId"));
    }

    protected void SelectFlat(int id)
    {
        Flat flat = entities[id].GetComponentInChildren<Flat>();
        Debug.Log("Loading texture " + flat.title.text);
        RenderSettings.skybox.SetTexture("_MainTex", flat.flatTexture);
        title.text = flat.title.text;
        subTitle.text = flat.subTitle.text;
        PlayerPrefs.SetInt("012_givisiez_flatId", id);

        entities[id].gameObject.SetActive(false);
        foreach (Transform entity in entities)
        {
            entity.gameObject.SetActive(true);
        }
    }
}
