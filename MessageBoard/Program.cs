class MessageBoard
{
  static void Main(string[] args)
  {
    if (args.Length >= 4 && args[1] == "->")
    {
      var Posting = new Posting
      {
        Author = args[0],
        Message = string.Join(" ", args.Skip(3)),
        CreatedAt = DateTime.Now
      };

      var projectRepository = new ProjectRepository();
      projectRepository.Post(args[2], Posting);

      var postings = projectRepository.Read(args[2]);
      foreach (var posting in postings)
      {
        Console.WriteLine($"{posting.Author}: {posting.Message} ({posting.CreatedAt})");
      }
    }
  }
}