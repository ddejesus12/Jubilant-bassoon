using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text livesText;
    private int lives;
    public Text loseText;
    Animator anim;


    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioSource musicSource;
    private bool facingRight = true;
    



    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Count: " + scoreValue.ToString();
        winText.text = "";
        lives = 3;
        livesText.text = "Lives: " + lives;
        loseText.text = "";

      
        musicSource.Play();
        anim = GetComponent<Animator>();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            musicSource.clip = musicClipThree;
            musicSource.Play();
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            musicSource.clip = musicClipThree;
            musicSource.Play();
            anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Count:" + scoreValue.ToString();

            Destroy(collision.collider.gameObject);
        
            
            
        }

        score.text = "Count:" + scoreValue.ToString();
        if (scoreValue >= 5)
        {
            SceneManager.LoadScene("Final12");
        }

        else if (scoreValue == 4)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            winText.text = "You win! Game created by Derron DeJesus!";
        }


        if (collision.collider.tag == "Enemy")
        {
          
            lives = lives - 1;
            livesText.text = "Lives: " + lives;
            Destroy(collision.collider.gameObject);

        }
        if (lives == 0)
        {
            Destroy(this);
            
            loseText.text = "You Lose!";
        }
       


    }

    



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
           

            if (Input.GetKey(KeyCode.W))
            {
                musicSource.clip = musicClipFour;
                musicSource.Play();
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
                
            }
           

        }
    }
    
}