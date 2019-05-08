﻿using UnityEngine;

public class MenuBullet : MonoBehaviour
{
    public float moveSpeed;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
