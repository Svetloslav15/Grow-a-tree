using GrowATree.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace GrowATree.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
