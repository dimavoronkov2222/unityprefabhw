using UnityEngine;
namespace SkeletonEditor
{
    public class EnemyController : MonoBehaviour
    {
        public int health = 100;
        public float attackRange = 1.5f;
        public float attackCooldown = 1.5f;
        public int attackDamage = 10;
        public float moveSpeed = 2f;
        public GameObject corpsePrefab;
        private float lastAttackTime;
        private Animator animator;
        private Transform player;
        private PlayerController playerController;
        private int hitCount = 0;
        private bool isWounded = false;
        void Start()
        {
            animator = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }
        void Update()
        {
            if (player == null) return;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else if (Time.time - lastAttackTime > attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        void MoveTowardsPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            animator.SetFloat("speedv", 1f);
        }
        void AttackPlayer()
        {
            animator.SetTrigger("Attack1h1");
            animator.SetFloat("speedv", 0.0f);
        }
        public void DealDamage()
        {
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            hitCount++;
            if (hitCount >= 10 && !isWounded)
            {
                isWounded = true;
                moveSpeed /= 2;
            }
            if (health <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            animator.SetTrigger("Fall1");
            if (corpsePrefab != null)
            {
                Instantiate(corpsePrefab, transform.position, transform.rotation);
            }
            Destroy(gameObject, 2f);
        }
    }
}