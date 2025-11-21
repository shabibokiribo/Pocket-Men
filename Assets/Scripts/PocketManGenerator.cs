using UnityEngine;

public class PocketManGenerator : MonoBehaviour
{
    public static PocketManGenerator Instance;

    [Header("Data Sources")]
    public PocketMan[] pocketManTypes; // drag in all your PocketMan ScriptableObjects

    // Full first name list
    public string[] firstNames = {
    "Tom", "Rick", "Jerry", "Karl", "Steve", "Honda", "Mario", "Chris", "Dean", "Andrew", "Bill", "Ronald", "Clay", "Giovanni", "Kent", "Clark", "Tim", "Robert", "Luis", "Louis", "Lindsay", "Paul", "Jonathan", "John",
    "George", "Dylan", "Jarvis", "Christian", "Tristan", "Chad", "Chaz", "Steven", "Ronnie", "Shane", "Chance", "Damien", "Gianmarco", "Sam", "Samuel", "Jorge", "Abraham", "Nick", "Richard", "DeAndre", "Guillermo", "Frank",
    "Mark", "Francios", "Hank", "David", "Dave", "Malcolm", "Micah", "Moses", "Jon", "Joe", "Joseph", "Ben", "Derean", "Malachi", "Brandom", "Brenden", "Zachary", "Zack", "Ncuti", "Jim", "Aaron", "Ian", "Ed", "Michael", "Mike", "Mikey",
    "DJ", "Evan", "Elijah", "Jason", "Tod", "Kris", "Rodney", "Rod", "Dante", "Jeremy", "Griffin", "Dan", "Danny", "Ethan", "Francisco", "Florence", "Valentino", "Jared", "Benny", "Varian", "Vincent", "Vinnie", "Grayson", "Ace", "CJ", "Cash", "Elliot",
    "Omar", "Muhammad", "Ahmad", "Trent", "Flynn", "Ryder", "Fischer", "Glenn", "Brent", "Brant", "Kai", "Kit", "Ken", "Rin", "Shaquille", "Jordan", "Rhys", "Reese", "Jebidiah", "Hassan", "Sean", "Cody", "Kody", "Spencer", "Caine", "Marlo",
    "Angelo", "Arturo", "Tyler", "Noel", "Bjorn", "Levi", "Bryson", "Jaden", "Anthony", "Manuel", "Will", "Corderro", "Zayn", "Justin", "Bruno", "Joey", "Malik", "Cole", "Prince", "Martin", "Sven", "Joaquin", "Jackson", "Forest", "Bert", "Ernie",
    "Dimitri", "Claude", "Klaus", "James", "Jimmy", "Harry", "Ned", "Brian", "Victor", "Lou", "Raphael", "Ralph", "Ross", "Chandler", "Kevin", "Stuart", "Phil", "Trevor", "Rhett", "Link", "Doug", "Ty", "Tyrone", "Jeffery", "Jeff", "Toby", "Max",
    "Maxwell", "Curtis", "Connor", "Duncan", "Saji", "Luka", "Dennis", "Luke", "Jack", "Marc", "Luigi", "Brock", "Kenny", "Josh", "Winston", "Alvin", "Arthur", "Alejandro", "Amir", "Elmer", "Hayden", "Xavier", "Jacob", "Jay", "Matt", "Charles", "Charlie",
    "Payne", "Kendrick", "Patrick", "Pat", "Xander", "Oliver", "Theo", "Theodore", "Liam", "Henry", "Ezra", "Benjamin", "Sebastian", "Daniel", "Rowan", "Adrian", "August", "Mateo", "Keith", "Killian", "Abel", "Archibald", "Andre", "Logan", "Lenny",
    "Lionel", "Marcel", "Matthew", "Carl", "Milo", "Colton", "Nate", "Calvin", "Colin", "Otis", "Perry", "Porter", "Quincy", "Derek", "Edmund", "Floyd", "Vaughn", "Wyatt", "Jesse", "Jamal", "Hikaru", "Dominic", "Magnus", "Julian", "Phillip", "Simon", "Enrique",
    "Eli", "Gabriel", "Silas", "Malik", "Asher", "Tobias", "Lucian", "Emmett", "Harrison", "Finn", "Orion", "Danteo", "Soren", "Cassius", "Leander"

};


