using MirageXR;
using UnityEngine;

[RequireComponent(typeof(PathSegmentsController))]
public class HomePathVisibilityController : MonoBehaviour
{
    [SerializeField] private GameObject[] hideableObjects;

    private PathSegmentsController segmentController;

    private void Start()
    {
        segmentController = GetComponent<PathSegmentsController>();
       

         OnShowActivitySelectionMenu();
    }



    private void OnShowActivitySelectionMenu()
    {
        SetVisibility(true);
    }


    private void SetVisibility(bool visibility)
    {
        segmentController.IsVisible = visibility;
        for (int i = 0; i < hideableObjects.Length; i++)
        {
            hideableObjects[i].SetActive(visibility);
        }
    }

}

