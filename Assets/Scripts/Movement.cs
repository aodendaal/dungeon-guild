using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private TMPro.TMP_Text locationText;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var cell = DungeonManager.Instance.GetCell(transform.position.x, transform.position.z + 1.0f);

            if (cell.IsWalkable)
            {
                transform.position += new Vector3(0.0f, 0.0f, 1.0f);
                model.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                DungeonManager.Instance.VisitCell(transform.position.x, transform.position.z);
                audioSource.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var cell = DungeonManager.Instance.GetCell(transform.position.x, transform.position.z - 1.0f);

            if (cell.IsWalkable)
            {
                transform.position += new Vector3(0.0f, 0.0f, -1.0f);
                model.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                DungeonManager.Instance.VisitCell(transform.position.x, transform.position.z);
                audioSource.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var cell = DungeonManager.Instance.GetCell(transform.position.x - 1.0f, transform.position.z);

            if (cell.IsWalkable)
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                model.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                DungeonManager.Instance.VisitCell(transform.position.x, transform.position.z);
                audioSource.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var cell = DungeonManager.Instance.GetCell(transform.position.x + 1.0f, transform.position.z);

            if (cell.IsWalkable)
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                model.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                DungeonManager.Instance.VisitCell(transform.position.x, transform.position.z);
                audioSource.Play();
            }
        }

        UpdateLocationText();
    }

    private void UpdateLocationText()
    {
        locationText.text = $"Floor: 00 / X {(int)transform.position.x:D2} / Y {(int)transform.position.z:D2}";
    }
}
