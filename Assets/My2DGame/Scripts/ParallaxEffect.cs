using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 시차에 의한 배경 움직임 구현
    /// </summary>
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public Camera cam;                     //카메라 오브젝트
        public Transform followTarget;         //플레이어

        private Vector2 startPosition;         //배경오브젝트의 최초 위치
        private float startZ;                  //배경 오브젝트의 최초 z 위치
        #endregion

        #region Propety
        //카메라가 시작 위치에서부터 이동한 거리
        public Vector2 CamMoveSinceStart => startPosition - (Vector2)cam.transform.position; 

        //플레이어와 배경과의 거리
        public float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

        //
        public float clippingPlane => cam.transform.position.z + (zDistanceFromTarget < 0f ? cam.farClipPlane : cam.nearClipPlane);

        //시차 계수
        private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
        #endregion

        #region Custom Method    
        private void Start()
        {
            //초기화
            startPosition = this.transform.position;
            startZ = this.transform.position.z;
        }

        //시차에 의한 배경 이동 위치 구하기
        private void Update()
        {
            Vector2 newPosition = startPosition + CamMoveSinceStart * parallaxFactor;
            this.transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
        }
        #endregion


    }

}
