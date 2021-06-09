using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public double time = 60;

    [SerializeField] Text timerText;

    private void Start()
    {
        timerText.text = time.ToString("N2");
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("N2");
        if (time <= 0)
        {
            time = 0;
        }
    }

    public void Initialize()
    {
        time = 60;
    }
}
