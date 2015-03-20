using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

    public enum E_PowerupType {
        PowerUpCamera,
        PowerDownJump
    }

    public GameObject powerUpCamera;
    public GameObject powerDownJump;

    public bool HasCameraPowerUp { get; set; }
    public bool HasJumpPowerDown { get; set; }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public GameObject GeneratePowerup(E_PowerupType type, Vector3 pos, Quaternion rot) {
        GameObject prefab = null;
        
        switch (type) {
            case E_PowerupType.PowerUpCamera:
                prefab = powerUpCamera;
                break;
            case E_PowerupType.PowerDownJump:
                prefab = powerDownJump;
                break;
        }

        return Instantiate(prefab, pos, rot) as GameObject;
    }

    public bool ProcessCollision(GameObject other) {
        if (other.name.Equals("Steak")) {
            HasCameraPowerUp = true;
            other.SetActive(false);
            return true;
        }
        
        if (other.name.Equals("Steak2")) {
            HasJumpPowerDown = true;
            other.SetActive(false);
            return true;
        }

        return false;
    }
}
