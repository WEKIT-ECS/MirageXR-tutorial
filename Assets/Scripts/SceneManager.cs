using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    private bool _flying;

    [SerializeField] private GameObject uploadingPanel;
    [SerializeField] private GameObject termOfUSePabel;

    [SerializeField] private GameObject existingPoiListPanel;
    [SerializeField] private GameObject addPoiListPanel;

    [SerializeField] private GameObject audioEditorPanel;
    [SerializeField] private GameObject audioEditorSettingPanel;

    [SerializeField] private GameObject onScreenCanvas;

    [SerializeField] private Slider fieldOfViewSlider;
    [SerializeField] private Slider cameraRotationSlider;

    [SerializeField] private GameObject modelLoginPanel;
    [SerializeField] private GameObject modelSearchPanel;
    [SerializeField] private GameObject modelPreviewPanel;


    [SerializeField] private GameObject moodlePanel;

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject moodleConfigurationPanel;

    [SerializeField] private Toggle flyCamera;

    [SerializeField] private GameObject notificationText;

    public Slider CameraRotationSlider
    {
        get
        {
            return cameraRotationSlider;
        }
    }


    private void Start()
    {
        if (!Instance)
            Instance = this;
        else if (this != Instance)
            Destroy(gameObject);
    }


    public void TogglepoiListPanelVisibility()
    {
        existingPoiListPanel.SetActive(!existingPoiListPanel.activeInHierarchy);
        addPoiListPanel.SetActive(!addPoiListPanel.activeInHierarchy);
    }

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



    public void ToggleModelSearchPanel()
    {
        modelLoginPanel.SetActive(!modelLoginPanel.activeInHierarchy);
        modelSearchPanel.SetActive(!modelSearchPanel.activeInHierarchy);
    }


    public void ShowModelPreviewPanel()
    {
        modelPreviewPanel.SetActive(!modelPreviewPanel.activeInHierarchy);
    }


    public void ToggleMoodleConfigurationPanelVisibility()
    {
        loginPanel.SetActive(!loginPanel.activeInHierarchy);
        moodleConfigurationPanel.SetActive(!moodleConfigurationPanel.activeInHierarchy);
    }


    public void ToggleMoodlePanelVisibility()
    {
        moodlePanel.SetActive(!moodlePanel.activeInHierarchy);
    }

    public void CaptureScreenShot()
    {
        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {
        onScreenCanvas.SetActive(false);
        fieldOfViewSlider.gameObject.SetActive(false);
        flyCamera.gameObject.SetActive(false);
        cameraRotationSlider.gameObject.SetActive(false);

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
        fieldOfViewSlider.gameObject.SetActive(true);
        flyCamera.gameObject.SetActive(true);
        cameraRotationSlider.gameObject.SetActive(true);
    }


    public void AdjustFOV()
    {
        Camera.main.fieldOfView = fieldOfViewSlider.value;
    }

    public void ResetFOV()
    {
        Camera.main.fieldOfView = 60;
    }

    public void RotateCamera()
    {
        var camRotation = Camera.main.transform.rotation;
        Camera.main.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, cameraRotationSlider.value);
    }


    public void ResetRotation()
    {
        var camRotation = Camera.main.transform.rotation;
        Camera.main.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
    }



    public void OnFlyToggle()
    {
        _flying = !_flying;

        SceneManager.Instance.CameraRotationSlider.interactable = _flying;

        if (_flying)
        {
            Destroy(Camera.main.GetComponent<Rigidbody>());
            Camera.main.GetComponent<SimpleCameraController>().enabled = false;
            Camera.main.GetComponent<FlyCamera>().enabled = true;
        }
        else
        {
            var rb =  Camera.main.gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.mass = 50;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            Camera.main.GetComponent<SimpleCameraController>().enabled = true;
            Camera.main.GetComponent<FlyCamera>().enabled = false;

        }
    }


    public void RotationNotification(bool visibility)
    {
        if (!_flying && visibility) notificationText.SetActive(true);
        else notificationText.SetActive(false);

    }

}
