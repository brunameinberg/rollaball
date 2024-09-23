using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Referência ao componente de texto na UI
    private float startTime;
    private bool isRunning = true;

    // Referência ao script do Player para acessar o score
    public PlayerController playerController; // Certifique-se de que você adiciona esse campo no Inspector

    void Start()
    {
        // Inicializa o tempo quando o jogo começa
        startTime = Time.time;
    }

    void Update()
    {
        if (isRunning)
        {
            // Verifica o valor do score do jogador
            if (playerController.count >= 20 || playerController.count < 0) // Substitua 'count' pela variável correta se for diferente
            {
                StopTimer(); // Para o cronômetro quando o score for >= 12
            }

            // Calcula o tempo decorrido desde o início
            float timeElapsed = Time.time - startTime;

            // Converte o tempo para minutos e segundos
            int minutes = Mathf.FloorToInt(timeElapsed / 60); // Divide por 60 para obter os minutos
            int seconds = Mathf.FloorToInt(timeElapsed % 60); // Obtém os segundos restantes

            // Atualiza o texto do timer
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Método para parar o timer
    public void StopTimer()
    {
        isRunning = false;
    }
}
