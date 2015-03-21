using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {
    
    [System.Flags]
    public enum E_PowerupType {
        None          = 1 << 0,
        PowerUpCamera = 1 << 1,
        PowerDownJump = 1 << 2
    }

    public GameObject powerUpCamera;
    public GameObject powerDownJump;

    private E_PowerupType m_ActivePowerups = E_PowerupType.None;

	void Start () {
	}
	
	void Update () {
	}

    public bool IsPowerupActive(E_PowerupType type) {
        return m_ActivePowerups.HasFlag(type);
    }

    public void SetPowerupActive(E_PowerupType type) {
        m_ActivePowerups = m_ActivePowerups.SetFlag(type);
    }

    public void RemovePowerup(E_PowerupType type) {
        m_ActivePowerups = m_ActivePowerups.RemoveFlag(type);
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
            other.SetActive(false);
            
            CancelInvoke("RemovePowerUpCamera");
            SetPowerupActive(E_PowerupType.PowerUpCamera);
            Invoke("RemovePowerUpCamera", 1.0f);

            return true;
        }
        
        if (other.name.Equals(E_PowerupType.PowerDownJump.ToString())) {
            other.SetActive(false);

            CancelInvoke("RemovePowerDownJump");
            SetPowerupActive(E_PowerupType.PowerDownJump);
            Invoke("RemovePowerDownJump", 1.0f);

            return true;
        }

        return false;
    }

    IEnumerator _UsePowerup(E_PowerupType type) {
        yield break;
    }

    void RemovePowerUpCamera() {
        RemovePowerup(E_PowerupType.PowerUpCamera);
    }

    void RemovePowerDownJump() {
        RemovePowerup(E_PowerupType.PowerDownJump);
    }
}

public static class PowerupTypeExtension
{
    public static bool                         HasFlag   (this PowerupManager.E_PowerupType flags, PowerupManager.E_PowerupType flag) { return ((int)flags & (int) flag) == (int)flag; }
    public static PowerupManager.E_PowerupType SetFlag   (this PowerupManager.E_PowerupType flags, PowerupManager.E_PowerupType flag) { return flags |  flag;                          }
    public static PowerupManager.E_PowerupType RemoveFlag(this PowerupManager.E_PowerupType flags, PowerupManager.E_PowerupType flag) { return flags & ~flag;                          }
}
