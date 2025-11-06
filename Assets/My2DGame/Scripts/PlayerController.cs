using UnityEngine;
using UnityEngine.InputSystem;

namespace My2DGame
{
    /// <summary>
    /// 플레이어를 제어하는 클래스
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        // 참조
        private Rigidbody2D rb2D;

        //이동
        [SerializeField]
        private float walkSpeed = 5f;               //걷는 속도

        // 입력 값
        private Vector2 inputMove = Vector2.zero;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            rb2D = this.GetComponent<Rigidbody2D>();
        }
        
        #endregion

        #region Custom Method    
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove =context.ReadValue<Vector2>();
            Debug.Log($"Move Input: {inputMove}");
        }

        private void FixedUpdate()
        {
            //좌우이동
            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
        }
        #endregion


        /* AI 버전
       #region Variables
       private Rigidbody2D rb;              // 리지디바디
       private Vector2 moveInput;           // 입력값 (x축 기준)
       [SerializeField] private float moveSpeed = 5f;   // 이동 속도
       #endregion

       #region Unity Event
       private void Awake()
       {
           rb = GetComponent<Rigidbody2D>();
       }

       private void FixedUpdate()
       {
           Move();
       }
       #endregion

       #region InputSystem Methods
       // Input Action에서 Move 액션 연결
       public void OnMove(InputAction.CallbackContext context)
       {
           // context.ReadValue<Vector2>() → (x, y) 입력값
           moveInput = context.ReadValue<Vector2>();
       }
       #endregion

       #region Custom Methods
       private void Move()
       {
           // Rigidbody2D.linearVelocity로 이동 처리 (최신 Unity 권장 방식)
           rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

           // 좌우 반전 처리
           if (moveInput.x > 0.01f)
               transform.localScale = new Vector3(1, 1, 1);
           else if (moveInput.x < -0.01f)
               transform.localScale = new Vector3(-1, 1, 1);
       }
       #endregion
       */
    }

}

