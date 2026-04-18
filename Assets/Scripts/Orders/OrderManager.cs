using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderChit[] orderSlots;
    [SerializeField] private IngredientData[] possibleIngredients;

    private void Start()
    {
        foreach (var chit in orderSlots)
        {
            GenerateOrderForChit(chit);
        }
    }

    public void GenerateOrderForChit(OrderChit chit)
    {
        int count = Random.value > 0.5f ? 2 : 3;
        List<OrderItemData> newData = new List<OrderItemData>();

        for (int i = 0; i < count; i++)
        {
            newData.Add(new OrderItemData { 
                ingredientData = possibleIngredients[Random.Range(0, possibleIngredients.Length)],
                isDelivered = false 
            });
        }

        chit.InitializeOrderChit(newData, this);
    }

    public void NotifyOrderCompleted(OrderChit chit)
    {
        StartCoroutine(RespawnOrderRoutine(chit));
    }

    private IEnumerator RespawnOrderRoutine(OrderChit chit)
    {
        yield return new WaitForSeconds(5f);
        GenerateOrderForChit(chit);
    }
}