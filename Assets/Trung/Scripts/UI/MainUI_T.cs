namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainUI_T : MonoBehaviour
    {
        private static MainUI_T _instance;
        public static MainUI_T instance { get => _instance; }

        [SerializeField] public GameObject _build_BTN = null;
        [SerializeField] public GameObject _edit_BTN = null;
        private void Start()
        {
            _build_BTN.SetActive(false);
            _edit_BTN.SetActive(false);
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void build_Status(bool status)
        {
            _build_BTN.SetActive(status);
        }

        public void edit_Status(bool status)
        {
            _edit_BTN.SetActive(status);
        }
    }

}