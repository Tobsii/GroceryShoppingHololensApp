using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeProducts : MonoBehaviour
{
    GameObject currentGazeTarget;
    GameObject newGazeTarget;

    bool nutriInfoShowing = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call this at gesture?
        LogCurrentGazeTarget();
    }

    void LogCurrentGazeTarget()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            // Debug.Log("User gaze is currently over game object: " + CoreServices.InputSystem.GazeProvider.GazeTarget);

            newGazeTarget = CoreServices.InputSystem.GazeProvider.GazeTarget;
            GetDistanzeToGazeTarget(newGazeTarget);
        } else
        {
            if (nutriInfoShowing)
            {
                // If nutriplate was there, hide it
                nutriInfoShowing = false;
                currentGazeTarget = null;
                GetComponent<DrawUI>().HideNutriScreen();
                // GetComponent<DrawUI>().HideBetterProduct();  // BP
            }
        }     
    }

    void CheckGazeTargetChange(GameObject newTarget)
    {
        if (GameObject.ReferenceEquals(newTarget, currentGazeTarget)){
            Debug.Log("Its the same object");
        } else
        {
            Debug.LogWarning("Its a new object");
            currentGazeTarget = newGazeTarget;
            nutriInfoShowing = true;
            GetComponent<DrawUI>().DrawNutriScreen(currentGazeTarget);
            // GetComponent<DrawUI>().DrawBetterProduct(currentGazeTarget);  // BP
        }
    }

    void GetDistanzeToGazeTarget(GameObject userGaze)
    {
        float dist = Vector3.Distance(Camera.main.transform.position, userGaze.transform.position);

        // GAZE DISTANZE
        if (dist <= 0.4)
        {
            Debug.Log("Distance to the Object the user gazes at: " + dist);
            //Debug.Log("ID Of the gazedobject: " + userGaze.GetComponent<ProductOverview>().id);
            CheckGazeTargetChange(userGaze);
        }
        else
        {
            if (nutriInfoShowing)
            {
                // If nutriplate was there, hide it
                nutriInfoShowing = false;
                currentGazeTarget = null;
                GetComponent<DrawUI>().HideNutriScreen();
                // GetComponent<DrawUI>().HideBetterProduct(); //BP
            }
           
        }
    }

}
