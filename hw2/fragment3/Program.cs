/* ORIGINAL CODE:

class fighter
{
    private int iDamage; 
    public string sName;

    public int fighterHealth { get; set; }
    public int fighterDamage { get; set; }
    public int Weapon_Status { get; set; }

    void logStatus(string name, int age, int health, int damage, int weaponStatus)
    {
        Console.WriteLine($"name:{name}, age:{age}, health:{health}, damage:{damage}, weaponStatus:{ weaponStatus}"); 
    }

    public int GetDamage()
    {
        // Weapon_Status * 5
        // Console.WriteLine($"Get Damage {iDamage}"); 
        return iDamage;
    }

    void atck()
    {
        Console.WriteLine("Go Attack!");
        // TO DO: implement attack 
    }

    public void Attack()
    {
        try
        {
            atck();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Go Attack Exception: {e}");
            throw e;
        }
    }
}
*/

class Fighter
{

    public string Name { get; set; }
    public string Age { get; set; }

    public int Health { get; set; }
    public int Damage { get; private set; }
    public int WeaponStatus { get; set; }

    public Fighter(string name, int age, int health, int damage, int weaponStatus)
    {
        Name = name;
        Age = age;
        Health = health;
        Damage = damage;
        WeaponStatus = weaponStatus;
    }

    public void LogStatus()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Health: {Health}, Damage: {Damage}, WeaponStatus: {WeaponStatus}");
    }

    public int CalculateDamage()
    {
        Damage = WeaponStatus * 5;
        return Damage;
    }

    private void PerformAttack()
    {
        Console.WriteLine("Go Attack!");
        // TODO: Implement attack logic
    }

    public void Attack()
    {
        try
        {
            PerformAttack();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attack failed: {ex.Message}");
            throw;
        }
    }
}
