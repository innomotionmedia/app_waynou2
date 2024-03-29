﻿// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Datasync.EFCore;
using System.ComponentModel.DataAnnotations;

namespace TodoAppService.NET6.Db
{
    /// <summary>
    /// The fields in this class must match the fields in Models/TodoItem.cs
    /// for the TodoApp.Data project.
    /// </summary>
    public class tblCategories : EntityTableData
    {
        [Required, MinLength(1)]
        public string title { get; set; } = "";

        public bool Kategorie_DE { get; set; }
    }
}
