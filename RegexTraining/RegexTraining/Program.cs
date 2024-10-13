
using Bogus;

var outputToConsole = false;
var fileName = "data.csv";
var numberOfRows = 10000;
var digitCount = numberOfRows.ToString().Length;

if(!outputToConsole)
{
    File.WriteAllText(fileName, "");
}

Print("Id,Firstname,Surname,Email,Date of birth,Telephone,Manager,Code,External code");

for(int i = 0; i < numberOfRows; i++)
{
    var faker = new Faker();
    var firstName = faker.Name.FirstName();
    var surname = faker.Name.LastName();
    var email = faker.Internet.Email();
    var telephone = faker.Phone.PhoneNumber("### ### ###");
    var code = faker.Random.AlphaNumeric(6);
    var dateOfBirth = faker.Person.DateOfBirth.ToShortDateString();
    var externalCode = faker.Random.AlphaNumeric(8);
    
    var startingStr = "";

    var isCorruptedRow = Random.Shared.Next(0, 20) == 0;

    if (isCorruptedRow)
    {
        startingStr += "//";
    }

    var isCorruptedStartingRow = Random.Shared.Next(0, 10) == 0;

    if (isCorruptedStartingRow)
    {
        var count = Random.Shared.Next(3, 8);
        var characters = faker.Random.Chars('a', 'z', count);
        startingStr = new string(characters);
    }

    var managerEmail = faker.Internet.Email();
    var isCorruptedManager = Random.Shared.Next(0, 16) == 0;
    if (isCorruptedManager)
    {
        var count = Random.Shared.Next(2, 6);
        var atIndex = managerEmail.IndexOf('@');
        var invalidCharacters = faker.Random.Chars('0', '9', count);
        managerEmail = string.Concat(managerEmail.AsSpan(0, atIndex), $".invalid{invalidCharacters.AsSpan()}", managerEmail.AsSpan(atIndex));
    }


    var output = $"{startingStr}{i.ToString().PadLeft(digitCount, '0')},{firstName},{surname},{email},{dateOfBirth},{telephone},{managerEmail},{code},{externalCode}";

    Print(output);
}

Console.WriteLine("Done.");

void Print(string text)
{
    if (outputToConsole)
    {
        Console.WriteLine(text);
    }
    else
    {
        File.AppendAllLines(fileName, [text]);
    }
}

// Tasks
// 1. Remove rows that are begining with "//" - they are corrupted
// 2. Fix Ids not to contain any letters
// 3. Remove external code column
// 4. Remove ".invalid ... @" part from the manager email
// 5. Remove padding by zeros from the Ids
// 6. Fix Date of birth to be in the format yyyy-MM-dd
// 7. Fix Telephone to be in the format +420 1234567890
