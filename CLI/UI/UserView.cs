using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class UserView
{
    private readonly IUserRepository userRepository;

    public UserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task Init()
    {
        await CreateDummyData();
    }

    private async Task<User> AddUserAsync(string name, string password)
    {
        User user = new User(name, password);
        User created = await userRepository.AddAsync(user);
        return created;
    }
    
    private async Task CreateDummyData()
    {
        await AddUserAsync("Danny", "Password123");
        await AddUserAsync("Bob", "Password1234");
        await AddUserAsync("Steve", "Password12345");
    }

    public async Task ManageUsers()
    {
        Console.WriteLine("1. Add user");
        Console.WriteLine("2. Update user");
        Console.WriteLine("3. Delete user");
        Console.WriteLine("4. Show users");
        Console.WriteLine("5. Abort");
        
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Username: ");
                string? username = Console.ReadLine();
                Console.WriteLine("Password: ");
                string? password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Invalid username or password");
                    return;
                }
                
                await userRepository.AddAsync(new User(username, password));
                Console.WriteLine("User added");
                break;
            case "2":
                Console.WriteLine("ID: ");
                string? idToUpdate = Console.ReadLine();
                User userToUpdate = await userRepository.getSingleAsync(Convert.ToInt32(idToUpdate));
                if (userToUpdate != null)
                {
                    Console.WriteLine("New username: ");
                    string usernameToUpdate = Console.ReadLine()!;
                    Console.WriteLine("New password: ");
                    string passwordToUpdate = Console.ReadLine()!;
                    await userRepository.UpdateAsync(new User(usernameToUpdate, passwordToUpdate));
                    Console.WriteLine("User updated");
                }
                else {
                    Console.WriteLine("Invalid ID");
                }

                break;
            case "3":
                Console.WriteLine("ID: ");
                string? idToDelete = Console.ReadLine();
                User userToDelete = await userRepository.getSingleAsync(Convert.ToInt32(idToDelete));
                if(userToDelete != null){
                    await userRepository.DeleteAsync(Convert.ToInt32(idToDelete));
                    Console.WriteLine("User deleted");
                }
                else
                    Console.WriteLine("Invalid ID");
                break; 
            case "4":
                var users = userRepository.getMany();
                foreach (var user in users)
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Username}");
                break;
            case "5":
                break;
        }
    }
}