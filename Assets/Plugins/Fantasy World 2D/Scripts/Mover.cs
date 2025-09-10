using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FantasyWorld2d
{
    public class Mover : MonoBehaviour
    {
        public float speed = 0.5f;
        public float damp = 0.15f;
        [SerializeField]
        private float maxDistanceX = 160;
        [SerializeField]
        private float minDistanceX = -140;
        [SerializeField]
        private float minDistanceY = -20;
        [SerializeField]
        private float maxDistanceY = 1.6f;

        public Transform Player;
        public bool canMoveY = false;

        void LateUpdate()
        {
            //Move Left (if you are using different OS you can change KeyCode acording to your system
            if (Input.GetKey(KeyCode.LeftArrow) && Player.position.x >= minDistanceX)
            {
                transform.position += Vector3.left * speed * damp;
            }
            //Move Right (if you are using different OS you can change KeyCode acording to your system
            if (Input.GetKey(KeyCode.RightArrow) && Player.position.x <= maxDistanceX)
            {
                transform.position += Vector3.right * speed * damp;
            }
            //Move Down works only in Underwater Level (if you are using different OS you can change KeyCode acording to your system
            if (Input.GetKey(KeyCode.DownArrow) && Player.position.y >= minDistanceY && canMoveY)
            {
                transform.position += Vector3.down * speed * damp;

            }
            //Move Up works only in Underwater Level (if you are using different OS you can change KeyCode acording to your system
            if (Input.GetKey(KeyCode.UpArrow) && Player.position.y <= maxDistanceY && canMoveY)
            {
                transform.position += Vector3.up * speed * damp;
            }
        }

    }
}

