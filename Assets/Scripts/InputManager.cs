using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private GestureRecognizer recognizer;
    private static bool isScanningForProducts = false;

    // Reference to other scripts
    private DrawUI drawUi;

    void Awake()
    {
        drawUi = GetComponent<DrawUI>();
    }

    private void Start()
    {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
    }

    private void TapHandler(TappedEventArgs obj)
    {
        if (isScanningForProducts)
        {
            isScanningForProducts = false;
        }
        else
        {
            isScanningForProducts = true;
        }

        // changeTextofIt.GetComponent<TextMesh>().text = "isScanningForProducts: " + isScanningForProducts;
        drawUi.WriteScanInfo(isScanningForProducts);
        Debug.Log("isScanningForProducts: " + isScanningForProducts);
    }

}
