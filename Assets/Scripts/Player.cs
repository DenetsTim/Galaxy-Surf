using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player player;

    private int pos = 0;
    private Rigidbody rb;
    private float start_pos;
    private int speed = 3;
    private bool jumping = false;

    [SerializeField] private Trigger right_trigger;
    [SerializeField] private Trigger left_trigger;

    private bool is_game = false;
    public bool IsGame { get { return is_game; } }
    private int score = 0;
    [SerializeField] private Text score_txt;

    private float last_z = 60;
    [SerializeField] private GameSpawner spawner;

    [SerializeField] private LayerMask layerMask;
    private bool isGrounded = false;
    private float rayDistance = 0.5f;

    private float x_speed = 0;
    private float final_pos = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        start_pos = transform.position.x;
        //Going(false);

        SwipeDetect.SwipeEvent += OnSwipe;

        if (!player)
            player = this;
    }

    public void Fly(int num)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        speed = transform.GetChild(num).GetComponent<ShipStats>().Speed;
        is_game = true;
    }

    private void OnSwipe(Vector2 direction)
    {
        if (direction.x > 0)
            Go(false);
        else if (direction.x < 0)
            Go(true);

        if (direction.y > 0)
            DoJump();
        else if (direction.y < 0)
            Under();
    }

    public void Go(bool isLeft)
    {
        if (isLeft)
        {
            if (pos != -1)
                Going(isLeft);
        }

        else
        {
            if(pos != 1)
                Going(isLeft);
        }
    }

    public void DoJump()
    {
        if(isGrounded && !jumping)
            Jump();
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
        jumping = true;
    }

    private void Update()
    {
        GroundCheck();
    }

    private void FixedUpdate()
    {
        if (is_game)
        {
            float x_velocity = 0;
            if ((transform.position.x + 0.001f >= final_pos * x_speed && x_speed > 0) || (transform.position.x - 0.001f <= final_pos && x_speed < 0))
                { rb.MovePosition(new Vector3(final_pos, transform.position.y, transform.position.z)); x_speed = 0; }
            else
                x_velocity = x_speed > 0 ? x_speed * -5 * (transform.position.x - final_pos) : x_speed * 5 * (transform.position.x - final_pos);

            rb.velocity = new Vector3(x_velocity, rb.velocity.y, speed * 1.5f + score/3000);
            score += speed;
            score_txt.text = "Score: " + score.ToString();
        }
        if(transform.position.z > last_z)
        {
            last_z += 100;
            spawner.Spawn((int)(last_z - 40));
        }
    }

    private void Going(bool isLeft)
    {
        if (!isLeft)
        {
            if (right_trigger.checkPos())
                { x_speed = 1; pos++; final_pos = start_pos + pos * 1.75f; }
        }
        else
        {
            if (left_trigger.checkPos())
                { x_speed = -1; pos--; final_pos = start_pos + pos * 1.75f; }
        }
    }

    private void death()
    {
        if (PlayerPrefs.GetInt("MaxScore", 0) < score)
            PlayerPrefs.SetInt("MaxScore", score);

        SwipeDetect.SwipeEvent -= OnSwipe;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>() || collision.gameObject.GetComponent<Prep>())
            jumping = false;
        if (collision.gameObject.GetComponent<Prep>())
            death();
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down);

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<Ground>() || hit.collider.gameObject.GetComponent<Prep>())
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
                isGrounded = true;
            }
        }
        else
            isGrounded = false;
    }

    private void Under()
    {
        if(jumping)
            rb.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
    }
}
