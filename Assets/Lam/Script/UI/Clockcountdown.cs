using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clockcountdown : MonoBehaviour
{
    public Image clockradial;
    float time_remain;
    public float max_time = 5.0f;
    private bool isTimerPaused = false;
    void Start()
    {
        time_remain = max_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerPaused)
        {
            if (time_remain > 0)
                {
                     time_remain -= Time.deltaTime;
                     clockradial.fillAmount = time_remain/max_time;

                }
            else
                {
            AudioAssitance.Instance.PlaySFX("Sound when enemy attacked");
            ResetTimer();
            isTimerPaused = true;
                }
        }
       //dieu kien de bat dau tiep
        
    }
    private void ResetTimer()
    {
        time_remain = max_time;
        clockradial.fillAmount = time_remain/max_time;
        
    }
    
}
