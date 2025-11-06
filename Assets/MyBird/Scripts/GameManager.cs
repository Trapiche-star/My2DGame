using UnityEngine;

namespace MyBird
{
    /// <summary>
    /// 게임 전체(흐름)를 관리하는 클래스
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //게임 시작 여부 체크
        private static bool isStart;

        //게임 오버 체크
        private static bool isDeath;

        //게임 스코어
        private static int score;

        public static float spawnValue = 0f;
        #endregion

        #region Property
        public static bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }

        public static bool IsDeath
        {
            get { return isDeath; }
            set { isDeath = value; }
        }

        public static int Score
        {
            get { return score; }
            set { score = value; }
        }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            IsStart = false;    // 아직 게임 시작 안 함
            IsDeath = false;    // 죽지 않은 상태
            Score = 0;          // 점수 초기화
            spawnValue = 0f;    // 스폰 난이도 값 초기화
        }
        private void Update()
        {
            #if UNITY_EDITOR
            //치팅 - 저장 데이터 삭제
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerPrefs.DeleteAll();
            }
            #endif
        }
        #endregion
    }
}
