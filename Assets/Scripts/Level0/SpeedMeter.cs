using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpeedMeter : MonoBehaviour
{
    public float Speed;
    public float StayTime;
    [SerializeField] private TMP_Text[] TMP_Texts;
    
    private float _timer;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private PlayerController _playerController;

    private void Start()
    {
        PrintToScreens($"Калибровка скорости"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        _playerController = player.GetComponent<PlayerController>();
        
        Debug.Log("Игрок зашёл!");
        
        _startPos = GetPlayerHeadPosition(player);
        _timer = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        PrintToScreens($"Идет замер скорости\n" +
                       $"Время: \t{(Time.time - _timer):F2}\n" +
                       $"Пройдено: \t{Vector3.Distance(_startPos, GetPlayerHeadPosition(player)):F2}");
    }

    private void OnTriggerExit(Collider other)
    {
        Transform player = other.GetPlayerTransform();
        if (player == null) return;
        
        _playerController = player.GetComponent<PlayerController>();
        
        Debug.Log("Игрок вышел!");
        
        _endPos = GetPlayerHeadPosition(player);
        float distance = Vector3.Distance(_startPos, _endPos);

        if (distance < 2f)
        {
            Debug.LogWarning("Расстояние не пройдено!");
            PrintToScreens($"Расстояние не пройдено!\nСкорость по умолчанию: 2");
            _playerController.speed = 2f;
            return;
        }
        
        StayTime = Time.time - _timer;
        Speed = distance / StayTime;
        
        _playerController.speed = Speed;
        
        Debug.Log("Расстояние пройдено!\n");
        Debug.Log($"Скорость: {Speed:F2}\n");
        Debug.Log($"Время: {StayTime:F2}\n");
        Debug.Log($"Расстояние: {distance:F2}");
        
        PrintToScreens($"Скорость: \t\t{Speed:F2}\n" +
                       $"Время: \t\t{StayTime:F2}\n" +
                       $"Расстояние: \t{distance:F2}");
    }

    private Vector3 GetPlayerHeadPosition(Transform player)
    {
        return player.GetComponentInChildren<Camera>().transform.position;
    }

    private void PrintToScreens(string text)
    {
        foreach (TMP_Text tmpText in TMP_Texts)
        {
            tmpText.text = text;
        }
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
