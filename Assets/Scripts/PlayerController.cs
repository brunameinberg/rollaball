using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public int count;
    private float movementX;
    private float movementY;
    public float speed = 10f; 
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject restartButton;

    public float fallThreshold = -10f; // min height to trigger fall
    private bool isGameActive = true;

    // Referências para os sons
    public AudioClip catmiaw;
    public AudioClip dogbark;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
        restartButton.SetActive(false);
        loseTextObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();

    }

    void OnMove(InputValue momentValue)
    {
        if (isGameActive){
            Vector2 movementVector = momentValue.Get<Vector2>();

            movementX = movementVector.x; 
            movementY = movementVector.y;
        }
    }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 20)
       {
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
            isGameActive = false;
       }
       if (count < 0){
            loseTextObject.SetActive(true);
            restartButton.SetActive(true);
            isGameActive = false;
       }
   }

    void FixedUpdate()
    {
        if (isGameActive) // Só permite movimento se o jogo estiver ativo
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            rb.freezeRotation = true; 

            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10f);  // Suave rotação
            }
        }

        if (transform.position.y < fallThreshold)
        {
            RestartGame(); 
        }
    
    }

    void OnTriggerEnter(Collider other) 
   {
    if (other.gameObject.CompareTag("PickUp")) 
       {
        other.gameObject.SetActive(false);
        count = count + 1;
        SetCountText();

        audioSource.PlayOneShot(catmiaw);
       }
    if (other.gameObject.CompareTag("Enemy"))
    {
        count = count - 1;
        SetCountText();

        audioSource.PlayOneShot(dogbark);
    }

   }


   public void RestartGame(){
        SceneManager.LoadSceneAsync(0);
    }


}
