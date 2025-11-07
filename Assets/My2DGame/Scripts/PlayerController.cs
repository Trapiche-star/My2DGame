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
        private Animator animator;
        private TouchingDirections touching; // [추가] 바닥/벽 감지용 클래스 참조

        // 이동 속도
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 5f;

        // 점프
        [SerializeField] private float jumpForce = 6f;

        // 입력 값
        private Vector2 inputMove = Vector2.zero;
        private bool isRunning = false;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touching = GetComponent<TouchingDirections>(); // [추가] TouchingDirections 연결
        }

        private void FixedUpdate()
        {
            Move();            
        }
        #endregion

        #region Input System Methods
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)
                isRunning = true;
            else if (context.canceled)
                isRunning = false;
        }

        // 스페이스바 눌렀을 때 점프 처리
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touching.IsGround) // [수정] TouchingDirections의 IsGround 사용
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f); // 기존 y속도 리셋
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetTrigger(AnimationString.JUMP); // 점프 애니메이션 호출
                Debug.Log("점프 트리거 발동!");
            }
        }
        #endregion

        #region Custom Methods
        private void Move()
        {
            float moveX = inputMove.x;
            float currentSpeed = isRunning ? runSpeed : walkSpeed;           

            // 좌우 이동
            rb2D.linearVelocity = new Vector2(moveX * currentSpeed, rb2D.linearVelocity.y);

            // 좌우 반전
            if (moveX > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (moveX < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            // 애니메이션 파라미터 갱신 (점프 중엔 이동 애니 정지)
            bool isMoving = Mathf.Abs(moveX) > 0.01f;
            bool grounded = touching.IsGround; // [수정] TouchingDirections 값 참조
            animator.SetBool(AnimationString.IS_MOVE, isMoving && grounded);
            animator.SetBool(AnimationString.IS_RUN, isRunning && grounded);
            animator.SetBool(AnimationString.IS_GROUNDED, grounded);
        }

       
        #endregion
    }
}

