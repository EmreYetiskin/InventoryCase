using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField]public Transform targetPosition; // Hedef pozisyon
    public float duration = 1.5f; // Hareket s�resi
    public float arcHeight = 1.5f; // Bombenin y�ksekli�i

    public void MoveObject()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = targetPosition.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp ile d�z hareket
            Vector3 currentPosition = Vector3.Lerp(startPos, endPos, t);

            // Arc hareketi (yukar� y�nl� e�ri ekleme)
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;

            transform.position = currentPosition;
            yield return null;
        }

        // Son pozisyona tam olarak oturt
        transform.position = endPos;
    }
}
