class Project
{
  public string Name { get; set; } = string.Empty;
  public List<Posting> Postings { get; set; } = new List<Posting>();
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
  }

  public IEnumerable<Posting> Read(string projectName)
  {
    if (projects.ContainsKey(projectName))
    {
      return projects[projectName].Postings;
    }
    return new List<Posting>();
  }
}