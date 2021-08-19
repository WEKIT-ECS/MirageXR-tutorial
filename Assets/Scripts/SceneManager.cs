using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [SerializeField] private GameObject uploadingPanel;
    [SerializeField] private GameObject termOfUSePabel;

    [SerializeField] private GameObject audioEditorPanel;
    [SerializeField] private GameObject audioEditorSettingPanel;

    [SerializeField] private GameObject onScreenCanvas;

    public void ToggleUploadingPanelVisibility()
    {
        uploadingPanel.SetActive(!uploadingPanel.activeInHierarchy);
    }

    public void ToggleTermOfUsePanelVisibility()
    {
        termOfUSePabel.SetActive(!termOfUSePabel.activeInHierarchy);
    }

    public void ToggleAudioEditorSettingPanelVisibility()
    {
        audioEditorPanel.SetActive(!audioEditorPanel.activeInHierarchy);
        audioEditorSettingPanel.SetActive(!audioEditorSettingPanel.activeInHierarchy);
    }

    public void CaptureScreenShot()
    {
        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {
        onScreenCanvas.SetActive(false);
        var random = new System.Random();

        string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Screenshots/";

        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName),2);
        Debug.Log(folderPath + screenshotName);

        yield return new WaitForSeconds(0.2f);
        onScreenCanvas.SetActive(true);
    }
}
