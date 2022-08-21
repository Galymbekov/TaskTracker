using Microsoft.AspNetCore.Mvc;
using static TaskTracker.Startup;

namespace TaskTracker.Controllers;

[Route("task/]")]
public class TaskController : Controller
{
    ITaskRepository TaskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        TaskRepository = taskRepository;
    }

    [HttpGet(Name = "GetAllTasks")]
    public IEnumerable<Task> Get()
    {
        return (IEnumerable<Task>)TaskRepository.Get();
    }

    [HttpGet("{id}", Name = "GetTask")]
    public IActionResult Get(int Id)
    {
        Task task = (Task)Get(Id);

        if (task == null)
        {
            return NotFound();
        }
        return new ObjectResult(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Task task)
    {
        if (task == null)
        {
            return BadRequest();
        }

        TaskRepository.Create(task);
        return CreatedAtRoute("GetTask", new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int Id, [FromBody] Task updateTask)
    {
        if (updateTask.Id != Id)
        {
            return BadRequest();
        }

        var task = TaskRepository.Get(Id);
        if (task == null)
        {
            return NotFound();
        }

        TaskRepository.Update(updateTask);
        return RedirectToRoute("GetAllTasks");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int Id)
    {
        if (Id == null)
        {
            return BadRequest();
        }
        var deleteTask = TaskRepository.Delete(Id);

        return new ObjectResult(deleteTask);
    }
}
