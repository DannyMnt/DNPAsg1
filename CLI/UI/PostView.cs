using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class PostView
{
    private readonly IPostRepository postRepository;

    public PostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ManagePosts()
    {
        Console.WriteLine("1. Add post");
        Console.WriteLine("2. Update post");
        Console.WriteLine("3. Delete post");
        Console.WriteLine("4. Show posts");
        Console.WriteLine("5. Show specific post");
        Console.WriteLine("6. Abort");
        
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Title: ");
                string? title = Console.ReadLine();
                Console.WriteLine("Body: ");
                string? body = Console.ReadLine();
                Console.WriteLine("User Id: ");
                int userId = Convert.ToInt32(Console.ReadLine());
                
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
                {
                    Console.WriteLine("Invalid title or body");
                    return;
                }
                await postRepository.AddAsync(new Post(title, body, userId));
                Console.WriteLine("Post added");
                break;
            case "2":
                Console.WriteLine("ID: ");
                string? idToUpdate = Console.ReadLine();
                Post postToUpdate = await postRepository.getSingleAsync(Convert.ToInt32(idToUpdate));
                if (postToUpdate != null)
                {
                    Console.WriteLine("New title: ");
                    string titleToUpdate = Console.ReadLine()!;
                    Console.WriteLine("New body: ");
                    string bodyToUpdate = Console.ReadLine()!;
                    await postRepository.UpdateAsync(new Post(titleToUpdate, bodyToUpdate, postToUpdate.UserId));
                    Console.WriteLine("Post updated");
                }
                else {
                    Console.WriteLine("Invalid ID");
                }
                break;
            case "3":
                Console.WriteLine("ID: ");
                string? idToDelete = Console.ReadLine();
                Post postToDelete = await postRepository.getSingleAsync(Convert.ToInt32(idToDelete));
                if(postToDelete != null){
                    await postRepository.DeleteAsync(Convert.ToInt32(idToDelete));
                    Console.WriteLine("Post deleted");
                }
                else
                    Console.WriteLine("Invalid ID");
                break; 
            case "4":
                var posts = postRepository.getMany();
                foreach (var post in posts)
                    Console.WriteLine($"ID: {post.Id}, Title: {post.Title} , Body: {post.Body} , User Id: {post.UserId}");
                break;
            case "5":
                Console.WriteLine("ID: ");
                string? idToShow = Console.ReadLine();
                Post postToShow = await postRepository.getSingleAsync(Convert.ToInt32(idToShow));
                Console.WriteLine($"ID: {postToShow.Id}, Title: {postToShow.Title} , Body: {postToShow.Body}, User Id: {postToShow.UserId}");
                break;
            case "6":
                break;
        }
    }
}