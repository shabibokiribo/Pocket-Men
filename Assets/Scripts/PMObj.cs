using UnityEngine;

public class PMObj : MonoBehaviour
{
    public string[] firstNames = { "Tom", "Rick", "Jerry", "Karl", "Steve", "Honda", "Mario", "Chris", "Dean", "Andrew", "Bill", "Ronald", "Clay", "Giovanni", "Kent", "Clark", "Tim", "Robert", "Luis", "Louis", "Lindsay", "Paul", "Jonathan", "John", 
    "George", "Dylan", "Jarvis", "Christian", "Tristan", "Chad", "Chaz", "Steven", "Ronnie", "Shane", "Chance", "Damien", "Gianmarco", "Sam", "Samuel", "Jorge", "George", "Abraham", "Nick", "Rick", "Richard", "DeAndre", "Guillermo", "Frank",
    "Mark", "Francios", "Hank", "David", "Dave", "Malcolm", "Micah", "Moses", "Jon", "Joe", "Joseph", "Ben", "Derean", "Malachi", "Brandom", "Brenden", "Zachary", "Zack", "Ncuti", "Jim", "Aaron", "Ian", "Ed", "Steven", "Michael", "Mike", "Mikey",
    "DJ", "Evan", "Elijah", "Jason", "Tod", "Kris", "Rodney", "Rod", "Dante", "Jeremy", "Griffin", "Dan", "Danny", "Ethan", "Francisco", "Florence", "Valentino", "Jared", "Benny", "Varian", "Vincent", "Vinnie", "Grayson", "Ace", "CJ", "Cash", "Elliot", 
    "Omar", "Muhammad", "Ahmad", "Trent", "Flynn", "Ryder", "Fischer", "Glenn", "Brent", "Brant", "Kai", "Kit", "Ken", "Rin", "Shaquille", "Jordan", "Rhys", "Reese", "Jebidiah", "Hassan", "Paul", "Sean", "Cody", "Kody", "Spencer", "Caine", "Marlo",
    "Angelo", "Arturo", "Tyler", "Noel", "Bjorn", "Levi", "Bryson", "Jaden", "Anthony", "Manuel", "Will", "Corderro", "Zayn", "Justin", "Bruno", "Joey", "Malik","Cole", "Prince", "Martin", "Sven", "Joaquin", "Jackson", "Forest", "Bert", "Ernie", 
    "Dimitri", "Claude", "Klaus", "James", "Jimmy", "Harry", "Ned", "Brian", "Victor", "Lou", "Raphael", "Ralph", "Ross", "Chandler", "Kevin", "Stuart", "Phil", "Trevor", "Rhett", "Link", "Doug", "Ty", "Tyrone", "Jeffery", "Jeff", "Toby", "Max",
    "Maxwell", "Curtis", "Connor", "Duncan", "Saji", "Luka", "Dennis", "Luke", "Jack", "Marc", "Luigi", "Brock", "Kenny", "Josh", "Winston", "Alvin", "Arthur", "Alejandro", "Amir", "Elmer", "Hayden", "Xavier", "Jacob", "Jay", "Matt", "Charles", "Charlie", 
    "Payne", "Kendrick", "Patrick", "Pat", "Ian", "Xander", "Oliver", "Theo", "Theodore", "Liam", "Henry", "Ezra", "Benjamin", "Sebastian", "Daniel", "Rowan", "Adrian", "August", "Mateo", "Keith", "Killian", "Abel", "Archibald", "Andre", "Logan", "Lenny",
    "Lionel", "Marcel", "Matthew", "Carl", "Milo", "Colton", "Nate", "Calvin", "Colin", "Otis", "Perry", "Porter", "Quincy", "Derek", "Edmund", "Floyd", "Vaughn", "Wyatt", "Jesse", "Jamal", "Hikaru", "Dominic", "Magnus", "Julian", "Phillip", "Simon", "Enrique"};
    
