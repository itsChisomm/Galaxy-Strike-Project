using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 100f;

    bool isFiring = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        ProcessFiring();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
    }
    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void ProcessFiring()
    {
            foreach (GameObject laser in lasers)

            {
                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isFiring;
            } 
                 
    }

    void MoveCrosshair()
    {
               
        crosshair.position = Mouse.current.position.ReadValue(); 
    }

    void MoveTargetPoint()
    {

        Vector3 targetPointPosition = new Vector3(crosshair.position.x , crosshair.position.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    void AimLasers()
    {
               foreach (GameObject laser in lasers)
        {
            Vector3 fireDirection = targetPoint.position - this.transform.position; // Calculate the direction from the laser to the target point
            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection); // Calculate the rotation needed to look at the target point
            laser.transform.rotation = rotationToTarget; // Aim the laser at the target point
        }
    }
}
