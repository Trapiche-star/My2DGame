using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 그라운드, 천정, 벽 체크
    /// </summary>
    public class TouchingDirections : MonoBehaviour
    {
        #region Variables        
        //참조
        //접촉하는 충돌체
        private CapsuleCollider2D touchingCol;  // 플레이어에 붙은 캡슐 콜라이더
        private Animator animator;              // 애니메이터

        //접촉면 범위
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float cellingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.1f;

        //접촉 조건
        [SerializeField]
        private ContactFilter2D contactFilter;

        //캐스트 결과
        private RaycastHit2D[] hitBufferHits = new RaycastHit2D[5];
        private RaycastHit2D[] cellingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];

        //
        [SerializeField] private bool isGround;
        [SerializeField] private bool isCelling;
        [SerializeField] private bool isWall;  // 내부 상태 저장용 (재귀 방지)
        #endregion

        #region Property
        public bool IsGround
        {
            get { return isGround; }
            private set { isGround = value; }
        }

        public bool IsCelling
        {
            get { return isCelling; }
            private set
            {
                isCelling = value;
                //애니메이션이 있다면 세팅
            }
        }

        public bool IsWall
        {
            get { return isWall; }
            private set { isWall = value; }
        }

        // 벽체크 방향: 오른쪽 보고 있으면 오른쪽, 왼쪽이면 왼쪽
        private Vector2 wallCheckDirection => (this.transform.localScale.x > 0f) ? Vector2.right : Vector2.left;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            touchingCol = this.GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // 아래로 캐스트 → 바닥 체크
            IsGround = (touchingCol.Cast(Vector2.down, contactFilter, hitBufferHits, groundDistance) > 0);

            // 위로 캐스트 → 천장 체크
            IsCelling = (touchingCol.Cast(Vector2.up, contactFilter, cellingHits, cellingDistance) > 0);

            // 좌우 캐스트 → 벽 체크 (방향은 localScale로 결정)
            IsWall = (touchingCol.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0);

            // 애니메이터로 상태 전달
            animator.SetBool("IsGrounded", IsGround);
            animator.SetBool("IsCelling", IsCelling);
            animator.SetBool("IsWall", IsWall);
        }
        #endregion

    }

}
