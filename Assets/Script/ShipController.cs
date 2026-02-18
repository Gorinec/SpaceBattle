using UnityEngine;
using TMPro;


public class ShipController : MonoBehaviour
{
    [Header("Точки двигателей")]
    public Transform leftEnginePoint;
    public Transform rightEnginePoint;

    [Header("Силы")]
    public float engineForce = 30f;
    public float forwardForce = 50f;
    public float rotationTorque = 20f;
    public float boostMultiplier = 2f;

    [Header("Форсаж")]
    public float maxBoost = 100f;
    public float currentBoost = 100f;
    public float boostDrainRate = 20f;
    public float boostRechargeRate = 10f;
    public float minBoostToUse = 20f;

    [Header("Эффект буста")]
    public GameObject boostEffect; 

    [Header("UI")]
    public TMP_Text boostText;

    private Rigidbody rb;
    private bool isBoosting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        if (boostEffect != null)
        {
            boostEffect.SetActive(false);
        }
    }

    void Update()
    {
        var keyboard = UnityEngine.InputSystem.Keyboard.current;
        if (keyboard == null) return;

        float currentForce = forwardForce;
        bool wasBoosting = isBoosting;
        isBoosting = false;

        // ПРИ НАЖАТИИ ЛЕВОГО SHIFT
        if (keyboard.leftShiftKey.isPressed && currentBoost >= minBoostToUse)
        {
            currentForce *= boostMultiplier;
            isBoosting = true;

            currentBoost -= boostDrainRate * Time.deltaTime;
            if (currentBoost < 0) currentBoost = 0;
        }
        else if (currentBoost < maxBoost)
        {
            currentBoost += boostRechargeRate * Time.deltaTime;
            if (currentBoost > maxBoost) currentBoost = maxBoost;
        }

        // ВКЛЮЧАЕМ/ВЫКЛЮЧАЕМ ЭФФЕКТ БУСТА
        if (boostEffect != null)
        {
            if (isBoosting && !boostEffect.activeSelf)
            {
                boostEffect.SetActive(true);
            }
            else if (!isBoosting && boostEffect.activeSelf)
            {
                boostEffect.SetActive(false);
            }
        }

        if (boostText != null)
        {
            if (isBoosting)
                boostText.text = $"ФОРСАЖ: {(int)currentBoost}/100";
            else
                boostText.text = $"Форсаж: {(int)currentBoost}/100";
        }

        if (keyboard.aKey.isPressed)
        {
            rb.AddTorque(0, -rotationTorque * Time.deltaTime, 0, ForceMode.VelocityChange);
        }

        if (keyboard.dKey.isPressed)
        {
            rb.AddTorque(0, rotationTorque * Time.deltaTime, 0, ForceMode.VelocityChange);
        }

        if (keyboard.wKey.isPressed)
        {
            rb.AddForce(transform.forward * currentForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (keyboard.sKey.isPressed)
        {
            rb.AddForce(-transform.forward * currentForce * 0.3f * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}