﻿using GrowATree.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;
using System.Globalization;

namespace GrowATree.Infrastructure.Files.Maps
{
    public class TodoItemRecordMap : ClassMap<TodoItemRecord>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
