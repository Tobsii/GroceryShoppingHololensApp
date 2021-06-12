using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupShelves : MonoBehaviour
{
    [Header("Regale")]
    public GameObject cerealShelf;
    public GameObject drinkShelf;
    public GameObject yoghurtShelf;
    public GameObject tutorialShelf;

    [Header("Hinweise und Buttons")]
    public GameObject cerealButton;
    public GameObject cerealText;
    public GameObject drinkButton;
    public GameObject drinkText;
    public GameObject yoghurtButton;
    public GameObject yoghurtText;
    public GameObject tutorialButton;
    public GameObject tutorialText;

    [Header("Other")]
    public GameObject mainCamera;
    public GameObject switchScanButton;

    public void PlaceCerealShelf()
    {
        // Lock ceral Movement (TODO: Near Interaction?)
        cerealShelf.GetComponent<ObjectManipulator>().enabled = false;
        cerealShelf.GetComponent<BoxCollider>().enabled = false;
        // cerealShelf.GetComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionTouchable>().enabled = false;

        Destroy(cerealText);
        Destroy(cerealButton);

        drinkShelf.SetActive(true);
        drinkText.SetActive(true);
        drinkButton.SetActive(true);
        // Position Button
        // drinkText.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 2;
    }

    public void PlaceDrinksShelf()
    {
        // Lock Drinks Shelf
        drinkShelf.GetComponent<ObjectManipulator>().enabled = false;
        drinkShelf.GetComponent<BoxCollider>().enabled = false;

        Destroy(drinkText);
        Destroy(drinkButton);
        
        yoghurtShelf.SetActive(true);
        yoghurtText.SetActive(true);
        yoghurtButton.SetActive(true);
    }

    public void PlaceYoghurtShelf()
    {
        // Lock Yoghurt Shelf
        yoghurtShelf.GetComponent<ObjectManipulator>().enabled = false;
        yoghurtShelf.GetComponent<BoxCollider>().enabled = false;

        Destroy(yoghurtText);
        Destroy(yoghurtButton);

        tutorialShelf.SetActive(true);
        tutorialText.SetActive(true);
        tutorialButton.SetActive(true);
    }

    public void PlacedTutorialShelf()
    {
        // Lock Tutorial Shelf
        tutorialShelf.GetComponent<ObjectManipulator>().enabled = false;
        tutorialShelf.GetComponent<BoxCollider>().enabled = false;

        Destroy(tutorialText);
        Destroy(tutorialButton);

        // Enable Hide/Show Button
        switchScanButton.SetActive(true);

        // Enable "Product Recognition" Scripts
        GetComponent<RecognizeProducts>().enabled = true;
        GetComponent<InputManager>().enabled = true;
    }

}
