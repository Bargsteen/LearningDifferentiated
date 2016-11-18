using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models
{

    public class EFAssignmentRepository : IAssignmentRepository
    {
        private ApplicationDbContext context;

        public EFAssignmentRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Assignment> Assignments => context.Assignments.Include(a => a.AnswerOptions);

        public void SaveAssignment(Assignment assignment)
        {
            if (assignment.AssignmentId == 0)
            {
                // New assignment
                context.Add(assignment);
            }
            else
            {
                // Update already existing assignment
                Assignment dbEntry = context.Assignments
                    .Include(a => a.AnswerOptions)
                    .FirstOrDefault(a => a.AssignmentId == assignment.AssignmentId);

                if (dbEntry != null)
                {
                    dbEntry.Headline = assignment.Headline;
                    dbEntry.Question = assignment.Question;
                    int amountOfAnswerOptionsInDb = dbEntry.AnswerOptions.Count();
                    
                    dbEntry.AnswerOptions = new List<AnswerOption>();
                    foreach (AnswerOption ao in assignment.AnswerOptions)
                    {
                        dbEntry.AnswerOptions.Add(ao);
                    }
                }
            }
            context.SaveChanges();
        }

        public Assignment DeleteAssignment(int assignmentId)
        {
            Assignment dbEntry = context.Assignments.FirstOrDefault(a => a.AssignmentId == assignmentId);
            if (dbEntry != null)
            {
                context.Assignments.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
