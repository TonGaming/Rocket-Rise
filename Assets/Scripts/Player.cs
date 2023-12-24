using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Section")]
    [SerializeField] float moveAmount = 10f;
    [SerializeField] float rotationAmount = 20f;

    [Header("Status Section")]
    [SerializeField] bool isDead = false;
    [SerializeField] bool isSuccess = false;

    // khai báo biến bool để làm switch
    bool NoClipSwitch;

    [Header("Particles Effects Section")]
    [SerializeField] ParticleSystem mainEnginePE;
    [SerializeField] ParticleSystem leftSidePE;
    [SerializeField] ParticleSystem rightSidePE;
    [SerializeField] ParticleSystem explosionPE;
    [SerializeField] ParticleSystem successPE;



    Rigidbody rocketRigidbody;

    AudioManager gameAudio;

    LevelManager gameLevels;

    CollisionHandler gameCollisions;


    private void Awake()
    {
        gameAudio = FindObjectOfType<AudioManager>();
        gameLevels = FindObjectOfType<LevelManager>();
        gameCollisions = FindObjectOfType<CollisionHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NoClipSwitch = false;
        isDead = false;
        
        rocketRigidbody = GetComponent<Rigidbody>();

        gameAudio.StopEngineAudio();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            ProcessThrust();
            ProcessRotation();

        }
        else if (isDead)
        {
            gameAudio.StopEngineAudio();
            mainEnginePE.Play();
            leftSidePE.Play();
            rightSidePE.Play();

        }

        CheatKeys();
    }



    void ProcessThrust()
    {
        // Pressing Keys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();

        }
        // if not pressing key or isDead
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        // nếu k ấn W và đang có âm thanh Engine thì tắt tiếng engine
        gameAudio.StopEngineAudio();

        // Dừng PE lại khi nhả phím 
        mainEnginePE.Stop();
    }

    private void StartThrusting()
    {
        // di chuyển
        rocketRigidbody.AddRelativeForce(Vector3.up * moveAmount, ForceMode.Force);

        // bật Particle Effects
        if (!mainEnginePE.isPlaying)
        {
            mainEnginePE.Play();
        }

        // khi ấn W và k có âm thanh engine thì sẽ bật tiếng
        gameAudio.PlayEngineAudio();


    }

    void ProcessRotation()
    {
        RotateRight();

        RotateLeft();

    }

    private void RotateLeft()
    {
        // Rotate on the left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rocketRigidbody.AddTorque(Vector3.forward * -rotationAmount, ForceMode.Force);

            if (!rightSidePE.isPlaying)
            {
                rightSidePE.Play();

            }
        }
        else
        //if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.LeftArrow))
        {
            rightSidePE.Stop();
        }
    }

    private void RotateRight()
    {
        // Rotate on the right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Xử lý quay phải
            rocketRigidbody.AddTorque(Vector3.forward * rotationAmount, ForceMode.Force);

            // Bật Particle Effects bên trái (đẩy sang phải) nếu PE đang k bật
            if (!leftSidePE.isPlaying)
            {
                leftSidePE.Play();

            }
        }
        else
        //if (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.RightArrow))
        {
            leftSidePE.Stop();
        }
    }

    void CheatKeys()
    {
        // if press L, auto win level
        if (Input.GetKey(KeyCode.L))
        {
            gameLevels.StartNextLevel();

            gameAudio.PlaySuccessAudio();

            ActivateSuccessPE();
            return;
        }
        // if press P, disable collisions (using foreach)
        if (Input.GetKeyDown(KeyCode.P))
        {
            NoClipSwitch = !NoClipSwitch;

            gameCollisions.SetGodMode(NoClipSwitch);

            Debug.Log("GOD MODE IS: " + NoClipSwitch);
        }
    }

    

    public void ActivateSuccessPE()
    {
        successPE.Play();
    }

    public void ActivateExplosionPE()
    {
        explosionPE.Play();
    }

    // Getter Setter
    public bool GetIsDeadStatus()
    {
        return isDead;
    }

    public void SetIsDeadStatus(bool isDeadValue)
    {
        isDead = isDeadValue;
    }

    public bool GetIsSuccessStatus()
    {
        return isSuccess;
    }

    public void SetIsSuccess(bool isSuccessValue)
    {
        isSuccess = isSuccessValue;
    }

}
