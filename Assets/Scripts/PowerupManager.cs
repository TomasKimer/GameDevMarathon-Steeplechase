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

        GameObject powerup = Instantiate(prefab, pos, rot) as GameObject;
        powerup.name = type.ToString();

        return powerup;
    }

    public bool ProcessCollision(GameObject other) {
        if (other.name.Equals(E_PowerupType.PowerUpCamera.ToString())) {
            HasCameraPowerUp = true;
            other.SetActive(false);
            return true;
        }
        
        if (other.name.Equals(E_PowerupType.PowerDownJump.ToString())) {
            HasJumpPowerDown = true;
            other.SetActive(false);
            return true;
        }

        return false;
    }
}
