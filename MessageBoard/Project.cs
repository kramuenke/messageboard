using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Project
{
  public string Name { get; set; } = string.Empty;
  public List<Posting> Postings { get; set; } = new List<Posting>();
  public List<string> Followers { get; set; } = new List<string>();
}

class ProjectRepository
{
  private Dictionary<string, Project> projects = new Dictionary<string, Project>();

  public void Post(string projectName, Posting posting)
  {
    if (!projects.ContainsKey(projectName))
    {
      projects[projectName] = new Project { Name = projectName };
    }
    projects[projectName].Postings.Add(posting);

    Save();
  }

  public void Follow(string projectName, string follower)
  {
    if (projects.ContainsKey(projectName))
    {
      if (!projects[projectName].Followers.Contains(follower))
      {
        projects[projectName].Followers.Add(follower);
        Save();
      }
    }
  }

  public IEnumerable<Posting> Read(string projectName)
  {
    if (projects.ContainsKey(projectName))
    {
      return projects[projectName].Postings;
    }
    return new List<Posting>();
  }

  private void Save()
  {
    string json = JsonSerializer.Serialize(projects, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("projects.json", json);
  }

  public void Load()
  {
    if (File.Exists("projects.json"))
    {
      string json = File.ReadAllText("projects.json");
      projects = JsonSerializer.Deserialize<Dictionary<string, Project>>(json) ?? new Dictionary<string, Project>();
    }
  }
}