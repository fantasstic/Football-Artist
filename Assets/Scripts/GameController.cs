using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BallController _ballController;

    private void Awake()
    {
        _ballController.Init();
    }
}

