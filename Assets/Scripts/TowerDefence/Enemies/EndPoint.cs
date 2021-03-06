﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public class EndPoint : MonoBehaviour
    {
        void Start()
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponent<BaseEnemy>().TakeDamage(Mathf.Infinity);
            Managers.ResourceManager.RemoveHealth(1);
        }
    }
}