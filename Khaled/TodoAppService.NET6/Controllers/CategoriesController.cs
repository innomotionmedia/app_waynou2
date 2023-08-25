// Copyright (c) Microsoft Corporation. All Rights Reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;
using TodoAppService.NET6.Db;

namespace TodoAppService.NET6.Controllers
{
    //[Authorize]
    [Route("tables/tblcategories")]
    public class CategoriesController : TableController<tblCategories>
    {
        public CategoriesController(MigrationDbContext context)
            : base(new EntityTableRepository<tblCategories>(context))
        {
        }
    }
}
