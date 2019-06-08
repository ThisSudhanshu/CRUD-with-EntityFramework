/* This controller is used to get the data from data base that is sorted , filtered and paginated. This API generates only raw data*/

using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.AspNetCore.Mvc;
using RajShoeApp.Model;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace RajShoeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShoeModelContext _context;

        public ProductsController(ShoeModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ShoeModels> GetShoeModels([FromQuery]QueryParams query)
        {
            var data = _context.ShoeData.ToList();
            var userQuery = from p in data
                            select p;
            var finalResult = userQuery;
            var tempResult = userQuery;
            var sortedData = userQuery;
            
            
            List<(string col, string opr, string value)> ps = new List<(string col, string opr, string value)>();
            var newReq = Request.Query;
            int count = newReq.Count;
            foreach (var con in newReq)
            {
                if (con.Key.Contains("["))
                {
                    var param1 = con.Key.Split('[', ']')[0];
                    var param2 = con.Key.Split('[', ']')[1];
                    var param3 = con.Value;
                    ps.Add((param1, param2, param3));
                }
                else
                {
                    var param1 = con.Key;
                    var param2 = string.Empty;
                    var param3 = con.Value;
                    ps.Add((param1, param2, param3));
                }
            }

            //assigning values
            query.skip = int.Parse(ps[0].value);
            query.take = int.Parse(ps[1].value);
            query.sortColumn = ps[2].value;
            query.sortDirection = ps[3].value;
            query.sortColumn1 = ps[4].value;
            query.filterColumn = ps[5].col;
            if (ps[5].value.All(char.IsDigit))
            {
                query.numValue = int.Parse(ps[5].value);
            }
            else
            {
                query.filterValue = ps[5].value;
            }
            if (ps.Count > 6)
            {
                if (ps[6].value.All(char.IsDigit))
                {

                    query.numValue1 = int.Parse(ps[6].value);

                }
                else
                {
                    query.filterValue1 = ps[6].value;
                }
            }
            if (ps.Count > 6)
            {
                query.opr2 = ps[6].opr;
                query.filterColumn1 = ps[6].col;
                query.filterValue1 = ps[6].value;
            }
            //end of values assignment


            //reflection props 
            var propertyInfo = typeof(ShoeModels).GetProperty(query.sortColumn);
            var someOrderBy = data.OrderBy(x => propertyInfo.GetValue(x, null));
            var someDescBy = data.OrderByDescending(x => propertyInfo.GetValue(x, null));
            //end of reflection props 
           

            //if else's to categorize
            if (query.sortDirection == "asc" || query.sortDirection == null)
            {
                var result = someOrderBy;
                sortedData = result;
            }
            else if (query.sortDirection == "desc")
            {
                var result = someDescBy;
                sortedData = result;
            }
            // end of sorting if-else cases     
            
            if (ps[5].opr != null || ps[6].opr != null)
            {
                query.opr = ps[5].opr;
                
                if (query.opr == "like" )
                {
                    
                    if (query.filterColumn == "Name" || query.filterColumn == "category" || query.filterColumn == "Brand" || query.filterColumn == "Colour" || query.filterColumn == "Size")
                    {
                        // only one filter here i.e. string matching
                        var newQuery = from p in sortedData.Where(a => a[query.filterColumn].Contains(query.filterValue))
                                       select p;                       
                        tempResult = newQuery;
                    }
                    
                }
                if (query.opr2 == "gt" || query.opr2 == "lt" || query.opr2 == "gte" || query.opr2 == "lte" || query.opr2 == "ne")
                {

                    if (query.filterColumn1 == "Price")
                    {
                        if (query.opr2 == "lt")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Price < query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "gt")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Price > query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "gte")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Price >= query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "lte")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Price <= query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "ne")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Price != query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                    }
                    if (query.filterColumn1 == "Weight")
                    {
                        if (query.opr2 == "lt")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Weight < query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "gt")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Weight > query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "gte")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Weight >= query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "lte")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Weight <= query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                        else if (query.opr2 == "ne")
                        {
                            var newQuery = from p in tempResult.Where(a => a.Weight != query.numValue1)
                                           select p;
                            finalResult = newQuery;
                        }
                    }
                }
            }
            else
            {
                sortedData.Skip(query.skip)
                    .Take(query.take)
                    .ToList();
                return sortedData;
            }
            var paginatedResult = finalResult.Skip(query.skip)
                .Take(query.take)
                .ToList();
            return paginatedResult;
            
        }
    }
}