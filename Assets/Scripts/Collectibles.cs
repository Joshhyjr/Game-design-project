using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    //Count the no of collectibles
    private int count = 0;
    [SerializeField] private Text score;

    [SerializeField] private AudioSource collectionSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectionSound.Play();
            Destroy(collision.gameObject);
            count++;
            score.text = "Score: " + count;
        }
    }
}
