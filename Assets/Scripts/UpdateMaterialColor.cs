using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMaterialColor : MonoBehaviour
{
    public void MarkAsVisited()
    {
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material.color = Color.gray;
    }
}
