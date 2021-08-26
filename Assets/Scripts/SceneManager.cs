using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    [SerializeField] private GameObject uploadingPanel;
    [SerializeField] private GameObject termOfUSePabel;

    [SerializeField] private GameObject existingPoiListPanel;
    [SerializeField] private GameObject addPoiListPanel;

    [SerializeField] private GameObject audioEditorPanel;
    [SerializeField] private GameObject audioEditorSettingPanel;

    [SerializeField] private GameObject onScreenCanvas;

    [SerializeField] private GameObject GuideText;
    [SerializeField] private Button captureButton;

    [SerializeField] private GameObject modelLoginPanel;
    [SerializeField] private GameObject modelSearchPanel;
    [SerializeField] private GameObject modelPreviewPanel;


    [SerializeField] private GameObject moodlePanel;

    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject moodleConfigurationPanel;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera screenShotCamera;
    [SerializeField] private GameObject playerCamera;

    [SerializeField] private GameObject MirageUIs;

    [SerializeField] private GameObject HololensModel;

    [SerializeField] private GameObject errorText;

    [SerializeField] private GameObject captureLabel;

    [SerializeField] private GameObject helmet;

    private bool screenShotMode;

    public bool ScreenShotMode
    {
        get
        {
            return screenShotMode;
        }
    }


    public bool CustomClipMode
    {
        get; set;
    }

    private void Start()
    {
        if (!Instance)
            Instance = this;
        else if (this != Instance)
            Destroy(gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ScreenShotModeToggle();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CaptureScreenShot();
        }
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
        if (!screenShotMode)
        {
            StartCoroutine(ShowErrorMessage());
            return;
        }

        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {
        onScreenCanvas.SetActive(false);
        GuideText.gameObject.SetActive(false);

        var random = new System.Random();

        string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Screenshots/";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                ".png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName),2);
        Debug.Log(folderPath + screenshotName);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(ShowCaptureLabel());
        onScreenCanvas.SetActive(true);
        GuideText.gameObject.SetActive(true);
    }


    IEnumerator ShowCaptureLabel()
    {
        captureLabel.SetActive(true);
        captureLabel.GetComponent<Animator>().Play("captureLabel");
        yield return new WaitForSeconds(1);
        captureLabel.SetActive(false);
        captureLabel.GetComponent<Animator>().StopPlayback();
    }


    IEnumerator ShowErrorMessage()
    {
        errorText.SetActive(true);
        yield return new WaitForSeconds(3);
        errorText.SetActive(false);
    }


    public void Exit()
    {
        Application.Quit();
    }


    public void SetGuideText(string text)
    {
        GuideText.GetComponent<Text>().text = text;
    }


    public void ToggleHololensVisibility()
    {
        HololensModel.SetActive(!HololensModel.activeInHierarchy);

        //re-position the helmet after hololens model is disabled or enabled
        if (!HololensModel.activeInHierarchy)
        {
            helmet.transform.localPosition = new Vector3(helmet.transform.localPosition.x, helmet.transform.localPosition.y - 0.02f, helmet.transform.localPosition.z + 0.01f);
        }
        else
        {
            helmet.transform.localPosition = new Vector3(helmet.transform.localPosition.x, helmet.transform.localPosition.y + 0.02f, helmet.transform.localPosition.z - 0.01f);
        }
    }


    public void ToggleUIVisibility()
    {
        MirageUIs.SetActive(!MirageUIs.activeInHierarchy);
    }


    private void ScreenShotModeToggle()
    {
        screenShotMode = !screenShotMode;
        screenShotCamera.fieldOfView = mainCamera.fieldOfView;

        if (screenShotMode)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            screenShotCamera.transform.position = mainCamera.transform.position;
            screenShotCamera.transform.rotation = mainCamera.transform.rotation;
            playerCamera.SetActive(false);
            screenShotCamera.enabled = true;
            mainCamera.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            playerCamera.SetActive(true);
            screenShotCamera.enabled = false;
            mainCamera.enabled = true;
        }

        captureButton.interactable = screenShotMode;

        var pMode = CustomClipMode ? "Cancel Pointing" : "Pointing";
        var scaptureMode = ScreenShotMode ? "Play mode" : "Screenshot mode";
        var modeDescription = ScreenShotMode ? "Camera movement: WSAD\nCamera rotation: Right click" : "Player movement: WSAD";
        SceneManager.Instance.SetGuideText($"P = Pointing\nTab = {scaptureMode}\n{modeDescription} ");

    }
}