    // Full last name list
    public string[] lastNames = {
    "Johnson", "Bennett", "Frost", "Michaels", "Gibson", "Patterson", "Kojima", "Brown", "Hucherson", "Smith", "Thomas", "Thompson", "Bowe", "Peck", "Paul", "Withers", "Taylor", "Pasillas", "Powell", "Woods", "Stone",
    "Ahgren", "Lovell", "Foucault", "Sims", "Simmons", "Simon", "Parker", "Child", "Mann", "Sweeney", "Sinclair", "Huntington III", "Luther", "Morgan", "Morgan Jr.", "Johnson Jr.", "Thatcher", "McCafferty", "Gonzales", "Hyunh", "Nguyen", "Xu", "Mason", "Davis",
    "Davis Jr.", "Chavez", "Ashford", "Brown II", "Rodriguez", "Wilson", "Moore", "Lopez", "West", "Nwadiwe", "Hernandez", "Hernandez-Garcia", "Garcia", "Anderson", "Jones", "Jones-Wilson", "Williams", "Linker", "Bell", "Bailey", "Armas", "Craft", "Wells",
    "Jacobson", "Christiansen", "Ayodele", "Bates", "Bates Jr.", "Beaudry", "Hughes", "Hughes-Thomas", "Chaney", "Murphy", "Cross", "Daniels", "Elliot", "Conigliaro", "Frazier", "Fuller", "Muller", "Hatch", "Garraghty", "North", "Kaufman", "Hansen", "Jamison",
    "Jamison-Wells", "Otuonye", "Vassallo", "Vasallo-Pasillas", "Krieger", "Castillo", "McClay", "Scott", "Kelly", "Kennedy", "Jackson", "Parks", "Reyes", "Reynolds", "Woodard", "Reade", "Topp", "Conner", "Connor-Bates", "Goode", "Bundy", "Garrison",
    "Chen", "Wang", "Harris", "Harrison", "Harris Jr.", "Chu", "Swan", "Escalante", "Lee", "Elridge", "Schmidt", "Foster", "Mendoza", "Hall", "Brewer", "Cole", "Martin", "Perez", "Clayton", "White", "Ramirez", "Ramirez-Perez", "Young", "Allen",
    "Torres", "Hill", "Green", "Adams", "Washington", "Baker", "Campbell", "Carter", "Mitchell", "Roberts", "Phillips", "Turner", "Montague", "Evans", "Morris", "Cooke", "Reed", "Ramos", "Kim", "Cox", "Todd", "Watson", "Price",
    "Wood", "James", "Sanders", "Myers", "Long", "Arnold", "Jenkins", "Perry", "Russell", "Butler", "Graham", "Wallace", "South", "East", "Herrera", "Medina", "Marshall", "Ford", "Henry", "Freeman", "Tucker", "Guzman", "Crawford",
    "Simpson", "Olson", "Porter", "Miranda", "Gordon", "Shaw", "Snyder", "Hunt", "Hicks", "Boyd", "Salazar", "Warren-Wilson", "Anthony Jr.", "Hecox", "Ferguson", "Schnider", "Rice", "Soto", "Weaver", "Grismer", "Ryan", "Nichols",
    "Gunn", "Dunn", "Spencer", "Agnew", "Lehan-Canto", "Santos", "Hart", "Cunningham", "Duncan-Arnold", "Knight", "Armstrong", "Armstrong III", "Riley", "Armstrong Jr.", "Delgado", "Perkins", "Hoffman", "Hoffman Jr.", "Berry", "Matthews", "Ray", "Carpenter",
    "Sandoval", "Chapman", "Wheeler", "Burke", "Larson", "Larson-Greene", "Franklin", "Jacobs", "Lynch", "Moreno", "Vega", "Le", "McCoy", "Fields", "Padilla", "Walsh Jr.", "Ali", "Ahmad", "Abdallah", "Rafiq", "Abaza", "Aamir", "Muhammad", "Johns", "Abdul",
    "Ahmed", "Sharma", "Gupta", "Singh", "Kumar", "Patel"
};


    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public PMInst GenerateRandomPocketMan(int minLevel = 1, int maxLevel = 5, string[] overrideFirstNames = null, string[] overrideLastNames = null)
    {
        if (pocketManTypes.Length == 0)
        {
            Debug.LogWarning("No PocketMan types assigned to generator!");
            return null;
        }

        PocketMan type = pocketManTypes[Random.Range(0, pocketManTypes.Length)];

        PMInst p = new PMInst();
        p.baseData = type;
        

        // Names: city overrides take priority
        string[] fNames = (overrideFirstNames != null && overrideFirstNames.Length > 0) ? overrideFirstNames : firstNames;
        string[] lNames = (overrideLastNames != null && overrideLastNames.Length > 0) ? overrideLastNames : lastNames;

        p.firstName = fNames[Random.Range(0, fNames.Length)];
        p.lastName = lNames[Random.Range(0, lNames.Length)];

        // Level
        p.level = Random.Range(minLevel, maxLevel + 1);

        // Stats
        p.health = Random.Range(type.minHealth, type.maxHealth + 1);
        p.attack = Random.Range(type.minAttack, type.maxAttack + 1);
        p.defense = Random.Range(type.minDefense, type.maxDefense + 1);

        // Moves (up to 2)
        int moveCount = Mathf.Min(2, type.possibleMoves.Length);
        p.moves = new string[moveCount];
        for (int i = 0; i < moveCount; i++)
        {
            string move;
            int safety = 0;
            do
            {
                move = type.possibleMoves[Random.Range(0, type.possibleMoves.Length)];
                safety++;
            } while (System.Array.Exists(p.moves, m => m == move) && safety < 10);

            p.moves[i] = move;
        }

        return p;
    }
}
