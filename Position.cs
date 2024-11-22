using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Position : MonoBehaviour
{
    [SerializeField] float initialSpeed = 6.4f;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] float maxSpeed = 12.0f;
    [SerializeField] float acceleration = 1.5f; // Adjust this value to control the acceleration
    [SerializeField] float rotate = 50.04f;
    [SerializeField] GameObject camera;

    SpriteRenderer sr;
    [SerializeField] Sprite sp;
    Sprite oldCar;



    // .........................
    int packagesPickedUp = 0;
    int packagesDelivered = 0;

    bool isPickupRespawning = false; // Flag to track if a pickup is respawning
    [SerializeField] float pickupRespawnDelay = 2.0f; // Delay before pickup respawn





    private float currentSpeed;
    private bool isColliding = false;

    
void TextInActive()
{

    text1.gameObject.SetActive(false);
}
    void Start()

    {

        sr = GetComponent<SpriteRenderer>();
        currentSpeed = initialSpeed;



        // Initialize pickup count display
        // 
    }

    void Update()
    {
        if (!isColliding && currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        float s = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        float r = Input.GetAxis("Horizontal") * rotate * Time.deltaTime;

        transform.Translate(0, s, 0);
        transform.Rotate(0, 0, r);
        camera.transform.position = transform.position + new Vector3(0, 0, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        currentSpeed = 4.0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
        // Debug.Log("Collision exit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.name == "SpeedUp")
        {
            currentSpeed += 12.0f;
            Destroy(collision.gameObject, 1);

        }

        if (collision.gameObject.name == "SpeedUp1")
        {
            currentSpeed += 12.0f;
            Destroy(collision.gameObject, 1);

        }

        if (collision.gameObject.name == "SpeedUp2")
        {
            currentSpeed += 12.0f;
            Destroy(collision.gameObject, 1);

        }

        if (collision.gameObject.name == "SpeedUp3")
        {
            currentSpeed += 12.0f;
            Destroy(collision.gameObject, 1);

        }

        if (collision.gameObject.name == "ChangeCar")
        {
            oldCar = sr.sprite;
            sr.sprite = sp;
            sr.color = Color.red;
            Destroy(collision.gameObject, 1);
        }
        if (collision.gameObject.name == "oldc")
        {
            sr.sprite = oldCar;
            sr.color = Color.white;
            Destroy(collision.gameObject, 3 );
        }

        if (collision.gameObject.name == "oldc1")
        {
            sr.sprite = oldCar;
            sr.color = Color.white;
            Destroy(collision.gameObject, 1);
        }

      // Check for pickup collisions
        if (collision.gameObject.name == "Pickup" && !isPickupRespawning)
        {
            Debug.Log("Package is picked up!"); // Display pickup message
            text1.gameObject.SetActive(true);
            text1.text="Package is picked up!";   
            Invoke("TextInActive",3f);
            StartCoroutine(RespawnPickup(collision.gameObject)); // Respawn pickup
            packagesPickedUp++; // Increment the number of packages picked up
        }

        // Check for delivery spot collisions
        if (collision.gameObject.name == "DeliverySpot" && packagesPickedUp > 0)
        {
            packagesDelivered+=1;
            Debug.Log("Package " + packagesDelivered+ " is delivered!"); 
            text1.gameObject.SetActive(true);
              text1.text="Package"+" "+packagesDelivered+"  "+"is delivered";

                Invoke("TextInActive",3f);
            // Display delivery message with package number
          //      packagesDelivered++; // Increment the number of packages delivered
            packagesPickedUp = 0; // Reset the number of packages picked up
        }


}
  IEnumerator RespawnPickup(GameObject pickup)
    {
        isPickupRespawning = true;
        yield return new WaitForSeconds(pickupRespawnDelay); // Wait for respawn delay
        pickup.SetActive(true); // Respawn pickup
        isPickupRespawning = false;
    }
}

