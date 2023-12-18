using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public int hitThreshold = 5; // 撞击阈值
    private int hitCount = 0;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        PlayerMovement player = collision.GetComponent<CapsuleCollider>().transform.root.GetComponent<PlayerMovement>();

        Debug.Log(collision.gameObject.name);

        if (player )//&& player.IsDashing
        {
            Debug.Log(collision.transform.root);
            //collision.gameObject.GetComponent<Dashing>().ResetDashCooldown();
            Dashing dashing = GetComponent<Dashing>();
            dashing.ResetDashCooldown();

            // 改变颜色
            Color currentColor = objectRenderer.material.color;
            float colorChange = 1.0f / hitThreshold;
            currentColor.r = Mathf.Max(0, currentColor.r - colorChange);
            currentColor.g = Mathf.Max(0, currentColor.g - colorChange);
            currentColor.b = Mathf.Max(0, currentColor.b - colorChange);
            objectRenderer.material.color = currentColor;

            hitCount++;
            if (hitCount >= hitThreshold)
            {
                Destroy(gameObject);
            }
        }
    }
}