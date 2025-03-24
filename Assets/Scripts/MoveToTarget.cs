using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    private Transform dropTargetPos; // Hedef pozisyon
    private float duration = 1.5f; // Hareket süresi
    private float arcHeight = 1.5f; // Bombenin yüksekliði
    private Vector3 currentPos;

    private void Start()
    {
        dropTargetPos = this.gameObject.GetComponent<Items>().targetPoint;
        currentPos= this.transform.position;    
    }
    public void MoveObject()
    {
        StartCoroutine(MoveRoutine());
    }
    public void ReturnToOriginalPosition()
    {
        StartCoroutine(ReturnPosition(currentPos));
    }

    IEnumerator MoveRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = dropTargetPos.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp ile düz hareket
            Vector3 currentPosition = Vector3.Lerp(startPos, endPos, t);

            // Arc hareketi (yukarý yönlü eðri ekleme)
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

            transform.position = currentPosition;
            yield return null;
        }

        // Son pozisyona tam olarak oturt
        transform.position = endPos;
    }
    IEnumerator ReturnPosition(Vector3 currentPos)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = currentPos;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp ile düz hareket
            Vector3 currentPosition = Vector3.Lerp(startPos, endPos, t);

            // Arc hareketi (yukarý yönlü eðri ekleme)
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

            transform.position = currentPosition;
            yield return null;
        }

        // Son pozisyona tam olarak oturt
        transform.position = endPos;
    }
}
