using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

    static public float timeSpent;
    private bool isGameRunning;

    public TextMeshProUGUI timetime; // 
    void Start()
    {
        // Загружаем сохранённое время
        timeSpent = PlayerPrefs.GetFloat("TimeSpent", 0f);
        isGameRunning = true; // Игра началась
    }

    public float timeUser;

    void Update()
    {
        if (isGameRunning)
        {
            // Увеличиваем время, проведённое в игре
            timeSpent += Time.deltaTime;
          

            // Обновляем отображение времени
            UpdateTimeDisplay();
        }
    }

    private void UpdateTimeDisplay()
    {
        // Преобразуем время в часы, минуты и секунды
        int hours = (int)(timeSpent / 3600);
        int minutes = (int)((timeSpent % 3600) / 60);
        int seconds = (int)(timeSpent % 60);

        // Выводим в текстовом формате
        timetime.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }

    private void OnApplicationQuit()
    {
        // Сохраняем время, когда приложение закрывается
        PlayerPrefs.SetFloat("TimeSpent", timeSpent);
        PlayerPrefs.Save();
    }
}


