using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Docsa.Character
{

    public class ProjectileNet : Projectile
    {
        public Hunter Thrower;
        Docsa targetDocsa;
        Rigidbody2D rb;
        Camera Camera;

        [SerializeField] GameObject netPrefab = null;
        [SerializeField] Transform m_tfnet = null;


        // Start is called before the first frame update
        void Start()
        {
           
        }

        public void Init(Docsa targetDocsa)
        {
            rb = GetComponent<Rigidbody2D>();
            targetDocsa = GameObject.Find("targetDocsa").GetComponent<Docsa>();
        }

        // Update is called once per frame
        void Update()
        {
            GuidedDocsa();
        }

        void GuidedDocsa()
        {
            
            Vector2 netPos = Camera.ScreenToWorldPoint(targetDocsa.transform.position);
            Vector2 net_direction = new Vector2(targetDocsa.transform.position.x - netPos.x,
            targetDocsa.transform.position.y - netPos.y); //net이 바라볼 방향 설정(타겟독사의 위치에서 헌터의 위치)
            m_tfnet.right = net_direction;

            GameObject net = Instantiate(netPrefab, m_tfnet.position, m_tfnet.rotation);
            net.GetComponent<Rigidbody2D>().velocity = net.transform.right * 10f;

            ///dir = (target.transform.position - transform.position).normalized;
            ///float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // netTarget = Quaternion.AngleAxis(angle, Vector3.forward);
            // transform.rotation = Quaternion.Slerp(transform.rotation, netTarget, Time.deltaTime * netSpeed);
            // rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
        }



        void OnTriggerEnter2D(Collider2D collider)
        {
            //문제가 있음...Debug 요망
            if (collider.gameObject.GetComponent<Docsa>() != null)
            {
                Thrower.Behaviour.GrabDocsa(targetDocsa);
            }
            Destroy(this.gameObject);
        }
    }
}