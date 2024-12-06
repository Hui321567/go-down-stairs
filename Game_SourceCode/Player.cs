using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5f;
    GameObject CurrentFloor;
    public int Hp;
    public GameObject HpBar;
    public Text ScoreText;
    int Score;
    float ScoreTime;
    public GameObject ReplayBottom;
    // Start is called before the first frame update
    void Start()
    {
        Hp = 10;
        Score = 0;
        ScoreTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("Run",true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run", false);
        }
        UpdateScore();
    }
    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "floor1")
        {
            if (Other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("撞到floor1");
                CurrentFloor = Other.gameObject;
                ModifyHp(1);
                UpdateHpBar();
                Other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (Other.gameObject.tag == "floor2")
        {
            if (Other.contacts[0].normal == new Vector2(0f, 1f))
            {
                Debug.Log("撞到floor2");
                CurrentFloor = Other.gameObject;
                ModifyHp(-3);
                UpdateHpBar();
                GetComponent<Animator>().SetTrigger("Hurt");
                Other.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (Other.gameObject.tag == "Ceiling")
        {
            Debug.Log("撞到Ceiling");
            CurrentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHp(-1);
            UpdateHpBar();
            GetComponent<Animator>().SetTrigger("Hurt");
            Other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "deathline")
        {
            Debug.Log("You lose!");
            Die();
        }
    }
    void ModifyHp(int num)
    {
        Hp += num;
        if (Hp > 10)
        {
            Hp = 10;
        }
        else if (Hp <= 0)
        {
            Hp = 0;
            Die();
        }
    }
    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (Hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            { 
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void UpdateScore()
    {
        ScoreTime += Time.deltaTime;
        if(ScoreTime > 5f)
        {
            Score++;
            ScoreTime = 0f;
            ScoreText.text = "地下" + Score.ToString() + "層";
        }
        

    }
    void Die()
    {
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0f;
        ReplayBottom.gameObject.SetActive(true);
    }
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HotPotTest");
    }
}
