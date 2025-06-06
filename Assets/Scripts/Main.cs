using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.ShowPanel<BeginPanel>();
    }

    private void Update()
    {
    }
}