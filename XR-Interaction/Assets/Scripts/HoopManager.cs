using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HoopManager : MonoBehaviour
{
    [SerializeField] private int points = 1;
    [SerializeField] private float delay = 2f;
    [SerializeField] private int difficultyMultiplier = 1;
    [SerializeField] private int maxDifficulty = 3;
    [SerializeField] private TextMeshPro difficultyText;
    [SerializeField] private TextMeshPro scoreText;
    private int score;
    private int shots;
    private string scoreString;
    private bool isScoring;
    private float originalZ;

    private void Awake()
    {
        originalZ = transform.position.z;
        score = 0;
        shots = 0;
        isScoring = false;
    }

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
        UpdateScore();
        yield return new WaitForSeconds(delay);
        isScoring = false;
    }

    public void SetDifficulty(int value)
    {
        points = points / difficultyMultiplier;
        difficultyMultiplier = value;
        points = points * difficultyMultiplier;
        float newZ = originalZ * difficultyMultiplier;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZ);
        transform.position = newPosition;
        difficultyText.text = difficultyMultiplier.ToString();
    }

    public void DifficultySlider(float value)
    {
        value *= maxDifficulty;
        int valueInt = Mathf.RoundToInt(value);
        if (valueInt == 0) valueInt = 1;
        SetDifficulty(valueInt);
    }

    public void AddShot()
    {
        shots++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreString = score.ToString() + " / " + shots.ToString();
        scoreText.text = scoreString;
    }
}
