using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField] private TMP_Text[] tmpTexts;
    
    private float timer;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Start()
    {
        PrintToScreens($"Калибровка скорости"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        Debug.Log("Игрок зашёл!");
        
        startPos = GetPlayerHeadPosition(player);
        timer = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        PrintToScreens($"Идет замер скорости\n" +
                       $"Время: \t{(Time.time - timer):F2}\n" +
                       $"Пройдено: \t{Vector3.Distance(startPos, GetPlayerHeadPosition(player)):F2}");
    }

    private void OnTriggerExit(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        Debug.Log("Игрок вышел!");
        
        endPos = GetPlayerHeadPosition(player);
        float distance = Vector3.Distance(startPos, endPos);

        if (distance < 2f)
        {
            Debug.LogWarning("Расстояние не пройдено!");
            ResetSpeed();
            return;
        }
        
        float stayTime = Time.time - timer;
        float speed = distance / stayTime;
        
        PlayerController.speed = speed;
        
        Debug.Log("Расстояние пройдено!\n");
        Debug.Log($"Скорость: {speed:F2}\n");
        Debug.Log($"Время: {stayTime:F2}\n");
        Debug.Log($"Расстояние: {distance:F2}");
        
        PrintToScreens($"Скорость: \t\t{speed:F2}\n" +
                       $"Время: \t\t{stayTime:F2}\n" +
                       $"Расстояние: \t{distance:F2}");
    }

    private Vector3 GetPlayerHeadPosition(Transform player)
    {
        return player.GetComponentInChildren<Camera>().transform.position;
    }

    private void PrintToScreens(string text)
    {
        foreach (TMP_Text tmpText in tmpTexts)
        {
            tmpText.text = text;
        }
    }

    public void ResetSpeed()
    {
        PlayerController.ResetSpeed();
        Debug.Log($"Скорость сброшена! Скорость: {PlayerController.speed:F2}");
        PrintToScreens($"Выставлена скорость по умолчанию: {PlayerController.speed:F2}");
    }
}

public static class ColliderExtension
{
    public static Transform GetPlayerTransform(this Collider other)
    {
        if (other.GetComponent<PlayerController>())
            return other.transform;
        return null;
    }
}
