using UnityEngine;

public class Job : MonoBehaviour
{
    public enum JobType
    {
        Farmer,
        Lumberjack,
        Miner,
        Builder,
        None
    }

    public JobType jobType = new JobType();

    private GameManager gm = GameManager.Instance;
    private int gainPerPerson = 2; // How much this particular job produces

    public void Work()
    {
        switch (jobType)
        {
            case JobType.Farmer:
                gm.ressources[2] += gainPerPerson;
                break;

            case JobType.Lumberjack:
                gm.ressources[0] += gainPerPerson;
                break;

            case JobType.Miner:
                gm.ressources[1] += gainPerPerson;
                break;

            case JobType.Builder:
            // WE NEED TO BUILD THE HOUSE IN LEGO CITY
                break;

            default:
                break;
        }
    }
}