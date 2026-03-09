using SocialMediaPlatform.Reddit.Core;

var config = new AppConfig("config.txt");
var controller = config.GetController();

Console.WriteLine("═══════════════════════════════════");
Console.WriteLine("   SocialMediaPlatform - Reddit");
Console.WriteLine("═══════════════════════════════════");

while (true)
{
    Console.WriteLine("\n── Main menu ──");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    Console.Write("Choices: ");

    var choice = Console.ReadLine();

    if (choice == "1") Register(controller);
    else if (choice == "2") Login(controller);
    else if (choice == "3") { Console.WriteLine("Exited."); break; }
    else Console.WriteLine("Invalid choice.");
}

static void Register(Controller controller)
{
    Console.Write("Username: ");
    var username = Console.ReadLine()!;
    Console.Write("Email: ");
    var email = Console.ReadLine()!;
    Console.Write("Password: ");
    var password = Console.ReadLine()!;

    try
    {
        controller.Register(username, email, password);
        Console.WriteLine($"Registration successful. Welcome to Reddit, {username}!");
        MainMenu(controller);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void Login(Controller controller)
{
    Console.Write("Username: ");
    var username = Console.ReadLine()!;
    Console.Write("Password: ");
    var password = Console.ReadLine()!;

    try
    {
        controller.Login(username, password);
        Console.WriteLine($"Logged in. Welcome back, {username}!");
        MainMenu(controller);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void MainMenu(Controller controller)
{
    while (true)
    {
        Console.WriteLine("\n── Main menu ──");
        Console.WriteLine("1. Create a Post");
        Console.WriteLine("2. See Timeline");
        Console.WriteLine("3. Create a Subreddit");
        Console.WriteLine("4. Join a Subreddit");
        Console.WriteLine("5. Write a Comment");
        Console.WriteLine("6. Write a Reply");
        Console.WriteLine("0. Logout");
        Console.Write("Choice: ");

        var choice = Console.ReadLine();

        if (choice == "1") CreatePost(controller);
        else if (choice == "2") ViewTimeline(controller);
        else if (choice == "3") CreateSubreddit(controller);
        else if (choice == "4") JoinSubreddit(controller);
        else if (choice == "5") AddComment(controller);
        else if (choice == "6") AddReply(controller);
        else if (choice == "0") { controller.Logout(); break; }
        else Console.WriteLine("Invalid choice.");
    }
}

static void CreatePost(Controller controller)
{
    Console.WriteLine("1. Timeline Post");
    Console.WriteLine("2. Subreddit Post");
    Console.Write("Choice: ");
    var choice = Console.ReadLine();

    Console.Write("Title: ");
    var title = Console.ReadLine()!;
    Console.Write("Content: ");
    var content = Console.ReadLine()!;

    try
    {
        if (choice == "1")
        {
            var post = controller.CreatePost(SocialMediaPlatform.Reddit.Core.Enum.PostType.Timeline, content);
            Console.WriteLine($"Post created succesfully. ID: {post.Id.Value}");
        }
        else if (choice == "2")
        {
            Console.Write("Subreddit ID: ");
            var subredditId = Console.ReadLine()!;
            var post = controller.CreatePost(SocialMediaPlatform.Reddit.Core.Enum.PostType.Subreddit, content);
            Console.WriteLine($"Post created succesfully. ID: {post.Id.Value}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void ViewTimeline(Controller controller)
{
    try
    {
        var posts = controller.GetTimeline();
        if (posts.Count == 0)
        {
            Console.WriteLine("Post not found.");
            return;
        }
        Console.WriteLine("\n── Timeline ──");
        foreach (var post in posts)
            Console.WriteLine($"[{post.Id.Value}] {post.Title ?? "No title"} | {post.CreatedAt:yyyy-MM-dd HH:mm}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void CreateSubreddit(Controller controller)
{
    Console.Write("Name (r/...): ");
    var name = Console.ReadLine()!;
    Console.Write("Description: ");
    var description = Console.ReadLine()!;

    try
    {
        var group = controller.CreateGroup(name, description);
        Console.WriteLine($"Subreddit created succesfully. ID: {group.Id.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void JoinSubreddit(Controller controller)
{
    Console.Write("Subreddit ID: ");
    var input = Console.ReadLine()!;

    try
    {
        var groupId = new SocialMediaPlatform.Core.Domain.ID.GroupId { Value = uint.Parse(input) };
        controller.JoinGroup(groupId);
        Console.WriteLine("Joined succesfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void AddComment(Controller controller)
{
    Console.Write("Post ID: ");
    var input = Console.ReadLine()!;
    Console.Write("Content: ");
    var content = Console.ReadLine()!;

    try
    {
        var postId = new SocialMediaPlatform.Core.Domain.ID.PostId { Value = uint.Parse(input) };
        var comment = controller.AddComment(postId, content);
        Console.WriteLine($"Comment created succesfully. ID: {comment.Id.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

static void AddReply(Controller controller)
{
    Console.Write("Comment ID: ");
    var input = Console.ReadLine()!;
    Console.Write("Content: ");
    var content = Console.ReadLine()!;
    try
    {
        var commentId = new SocialMediaPlatform.Core.Domain.ID.CommentId { Value = uint.Parse(input) };
        var comment = controller.ReplyToComment(commentId, content);
        Console.WriteLine($"Reply created succesfully. ID: {comment.Id.Value}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}