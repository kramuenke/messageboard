using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Project
{
  public string Name { get; set; } = string.Empty;
  public List<Posting> Postings { get; set; } = new List<Posting>();
}

class Posting {
  public string Author { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}

class MessageBoardData
{
  public Dictionary<string, Project> Projects { get; set; } = new Dictionary<string, Project>();
  public Dictionary<string, List<string>> Followers { get; set; } = new Dictionary<string, List<string>>();
}

class ProjectRepository
{
  private MessageBoardData messageBoard = new MessageBoardData();

  public void Post(string projectName, Posting posting)
  {
    if (!messageBoard.Projects.ContainsKey(projectName))
    {
      messageBoard.Projects[projectName] = new Project { Name = projectName };
    }
    messageBoard.Projects[projectName].Postings.Add(posting);

    Save();
  }

  public void Follow(string follower, string projectName)
  {
    if (!messageBoard.Followers.ContainsKey(follower))
    {
      messageBoard.Followers[follower] = new List<string>();
    }
    if (!messageBoard.Followers[follower].Contains(projectName))
    {
      messageBoard.Followers[follower].Add(projectName);
    }

    Save();
  }

  public IEnumerable<Posting> Read(string projectName)
  {
    if (messageBoard.Projects.ContainsKey(projectName))
    {
      return messageBoard.Projects[projectName].Postings;
    }
    return new List<Posting>();
  }

  public IEnumerable<Project> Wall(string follower)
  {
    if (messageBoard.Followers.ContainsKey(follower))
    {
      return messageBoard.Followers[follower]
        .Select(projectName => messageBoard.Projects.GetValueOrDefault(projectName))
        .Where(project => project != null);
    }
    return Enumerable.Empty<Project>();
  }

  private void Save()
  {
    string json = JsonSerializer.Serialize(messageBoard, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("messageboard.json", json);
  }

  public void Load()
  {
    if (File.Exists("messageboard.json"))
    {
      string json = File.ReadAllText("messageboard.json");
      messageBoard = JsonSerializer.Deserialize<MessageBoardData>(json) ?? new MessageBoardData();
    }
  }
}