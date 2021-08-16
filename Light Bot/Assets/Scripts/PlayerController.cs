using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum ActionType
    {
        MOVE_FORWARD,
        ROT_LEFT,
        ROT_RIGHT,
        TOGGLE_LIGHT,
        JUMP
    }
    
    public float lerpSpeed = 1.0f;

    public int score;

    Vector3 currentPos;
    Vector3 targetPos;

    float currentYpos;
    float targetYpos;

    float lerpTime = 0.0f;

    public List<ActionType> actions = new List<ActionType>();
    public int currentActionIndex = 0;
    GameObject currentPlatform;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        targetPos = transform.position;

        currentYpos = transform.rotation.eulerAngles.y;
        targetYpos = transform.rotation.eulerAngles.y;

        SnapToPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        ActionController();
        TempKey();

        lerpTime += Time.deltaTime * lerpSpeed;
        if(lerpTime > 1.0f)
        {
            lerpTime = 1.0f;
            currentPos = targetPos;
            currentYpos = targetYpos;
            SnapToPlatform();            
        }

        transform.position = Vector3.Lerp(currentPos, targetPos, lerpTime);
        transform.rotation = Quaternion.Euler(0, Mathf.Lerp(currentYpos, targetYpos, lerpTime), 0);
    }

    public void MoveForward()
    {
        targetPos = currentPos + transform.forward;
        lerpTime = 0.0f;
    }

    public void RotCW()
    {
        targetYpos += 90.0f;
        lerpTime = 0.0f;
    }

    public void RotCCW()
    {
        targetYpos -= 90.0f;
        lerpTime = 0.0f;
    }

    public void Jump()
    {
        targetPos = currentPos + transform.forward + transform.up;
        lerpTime = 0.0f;
    }

    void TempKey()
    {
        if (CanPreformAction() == false)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotCCW();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotCW();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLight();
        }
    }

    void ActionController()
    {
        if (CanPreformAction() == false)
            return;

        if (currentActionIndex >= actions.Count)
            return;

        var action = actions[currentActionIndex];
        currentActionIndex += 1;

        switch (action)
        {
            case ActionType.MOVE_FORWARD: MoveForward(); break;
            case ActionType.ROT_LEFT: RotCCW(); break;
            case ActionType.ROT_RIGHT: RotCW(); break;
            case ActionType.TOGGLE_LIGHT: ToggleLight(); break;
            case ActionType.JUMP: Jump(); break;
        }
    }

    bool CanPreformAction()
    {
        return lerpTime == 1.0f;
    }

    void SnapToPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var hitPlatform = hit.collider.gameObject;
            if (hitPlatform != currentPlatform)
            {
                currentPlatform = hitPlatform;
                targetPos = hit.point + (Vector3.up * 0.01f);
            }
        }
    }
    void ToggleLight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            var platform = hit.collider.gameObject.GetComponent<PlatformController>();
            if (platform)
            {
                platform.ToggleLight();
                lerpTime = 0;
                score++;
            }
        }
    }
}
