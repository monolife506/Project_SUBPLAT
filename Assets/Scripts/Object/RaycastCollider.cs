﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collider가 도중에 변경되지 않는 Raycast로 충돌을 감지하는 Object
[RequireComponent(typeof(BoxCollider2D))]
public class RaycastCollider : MonoBehaviour
{
    public const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    [HideInInspector] public float gravity;
    [HideInInspector] public float fallingSpeedMax;

    public GameObject player;
    [HideInInspector] public float horizontalRaySpacing;
    [HideInInspector] public float verticalRaySpacing;
    [HideInInspector] public BoxCollider2D mainCollider;
    public LayerMask WhatIsGround; // 땅으로 인식하는 Layer

    public RaycastOrigins raycastOrigins;

    public virtual void Start()
    {
        mainCollider = GetComponent<BoxCollider2D>();
        gravity = -2 * player.GetComponent<PlayerMovement>().maxJumpHeight / Mathf.Pow(player.GetComponent<PlayerMovement>().timeToJumpApex, 2f);
        fallingSpeedMax = player.GetComponent<PlayerMovement>().fallingSpeedMax;
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = mainCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = mainCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}