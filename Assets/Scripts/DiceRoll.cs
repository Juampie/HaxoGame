using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject _diceMash;
    private Rigidbody _rigidbody;
    private bool _rolling = false;


    // Список значений на верхних сторонах кубика
    private int[] _values = { 3, 1, 6, 5, 2, 4 };

    // Углы поворота кубика
    private Vector3[] _rotations =
    {
        new Vector3(0, 0, 0), // 3
        new Vector3(0, 0, 90), // 1
        new Vector3(0, 0, -90), // 6
        new Vector3(90, 0, 0), // 5
        new Vector3(-90, 0, 0), // 2
        new Vector3(180, 0, 0) // 4
    };

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_rolling)
        {
            
            Roll();
        }
    }

    // Запускает кубик вращаться
    private void Roll()
    {
        _diceMash.SetActive(true);
        _rolling = true;
        _rigidbody.isKinematic = false;
        Vector3 torque = new Vector3(Random.Range(200, 500), Random.Range(200, 500), Random.Range(200, 500));
        _rigidbody.AddTorque(torque);
        StartCoroutine(StopRolling());
    }

    IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(Random.Range(2f,6f));
        _rolling = false;
        _rigidbody.isKinematic = true;

        // Находим сторону кубика, которая оказалась сверху
        float maxDot = -1.0f;
        int maxIndex = -1;
        for (int i = 0; i < 6; i++)
        {
            float dot = Vector3.Dot(transform.up, Quaternion.Euler(_rotations[i]) * Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                maxIndex = i;
            }
        }

        // Поворачиваем кубик так, чтобы верхняя сторона была вверху
        transform.rotation = Quaternion.Euler(_rotations[maxIndex]);

        // Выводим значение на экран, если оно изменилось
        int value = _values[maxIndex];
        
            Debug.Log("Результат: " + value);
        
        _diceMash.SetActive(false);
    }
}
