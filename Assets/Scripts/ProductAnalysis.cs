using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ProductAnalysis : MonoBehaviour
{

    /// Allows this class to behave like a singleton
    public static ProductAnalysis Instance;

    /// FOR CONNECTION 
    /// Base endpoint of Product Recognition Service
    const string baseEndpoint = "https://eatfit-service.foodcoa.ch/";
        
    string api_username = Environment.GetEnvironmentVariable("food_api_user");
    string api_password = Environment.GetEnvironmentVariable("food_api_passwd");

    // Other scripts
    private DrawUI drawUi;


    /// Initialises this class
    private void Awake()
    {
        // Allows this instance to behave like a singleton
        Instance = this;

        // Add the ImageCapture Class to this Game Object -> WHY????
        gameObject.AddComponent<ImageCapture>();

        drawUi = GetComponent<DrawUI>();

    }

    // just for testing
    private void Start()
    {
        Debug.Log("Product Analysis Start");
        StartCoroutine(DetectProductsFromId(7612100023099));
        StartCoroutine(DetectBetterProduct(7612100023099));
    }


    string authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    // Detects Products from an image
    internal IEnumerator DetectProductsFromId(long id)
    {
        string authorization = authenticate(api_username, api_password);
        string url = baseEndpoint + "products/" + id;

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("AUTHORIZATION", authorization);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            FoodObject item = JsonUtility.FromJson<FoodObject>(www.downloadHandler.text);
            Debug.Log(item.success);
            Debug.Log(item.products[0].product_name_de);
            // CreateLabel(item);
            drawUi.WriteProduct(item);
        }
    }

    // Detects Products better than the one in view from an image
    internal IEnumerator DetectBetterProduct(long id)
    {
        string authorization = authenticate(api_username, api_password);
        string url = baseEndpoint + "products/better-products/" + id+ "?sortBy=totalFat&resultType=array&marketRegion=ch&retailer=migros";

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("AUTHORIZATION", authorization);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            FoodObject betterItems = JsonUtility.FromJson<FoodObject>(www.downloadHandler.text);
            Debug.Log(betterItems.products[0].product_name_de);
        }
    }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
/*
[Serializable]
public class Ingredient
{
    public string lang 
    public string text 
}
[Serializable]
public class Nutrient
{
    public string name 
    public double amount 
    public string unit_of_measure 
}
[Serializable]
public class NutriScoreFacts
{
    public object fvpn_total_percentage 
    public double fvpn_total_percentage_estimated 
    public object fruit_percentage 
    public object vegetable_percentage 
    public object pulses_percentage 
    public object nuts_percentage 
    public object fruit_percentage_dried 
    public object vegetable_percentage_dried 
    public object pulses_percentage_dried 
    public double ofcom_n_energy_kj 
    public double ofcom_n_saturated_fat 
    public double ofcom_n_sugars 
    public double ofcom_n_salt 
    public double ofcom_p_protein
    public double ofcom_p_fvpn
    public double ofcom_p_dietary_fiber
    public object ofcom_n_energy_kj_mixed 
    public object ofcom_n_saturated_fat_mixed 
    public object ofcom_n_sugars_mixed 
    public object ofcom_n_salt_mixed 
    public object ofcom_p_protein_mixed 
    public object ofcom_p_fvpn_mixed 
    public object ofcom_p_dietary_fiber_mixed
}
[Serializable]
public class Product
{
    public long gtin
    public object product_name_en
    public string product_name_de
    public object product_name_fr
    public object product_name_it
    public string producer 
    public object product_size
    public object product_size_unit_of_measure
    public object serving_size
    public object comment
    public string image
    public object back_image
    public object major_category
    public object minor_category
    public List<object> allergens
    public List<Ingredient> ingredients
    public List<Nutrient> nutrients
    public string source
    public bool source_checked
    public object health_percentage
    public bool weighted_article
    public int price
    public int ofcom_value
    public string nutri_score_final
    public NutriScoreFacts nutri_score_facts
}
[Serializable]
public class FoodObject
{
    public bool success
    public List<Product> products
}
*/