    public string[] lastNames = { "Johnson", "Bennett", "Frost", "Michaels", "Gibson", "Patterson", "Kojima", "Brown", "Hucherson", "Smith", "Thomas", "Thompson", "Bowe", "Peck", "Paul", "Withers", "Taylor", "Pasillas", "Powell", "Woods", "Stone",
    "Ahgren", "Lovell", "Foucault", "Sims", "Simmons", "Simon", "Parker", "Child", "Mann", "Sweeney", "Sinclair", "Huntington III", "Luther", "Morgan", "Morgan Jr.", "Johnson Jr.", "Thatcher", "McCafferty", "Gonzales", "Hyunh", "Nguyen", "Xu", "Mason", "Davis",
    "Davis Jr.", "Chavez", "Ashford", "Brown II", "Rodriguez", "Wilson", "Moore", "Lopez", "West", "Nwadiwe", "Hernandez", "Hernandez-Garcia", "Garcia", "Anderson", "Honda", "Jones", "Jones-Wilson", "Williams", "Linker", "Bell", "Bailey", "Armas", "Craft", "Wells"
    , "Jacobson", "Christiansen", "Ayodele", "Bates", "Bates Jr.", "Beaudry", "Hughes", "Hughes-Thomas", "Chaney", "Murphy", "Cross", "Daniels", "Elliot", "Conigliaro", "Frazier", "Fuller", "Muller", "Hatch", "Garraghty", "North", "Kaufman", "hansen", "Jamison",
    "Jamison-Wells", "Otuonye", "Vassallo", "Vasallo-Pasillas", "Krieger", "Castillo", "McClay", "Scott", "Kelly", "Kennedy", "Jackson", "Parks", "Reyes", "Reynolds", "Woodard", "Reade", "Topp", "Conner", "Connor", "Connor-Bates", "Goode", "Bundy", "Garrison",
    "Chen", "Wang", "Harris", "Harrison", "Harris Jr.", "Chu", "Swan", "Escalante", "Lee", "Elridge", "Schmidt", "Moore", "Foster", "Mendoza", "Hall", "Brewer", "Cole", "Lopez", "Martin", "Perez", "Clayton", "White", "Ramirez", "Ramirez-Perez", "Young", "Allen",
    "Torres", "Hill", "Green", "Adams", "Washington", "Baker", "Campbell", "Carter", "Mitchell", "Roberts", "Phillips", "Turner", "Montague", "Evans", "Bailey", "Morris", "Murphy", "Cooke", "Reed", "Kelly", "Ramos", "Kim", "Cox", "Todd", "Watson", "Price",
    "Wood", "James", "Sanders", "Patel", "Myers", "Long", "Arnold", "Jenkins", "Perry", "Russell", "Butler", "Graham", "Wallace", "West", "South", "East", "Herrera", "Gibson", "Medina", "Marshall", "Ford", "Henry", "Freeman", "Tucker", "Guzman", "Crawford",
    "Simpson", "Olson", "Porter", "Miranda", "Manuel", "Gordon", "Shaw", "Snyder", "Mason", "Hunt", "Hicks", "Boyd", "Salazar", "Warren", "Warren-Wilson", "Anthony Jr.", "Hecox", "Ferguson", "Schnider", "Rice", "Soto", "Weaver", "Grismer", "Ryan", "Nichols",
    "Gunn", "Dunn", "Spencer", "Agnew", "Lehan-Canto", "Santos", "Hart", "Elliot", "Cunningham", "Duncan-Arnold", "Knight", "Armstrong", "Armstrong III", "Riley", "Armstrong Jr.", "Delgado", "Perkins", "Hoffman", "Hoffman Jr.", "Berry", "Matthews", "Ray", "Carpenter",
    "Sandoval", "Chapman", "Wheeler", "Burke", "Larson", "Larson-Greene", "Franklin", "Jacobs", "Lynch", "Moreno", "Vega", "Le", "McCoy", "Fields", "Padilla", "Walsh Jr.", "Ali", "Ahmad", "Abdallah", "Rafiq", "Abaza", "Aamir", "Muhammad", "Johns", "Abdul",
    "Ahmed", "Sharma", "Gupta", "Singh", "Kumar", "Patel"};

    public string firstName;
    public string lastName;

    public PocketManData type;

    public int health;
    public int attack;
    public int defense;

    public string[] moves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
