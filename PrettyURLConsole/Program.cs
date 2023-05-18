// See https://aka.ms/new-console-template for more information


//enter long url 

//create a random unique combination 

//return 
//ourdomain/uniquecombination

//store:
//long url, guid, random unique comb, resulted url

//when a call is made to any url that starts with
//ourdomain

//            search in our db to see whwere is the renaodm unique combination
//            fetch the original url 
//            make a http call to the original url 


using PrettyURLConsole;

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Hello!");

Console.WriteLine("Enter a url you want to make pretty");
string longUrl = Console.ReadLine();

Console.WriteLine("Now enter your alias");
string alias = Console.ReadLine();

Console.WriteLine("let's shorten this");

var service = new ShortenService();

var prettyReturned = service.CreateNewPrettyURL(longUrl, alias);

Console.WriteLine(value: $"Here is your Prettier URL: {prettyReturned.PrettyUrl}");

Console.WriteLine("Try to navigate to it");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Note. I have no clue, so plese be mindful");
Console.WriteLine("As I have no idea on how the world would know about PrettyURL and how to redirect any http that has 'prettyurl/' in it");
Console.WriteLine("You'll have try this from here, to see if it works");
Console.WriteLine("Press any key to do so");
Console.ForegroundColor = ConsoleColor.Blue;
Console.ReadLine();

service.GoToURL(prettyReturned.PrettyUrl);

Console.WriteLine("Hooray!");

