using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerIndex;
    private Camera cam;
    [SerializeField] public GameObject car;
    private CinemachineVirtualCamera virtualCamera;
    public TextMeshProUGUI lifeText;
    private int life = 100;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCamera.Follow = car.transform;
        virtualCamera.LookAt = car.transform;

        cam = GetComponentInChildren<Camera>();

        Vector2 pos;

        cam.cullingMask &= ~(1 << 6);
        cam.cullingMask &= ~(1 << 7);
        cam.cullingMask &= ~(1 << 8);
        cam.cullingMask &= ~(1 << 9);

        switch (playerIndex)
        {
            case 1:
                gameObject.layer = 6;
                cam.cullingMask |= (1 << 6);
                pos = new Vector2(0, 0);
                car.GetComponent<CarControl>().throttleInputName = "[P1]Throttle";
                car.GetComponent<CarControl>().brakeInputName = "[P1]Brake";
                car.GetComponent<CarControl>().steeringInputName = "[P1]Steering";
                car.GetComponent<CarControl>().jumpInputName = "[P1]Jump";
                break;
            case 2:
                gameObject.layer = 7;
                cam.cullingMask |= (1 << 7);
                pos = new Vector2(0.5f, 0);
                car.GetComponent<CarControl>().throttleInputName = "[P2]Throttle";
                car.GetComponent<CarControl>().brakeInputName = "[P2]Brake";
                car.GetComponent<CarControl>().steeringInputName = "[P2]Steering";
                car.GetComponent<CarControl>().jumpInputName = "[P2]Jump";
                break;
            case 3:
                gameObject.layer = 8;
                cam.cullingMask |= (1 << 8);
                pos = new Vector2(0, 0.5f);
                car.GetComponent<CarControl>().throttleInputName = "[P3]Throttle";
                car.GetComponent<CarControl>().brakeInputName = "[P3]Brake";
                car.GetComponent<CarControl>().steeringInputName = "[P3]Steering";
                car.GetComponent<CarControl>().jumpInputName = "[P3]Jump";
                break;
            case 4:
                gameObject.layer = 9;
                cam.cullingMask |= (1 << 9);
                pos = new Vector2(0.5f, 0.5f);
                car.GetComponent<CarControl>().throttleInputName = "[P4]Throttle";
                car.GetComponent<CarControl>().brakeInputName = "[P4]Brake";
                car.GetComponent<CarControl>().steeringInputName = "[P4]Steering";
                car.GetComponent<CarControl>().jumpInputName = "[P4]Jump";
                break;
            default:
                pos = new Vector2(0, 0);
                Debug.LogError("Player index invalid : should be between 1 and 4.");
                break;
        }

        Vector2 size = new Vector2(0.5f, 0.5f);

        cam.rect = new Rect(pos, size);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        lifeText.text = life.ToString();
    }

    public int GetSpeed()
    {
        return (int)car.GetComponent<CarControl>().GetComponent<Rigidbody>().velocity.magnitude;
    }
}
