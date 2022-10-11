using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{
    Controle controles;
    float velocidade = 4;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    float x, y;
    float attackTime = .25f;
    float attackCounter = .25f;
    bool Attacking;

    // Start is called before the first frame update

    private void Awake()
    {
        controles = new Controle();
    }

    private void OnEnable()
    {
        controles.Enable();
    }

    private void OnDisable()
    {
        controles.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
         y = controles.Jogo.NorteSul.ReadValue<float>();
         x = controles.Jogo.LesteOeste.ReadValue<float>();
     //   Vector2 direcao = new Vector2(x, y) * Time.fixedTime;
     //   direcao.Normalize();
     //   // transform.Translate (direcao* velocidade*Time.deltaTime,0);
     //   Vector2 posicao = (Vector2)transform.position + direcao * velocidade * Time.fixedDeltaTime;
     //   rb.MovePosition(posicao);

        if (Attacking)
        {
            rb.velocity = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("ATTACKING", false);
                Attacking = false;
            }
        }


        if (controles.Jogo.Ataque.WasPressedThisFrame())
        {
            attackCounter = attackTime;
            animator.SetBool("ATTACKING",true);
            Attacking = true;
        }


    }
    private void FixedUpdate()
    {

   
        Vector2 direcao = new Vector2(x, y);
        float magnitude = direcao.sqrMagnitude;
        animator.SetFloat("VELOCIDADE", direcao.sqrMagnitude);
        animator.SetFloat("HORIZONTAL", x);
        animator.SetFloat("VERTICAL", y);

        if(magnitude > 0)
        {
            if (audioSource.isPlaying == false)
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }


        direcao.Normalize();
        rb.velocity = direcao * velocidade;
    }
}

