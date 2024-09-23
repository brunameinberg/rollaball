using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador (gato)
    public float speed = 5f; // Velocidade do inimigo
    private Rigidbody rb; // Referência ao Rigidbody do inimigo
    private bool isGameActive = true;
    public PlayerController playerController; // Referência ao PlayerController


    void Start()
    {
        // Obtenha o componente Rigidbody do inimigo
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (isGameActive && player != null)
        {
            // Direção para o jogador
            Vector3 direction = (player.position - transform.position).normalized;

            // Movimento usando Rigidbody
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

            // Rotaciona o inimigo para olhar na direção do jogador
            transform.LookAt(player);

            // Verifica a pontuação do jogador para parar o movimento
            if (playerController.count >= 20 || playerController.count < 0)
            {
                StopMovement();
            }
        }
    }

    public void StopMovement()
    {
        isGameActive = false; // Para o movimento dos inimigos
    }
}
