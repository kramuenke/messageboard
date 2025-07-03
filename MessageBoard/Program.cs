class MessageBoard
{
  static void Main(string[] args)
  {
    var projectRepository = new ProjectRepository();
    projectRepository.Load();

    switch (args.Length > 1 ? args[1] : "")
    {
      case "->":
        if (args.Length >= 4 && args[2].StartsWith("@"))
        {
          var posting = new Posting
          {
            Author = args[0],
            Message = string.Join(" ", args.Skip(3)),
            CreatedAt = DateTime.Now
          };

          projectRepository.Post(ProjectName(args[2]), posting);
        }
        break;
      case "following":
        if (args.Length == 3) {
          projectRepository.Follow(args[0], args[2]);
        }
        break;
      case "wall":
        break;
      default:
        var postings = projectRepository.Read(args[0]);
        foreach (var posting in postings)
        {
          Console.WriteLine($"{posting.Author}:");
          var timeAgo = DateTime.Now - posting.CreatedAt;
          var minutesAgo = (int)timeAgo.TotalMinutes;
          Console.WriteLine($"{posting.Message} ({minutesAgo} minutes ago)");
        }
        break;
    }
  }

  static string ProjectName(string input)
  {
    return input.Length > 0 ? input.Substring(1) : input;
  }
}