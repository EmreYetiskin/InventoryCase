using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField]public Transform targetPosition; // Hedef pozisyon
    public float duration = 1.5f; // Hareket süresi
    public float arcHeight = 1.5f; // Bombenin yüksekliði

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
