using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Score : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int points = 1;
    [SerializeField] private float delay = 2f;
    bool isScoring;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();

        if (grab != null && !grab.isSelected && !isScoring)
        {
            StartCoroutine(AddScore());
        }
    }

    private IEnumerator AddScore()
    {
        isScoring = true;
        score += points;
        Debug.Log("Score: " + score);
        yield return new WaitForSeconds(delay);
        isScoring = false;
    }
}
