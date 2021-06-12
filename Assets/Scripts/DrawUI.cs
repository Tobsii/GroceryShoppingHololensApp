using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DrawUI : MonoBehaviour
{
    [Header("For Debugging")]
    public GameObject productIngredients;
    public GameObject productNutrition;
    public GameObject productName;
    public GameObject productScore;
    public GameObject scanStateInfo;
    public GameObject interfaceObject;

    [Header("Tabelle Nutriinfo")]
    public Text energyValue;
    public Text totalFatValue;
    public Text saturatedFatValue;
    public Text carbonhydrateValue;
    public Text sugarValue;
    public Text fibreValue;
    public Text proteinValue;
    public Text saltValue;
    public GameObject NutriInfoPlate;

    [Header("Besseres Produkt Vorschlag")]
    public GameObject betterProductPlate;
    public Text betterProductName;
    public Text betterProductScore;
    public Text betterProductPrice;

    [Header("Other")]
    public Material m_ScanButtonMaterial;
    public Material ScanOn;
    public Material ScanOff;
    public GameObject productShelves;
    public GameObject buttonScanOnOff;
    public GameObject toggleButtonBackplate;

    public void DrawNutriScreen(GameObject plate)
    {
        Debug.Log("Inside Draw NutriScreen");
        
        // Move Screen to right View
        // NutriInfoPlate.transform.position = new Vector3(plate.transform.position.x-0.15f, plate.transform.position.y, plate.transform.position.z);
        NutriInfoPlate.GetComponent<RadialView>().enabled = true;

        DrawBetterProductTest(plate); //TEST

        // Call Function to fill screen
        StartCoroutine(GetComponent<ProductAnalysis>().DetectProductsFromId(plate.GetComponent<ProductOverview>().id));
    }

    public void HideNutriScreen()
    {
        // Move the nutri screen under the floor
        NutriInfoPlate.GetComponent<RadialView>().enabled = false;
        NutriInfoPlate.transform.position = new Vector3(0, -8, 0);
    }

    public void DrawBetterProduct(GameObject product)
    {
        Debug.Log("Inside Draw Better Product"); 

        betterProductName.text = "Bessere Alternative: " + product.GetComponent<BetterProduct>().productName;
        betterProductScore.text = "NutriScore: "+ product.GetComponent<BetterProduct>().productScore;
        betterProductPrice.text = "Preis: " + (product.GetComponent<BetterProduct>().productPrice).ToString("0.00");

        NutriInfoPlate.transform.position = new Vector3(product.transform.position.x, product.transform.position.y, product.transform.position.z);
        betterProductPlate.GetComponent<RadialView>().enabled = true;
    }

    public void DrawBetterProductTest(GameObject product)
    {
        betterProductName.text = "Bessere Alternative: " + product.GetComponent<BetterProduct>().productName;
        betterProductScore.text = "NutriScore: " + product.GetComponent<BetterProduct>().productScore;
        betterProductPrice.text = "Preis: " + (product.GetComponent<BetterProduct>().productPrice).ToString("0.00") + "Fr";
    }

    public void HideBetterProduct()
    {
        betterProductPlate.GetComponent<RadialView>().enabled = false;
        betterProductPlate.transform.position = new Vector3(0, -8, 0);
    }

    public void WriteProduct(FoodObject blop)
    {
        //productIngredients.GetComponent<TextMesh>().text = blop.products[0].product_name_de + Environment.NewLine + blop.products[0].nutri_score_final + Environment.NewLine + blop.products[0].ingredients[0].text;

        productIngredients.GetComponent<TextMesh>().text = blop.products[0].ingredients[0].text;
        productNutrition.GetComponent<TextMesh>().text = "TODO";
        productName.GetComponent<TextMesh>().text = blop.products[0].product_name_de;
        productScore.GetComponent<TextMesh>().text = blop.products[0].nutri_score_final;

        // Fill in Nährwerttabelle
        for (int i = 0; i < blop.products[0].nutrients.Length; i++)
        {
            switch (blop.products[0].nutrients[i].name)
            {
                case "energyKcal":
                    Debug.Log("Case Energy");
                    // plate.transform.Find("ContentBackPlate/ObjectCollection/EnergyValue").GetComponent<Text>().text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    energyValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "totalFat":
                    Debug.Log("Case Fat");
                    totalFatValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "saturatedFat":
                    Debug.Log("Case gesättigtes Fat");
                    saturatedFatValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "totalCarbohydrate":
                    Debug.Log("Case Kohlenhydrate");
                    carbonhydrateValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "sugars":
                    Debug.Log("Case Zucker");
                    sugarValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "protein":
                    Debug.Log("Case Eiweiss");
                    proteinValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "dietaryFiber":
                    Debug.Log("Case Fiber");
                    fibreValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                case "salt":
                    Debug.Log("Case Salt");
                    saltValue.text = blop.products[0].nutrients[i].amount + blop.products[0].nutrients[i].unit_of_measure;
                    break;
                default:
                    Debug.Log("Unknown case");
                    break;
            }

        }
    }

    public void WriteScanInfo(bool s)
    {
        scanStateInfo.GetComponent<TextMesh>().text = "isScanningForProducts: " + s;
    }

    public void ShowAllUi()
    {
       productShelves.SetActive(true);
       // m_ScanButtonMaterial.color = Color.red;
       // buttonScanOnOff.GetComponent<MeshRenderer>().material = ScanOn;
       toggleButtonBackplate.GetComponent<MeshRenderer>().material = ScanOn;
    }

    public void HideAll()
    {
        // m_ScanButtonMaterial.color = Color.blue;
        // buttonScanOnOff.GetComponent<MeshRenderer>().material = ScanOff;
        productShelves.SetActive(false);
        toggleButtonBackplate.GetComponent<MeshRenderer>().material = ScanOff;
    }

}
