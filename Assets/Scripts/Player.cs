using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlatformId;

    bool m_Jumped;
    bool m_powerSetted;

    Rigidbody2D m_rb;
    Animator m_anim;

    float m_currentPowerBarValue = 0;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.Ins.IsGameStarted)
        {
            setPower();

            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
            }
        }    
    }

    void setPower()
    {
        if(m_powerSetted && !m_Jumped)
        {
            jumpForce.x += jumpForceUp.x + Time.deltaTime;
            jumpForce.y += jumpForceUp.y + Time.deltaTime;

            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);

            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

            m_currentPowerBarValue += GameManager.Ins.powerBarUp * Time.deltaTime;
            GameGUIManager.Ins.UpdatePowerBar(m_currentPowerBarValue, 1);
        }    

    }    

    public void SetPower(bool isHoldingMouse)
    {
        m_powerSetted = isHoldingMouse;
        if(!m_powerSetted && !m_Jumped)
        {
            Jump();
        }    

    }    

    void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0)
            return;

        m_rb.velocity = jumpForce;

        m_Jumped = true;

        if (m_anim)
            m_anim.SetBool("jumped", true);

        AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tagconst.GROUND))
        {
            Platform p = collision.transform.root.GetComponent<Platform>();
            if (m_Jumped)
            {
                m_Jumped = false;

                if (m_anim)
                    m_anim.SetBool("jumped", false);
                if (m_rb)
                    m_rb.velocity = Vector2.zero;

                jumpForce = Vector2.zero;

                m_currentPowerBarValue = 0;
                GameGUIManager.Ins.UpdatePowerBar(m_currentPowerBarValue, 1);
            }

            if(p && p.id != lastPlatformId)
            {
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);

                lastPlatformId = p.id;

                GameManager.Ins.AddScore();
            }
        }
        if(collision.CompareTag(Tagconst.DEAD_ZONE))
        {
            GameGUIManager.Ins.ShowGameOverDialog();

            AudioController.Ins.PlaySound(AudioController.Ins.gameover);

            Destroy(gameObject);

        }    
    }
    
}
