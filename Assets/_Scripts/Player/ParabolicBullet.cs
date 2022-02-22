using System;
using UnityEngine;

namespace TEST_ZONE
{
    public class ParabolicBullet : MonoBehaviour
    {
        private float speed;
        private Vector3 startPosition;
        private Vector3 startForward;
        private Vector3 wind;

        private bool isInitialized = false;
        private float startTime = -1;

        public GameObject hitmark;
        
        public void Initialize(Transform startPoint)
        {
            startPosition = startPoint.position;
            startForward = startPoint.forward;
            this.speed = GetComponent<BulletData>().muzzleVelocity;
            wind = GetComponent<BulletData>().windSpeedVector;
            
            isInitialized = true;
        }
        
        private Vector3 FindPointOnParabola(float time)
        {
            Vector3 movementVec = startForward * speed * time;
            Vector3 windVec = new Vector3(wind.x, 0, wind.y) * time * time;
            Vector3 gravityVec = Physics.gravity * time * time;

            return startPosition + gravityVec + movementVec + windVec;
        }

        private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
        {
            Debug.DrawLine(startPoint, endPoint);
            return Physics.Linecast(startPoint, endPoint, out hit);
        }
        
        private void FixedUpdate()
        {
            if (!isInitialized) return;
            if (startTime < 0) startTime = Time.time;

            float currentTime = Time.time - startTime;
            float nextTime = currentTime + Time.fixedDeltaTime;

            Vector3 currentPoint = FindPointOnParabola(currentTime);
            Vector3 nextPoint = FindPointOnParabola(nextTime);

            if (!CastRayBetweenPoints(currentPoint, nextPoint, out RaycastHit hit)) return;
            
            GameObject mark = Instantiate(hitmark);
            mark.transform.position = hit.point;
            Destroy(this.gameObject);
        }

        private void Update()
        {
            if (!isInitialized || startTime < 0) return;
            
            float currentTime = Time.time - startTime;
            Vector3 currentPoint = FindPointOnParabola(currentTime);
            transform.position = currentPoint;
        }
    }
}