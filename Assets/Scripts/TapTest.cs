using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

public class TapTest : MonoBehaviour
{
    private GestureRecognizer recognizer;

    private void Start()
    {
        Debug.Log("Tap was started");
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
    }

    private void TapHandler(TappedEventArgs obj)
    {
        Debug.Log("Tap was pressed");
        TextMesh t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
        t.text = "Tap was pressed";
    }
}
