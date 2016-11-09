using System.Collections.Generic;

namespace ADL.Models {

    public interface IAssignmentRepository {
        IEnumerable<Assignment> Assignments { get; }
        void Add(Assignment assignment);
        void Delete(int assignmentID);
    }
}
