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

    public void TapHandler(TappedEventArgs obj)
    {
        if (isScanningForProducts)
        {
            // hide ui
            drawUi.HideAll();
            isScanningForProducts = false;
        }
        else
        {
            // shows ui
            drawUi.ShowAllUi();
            isScanningForProducts = true;
        }

        // changeTextofIt.GetComponent<TextMesh>().text = "isScanningForProducts: " + isScanningForProducts;
        drawUi.WriteScanInfo(isScanningForProducts);
        Debug.Log("isScanningForProducts: " + isScanningForProducts);
    }

    public void ScanButtonHandler()
    {
        if (isScanningForProducts)
        {
            // hide ui
            // change button
            drawUi.HideAll();
            isScanningForProducts = false;
        }
        else
        {
            // shows ui
            // change button
            drawUi.ShowAllUi();
            isScanningForProducts = true;
        }

        // changeTextofIt.GetComponent<TextMesh>().text = "isScanningForProducts: " + isScanningForProducts;
        drawUi.WriteScanInfo(isScanningForProducts);
        Debug.Log("isScanningForProducts: " + isScanningForProducts);
    }


}
