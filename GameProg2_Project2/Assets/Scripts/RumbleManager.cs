using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;
    [SerializeField] private Gamepad gamePad;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float lowFreq, float highFreq, float duration)
    {
        gamePad = Gamepad.current;
        if(gamePad != null)
        {
            gamePad.SetMotorSpeeds(lowFreq, highFreq);
            StartCoroutine(StopRumble(duration, gamePad));
        }
    }

    IEnumerator StopRumble(float duration, Gamepad gamePad)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration);
        gamePad.SetMotorSpeeds(0, 0);
    }
}
