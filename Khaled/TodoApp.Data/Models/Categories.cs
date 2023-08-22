// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using Microsoft.Datasync.Client;
using System;

namespace TodoApp.Data.Models
{
    /// <summary>
    /// The model that is for the TodoItem pulled from the service.  This must
    /// match what is coming from the service.
    /// </summary>
    public class tblCategories : DatasyncClientData, IEquatable<tblCategories>
    {
        public string title { get; set; } = "";

        public bool Kategorie_DE { get; set; }

        public bool isSelected { get; set; }

        public bool Equals(tblCategories other)
            => other != null && other.Id == Id;


    }
}
