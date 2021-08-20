using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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


    private Camera _cam;
    private GameObject _player;

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


        _cam = Camera.main;
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player.GetComponent<SimpleCameraController>() == null)
            _player.AddComponent<SimpleCameraController>();
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
        _cam.fieldOfView = fieldOfViewSlider.value;
    }

    public void ResetFOV()
    {
        _cam.fieldOfView = 60;
        fieldOfViewSlider.value = 60;
    }

    public void RotateCamera()
    {
        var camRotation = _cam.transform.rotation;
        _cam.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, cameraRotationSlider.value);
    }


    public void ResetRotation()
    {
        var camRotation = _cam.transform.rotation;
        _cam.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
    }



    public void OnFlyToggle()
    {
        _flying = !_flying;

        CameraRotationSlider.interactable = _flying;

        if (_flying)
        {
            Destroy(_player.GetComponent<Rigidbody>());

            if (_player.GetComponent<SimpleCameraController>())
                Destroy(_player.GetComponent<SimpleCameraController>());
            _player.AddComponent<FlyCamera>();
        }
        else
        {
            var rb = _player.gameObject.AddComponent<Rigidbody>();
            rb.mass = 50;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.freezeRotation = true;

            if (_player.GetComponent<FlyCamera>())
                Destroy(_player.GetComponent<FlyCamera>());
            _player.AddComponent<SimpleCameraController>();
        }
    }


    public void RotationNotification(bool visibility)
    {
        if (!_flying && visibility) notificationText.SetActive(true);
        else notificationText.SetActive(false);

    }


    public void Exit()
    {
        Application.Quit();
    }
}
