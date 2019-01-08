using System;
using System.Collections.Generic;
using Data;
using Newtonsoft.Json;

namespace Database.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var task = new Task
                {
                    Activity = "asokdaosk",
                    Description = "asodkao",
                    Due = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Officers = "ich, du",
                    Progress = 22,
                    Title = "task",
                    Proofs = new List<Proof> { new Proof { Id = Guid.NewGuid(), Title = "proof1" }, new Proof { Id = Guid.NewGuid(), Title = "proof2" } }
                };

                context.Tasks.Add(task);
                context.SaveChanges();

                var dbTask = context.Tasks.Find(task.Id);
                var json = JsonConvert.SerializeObject(dbTask);
                Console.WriteLine(json);
            }
        }
    }
}
