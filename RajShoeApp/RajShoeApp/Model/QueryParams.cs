using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RajShoeApp.Model
{
    public class QueryParams
    {
        //public QueryParams()
        //{
        //    skip = 0;
        //    take = 10;
        //    sortColumn1 = "Id";
        //    sortDirection = "asc";
        //}   
        [Required]
        public int skip { get; set; }
        [Required]
        public int take { get; set; }
        [Required]
        public string sortColumn { get; set; }
        [Required]
        public string sortColumn1 { get; set; }
        [Required]
        public string sortDirection { get; set; }       
        public string opr { get; set; }
        public string opr2 { get; set; }
        public string filterColumn1 { get; set; }
        public string filterColumn { get; set; }
        public string filterValue { get; set; }
        public string filterValue1 { get; set; }
        public int numValue { get; set; }
        public int numValue1 { get; set; }
        
    }
}
