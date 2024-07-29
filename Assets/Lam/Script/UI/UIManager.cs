using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance => _instance;
    [SerializeField] private GameObject _loseAnnounc;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private TransitionEffect _transieffect;

    private void Start() {
        if (instance == null )
        {
            _instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void LoseUI()
    {
        _loseAnnounc.SetActive(true);
    }

    public void WinUI()
    {
        _winUI.SetActive(true);
    }

    public void OKLose()
    {
        GameManager.instance.EndBattle();
    }

    public void OkWin()
    {
        GameManager.instance.EndBattle();
    }
}
