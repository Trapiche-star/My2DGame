using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// Animator 파라미터 이름 관리 클래스
    /// </summary>
    public static class AnimationString
    {
        public const string IS_MOVE = "IsMove";
        public const string IS_RUN = "IsRun";
        public const string IS_GROUNDED = "IsGrounded"; // 바닥 감지
        public const string JUMP = "Jump";               // 점프 트리거
    }
}
