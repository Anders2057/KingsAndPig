using UnityEngine;

public class DoorsEvents : MonoBehaviour
{
    [SerializeField] private GameObject entranceDoor;
    [SerializeField] private Animator animatorEntranceDoor;

    private int idOpenDoor;

    void OnEnable()
    {
        entranceDoor = GameObject.FindGameObjectWithTag("EntranceDoor");
        animatorEntranceDoor = entranceDoor.GetComponent<Animator>();

        idOpenDoor = Animator.StringToHash("OpenDoor");
    }

    public void DoorOut()
    {
        animatorEntranceDoor.SetTrigger(idOpenDoor);
    }
}
