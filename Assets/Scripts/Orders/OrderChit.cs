using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Collections;

[System.Serializable]
public class OrderItemData
{
    public IngredientData ingredientData;
    public bool isDelivered;
}

public class OrderChit : MonoBehaviour
{
    [SerializeField] private OrderItem orderItemPrefab;
    [SerializeField] private Transform orderItemHolder;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button serveButton;

    private List<OrderItemData> _orderData;
    private List<OrderItem> _spawnedUIItems = new List<OrderItem>();
    private bool _isPlayerInZone = false;
    private float _startTime;
    private OrderManager _orderManager;
    private Coroutine _timerCoroutine;

    private void OnEnable()
    {
        serveButton.onClick.AddListener(OnServerButtonClick);
        ServingTable.OnPlayerAtCounter += SetPlayerZoneStatus;
    }

    private void OnDisable()
    {
        serveButton.onClick.RemoveListener(OnServerButtonClick);
        ServingTable.OnPlayerAtCounter -= SetPlayerZoneStatus;
    }

    public void InitializeOrderChit(List<OrderItemData> orderItemsData, OrderManager manager)
    {
        _orderManager = manager;
        _orderData = orderItemsData;
        _startTime = Time.time;

        SpawnOrderItems(orderItemsData);

        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        _timerCoroutine = StartCoroutine(UpdateTimer());

        ValidateHand();
    }

    private void SpawnOrderItems(List<OrderItemData> orderItemsData)
    {
        foreach (Transform child in orderItemHolder) Destroy(child.gameObject);
        _spawnedUIItems.Clear();

        foreach (OrderItemData item in orderItemsData)
        {
            OrderItem uiItem = Instantiate(orderItemPrefab, orderItemHolder);
            uiItem.IntializeOrderItem(item.ingredientData);
            _spawnedUIItems.Add(uiItem);
        }
    }

    public void SetPlayerZoneStatus(bool inZone)
    {
        _isPlayerInZone = inZone;
        ValidateHand();
    }

    private void ValidateHand()
    {
        if (!_isPlayerInZone)
        {
            serveButton.interactable = false;
            return;
        }

        var hand = PlayerController.Instance.Hand;
        if (!hand.IsHandFull())
        {
            serveButton.interactable = false;
            return;
        }

        IngredientData heldItem = hand.GetHeldItemData();
        bool isProcessed = hand.IsHeldItemProcessed();

        bool isReadyToServe = !heldItem.RequiresProcessing || hand.IsHeldItemProcessed();
        bool canServe = _orderData.Any(item => !item.isDelivered && item.ingredientData == heldItem && isReadyToServe);
        
        serveButton.interactable = canServe;
    }

    private void OnServerButtonClick()
    {
        var hand = PlayerController.Instance.Hand;
        IngredientData heldItem = hand.GetHeldItemData();

        for (int i = 0; i < _orderData.Count; i++)
        {
            if (!_orderData[i].isDelivered && _orderData[i].ingredientData == heldItem)
            {
                _orderData[i].isDelivered = true;
                _spawnedUIItems[i].SetDeliveryStatusIcon(true);
                hand.ClearHand();
                break;
            }
        }

        if (_orderData.All(x => x.isDelivered))
        {
            CompleteOrder();
        }
        else
        {
            ValidateHand();
        }
    }

    private void CompleteOrder()
    {
        if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
        serveButton.interactable = false;

        int totalVal = _orderData.Sum(x => x.ingredientData.scoreValue);
        int seconds = Mathf.FloorToInt(Time.time - _startTime);
        int finalScore = totalVal - seconds;

        ScoreManager.Instance.AddScore(finalScore, transform.position);

        Debug.Log($"<color=green>[Order Done]</color> {totalVal} - {seconds}s = {finalScore}");

        _orderManager.NotifyOrderCompleted(this);
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            timerText.text = Mathf.FloorToInt(Time.time - _startTime).ToString() + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}