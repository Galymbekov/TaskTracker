using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using Task = TaskTracker.Models.Task;

namespace TaskTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<EFTaskDBContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<ITaskRepository, EFTaskRepository>();
        }

        public interface ITaskRepository
        {
            IEnumerable<Task> Get();
            Task Get(int id);
            void Create(Task task);
            void Update(Task task);
            Task Delete(int id);
            void Update(System.Threading.Tasks.Task updateTask);
            void Create(System.Threading.Tasks.Task task);
        }

        public class EFTaskRepository : ITaskRepository
        {
            EFTaskDBContext Context;
            public IEnumerable<Task> Get()
            {
                return Context.Tasks;
            }

            public Task Get(int Id)
            {
                return Context.Tasks.Find(Id);
            }
            public EFTaskRepository(EFTaskDBContext context)
            {
                Context = context;
            }

            public void Create(Task task)
            {
                Context.Tasks.Add(task);
                Context.SaveChanges();
            }

            public void Update(Task updateTask)
            {
                Task currentTask = Get(updateTask.Id);
                currentTask.Name = updateTask.Name;
                currentTask.Description = updateTask.Description;

                Context.Tasks.Update(currentTask);
                Context.SaveChanges();
            }

            public Task Delete(int Id)
            {
                Task task = Get(Id);

                if (task != null)
                {
                    Context.Tasks.Remove(task);
                    Context.SaveChanges();
                }

                return task;
            }

            public void Update(System.Threading.Tasks.Task updateTask)
            {
                throw new NotImplementedException();
            }

            public void Create(System.Threading.Tasks.Task task)
            {
                throw new NotImplementedException();
            }
        }

    }
}
