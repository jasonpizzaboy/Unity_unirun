using UnityEngine;

                                                                    // PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip;                                      // 사망시 재생할 오디오 클립
   public float jumpForce = 700f;                                   // 점프 힘

   private int jumpCount = 0;                                       // 누적 점프 횟수
   private bool isGrounded = false;                                 // 바닥에 닿았는지 나타냄
   private bool isDead = false;                                     // 사망 상태

   private Rigidbody2D playerRigidbody;                             // 사용할 리지드바디 컴포넌트
   private Animator animator;                                       // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio;                                 // 사용할 오디오 소스 컴포넌트

   private void Start() {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio=GetComponent<AudioSource>();
   }

   private void Update() {
                                                                    // 사용자 입력을 감지하고 점프하는 처리
       if(isDead)
       {
            return;
       }

       if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {                                                           //좌클릭 0 우클릭 1 점프카운트가 2보다 작을때 (GetMOuseButton 누르는 동안 계속)
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;                //제대로 뛰기 위해서 0으로 맞춤
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
       else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {                                                           //마우스를 눌렀다가 떼는 순간 올라갈때 떨어질때 말고 자연스럽게 떨어지기 위해 0.05 곱한다.
            playerRigidbody.velocity = playerRigidbody.velocity * 0.05f;
        }

        animator.SetBool("Grounded", isGrounded);                   //애니메이터에 있는 그라운디드에 이즈드라운디드 값을 넣어라 애니메이션을 바꿔줌

   }


   private void Die() {                                              // 사망 처리
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other)
    {                                                                 // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }

        if (other.tag == "Item" && !isDead)
        {
            Item item = other.GetComponent<Item>();

            if(item != null)
            {
                item.Use(gameObject);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {         //뭐하고 충돌했는지 충돌체가 들어옴
        // 바닥에 닿았음을 감지하는 처리
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision)
    {// 바닥에서 벗어났음을 감지하는 처리 점프하는 순간
        isGrounded = false;
    }
}