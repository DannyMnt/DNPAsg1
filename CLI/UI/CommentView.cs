using Entities;
using RepositoryContracts;

namespace CLI.UI;

public class CommentView
{
    private readonly ICommentRepository commentRepository;

    public CommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ManageComments()
    {
        Console.WriteLine("1. Add comment");
        Console.WriteLine("2. Update comment");
        Console.WriteLine("3. Delete comment");
        Console.WriteLine("4. Show comments");
        Console.WriteLine("5. Abort");
        
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.WriteLine("Body: ");
                string? body = Console.ReadLine();
                Console.WriteLine("Post ID: ");
                string? postId = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(body) || string.IsNullOrWhiteSpace(postId))
                {
                    Console.WriteLine("Invalid body or post id");
                    return;
                }
                
                await commentRepository.AddAsync(new Comment(1, body, Convert.ToInt32(postId)));
                Console.WriteLine("Comment added");
                break;
            case "2":
                Console.WriteLine("ID: ");
                string? idToUpdate = Console.ReadLine();
                Comment commentToUpdate = await commentRepository.getSingleAsync(Convert.ToInt32(idToUpdate));
                if (commentToUpdate != null)
                {
                    Console.WriteLine("New body: ");
                    string bodyToUpdate = Console.ReadLine()!;
                    await commentRepository.UpdateAsync(new Comment(commentToUpdate.Id, bodyToUpdate, 
                        commentToUpdate.PostId));
                    Console.WriteLine("Comment updated");
                }
                else {
                    Console.WriteLine("Invalid ID");
                }

                break;
            case "3":
                Console.WriteLine("ID: ");
                string? idToDelete = Console.ReadLine();
                Comment commentToDelete = await commentRepository.getSingleAsync(Convert.ToInt32(idToDelete));
                if(commentToDelete != null){
                    await commentRepository.DeleteAsync(Convert.ToInt32(idToDelete));
                    Console.WriteLine("Comment deleted");
                }
                else
                    Console.WriteLine("Invalid ID");
                break; 
            case "4":
                var comments = commentRepository.getMany();
                foreach (var comment in comments)
                    Console.WriteLine($"ID: {comment.Id}, Body: {comment.Body}, Post: {comment.PostId}");
                break;
            case "5":
                break;
        }
    }
}