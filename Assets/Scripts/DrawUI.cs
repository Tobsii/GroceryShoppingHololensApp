using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DrawUI : MonoBehaviour
{
    public GameObject productInfo;
    public GameObject scanStateInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteProduct(FoodObject blop)
    {
        productInfo.GetComponent<TextMesh>().text = blop.products[0].product_name_de + Environment.NewLine + blop.products[0].nutri_score_final + Environment.NewLine + blop.products[0].ingredients[0].text;
    }

    public void WriteScanInfo(bool s)
    {
        scanStateInfo.GetComponent<TextMesh>().text = "isScanningForProducts: " + s;
    }



    

}
