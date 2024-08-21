using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private GameObject CharacterFollow;
    [SerializeField] private GameObject ShadowFollow;

    Rigidbody characterRB;
    Transform shadowTransform;
    void Start()
    {
        characterRB = CharacterFollow.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
