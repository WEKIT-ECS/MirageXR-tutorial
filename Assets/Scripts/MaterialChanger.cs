using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private GameObject sourceObject;
    [SerializeField] private Material solidMaterial;
    [SerializeField] private Material wirefraneMaterial;

    bool _solidMat;

    public void ToggleMaterial()
    {
        _solidMat = !_solidMat;


        foreach (var mat in sourceObject.GetComponentsInChildren<Renderer>())
        {
            if (_solidMat)
            {
                mat.material = solidMaterial;
                GetComponentInChildren<TextMeshProUGUI>().text = "Wired";
            }
            else
            {
                mat.material = wirefraneMaterial;
                GetComponentInChildren<TextMeshProUGUI>().text = "Solid";
            }

        }
    }
}
