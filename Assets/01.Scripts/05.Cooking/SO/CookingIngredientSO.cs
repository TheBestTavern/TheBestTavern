using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame/Ingredient")]
public class CookingIngredientSO : ScriptableObject
{
    //[SerializeField] private string originalName;

    //[Header("원본 재료")]
    //[SerializeField] private GameObject originalPrefab;

    //[Header("토막난 재료")]
    //[SerializeField] private GameObject slicedPrefab;
    public string prefabAdress;
    public int[] foodCategoryID;

    // 컬러
    // 다 익은 컬러
}
