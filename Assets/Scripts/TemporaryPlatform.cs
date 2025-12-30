using System.Collections;
using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        this.gameObject.SetActive(false);
    }
}