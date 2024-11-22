using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using E_ecommerce.Data.DTO.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Queries.Models
{
    public class IsRoleExistByNameModel : IRequest<Response<IsExist>>
    {
        [Required]
        public string Name { get; set; }
        public IsRoleExistByNameModel(string name)
        {
            Name = name;
        }
    }
}
