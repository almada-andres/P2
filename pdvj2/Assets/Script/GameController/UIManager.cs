using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mensajeTumbaTimy;
    [SerializeField] private Image imagenTumbaTimy;

    private void Start()
    {
        GameManager.Instance.RegistrarUI(mensajeTumbaTimy, imagenTumbaTimy);
    }
}