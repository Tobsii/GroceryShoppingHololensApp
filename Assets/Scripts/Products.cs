using System;

[Serializable]
public class Products
{
    public long gtin;
    public string product_name_de;
    public Allergens[] allergens;
    public Ingredients[] ingredients;
    public Nutrients[] nutrients;
    public string nutri_score_final;
}
