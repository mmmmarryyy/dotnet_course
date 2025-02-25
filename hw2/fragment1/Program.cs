/* ORIGINAL CODE:
class DataOrg
{

    public string name; 
    public int age, score;

    int nameLen;

    public string[] getStmt()
    {
        if (name != null)
        {
            string[] row = new string[3]; 
            row[0] = name;
            row[1] = $"{age * 0.83}"; 
            row[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return row;
        }
        return null; 
    }

    public int calcNamlen()
    {
        if (name == null)
        {
            return -1;
        } 
        else
        {
            if (name.Length < 3)
                return 0;
            if (age < 18 || age > 65) 
                return 0;
            if (score == -1) 
                return 0;
            nameLen = name.Length * 4;
            return 0; 
        }
    }

    public void SetValue(string name, string value)
    {
        if (name.Equals("age"))
        {
            age = Int32.Parse(value);
            return; 
        }
        if (name.Equals("score"))
        {
            score = Int32.Parse(value);
            return; 
        }
    } 
}
*/

using System;

class DataOrg
{
    private const int MinAge = 18;
    private const int MaxAge = 65;
    private const int InvalidScore = -1;
    private const int NameLengthMultiplier = 4;
    private const double AgeProportionalityCoefficient = 0.83;

    public string Name { get; private set; }
    public int Age { get; private set; }
    public int Score { get; private set; }
    public int NameLength { get; private set; }

    public string[] GetStatement()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return null;
        }

        return new string[]
        {
            Name,
            $"{Age * AgeProportionalityCoefficient}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
    }

    public int CalculateNameLength()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return -1;
        }

        if (Name.Length < 3 || Age < MinAge || Age > MaxAge || Score == InvalidScore)
        {
            return 0;
        }

        NameLength = Name.Length * NameLengthMultiplier;
        return NameLength;
    }

    public void SetAge(int ageValue)
    {
        Age = ageValue;
    }

    public void SetScore(int scoreValue)
    {
        Score = scoreValue;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}
