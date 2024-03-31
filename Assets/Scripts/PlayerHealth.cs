using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anim;
    [SerializeField] private AudioSource deathSound;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
    }
    private void Die()
    {
        deathSound.Play();
        player.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
