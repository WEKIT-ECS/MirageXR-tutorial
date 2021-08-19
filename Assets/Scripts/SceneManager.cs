using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [SerializeField] private GameObject uploadingPanel;
    [SerializeField] private GameObject termOfUSePabel;

    public void ToggleUploadingPanelVisibility()
    {
        uploadingPanel.SetActive(!uploadingPanel.activeInHierarchy);
    }

    public void ToggleTermOfUsePanelVisibility()
    {
        termOfUSePabel.SetActive(!termOfUSePabel.activeInHierarchy);
    }
}
