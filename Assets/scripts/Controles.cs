using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{
    Controle controles;
    float velocidade = 4;
    Rigidbody2D rb;
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
    }

    // Update is called once per frame
    void Update()
    {
        float y = controles.Jogo.NorteSul.ReadValue<float>();
        float x = controles.Jogo.LesteOeste.ReadValue<float>();
        Vector2 direcao = new Vector2(x, y) * Time.fixedTime;
        direcao.Normalize();
        // transform.Translate (direcao* velocidade*Time.deltaTime,0);
        Vector2 posicao = (Vector2)transform.position + direcao * velocidade * Time.fixedDeltaTime;
        rb.MovePosition(posicao);
        if (controles.Jogo.Ataque.WasPressedThisFrame())
        {
            Debug.Log("ataque!!!");
        }
    }
        private void FixedUpdate()
    {
        
    }
}

