namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using Trung.Scene.XTrung.Scene;
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Main : MonoBehaviour
    {
        private static UI_Main _instance = null; public static UI_Main instance { get { return _instance; } }

        [SerializeField] public GameObject _elements = null;
        [SerializeField] private Button _shopButton = null;

        [SerializeField] public BuildGrid _grid = null;
        [SerializeField] public Building[] _buildingPrefabs = null;

        private bool _active = true; public bool isActive { get { return _active; } }

        private void Awake()
        {
            _instance = this;
            _elements.SetActive(true);
        }

        private void Start()
        {
            _shopButton.onClick.AddListener(ShopButtonClicked);
        }

        private void ShopButtonClicked()
        {
            UI_Build.instance.Cancel();
            UI_Shop.instance.SetStatus(true);
            SetStatus(false);
        }

        public void SetStatus(bool status)
        {
            _active = status;
            _elements.SetActive(status);
        }
    }

}