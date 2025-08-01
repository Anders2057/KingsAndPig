using UnityEngine;

public class DoorsEvents : MonoBehaviour
{
    [SerializeField] private GameObject entranceDoor;
    [SerializeField] private Animator animatorEntranceDoor;

    private int idOpenDoor = Animator.StringToHash("OpenDoor");

    void OnEnable()
    {
        entranceDoor = GameObject.FindGameObjectWithTag("EntranceDoor");
        animatorEntranceDoor = entranceDoor.GetComponent<Animator>();
    }

    public void DoorOut()
    {
        animatorEntranceDoor.SetTrigger(idOpenDoor);
    }
}
