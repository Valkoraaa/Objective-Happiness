using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour {

	[Header("Character occupations")]
	public Job job;
	public Job school;
    public House house;

	private GameManager gm;

	[SerializeField] private int foodConsumption = 1; // The needed amount of food to survive

    public bool isTired = false;

    // Initialises game character by passing the game objects and accessing their scripts
    public void Init() // Newborn function, doesn't take any arguments
    {
        job = null;
        house = null;
        school = null;
        gm = GameManager.Instance; // Reference to the game manager
    }
    public void Init(GameObject actualJob, GameObject actualHouse) // Receives game objects as arguments, then gets their scripts
    {
        job = actualJob.GetComponent<Job>(); // Applying scripts
        house = actualHouse.GetComponent<House>();
        school = null;
        gm = GameManager.Instance; // Reference to the game manager
    }

    private void Awake()
    {
        GoHouse();
        Debug.Log("Game start!");
    }

    // Cycles through his day: goes to job, exhausts
    public void CycleDay()
    {
        // If isn't tired -> goes to school or to job
        if (house == null) // Doesn't job if he doesn't have a house
        {
            isTired = true;
            gm.prosperity -= 1;
        }
        else GoHouse(); //va a sa maison

        if (gm.food > 0) gm.food -= foodConsumption;
        else Die();

        if (!isTired) GoWork();
    }

	public void Die()
    {
		// Character dies
		// TODO: plays animation and despawns
        gm.prosperity -= 1;
		Destroy(gameObject); // Self destruction
    }

    public void GoWork()
    {
        transform.position = Vector3.MoveTowards(transform.position, job.transform.position, 2 * Time.deltaTime);
        house.isTaken = true;
        job.Work();
        // Work for some time

        isTired = true; // Gets tired after work
    }
    public void GoHouse()
    {
        transform.position = Vector3.MoveTowards(transform.position, house.transform.position, 2 * Time.deltaTime);
        // Sleeps for some time
        gm.food -= foodConsumption; // Eats
        isTired = false; // Rests and gets energized
    }
}