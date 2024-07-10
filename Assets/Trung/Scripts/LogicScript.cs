using System.Collections;
using TMPro;
using Trung.Scene;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    private static LogicScript _instance;
    public static LogicScript instance { get => _instance; }

    [SerializeField] private Button _arrowButton = null;
    public GameObject _listBuilding = null;
    private bool _isOpen = false;
    private RectTransform _buttonRectTransform;
    private RectTransform _listBuildingRectTransform;

    public BuildingController_T _currentBuild = null;
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _buttonRectTransform = _arrowButton.GetComponent<RectTransform>();
        _listBuildingRectTransform = _listBuilding.GetComponent<RectTransform>();
        _arrowButton.onClick.AddListener(ButtonArrowAction);
    }

    public void ButtonArrowAction()
    {
        if (_isOpen)
        {
            StartCoroutine(MoveButton(new Vector2(7.243286f, 0), new Vector2(-28, -3.7f), new Vector3(0f, 0f, -90), 0.2f));
            _isOpen = false;
        }
        else
        {
            StartCoroutine(MoveButton(new Vector2(62, 0), new Vector2(28, -3.7f), new Vector3(0f, 0f, 90), 0.2f));
            _isOpen = true;
        }
    }


    private IEnumerator MoveButton(Vector2 targetPosition, Vector2 targetPosition2, Vector3 targetRotation, float duration)
    {
        float timeElapsed = 0;
        Vector2 startPosition = _buttonRectTransform.anchoredPosition;
        Vector2 startPosition2 = _listBuildingRectTransform.anchoredPosition;
        _buttonRectTransform.localRotation = Quaternion.Euler(targetRotation);
        while (timeElapsed < duration)
        {
            _listBuildingRectTransform.anchoredPosition = Vector2.Lerp(startPosition2, targetPosition2, timeElapsed / duration);
            _buttonRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _listBuildingRectTransform.anchoredPosition = targetPosition2;
        _buttonRectTransform.anchoredPosition = targetPosition;
    }

    private void RoLateBuilding(BuildingController_T build, float degree)
    {
        BuildingController_T temp = build;
        temp.transform.Rotate(0, degree, 0);
    }

    public void OnRolateClick()
    {
        RoLateBuilding(_currentBuild, 90);
    }

    public void OnUpdateClick()
    {
        Debug.Log("Update");
    }

    public void OnSellClick()
    {
        Debug.Log("Sell");
    }

    public void OnConfirmClick()
    {
        PlaceSystem.instance.ConfirmBuild();
    }

    public void OnCancelClick()
    {
        PlaceSystem.instance.CancelBuild();
        MainUI_T.instance.build_Status(false);
    }
    
}
