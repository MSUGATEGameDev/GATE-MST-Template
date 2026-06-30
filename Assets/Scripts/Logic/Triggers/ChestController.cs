using System.Collections;
using UnityEngine;

public class ChestController : GameTrigger
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameTrigger --\n" +
    "Allows a chest to auto-open when the player is present or be locked to color-coded keys.";
    [Header("Chest Settings")]
    [Tooltip("")] public bool lockedWithKey = false;
    public ColorManager.StandardColor keyColor;
    [Header("Components")]
    public GameObject lockIcon;
    public bool opened = false;

    public ParticleSystem particles;
    public GameObject particleObject;
    public Light explosionLight;
    public GameObject lightObject;

    // Internal Logic
    bool stillLocked = false;

    private void Start()
    {
        if (lockedWithKey)
        {
            stillLocked = true;
            lockIcon.SetActive(true);
            Material[] mat = new Material[2] {ColorManager.singleton.standardHolos[(int)keyColor], ColorManager.singleton.holoBlack };
            foreach (Renderer rend in lockIcon.GetComponentsInChildren<Renderer>())
            {
                rend.materials = mat;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            if (lockedWithKey && stillLocked)
            {
                if (ObjectivesManager.CheckKey(keyColor))
                {
                    HUD.DisplayNotice("Chest Unlocked");
                    lockIcon.SetActive(false);
                    stillLocked = false;
                    ChestUnlock();
                }
                else
                {
                    HUD.DisplayNotice(keyColor.ToString() + " key needed.");
                }
            }
            else
            {
                ChestUnlock();
            }
        }
    }
    void ChestUnlock()
    {
        opened = true;
        ActivateItems();
        lightObject.SetActive(true);
        particleObject.SetActive(true);
        particles.Play();
        StartCoroutine(StopChestAnim());
    }
    IEnumerator StopChestAnim()
    {
        while (explosionLight.intensity > 0) 
        {
            yield return null;
            explosionLight.intensity -= Time.deltaTime*2;
        }
        yield return new WaitForSeconds(3);
        particleObject.SetActive(false);
        lightObject.SetActive(false);
    }
}
