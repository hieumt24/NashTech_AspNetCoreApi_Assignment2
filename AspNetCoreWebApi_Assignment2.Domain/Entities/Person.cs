using AspNetCoreWebApi_Assignment2.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi_Assignment2.Domain.Entities
{
    public class Person : BaseEntity
    {
        /// <summary>
        /// ID is Guid type
        /// </summary>
        [Key]
        public Guid Id { get; set; }

       
    }
}