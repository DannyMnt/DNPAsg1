using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }
    
    public async Task StartAsync()
    {
        Console.WriteLine("CliApp started");
        var userView = new UserView(_userRepository);
        var postView = new PostView(_postRepository);
        var commentView = new CommentView(_commentRepository);
        
        await userView.Init();

        string? input;
        do
        {
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("4. Exit");

            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await userView.ManageUsers();
                    break;
                case "2":
                    await postView.ManagePosts();
                    break;
                case "3":
                    await commentView.ManageComments();
                    break;
            }
        } while (input != "4");
    }
}